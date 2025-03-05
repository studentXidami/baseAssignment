using System;
using System.Collections.Generic;
using System.Linq;

namespace zhishuyinzi
{
    public static class PrimeFactorizer
    {
        public static void Execute()
        {
            int number = GetUserNumber();
            List<int> primeFactors = Factorize(number);
            DisplayPrimeFactors(primeFactors);
        }

        private static int GetUserNumber()
        {
            int number;
            do
            {
                Console.Write("请输入一个正整数：");
            } while (!int.TryParse(Console.ReadLine(), out number) || number <= 0);

            return number;
        }

        private static List<int> Factorize(int number)
        {
            List<int> factors = new List<int>();

            HandleEvenFactors(ref number, factors);
            HandleOddFactors(ref number, factors);
            AddRemainingPrimeFactor(number, factors);

            return factors;
        }

        private static void HandleEvenFactors(ref int number, List<int> factors)
        {
            while (number % 2 == 0)
            {
                factors.Add(2);
                number /= 2;
            }
        }

        private static void HandleOddFactors(ref int number, List<int> factors)
        {
            for (int divisor = 3; divisor * divisor <= number; divisor += 2)
            {
                while (number % divisor == 0)
                {
                    factors.Add(divisor);
                    number /= divisor;
                }
            }
        }

        private static void AddRemainingPrimeFactor(int number, List<int> factors)
        {
            if (number > 1)
            {
                factors.Add(number);
            }
        }

        private static void DisplayPrimeFactors(List<int> factors)
        {
            if (!factors.Any())
            {
                Console.WriteLine("该数没有素数因子。");
                return;
            }

            var uniqueFactors = factors.Distinct().OrderBy(f => f);
            Console.WriteLine("素数因子为：" + string.Join(", ", uniqueFactors));
        }
    }

    public class Program
    {
        public static void Main()
        {
            PrimeFactorizer.Execute();
        }
    }
}