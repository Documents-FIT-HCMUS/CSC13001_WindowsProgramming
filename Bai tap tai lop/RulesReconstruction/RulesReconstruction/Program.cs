using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesReconstruction
{
    class Program
    {

        static void Main()
        {
            var preset = new List<IRenameRule>();
            var factory = new RuleParserFactory();
            
            string original = "Do Hai Thang vietnamworks.com.pdf";
            string newname = original;

            using (var reader = new StreamReader("preset01.txt"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string ruleType;
                    int ruleTypeIndex = line.IndexOf(" ");
                    if (ruleTypeIndex < 0)
                        ruleType = line;
                    else
                        ruleType = line.Substring(0, ruleTypeIndex);

                    // Create parser base on rule type
                    IRenameRuleParser parser = factory.Create(ruleType);
                    IRenameRule rule = parser.Parse(line);
                    preset.Add(rule);

                    Console.WriteLine(line);
                }
            }

            foreach (var rule in preset)
                newname = rule.Rename(newname);
            Console.WriteLine(newname);
        }
    }
}
