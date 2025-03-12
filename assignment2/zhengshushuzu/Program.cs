using System;
using System.Linq;

namespace ArrayStatisticsApp
{
    public struct StatisticsResult
    {
        public int Max { get; set; }
        public int Min { get; set; }
        public double Average { get; set; }
        public int Sum { get; set; }
    }

    public static class ArrayStatisticsCalculator
    {
        public static StatisticsResult Calculate(int[] numbers)
        {
            if (numbers == null || numbers.Length == 0)
                throw new ArgumentException("数组不能为空");

            int max = numbers[0];
            int min = numbers[0];
            int sum = 0;

            foreach (int num in numbers)
            {
                max = Math.Max(max, num);
                min = Math.Min(min, num);
                sum += num;
            }

            return new StatisticsResult
            {
                Max = max,
                Min = min,
                Sum = sum,
                Average = (double)sum / numbers.Length
            };
        }
    }

    public static class StatisticsProcessor
    {
        public static void Execute()
        {
            int[] numbers = GetUserInputArray();
            StatisticsResult result = ArrayStatisticsCalculator.Calculate(numbers);
            DisplayStatisticsResult(result);
        }

        private static int[] GetUserInputArray()
        {
            while (true)
            {
                Console.Write("请输入整数数组（用逗号或空格分隔）：");
                string input = Console.ReadLine();

                if (TryParseInput(input, out int[] numbers))
                {
                    return numbers;
                }

                Console.WriteLine("输入无效，请重新输入");
            }
        }

        private static bool TryParseInput(string input, out int[] numbers)
        {
            numbers = null;
            if (string.IsNullOrWhiteSpace(input)) return false;

            string[] segments = input.Split(new[] { ',', ' ' }, StringSplitOptions.RemoveEmptyEntries);
            int[] parsedNumbers = new int[segments.Length];

            for (int i = 0; i < segments.Length; i++)
            {
                if (!int.TryParse(segments[i], out parsedNumbers[i]))
                {
                    return false;
                }
            }

            numbers = parsedNumbers;
            return numbers.Length > 0;
        }

        private static void DisplayStatisticsResult(StatisticsResult result)
        {
            Console.WriteLine("统计结果：");
            Console.WriteLine($"最大值：{result.Max}");
            Console.WriteLine($"最小值：{result.Min}");
            Console.WriteLine($"平均值：{result.Average:F2}");
            Console.WriteLine($"元素总和：{result.Sum}");
        }
    }

    public class Program
    {
        public static void Main()
        {
            StatisticsProcessor.Execute();
        }
    }
}