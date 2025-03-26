using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderManagementSystem
{
    public class Customer
    {
        public string Name { get; set; }

        public Customer(string name) => Name = name;

        public override bool Equals(object obj) => obj is Customer c && Name == c.Name;
        public override int GetHashCode() => HashCode.Combine(Name);
        public override string ToString() => $"Customer: {Name}";
    }

    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }

        public Product(string name, decimal price) => (Name, Price) = (name, price);

        public override bool Equals(object obj) => obj is Product p && Name == p.Name;
        public override int GetHashCode() => HashCode.Combine(Name);
        public override string ToString() => $"{Name} (${Price})";
    }

    public class OrderDetails
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal => Product.Price * Quantity;

        public OrderDetails(Product product, int quantity) =>
            (Product, Quantity) = (product, quantity);

        public override bool Equals(object obj) => obj is OrderDetails d && Product.Equals(d.Product);
        public override int GetHashCode() => HashCode.Combine(Product);
        public override string ToString() =>
            $"{Product} × {Quantity} = {Subtotal:C}";
    }

    public class Order
    {
        public string OrderId { get; set; }
        public Customer Customer { get; set; }
        public List<OrderDetails> Details { get; } = new List<OrderDetails>();
        public decimal TotalAmount => Details.Sum(d => d.Subtotal);

        public Order(string orderId, Customer customer) =>
            (OrderId, Customer) = (orderId, customer);

        public void AddDetail(OrderDetails detail)
        {
            if (Details.Contains(detail))
                throw new ArgumentException($"Product {detail.Product.Name} already exists in order");
            Details.Add(detail);
        }

        public override bool Equals(object obj) => obj is Order o && OrderId == o.OrderId;
        public override int GetHashCode() => HashCode.Combine(OrderId);
        public override string ToString() =>
            $"Order {OrderId}\nCustomer: {Customer}\n{string.Join("\n", Details)}\nTotal: {TotalAmount:C}\n";
    }

    public class OrderService
    {
        private List<Order> orders = new List<Order>();

        public void AddOrder(Order order)
        {
            if (orders.Any(o => o.Equals(order)))
                throw new ArgumentException($"Order {order.OrderId} already exists");
            orders.Add(order);
        }

        public void RemoveOrder(string orderId)
        {
            var order = orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
                throw new KeyNotFoundException($"Order {orderId} not found");
            orders.Remove(order);
        }

        public void UpdateOrder(Order newOrder)
        {
            var index = orders.FindIndex(o => o.OrderId == newOrder.OrderId);
            if (index == -1)
                throw new KeyNotFoundException($"Order {newOrder.OrderId} not found");
            orders[index] = newOrder;
        }

        public IEnumerable<Order> QueryAll() => orders.OrderBy(o => o.TotalAmount);

        public IEnumerable<Order> QueryByOrderId(string orderId) =>
            orders.Where(o => o.OrderId == orderId).OrderBy(o => o.TotalAmount);

        public IEnumerable<Order> QueryByProductName(string productName) =>
            orders.Where(o => o.Details.Any(d => d.Product.Name == productName))
                 .OrderBy(o => o.TotalAmount);

        public IEnumerable<Order> QueryByCustomer(string customerName) =>
            orders.Where(o => o.Customer.Name == customerName)
                 .OrderBy(o => o.TotalAmount);

        public IEnumerable<Order> QueryByAmount(decimal amount) =>
            orders.Where(o => o.TotalAmount >= amount)
                 .OrderBy(o => o.TotalAmount);

        public void Sort(Comparison<Order> comparison = null) =>
            orders.Sort(comparison ?? ((x, y) => x.OrderId.CompareTo(y.OrderId)));
    }

    class Program
    {
        static void Main()
        {
            TestOrders();
            Console.ReadLine();
        }

        static void TestOrders()
        {
            var service = new OrderService();
            var customer1 = new Customer("Alice");
            var customer2 = new Customer("Bob");

            var product1 = new Product("Laptop", 1200m);
            var product2 = new Product("Mouse", 25m);
            var product3 = new Product("Keyboard", 60m);

            // 测试添加订单
            try
            {
                var order1 = new Order("2023001", customer1);
                order1.AddDetail(new OrderDetails(product1, 1));
                order1.AddDetail(new OrderDetails(product2, 2));
                service.AddOrder(order1);

                var order2 = new Order("2023002", customer2);
                order2.AddDetail(new OrderDetails(product3, 3));
                service.AddOrder(order2);

                Console.WriteLine("Added orders successfully:");
                service.QueryAll().ToList().ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Add order error: {ex.Message}");
            }

            // 测试重复订单
            try
            {
                var order3 = new Order("2023001", customer1);
                service.AddOrder(order3);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nExpected error: {ex.Message}");
            }

            // 测试查询
            Console.WriteLine("\nQuery by product 'Keyboard':");
            service.QueryByProductName("Keyboard").ToList().ForEach(Console.WriteLine);

            // 测试删除
            try
            {
                service.RemoveOrder("2023002");
                Console.WriteLine("\nAfter removing order 2023002:");
                service.QueryAll().ToList().ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Remove error: {ex.Message}");
            }

            // 测试修改
            try
            {
                var updatedOrder = new Order("2023001", customer1);
                updatedOrder.AddDetail(new OrderDetails(product1, 2)); // 修改数量
                service.UpdateOrder(updatedOrder);
                Console.WriteLine("\nAfter updating order 2023001:");
                service.QueryAll().ToList().ForEach(Console.WriteLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Update error: {ex.Message}");
            }
        }
    }
}