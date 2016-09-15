namespace Eng.Vector.Win.UI.Controls
{
    #region Namespaces

    using System;
    using System.Windows.Forms;

    using Eng.Vector.Domain.Model;
    using Eng.Vector.Domain.Model.Integration;
    using Eng.Vector.Globalization;
    using Eng.Vector.Util;

    #endregion

    public partial class EisDirectoryTabPage : TabPage
    {
        #region Events

        public event EventHandler<EisDirectorySelectionChangeEventArgs> SelectionChanged;

        #endregion

        #region Ctor(s)

        public EisDirectoryTabPage(string eisID)
            : base(eisID)
        {
            InitializeComponent();

            EisConfig eisConfig = Helper.Factory.Of.Vector.EisConfiguration.EisConfig;

            TransferDirectoryInfo inputDirectoryInfo = eisConfig.GetDirectoryInfo(eisID, TransferDirection.Input);
            inputRootFilePathBox.Text = inputDirectoryInfo.RootPath;
            inputDumpFilePathBox.Text = inputDirectoryInfo.DumpPath;
            inputBlackListFilePathBox.Text = inputDirectoryInfo.BlackListPath;

            TransferDirectoryInfo outputDirectoryInfo = eisConfig.GetDirectoryInfo(eisID, TransferDirection.Output);
            outputRootFilePathBox.Text = outputDirectoryInfo.RootPath;

            InitializeLabels();
        }

        #endregion

        #region Event handlers

        private void inputRootFilePathBox_FilePathSelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(inputRootFilePathBox.Text, TransferDirection.Input, DirectoryHierarchicalScope.Root);
        }

        private void inputDumpFilePathBox_FilePathSelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(inputDumpFilePathBox.Text, TransferDirection.Input, DirectoryHierarchicalScope.Dump);
        }

        private void inputBlackListFilePathBox_FilePathSelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(inputBlackListFilePathBox.Text, TransferDirection.Input, DirectoryHierarchicalScope.BlackList);
        }

        private void outputRootFilePathBox_FilePathSelectionChanged(object sender, EventArgs e)
        {
            OnSelectionChanged(outputRootFilePathBox.Text, TransferDirection.Output, DirectoryHierarchicalScope.Root);
        }

        #endregion

        #region Methods

        private void InitializeLabels()
        {
            // Input
            inputPathsGroupBox.Text = Labeling.Factory.Get.InputPaths.ToString();
            inputRootPathLabel.Text = Labeling.Factory.Get.RootPath.ToString();
            inputDumpPathLabel.Text = Labeling.Factory.Get.DumpPath.ToString();
            inputBlackListPathLabel.Text = Labeling.Factory.Get.BlackListPath.ToString();

            // Output
            outputPathsGroupBox.Text = Labeling.Factory.Get.OutputPaths.ToString();
            outputRootPathLabel.Text = Labeling.Factory.Get.RootPath.ToString();
        }

        public void ResetSelection()
        {
            OnSelectionChanged(inputRootFilePathBox.Text, TransferDirection.Input, DirectoryHierarchicalScope.Root);
        }

        protected virtual void EisDirectorySelectionChanged(EisDirectorySelectionChangeEventArgs args)
        {
            if (SelectionChanged != null) SelectionChanged(this, args);
        }

        private void OnSelectionChanged(string directoryPath, TransferDirection direction, DirectoryHierarchicalScope hierarchicalScope)
        {
            EisDirectorySelectionChangeEventArgs args = new EisDirectorySelectionChangeEventArgs(Text, directoryPath, direction, hierarchicalScope);
            EisDirectorySelectionChanged(args);

            inputRootFilePathBox.Checked = false;
            inputDumpFilePathBox.Checked = false;
            inputBlackListFilePathBox.Checked = false;
            outputRootFilePathBox.Checked = false;

            switch (direction)
            {
                case TransferDirection.Input:
                    switch (hierarchicalScope)
                    {
                        case DirectoryHierarchicalScope.Root:
                            inputRootFilePathBox.Checked = true;
                            break;
                        case DirectoryHierarchicalScope.Dump:
                            inputDumpFilePathBox.Checked = true;
                            break;
                        case DirectoryHierarchicalScope.BlackList:
                            inputBlackListFilePathBox.Checked = true;
                            break;
                    }
                    break;
                case TransferDirection.Output:
                    outputRootFilePathBox.Checked = true;
                    break;
            }
        }

        #endregion
    }
}