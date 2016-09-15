#region Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;

using Eng.Diagnostic;

#endregion

namespace Eng.Vector.Domain.Model.Integration
{
    public class EisConfig : ICollection<Eis>
    {
        #region Fields

        private readonly HashSet<Eis> _list = new HashSet<Eis>();
        private readonly Type _classType = typeof(EisConfig);

        #endregion

        public IList<Eis> List
        {
            get { return _list.ToList(); }
        }

        public void Add(Eis item)
        {
            if (!_list.Add(item))
            {
                Trace.Write.Warn(string.Format("EIS '{0}' is already configured.", item.ID));
            }
        }

        public void Clear()
        {
            _list.Clear();
        }

        public bool Contains(Eis item)
        {
            return _list.Contains(item);
        }

        public void CopyTo(Eis[] array, int arrayIndex)
        {
            _list.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _list.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(Eis item)
        {
            return _list.Remove(item);
        }

        public IEnumerator<Eis> GetEnumerator()
        {
            return _list.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Eis GetEis(string eisID)
        {
            return List.FirstOrDefault(x => x.ID == eisID);
        }

        public TransferNetworkAccessInfo GetNetworkAccessInfo(string eisID)
        {
            foreach (Eis eis in _list)
            {
                if (string.Equals(eis.ID, eisID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return eis.NetworkAccessInfo;
                }
            }

            return new TransferNetworkAccessInfo();
        }

        public TransferFileInfo GetFileInfo(string eisID, TransferDirection transferDirection)
        {
            foreach (Eis eis in _list)
            {
                if (string.Equals(eis.ID, eisID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return transferDirection == TransferDirection.Input ? eis.Input.FileInfo : eis.Output.FileInfo;
                }
            }

            return new TransferFileInfo();
        }

        public TransferDirectoryInfo GetDirectoryInfo(string eisID, TransferDirection transferDirection)
        {
            foreach (Eis eis in _list)
            {
                if (string.Equals(eis.ID, eisID, StringComparison.CurrentCultureIgnoreCase))
                {
                    return transferDirection == TransferDirection.Input ? eis.Input.DirectoryInfo : eis.Output.DirectoryInfo;
                }
            }

            return new TransferDirectoryInfo();
        }

        public IEnumerable<TransferFilePath> GetFilePaths(string eisID,
                                                          TransferDirection direction = TransferDirection.Input,
                                                          DirectoryHierarchicalScope hierarchicalScope = DirectoryHierarchicalScope.Root)
        {
            const string METHOD_NAME = "GetFilePaths`1";
            int debugPosition = 1;

            Trace.Write.Debug("START", _classType, METHOD_NAME, debugPosition++);

            IList<TransferFilePath> eisFilePaths = new List<TransferFilePath>();

            string directoryPath = string.Empty;

            TransferDirectoryInfo directoryInfo = GetDirectoryInfo(eisID, direction);
            switch (hierarchicalScope)
            {
                case DirectoryHierarchicalScope.Root:
                    directoryPath = directoryInfo.RootPath;
                    break;
                case DirectoryHierarchicalScope.Dump:
                    directoryPath = directoryInfo.DumpPath;
                    break;
                case DirectoryHierarchicalScope.BlackList:
                    directoryPath = directoryInfo.BlackListPath;
                    break;
            }

            try
            {
                WindowsIdentity identity = WindowsIdentity.GetCurrent();
                Trace.Write.Debug(string.Format("User's Windows logon name: {0}", identity.Name), _classType, METHOD_NAME, debugPosition++);
                Trace.Write.Debug(string.Format("{0} is {1}authenticated", identity.Name, identity.IsAuthenticated ? "" : "not "), _classType, METHOD_NAME, debugPosition++);
                Trace.Write.Debug(string.Format("directoryPath: {0}", directoryPath), _classType, METHOD_NAME, debugPosition++);

                string[] filePaths = { };

                TransferNetworkCredentials networkCredentials = GetNetworkAccessInfo(eisID).Credentials;
                if (networkCredentials.ExecuteImpersonation)
                {
                    bool authenticated = NetworkShareAccessProvider.ImpersonateUser(networkCredentials.UserName, networkCredentials.Domain, networkCredentials.Password);

                    identity = WindowsIdentity.GetCurrent();
                    Trace.Write.Debug(string.Format("Impersonate user's Windows logon name: {0}", identity.Name), _classType, METHOD_NAME, debugPosition++);
                    Trace.Write.Debug(string.Format("{0} is {1}authenticated", identity.Name, identity.IsAuthenticated ? "" : "not "), _classType, METHOD_NAME, debugPosition);

                    if (authenticated)
                    {
                        filePaths = Directory.GetFiles(directoryPath);
                        NetworkShareAccessProvider.UndoImpersonation();
                    }
                    else
                    {
                        Trace.Write.Error(NetworkShareAccessProvider.GetLastError(), _classType, METHOD_NAME);
                    }
                }
                else
                {
                    filePaths = Directory.GetFiles(directoryPath);
                }

                foreach (string filePath in filePaths)
                {
                    TransferFilePath transferFilePath = new TransferFilePath(filePath) { Specialization = new TransferID { EisID = eisID } };
                    eisFilePaths.Add(transferFilePath);
                }
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, _classType, METHOD_NAME);
            }

            return eisFilePaths;
        }

        public IEnumerable<TransferFilePath> GetFilePaths(TransferDirection direction = TransferDirection.Input,
                                                          DirectoryHierarchicalScope hierarchicalScope = DirectoryHierarchicalScope.Root)
        {
            const string METHOD_NAME = "GetFilePaths`2";

            Trace.Write.Debug("START", _classType, METHOD_NAME);

            List<TransferFilePath> eisFilePaths = new List<TransferFilePath>();

            foreach (Eis eis in _list)
            {
                eisFilePaths.AddRange(GetFilePaths(eis.ID, direction, hierarchicalScope));
            }

            return eisFilePaths;
        }
    }
}