using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesReconstruction
{
    interface IRenameRuleParser
    {
        IRenameRule Parse(string line);
    }
    class ReplaceRuleParser : IRenameRuleParser
    {
        public static string Key = "Replace";
        public IRenameRule Parse(string line)
        {
            // Split rule type
            string[] tokens = line.Split(new string[] { "Replace " }, StringSplitOptions.None);
            // Split needles and replacer
            string[] parts = tokens[1].Split(new string[] { " => " }, StringSplitOptions.None);
            // Remove [ and ]
            string[] words = parts[0].Substring(1, parts[0].Length - 2).Split(new string[] { ", " }, StringSplitOptions.None);
            
            List<string> needles = new List<string>();
            foreach (string word in words)
            {
                // Split "
                needles.Add(word.Substring(1, word.Length - 2));
            }

            string replacer = parts[1].Substring(1, parts[1].Length - 2);

            IRenameRule rule = new ReplaceRule(needles, replacer);
            return rule;
        }
    }
    class AddPrefixRuleParser : IRenameRuleParser
    {
        public static string Key = "AddPrefix";
        public IRenameRule Parse(string line)
        {
            string[] tokens = line.Split(new string[] { "AddPrefix " }, StringSplitOptions.None);

            string prefix = tokens[1].Replace("\"", "");
            IRenameRule rule = new AddPrefixRule(prefix);

            return rule;
        }
    }
    class AllLowerRuleParser : IRenameRuleParser
    {
        public static string Key = "AllLower";

        public IRenameRule Parse(string line)
        {
            IRenameRule rule = new AllLowerRule();
            return rule;
        }
    }
}
