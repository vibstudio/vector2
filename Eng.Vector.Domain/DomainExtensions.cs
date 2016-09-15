#region Namespaces

using System.Collections.Generic;
using System.Linq;

using Eng.Vector.Domain.Model;

#endregion

namespace Eng.Vector.Domain
{
    public static class DomainExtensions
    {
        public static IEnumerable<TransferFilePath> ToFilePaths(this IEnumerable<string> filePaths, string eisID)
        {
            return filePaths.Select(filePath => new TransferFilePath(filePath) { Specialization = new TransferID { EisID = eisID } }).ToList();
        }

        public static TransferFilePath ToFilePath(this string filePath, string eisID)
        {
            return new TransferFilePath(filePath) { Specialization = new TransferID { EisID = eisID } };
        }
    }
}