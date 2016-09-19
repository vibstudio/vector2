#region Namespaces

using System;
using System.IO;

using Eng.Diagnostic;
using Eng.IO.Path;
using Eng.Vector.Domain;
using Eng.Vector.Domain.Model.Integration;

#endregion

namespace Eng.Vector.Util
{
    public interface IFileMovingHelper
    {
        void Dump(TransferNetworkCredentials networkCredentials,
                  TransferDirectoryInfo directoryInfo,
                  FilePath file);

        void Reject(TransferNetworkCredentials networkCredentials,
                    TransferDirectoryInfo directoryInfo,
                    FilePath file);

        void Reject(TransferNetworkCredentials networkCredentials,
                    TransferDirectoryInfo directoryInfo,
                    FilePath file,
                    Action notifiyTransfer);
    }

    internal class FileMovingHelper : IFileMovingHelper
    {
        private static readonly Type classType = typeof(EisConfig);

        public void Dump(TransferNetworkCredentials networkCredentials,
                         TransferDirectoryInfo directoryInfo,
                         FilePath file)
        {
            const string METHOD_NAME = "Dump`1";

            string dumpFilePath = Path.Combine(directoryInfo.DumpPath, file.BackupFileName);

            if (networkCredentials.ExecuteImpersonation)
            {
                bool autenticated = NetworkShareAccessProvider.ImpersonateUser(networkCredentials.UserName,
                                                                               networkCredentials.Domain,
                                                                               networkCredentials.Password);
                if (autenticated)
                {
                    File.Move(file.Path, dumpFilePath);
                    NetworkShareAccessProvider.UndoImpersonation();
                }
                else
                {
                    Trace.Write.Error(NetworkShareAccessProvider.GetLastError(), classType, METHOD_NAME);
                }
            }
            else
            {
                File.Move(file.Path, dumpFilePath);
            }
        }

        public void Reject(TransferNetworkCredentials networkCredentials,
                           TransferDirectoryInfo directoryInfo,
                           FilePath file)
        {
            const string METHOD_NAME = "Reject`1";

            string blackListFilePath = Path.Combine(directoryInfo.BlackListPath, file.BackupFileName);

            if (networkCredentials.ExecuteImpersonation)
            {
                bool autenticated = NetworkShareAccessProvider.ImpersonateUser(networkCredentials.UserName,
                                                                               networkCredentials.Domain,
                                                                               networkCredentials.Password);
                if (autenticated)
                {
                    File.Move(file.Path, blackListFilePath);
                    NetworkShareAccessProvider.UndoImpersonation();
                }
                else
                {
                    Trace.Write.Error(NetworkShareAccessProvider.GetLastError(), classType, METHOD_NAME);
                }
            }
            else
            {
                File.Move(file.Path, blackListFilePath);
            }
        }

        public void Reject(TransferNetworkCredentials networkCredentials,
                           TransferDirectoryInfo directoryInfo,
                           FilePath file,
                           Action notifiyTransfer)
        {
            Reject(networkCredentials, directoryInfo, file);

            notifiyTransfer();
        }
    }
}