using System;

// 形状基类
public abstract class Shape
{
    public abstract double Area { get; }
}

// 圆形类
public class Circle : Shape
{
    private double _radius;

    public Circle(double radius)
    {
        _radius = radius;
    }

    public override double Area => Math.PI * _radius * _radius;
}

// 矩形类
public class Rectangle : Shape
{
    private double _length;
    private double _width;

    public Rectangle(double length, double width)
    {
        _length = length;
        _width = width;
    }

    public override double Area => _length * _width;
}

// 三角形类
public class Triangle : Shape
{
    private double _base;
    private double _height;

    public Triangle(double @base, double height)
    {
        _base = @base;
        _height = height;
    }

    public override double Area => 0.5 * _base * _height;
}

// 形状类型枚举
public enum ShapeType
{
    Circle,
    Rectangle,
    Triangle
}

// 形状工厂类
public static class ShapeFactory
{
    private static Random _random = new Random();

    public static Shape CreateShape(ShapeType type)
    {
        // 生成1-10之间的随机参数
        switch (type)
        {
            case ShapeType.Circle:
                return new Circle(_random.NextDouble() * 9 + 1);
            case ShapeType.Rectangle:
                return new Rectangle(_random.NextDouble() * 9 + 1,
                                   _random.NextDouble() * 9 + 1);
            case ShapeType.Triangle:
                return new Triangle(_random.NextDouble() * 9 + 1,
                                   _random.NextDouble() * 9 + 1);
            default:
                throw new ArgumentException("Invalid shape type");
        }
    }

    public static ShapeType GetRandomType()
    {
        int typesCount = Enum.GetValues(typeof(ShapeType)).Length;
        return (ShapeType)_random.Next(typesCount);
    }
}

class Program
{
    static void Main()
    {
        double totalArea = 0;

        for (int i = 0; i < 10; i++)
        {
            ShapeType type = ShapeFactory.GetRandomType();
            Shape shape = ShapeFactory.CreateShape(type);
            totalArea += shape.Area;
        }

        Console.WriteLine($"Total area of all shapes: {totalArea:F2}");
    }
}