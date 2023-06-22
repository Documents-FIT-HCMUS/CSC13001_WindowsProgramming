using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphicsParser
{

    class ShapeParserFactory
    {
        private Dictionary<string, IShapeParser> _prototypes = null;
        public ShapeParserFactory()
        {
            _prototypes = new Dictionary<string, IShapeParser>
            {
                { LineParser.Key, new LineParser() },
                { RectangleParser.Key, new RectangleParser() },
                { EllipseParser.Key, new EllipseParser() },
            };
        }
        public IShapeParser Create(string type)
        {
            IShapeParser parser = _prototypes[type];
            return parser;
        }
    }
}
