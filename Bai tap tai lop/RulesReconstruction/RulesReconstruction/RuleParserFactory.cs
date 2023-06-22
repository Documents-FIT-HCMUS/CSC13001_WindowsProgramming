using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RulesReconstruction
{
    class RuleParserFactory
    {
        private Dictionary<string, IRenameRuleParser> _prototypes = null;
        public RuleParserFactory()
        {
            _prototypes = new Dictionary<string, IRenameRuleParser>
            {
                { AddPrefixRuleParser.Key, new AddPrefixRuleParser() },
                { ReplaceRuleParser.Key, new ReplaceRuleParser() },
                { AllLowerRuleParser.Key, new AllLowerRuleParser() },
            };
        }
        public IRenameRuleParser Create(string type)
        {
            IRenameRuleParser parser = _prototypes[type];
            return parser;
        }
    }
}
