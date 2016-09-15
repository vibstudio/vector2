using Eng.IO;
using Eng.Vector.Domain.Abstractions;

namespace Eng.Vector.Domain.Model.Integration
{
    public class TransferFileInfo
    {
        //public string ID { get; set; }

        //public string Category { get; set; }

        //public FileType Extension { get; set; }

        public bool IsZipped { get; set; }

        public IRulesParsing RulesParser { get; set; }

        public IFileValidation[] Validators { get; set; }

        //public FilePath[] FilePaths { get; set; }
    }
}
