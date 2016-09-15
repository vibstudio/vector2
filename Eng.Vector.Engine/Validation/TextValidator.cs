#region Namespaces

using System;
using System.IO;

using Eng.IO;
using Eng.IO.Path;
using Eng.Vector.Exceptions;

#endregion

namespace Eng.Vector.Engine.Validation
{
    public class TextValidator : IFileValidation
    {
        #region IValidation implementation

        public FileValidationResult Validate(FilePath file)
        {
            FileValidationResult result = new FileValidationResult(file, ValidatorName, true);

            bool isTextFile = IsTextFile(file.Path);

            if (isTextFile) return result;

            throw new TextualValidationFailedException();
        }

        public string ValidatorName
        {
            get { return GetType().Name; }
        }

        public void Reset()
        {
        }

        #endregion

        #region Methods

        private bool IsTextFile(string fileName)
        {
            int totalCount = 0;
            int trueCount = 0;

            using (StreamReader reader = new StreamReader(fileName))
            {
                do
                {
                    int ch = reader.Read();
                    if (((ch >= 32 && ch <= 126) || (ch >= 9 && ch <= 13)) && !(ch >= 0 && ch <= 8) && !(ch >= 14 && ch <= 31))
                    {
                        trueCount++;
                    }
                    totalCount++;
                }
                while (totalCount <= 50 && reader.EndOfStream != true);
            }

            return trueCount > totalCount * .99;
        }

        #endregion
    }
}
