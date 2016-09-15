#region Namespaces

using Eng.Vector.Domain.Model;
using Eng.Vector.Domain.Model.Integration;

#endregion

namespace Eng.Vector.Domain.Abstractions
{
    public interface IRunnableTransferOutput
    {
        TransferOutputResultCollection Run();

        TransferOutputResultCollection Run(Eis eis);
    }
}