using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesReconstruction
{
    interface IRenameRule
    {
        string Rename(string original);
    }
    class ReplaceRule : IRenameRule
    {
        public List<string> Needles { get; set; }
        public string Replacer { get; set; }

        public ReplaceRule(List<string> needles, string replacer)
        {
            this.Needles = needles;
            this.Replacer = replacer;
        }

        public string Rename(string original)
        {
            string result = original;

            foreach (var needle in Needles)
            {
                result = result.Replace(needle, Replacer);
            }

            return result;
        }
    }
    class AddPrefixRule : IRenameRule
    {
        public string Prefix { get; set; }
        public AddPrefixRule(string prefix)
        {
            Prefix = prefix;
        }
        public string Rename(string original)
        {
            string result = $"{Prefix}{original}";
            return result;
        }
    }
    class AllLowerRule : IRenameRule
    {
        public string Rename(string original)
        {
            return original.ToLower();
        }
    }
}
