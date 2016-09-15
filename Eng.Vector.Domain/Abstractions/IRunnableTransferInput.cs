#region Namespaces

using System.Collections.Generic;

using Eng.Collections;
using Eng.Vector.Domain.Model;

#endregion

namespace Eng.Vector.Domain.Abstractions
{
    public interface IRunnableTransferInput
    {
        void RunAsync(IEnumerable<TransferFilePath> files);

        OperationResultCollection<TransferFilePath> Run(IEnumerable<TransferFilePath> files);

        OperationResult<TransferFilePath> Run(TransferFilePath filePath);
    }
}