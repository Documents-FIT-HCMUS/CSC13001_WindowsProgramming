using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contract
{
    public interface IShape
    {
        string MagicWord { get; }
    }
    public interface IShapeToStringDataConverter
    {
        string MagicWord { get; }
        string Convert(IShape shape);
        IShape ConvertBack(string buffer);
    }
    public interface IShapeToStringUIConverter
    {
        string MagicWord { get; }
        string convert(IShape shape);
        IShape ConvertBack(string buffer);
    }
}
