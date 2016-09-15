namespace Eng.Vector.Engine.Parsing
{
    #region Namespaces

    using System.Linq;

    using Eng.Vector.Domain.Abstractions;
    using Eng.Vector.Exceptions;

    #endregion

    public class PositionalRulesParser : IRulesParsing
    {
        #region Fields

        private readonly int _startPos;
        private readonly int _lenght;

        #endregion

        #region Ctor(s)

        public PositionalRulesParser(int startPos, int lenght)
        {
            _startPos = startPos;
            _lenght = lenght;
        }

        #endregion

        #region IRulesParsing implementation

        public string ExtractRule(string str)
        {
            string rule = str.Substring(_startPos, _lenght);
            if (CandidateRules.Contains(rule)) return rule;
            throw new NotRecognizedRulesException(str, CandidateRules);
        }

        public string[] CandidateRules { get; set; }

        #endregion
    }
}
