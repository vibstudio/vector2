namespace Eng.Vector.Win.UI.Controls
{
    using System.Drawing;
    using System.Windows.Forms;

    partial class EisDirectoryTabPage
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();

            inputPathsGroupBox = new GroupBox();
            inputRootPathLabel = new Label();
            inputDumpPathLabel = new Label();
            inputBlackListPathLabel = new Label();
            outputPathsGroupBox = new GroupBox();
            outputRootPathLabel = new Label();
            outputRootFilePathBox = new FilePathTextField();
            inputBlackListFilePathBox = new FilePathTextField();
            inputDumpFilePathBox = new FilePathTextField();
            inputRootFilePathBox = new FilePathTextField();
            inputPathsGroupBox.SuspendLayout();
            outputPathsGroupBox.SuspendLayout();
            SuspendLayout();
            // 
            // inputPathsGroupBox
            // 
            inputPathsGroupBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                        | AnchorStyles.Right;
            inputPathsGroupBox.Controls.Add(inputBlackListFilePathBox);
            inputPathsGroupBox.Controls.Add(inputBlackListPathLabel);
            inputPathsGroupBox.Controls.Add(inputDumpFilePathBox);
            inputPathsGroupBox.Controls.Add(inputDumpPathLabel);
            inputPathsGroupBox.Controls.Add(inputRootPathLabel);
            inputPathsGroupBox.Controls.Add(inputRootFilePathBox);
            inputPathsGroupBox.Location = new Point(7, 6);
            inputPathsGroupBox.Name = "inputPathsGroupBox";
            inputPathsGroupBox.Size = new Size(664, 173);
            inputPathsGroupBox.TabIndex = 0;
            inputPathsGroupBox.TabStop = false;
            inputPathsGroupBox.Text = "Input paths";
            // 
            // inputRootPathLabel
            // 
            inputRootPathLabel.AutoSize = true;
            inputRootPathLabel.Location = new Point(6, 25);
            inputRootPathLabel.Name = "inputRootPathLabel";
            inputRootPathLabel.Size = new Size(30, 13);
            inputRootPathLabel.TabIndex = 1;
            inputRootPathLabel.Text = "Root";
            // 
            // inputDumpPathLabel
            // 
            inputDumpPathLabel.AutoSize = true;
            inputDumpPathLabel.Location = new Point(6, 75);
            inputDumpPathLabel.Name = "inputDumpPathLabel";
            inputDumpPathLabel.Size = new Size(35, 13);
            inputDumpPathLabel.TabIndex = 2;
            inputDumpPathLabel.Text = "Dump";
            // 
            // inputBlackListPathLabel
            // 
            inputBlackListPathLabel.AutoSize = true;
            inputBlackListPathLabel.Location = new Point(6, 125);
            inputBlackListPathLabel.Name = "inputBlackListPathLabel";
            inputBlackListPathLabel.Size = new Size(49, 13);
            inputBlackListPathLabel.TabIndex = 4;
            inputBlackListPathLabel.Text = "Black list";
            // 
            // outputPathsGroupBox
            // 
            outputPathsGroupBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                        | AnchorStyles.Right;
            outputPathsGroupBox.Controls.Add(outputRootPathLabel);
            outputPathsGroupBox.Controls.Add(outputRootFilePathBox);
            outputPathsGroupBox.Location = new Point(7, 183);
            outputPathsGroupBox.Name = "outputPathsGroupBox";
            outputPathsGroupBox.Size = new Size(664, 75);
            outputPathsGroupBox.TabIndex = 1;
            outputPathsGroupBox.TabStop = false;
            outputPathsGroupBox.Text = "Output paths";
            // 
            // outputRootPathLabel
            // 
            outputRootPathLabel.AutoSize = true;
            outputRootPathLabel.Location = new Point(6, 26);
            outputRootPathLabel.Name = "outputRootPathLabel";
            outputRootPathLabel.Size = new Size(30, 13);
            outputRootPathLabel.TabIndex = 1;
            outputRootPathLabel.Text = "Root";
            // 
            // outputRootFilePathBox
            // 
            outputRootFilePathBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                           | AnchorStyles.Right;
            outputRootFilePathBox.Checked = false;
            outputRootFilePathBox.Location = new Point(6, 41);
            outputRootFilePathBox.Name = "outputRootFilePathBox";
            outputRootFilePathBox.Size = new Size(653, 26);
            outputRootFilePathBox.TabIndex = 0;
            outputRootFilePathBox.Text = null;
            outputRootFilePathBox.FilePathSelectionChanged += outputRootFilePathBox_FilePathSelectionChanged;
            // 
            // inputBlackListFilePathBox
            // 
            inputBlackListFilePathBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                               | AnchorStyles.Right;
            inputBlackListFilePathBox.Checked = false;
            inputBlackListFilePathBox.Location = new Point(6, 140);
            inputBlackListFilePathBox.Name = "inputBlackListFilePathBox";
            inputBlackListFilePathBox.Size = new Size(653, 26);
            inputBlackListFilePathBox.TabIndex = 5;
            inputBlackListFilePathBox.Text = null;
            inputBlackListFilePathBox.FilePathSelectionChanged += inputBlackListFilePathBox_FilePathSelectionChanged;
            // 
            // inputDumpFilePathBox
            // 
            inputDumpFilePathBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                          | AnchorStyles.Right;
            inputDumpFilePathBox.Checked = false;
            inputDumpFilePathBox.Location = new Point(6, 90);
            inputDumpFilePathBox.Name = "inputDumpFilePathBox";
            inputDumpFilePathBox.Size = new Size(653, 26);
            inputDumpFilePathBox.TabIndex = 3;
            inputDumpFilePathBox.Text = null;
            inputDumpFilePathBox.FilePathSelectionChanged += inputDumpFilePathBox_FilePathSelectionChanged;
            // 
            // inputRootFilePathBox
            // 
            inputRootFilePathBox.Anchor = (AnchorStyles.Top | AnchorStyles.Left)
                                          | AnchorStyles.Right;
            inputRootFilePathBox.Checked = true;
            inputRootFilePathBox.Location = new Point(6, 40);
            inputRootFilePathBox.Name = "inputRootFilePathBox";
            inputRootFilePathBox.Size = new Size(653, 26);
            inputRootFilePathBox.TabIndex = 0;
            inputRootFilePathBox.Text = null;
            inputRootFilePathBox.FilePathSelectionChanged += inputRootFilePathBox_FilePathSelectionChanged;
            // 
            // EisDirectoryTabPage
            // 
            Controls.Add(outputPathsGroupBox);
            Controls.Add(inputPathsGroupBox);
            Name = "EisDirectoryTabPage";
            Size = new Size(678, 265);
            inputPathsGroupBox.ResumeLayout(false);
            inputPathsGroupBox.PerformLayout();
            outputPathsGroupBox.ResumeLayout(false);
            outputPathsGroupBox.PerformLayout();
            ResumeLayout(false);
        }

        #endregion


        private FilePathTextField inputBlackListFilePathBox;
        private Label inputBlackListPathLabel;
        private FilePathTextField inputDumpFilePathBox;
        private Label inputDumpPathLabel;
        private GroupBox inputPathsGroupBox;
        private FilePathTextField inputRootFilePathBox;
        private Label inputRootPathLabel;
        private GroupBox outputPathsGroupBox;
        private FilePathTextField outputRootFilePathBox;
        private Label outputRootPathLabel;
    }
}
