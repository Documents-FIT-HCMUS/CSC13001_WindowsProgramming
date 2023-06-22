using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsParser
{
    abstract class Shape
    {
        public Point StartPoint { get; set; }
        public Point EndPoint { get; set; }
        public abstract void Draw();
    }
    class Line : Shape
    {
        public Line (Point start, Point end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        public override void Draw()
        {
            Console.WriteLine($"Line: ({StartPoint}, {EndPoint})");
        }
    }
    class Rectangle : Shape
    {
        public Rectangle(Point start, Point end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        public override void Draw()
        {
            Console.WriteLine($"Rectangle: ({StartPoint}, {EndPoint})");
        }
    }
    class Ellipse : Shape
    {
        public Ellipse(Point start, Point end)
        {
            StartPoint = start;
            EndPoint = end;
        }
        public override void Draw()
        {
            Console.WriteLine($"Ellipse: ({StartPoint}, {EndPoint})");
        }
    }
}
