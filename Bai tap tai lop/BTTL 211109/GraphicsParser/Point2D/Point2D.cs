using Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Point
{
    public class Point2D : IShape
    {
        public string MagicWord { get => "Point"; }
        public int X { get; set; }
        public int Y { get; set; }
    }
    public class PointToStringUIConverter : IShapeToStringUIConverter
    {
        public string MagicWord { get => "Point"; }
        public string convert(IShape shape)
        {
            Point2D line = (Point2D)shape;
            var builder = new StringBuilder();
            builder.Append("(");
            builder.Append(line.X);
            builder.Append(",");
            builder.Append(line.Y);
            builder.Append(")");

            string result = builder.ToString();
            return result;
        }

        public IShape ConvertBack(string buffer)
        {
            throw new NotImplementedException();
        }
    }
}
