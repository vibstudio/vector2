using Eng.Collections;

namespace Eng.Vector.Win.UI.Controls
{
    #region Namespaces

    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;

    using Eng.Diagnostic;
    using Eng.Vector.Domain;
    using Eng.Vector.Domain.Model;
    using Eng.Vector.Domain.Model.Integration;
    using Eng.Vector.Engine.Transfer;
    using Eng.Vector.Globalization;
    using Eng.Vector.Util;

    #endregion

    public partial class EisDetailPanel : UserControl
    {
        #region Fields

        private readonly string _eisID;
        private readonly TransferDirection _transferDirection;
        
        private TransButton _transButton;

        #endregion

        #region Ctor(s)
        
        public EisDetailPanel(string eisID, string directoryPath, TransferDirection transferDirection, DirectoryHierarchicalScope hierarchicalScope)
        {
            InitializeComponent();

            _eisID = eisID;
            _transferDirection = transferDirection;

            InitializeDetail(_transferDirection, hierarchicalScope, directoryPath);
        }

        #endregion

        #region Overrides
        
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            Dock = DockStyle.Fill;
        }

        #endregion

        #region Event handlers

        void FileModeClick(object sender, EventArgs e)
        {
            fileListBox.SetSelected(0, true);
        }

        void DirectoryModeClick(object sender, EventArgs e)
        {
            fileListBox.ClearSelected();
        }

        private void AsyncModeClick(object sender, EventArgs e)
        {
            fileListBox.ClearSelected();
        }

        private void TransButtonRun(object sender, EventArgs e)
        {
            if (_transferDirection == TransferDirection.Input)
            {
                if (fileListBox.Items.Count > 0 && !string.IsNullOrEmpty(_eisID))
                {
                    switch (_transButton.ExecutionMode)
                    {
                        case TransferMode.Asynchronous:
                            PerformAsynchronousTransferInput();
                            break;
                        case TransferMode.Massive:
                            PerformMassiveTransferInput();
                            break;
                        case TransferMode.File:
                            PerformSingleTransferInput();
                            break;
                    }
                }
            }
            else
            {
                PerformTransferOutput();
            }
        }

        #region MassiveTransferInput

        private void PerformMassiveTransferInputDoWork(object sender, DoWorkEventArgs e)
        {
            IEnumerable<TransferFilePath> filePaths = fileListBox.Items.Cast<string>().ToArray().ToFilePaths(_eisID);

            e.Result = TransferInputManager.Factory.Run(filePaths);
        }

        private void PerformMassiveTransferInputRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                OperationResultCollection<TransferFilePath> resultCollection = (OperationResultCollection<TransferFilePath>)e.Result;

