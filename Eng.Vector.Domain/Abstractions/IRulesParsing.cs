namespace Eng.Vector.Domain.Abstractions
{
    public interface IRulesParsing
    {
        string ExtractRule(string str);

        string[] CandidateRules { get; set; }
    }
}
