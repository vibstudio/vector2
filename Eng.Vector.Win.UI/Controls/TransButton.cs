namespace Eng.Vector.Win.UI.Controls
{
    #region Namespaces

    using System;
    using System.Linq;
    using System.Windows.Forms;

    using Eng.Vector.Domain.Model;
    using Eng.Vector.Globalization;

    #endregion

    public partial class TransButton : Panel
    {
        #region Ctor(s)

        public TransButton(TransferDirection transferDirection, string[] filePaths)
        {
            InitializeComponent();

            InitializeLabels();

            InitializePanel(transferDirection, filePaths);
        }

        #endregion

        #region Overrides

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Dock = DockStyle.Fill;
        }

        #endregion
        
        #region Properties

        public TransferMode ExecutionMode { get; private set; }

        #endregion

        #region Events

        public event EventHandler FileModeClick
        {
            add { fileModeRadioButton.Click += value; }
            remove { fileModeRadioButton.Click -= value; }
        }

        public event EventHandler DirectoryModeClick
        {
            add { directoryModeRadioButton.Click += value; }
            remove { directoryModeRadioButton.Click -= value; }
        }

        public event EventHandler AsyncModeClick
        {
            add { asyncModeRadioButton.Click += value; }
            remove { asyncModeRadioButton.Click -= value; }
        }

        public event EventHandler Run
        {
            add { runButton.Click += value; }
            remove { runButton.Click -= value; }
        }

        #endregion

        #region Event handlers

        private void directoryModeRadioButton_Click(object sender, EventArgs e)
        {
            ExecutionMode = TransferMode.Massive;
        }

        private void fileModeRadioButton_Click(object sender, EventArgs e)
        {
            ExecutionMode = TransferMode.File;
        }

        private void asyncModeRadioButton_Click(object sender, EventArgs e)
        {
            ExecutionMode = TransferMode.Asynchronous;
        }

        #endregion

        #region Methods

        private void InitializeLabels()
        {
            runButton.Text = Labeling.Factory.Get.Run.ToString();
        }

        private void InitializePanel(TransferDirection transferDirection, string[] filePaths)
        {
            if (directoryModeRadioButton.Checked)
            {
                ExecutionMode = TransferMode.Massive;
            }
            else if (fileModeRadioButton.Checked)
            {
                ExecutionMode = TransferMode.File;
            }
            else if (asyncModeRadioButton.Checked)
            {
                ExecutionMode = TransferMode.Asynchronous;
            }

            switch (transferDirection)
            {
                case TransferDirection.Output:
                    runningModePanel.Visible = false;
                    break;
                case TransferDirection.Input:
                    if (filePaths == null || !filePaths.Any())
                    {
                        Enabled = false;
                    }
                    break;
            }
        }

        #endregion
    }
}