                // Operation performed
                if (!resultCollection.ThereAreErrors)
                {
                    MessageBox.Show(Labeling.Factory.Get.OperationPerformed.ToString(),
                                    Labeling.Factory.Get.InfoCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                // Operation failed
                else
                {
                    IList<string> failureMessages =
                        (from result in resultCollection
                         where !result.IsPerformed
                         select BuildFailureMessage(result.Output.FileName, result.Message)).ToList();

                    MessageBox.Show(Labeling.Factory.Get.OperationFailed(failureMessages).ToString(),
                                    Labeling.Factory.Get.ErrorCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }

                fileListBox.Items.Clear();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, Labeling.Factory.Get.ErrorCaption.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #region TransferOutput

        private void PerformTransferOutputDoWork(object sender, DoWorkEventArgs e)
        {
            Eis eis = Helper.Factory.Of.Vector.EisConfiguration.EisConfig.GetEis(_eisID);
            e.Result = TransferOutputManager.Factory.Run(eis);
        }

        private void PerformTransferOutputRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                TransferOutputResultCollection resultCollection = (TransferOutputResultCollection)e.Result;

                if (!resultCollection.ThereAreErrors)
                {
                    MessageBox.Show(Labeling.Factory.Get.OperationPerformed.ToString(),
                                    Labeling.Factory.Get.InfoCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    Eis eis = Helper.Factory.Of.Vector.EisConfiguration.EisConfig.GetEis(_eisID);
                    RefreshFileList(eis.Output.DirectoryInfo.RootPath);
                }
                else
                {
                    IList<string> failureMessages =
                        (from result in resultCollection
                         where !result.IsPerformed
                         select BuildFailureMessage(result.Output, result.Message)).ToList();

                    MessageBox.Show(Labeling.Factory.Get.OperationFailed(failureMessages).ToString(),
                                    Labeling.Factory.Get.ErrorCaption.ToString(),
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
            catch (Exception exception)
            {
                Trace.Write.Error(exception, GetType(), "PerformTransferOutputRunWorkerCompleted");
                MessageBox.Show(exception.Message, Labeling.Factory.Get.ErrorCaption.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        #endregion

        #endregion

        #region Methods

        #region Transfers

        private void PerformAsynchronousTransferInput()
        {
            TransferInputManager.Factory.RunAsync(Helper.Factory.Of.Vector.EisConfiguration.EisConfig.GetFilePaths(_eisID));
        }

        private void PerformMassiveTransferInput()
        {
            Cursor = Cursors.WaitCursor;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += PerformMassiveTransferInputDoWork;
            worker.RunWorkerCompleted += PerformMassiveTransferInputRunWorkerCompleted;
            worker.RunWorkerAsync(this);
        }

        private void PerformSingleTransferInput()
        {
            OperationResult result = TransferInputManager.Factory.Run(fileListBox.GetItemText(fileListBox.SelectedItem).ToFilePath(_eisID));

            if (result.IsPerformed)
            {
                MessageBox.Show(Labeling.Factory.Get.OperationPerformed.ToString(),
                                Labeling.Factory.Get.InfoCaption.ToString(),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(result.Message,
                                Labeling.Factory.Get.ErrorCaption.ToString(),
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Error);
            }

            fileListBox.Items.RemoveAt(fileListBox.SelectedIndex);
        }

        private void PerformTransferOutput()
        {
            Cursor = Cursors.WaitCursor;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += PerformTransferOutputDoWork;
            worker.RunWorkerCompleted += PerformTransferOutputRunWorkerCompleted;
            worker.RunWorkerAsync(this);
        }

        #endregion
        
        private string BuildFailureMessage(string fileName, string failureMessage)
        {
            const string FAILURE_MESSAGE_FORMAT = "{0} -> {1}";
            return string.Format(FAILURE_MESSAGE_FORMAT, fileName, failureMessage);
        }
        
        private void InitializeDetail(TransferDirection transferDirection, DirectoryHierarchicalScope hierarchicalScope, string directoryPath)
        {
            fileListBox.Items.Clear();
            runningPanel.Controls.Clear();
            runningPanel.Visible = false;

            if (Directory.Exists(directoryPath))
            {
                string[]  filePaths = Directory.GetFiles(directoryPath);
                if (filePaths.Any())
                {
                    fileListBox.Items.AddRange(filePaths);
                    fileListBox.SetSelected(0, true);
                }

                if (hierarchicalScope != DirectoryHierarchicalScope.Dump && hierarchicalScope != DirectoryHierarchicalScope.BlackList)
                {
                    _transButton = new TransButton(transferDirection, filePaths);
                    _transButton.Run += TransButtonRun;
                    _transButton.FileModeClick += FileModeClick;
                    _transButton.DirectoryModeClick += DirectoryModeClick;
                    _transButton.AsyncModeClick += AsyncModeClick;

                    runningPanel.Controls.Add(_transButton);

                    runningPanel.Visible = true;
                }
            }
        }

        private void RefreshFileList(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            if (files.Any())
            {
                fileListBox.Items.Clear();
                fileListBox.Items.AddRange(files);
            }
        }

        #endregion
    }
}