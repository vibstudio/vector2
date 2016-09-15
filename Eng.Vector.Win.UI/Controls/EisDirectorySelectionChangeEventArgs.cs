namespace Eng.Vector.Win.UI.Controls
{
    #region Namespaces

    using System;

    using Eng.Vector.Domain.Model;

    #endregion

    public class EisDirectorySelectionChangeEventArgs : EventArgs
    {
        public EisDirectorySelectionChangeEventArgs(string eisID,
                                                    string directoryPath,
                                                    TransferDirection direction,
                                                    DirectoryHierarchicalScope hierarchicalScope)
        {
            EisID = eisID;
            DirectoryPath = directoryPath;
            Direction = direction;
            HierarchicalScope = hierarchicalScope;
        }

        public string EisID { get; private set; }

        public string DirectoryPath { get; private set; }

        public TransferDirection Direction { get; private set; }

        public DirectoryHierarchicalScope HierarchicalScope { get; private set; }
    }
}