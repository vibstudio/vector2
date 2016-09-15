using Eng.Vector.Globalization;

namespace Eng.Vector.Exceptions
{
    public class NotRecognizedRulesException : ManagedException
    {
        public NotRecognizedRulesException(string str, string[] candidateRules)
            : base(Labeling.Factory.Get.RulesNotRecognized(str, candidateRules).ToString())
        {
        }
    }
}