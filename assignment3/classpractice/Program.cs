using System;

// 定义一个接口，用于定义形状的基本功能
public interface IShape
{
    double Area { get; }  // 面积属性
    bool IsValid { get; } // 判断形状是否合法
}

// 抽象类，提供形状的基本实现
public abstract class Shape : IShape
{
    public abstract double Area { get; }
    public abstract bool IsValid { get; }
}

// 长方形类
public class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(double length, double width)
    {
        _length = length;
        _width = width;
    }

    public override double Area
    {
        get
        {
            if (!IsValid)
                return 0;
            return _length * _width;
        }
    }

    public override bool IsValid
    {
        get
        {
            return _length > 0 && _width > 0;
        }
    }
}

// 正方形类（继承自长方形类）
public class Square : Rectangle
{
    public Square(double side) : base(side, side)
    {
    }
}

// 三角形类
public class Triangle : Shape
{
    private double _sideA;
    private double _sideB;
    private double _sideC;

    public Triangle(double sideA, double sideB, double sideC)
    {
        _sideA = sideA;
        _sideB = sideB;
        _sideC = sideC;
    }

    public override double Area
    {
        get
        {
            if (!IsValid)
                return 0;
            double s = (_sideA + _sideB + _sideC) / 2;
            return Math.Sqrt(s * (s - _sideA) * (s - _sideB) * (s - _sideC));
        }
    }

    public override bool IsValid
    {
        get
        {
            return (_sideA + _sideB > _sideC) &&
                   (_sideA + _sideC > _sideB) &&
                   (_sideB + _sideC > _sideA);
        }
    }
}

// 测试类
public class Program
{
    public static void Main()
    {
        // 测试长方形
        Rectangle rect = new Rectangle(3, 4);
        Console.WriteLine("长方形面积: " + rect.Area);
        Console.WriteLine("长方形是否合法: " + rect.IsValid);

        // 测试正方形
        Square square = new Square(5);
        Console.WriteLine("正方形面积: " + square.Area);
        Console.WriteLine("正方形是否合法: " + square.IsValid);

        // 测试三角形
        Triangle triangle1 = new Triangle(3, 4, 5);
        Console.WriteLine("三角形面积: " + triangle1.Area);
        Console.WriteLine("三角形是否合法: " + triangle1.IsValid);

        // 测试非法三角形
        Triangle triangle2 = new Triangle(1, 1, 3);
        Console.WriteLine("非法三角形面积: " + triangle2.Area);
        Console.WriteLine("非法三角形是否合法: " + triangle2.IsValid);
    }
}