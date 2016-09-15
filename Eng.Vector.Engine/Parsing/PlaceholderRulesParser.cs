namespace Eng.Vector.Engine.Parsing
{
    #region Namespaces

    using System;
    using System.Linq;

    using Eng.Vector.Domain.Abstractions;
    using Eng.Vector.Exceptions;

    #endregion

    public class PlaceholderRulesParser : IRulesParsing
    {
        #region Fields

        private readonly string _placeholder;
        private readonly int _index;

        #endregion

        #region Ctor(s)

        public PlaceholderRulesParser(string placeholder, int index)
        {
            _placeholder = placeholder;
            _index = index;
        }

        #endregion

        #region IRulesParsing implementation

        public string ExtractRule(string str)
        {
            string[] fragments = str.Split(Convert.ToChar(_placeholder));
            string rule = fragments[_index];
            if (CandidateRules.Contains(rule)) return rule;
            throw new NotRecognizedRulesException(str, CandidateRules);
        }

        public string[] CandidateRules { get; set; }

        #endregion

    }
}
