using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsParser
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ShapeParserFactory();
            using (var reader = new StreamReader("mypaint.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    int shapeTypeIndex = line.IndexOf(" ");
                    string shapeType = line.Substring(0, shapeTypeIndex);

                    IShapeParser parser = factory.Create(shapeType);
                    Shape shape = parser.Parse(line);
                    shape.Draw();
                }
            }
        }
    }
}
