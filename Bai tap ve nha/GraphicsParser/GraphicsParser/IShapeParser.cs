using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsParser
{
    interface IShapeParser
    {
        Shape Parse(string str);
    }
    class LineParser : IShapeParser
    {
        public static string Key = "Line";
        public Shape Parse(string str)
        {
            string[] tokens = str.Split(new string[] { "Line " }, StringSplitOptions.None);
            string[] parts = tokens[1].Replace(" ", "").Split(new string[] { "(" }, StringSplitOptions.None);
            string[] words1 = parts[2].Substring(0, parts[2].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);
            string[] words2 = parts[3].Substring(0, parts[3].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);

            var point1 = new Point(int.Parse(words1[0]), int.Parse(words1[1]));
            var point2 = new Point(int.Parse(words2[0]), int.Parse(words2[1]));
            Shape shape = new Line(point1, point2);
            return shape;
        }
    }
    class RectangleParser : IShapeParser
    {
        public static string Key = "Rectangle";
        public Shape Parse(string str)
        {
            string[] tokens = str.Split(new string[] { "Rectangle " }, StringSplitOptions.None);
            string[] parts = tokens[1].Replace(" ", "").Split(new string[] { "(" }, StringSplitOptions.None);
            string[] words1 = parts[2].Substring(0, parts[2].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);
            string[] words2 = parts[3].Substring(0, parts[3].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);

            var point1 = new Point(int.Parse(words1[0]), int.Parse(words1[1]));
            var point2 = new Point(int.Parse(words2[0]), int.Parse(words2[1]));
            Shape shape = new Rectangle(point1, point2);
            return shape;
        }
    }
    class EllipseParser : IShapeParser
    {
        public static string Key = "Ellipse";
        public Shape Parse(string str)
        {
            string[] tokens = str.Split(new string[] { "Ellipse " }, StringSplitOptions.None);
            string[] parts = tokens[1].Replace(" ", "").Split(new string[] { "(" }, StringSplitOptions.None);
            string[] words1 = parts[2].Substring(0, parts[2].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);
            string[] words2 = parts[3].Substring(0, parts[3].Length - 2).Split(new string[] { "," }, StringSplitOptions.None);

            var point1 = new Point(int.Parse(words1[0]), int.Parse(words1[1]));
            var point2 = new Point(int.Parse(words2[0]), int.Parse(words2[1]));
            Shape shape = new Ellipse(point1, point2);
            return shape;
        }
    }
}
