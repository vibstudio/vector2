#region Namespaces

using System;
using System.Linq;

using Eng.IO;
using Eng.IO.Path;
using Eng.Vector.Exceptions;

#endregion

namespace Eng.Vector.Engine.Validation
{
    public class FileFormatValidator : IFileValidation
    {
        #region Fields

        private readonly string[] _extensions;

        #endregion

        #region Ctor(s)

        public FileFormatValidator(string[] extensions)
        {
            _extensions = extensions;
        }

        #endregion

        #region IValidation implementation

        public FileValidationResult Validate(FilePath file)
        {
            if (_extensions.Any(extension => string.Equals(file.FileExtensionWithoutDot, extension, StringComparison.CurrentCultureIgnoreCase)))
            {
                return new FileValidationResult(file, ValidatorName, true);
            }

            throw new FileFormatValidationFailedException(_extensions);
        }

        public string ValidatorName
        {
            get { return GetType().Name; }
        }

        public void Reset()
        {
        }

        #endregion
    }
}
