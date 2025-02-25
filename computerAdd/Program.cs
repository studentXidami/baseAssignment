using System;

class Program
{
    static void Main()
    {
        double a = double.Parse(Console.ReadLine());
        char op = char.Parse(Console.ReadLine());
        double b = double.Parse(Console.ReadLine());
        Console.WriteLine(op == '+' ? a + b : op == '-' ? a - b : op == '*' ? a * b : a / b);
    }
}