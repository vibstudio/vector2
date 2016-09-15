#region Namespaces

using Eng.Vector.Domain.Abstractions;
using Eng.Vector.Exceptions;

#endregion

namespace Eng.Vector.Engine.Parsing
{
    public class ContainsRulesParser : IRulesParsing
    {
        #region IRulesParsing implementation

        public string ExtractRule(string str)
        {
            foreach (string rule in CandidateRules)
            {
                if (str.ToUpper().Contains(rule.ToUpper()))
                {
                    return rule;
                }
            }

            throw new NotRecognizedRulesException(str, CandidateRules);
        }

        public string[] CandidateRules { get; set; }

        #endregion
    }
}