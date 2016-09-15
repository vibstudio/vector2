namespace Eng.Vector.Win.UI.Controls
{
    partial class TransButton
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

            this.runningModePanel = new System.Windows.Forms.Panel();
            this.runninModeTableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.directoryModeRadioButton = new System.Windows.Forms.RadioButton();
            this.fileModeRadioButton = new System.Windows.Forms.RadioButton();
            this.asyncModeRadioButton = new System.Windows.Forms.RadioButton();
            this.runningPanel = new System.Windows.Forms.Panel();
            this.runButton = new System.Windows.Forms.Button();
            this.runningModePanel.SuspendLayout();
            this.runninModeTableLayoutPanel.SuspendLayout();
            this.runningPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // runningModePanel
            // 
            this.runningModePanel.Controls.Add(this.runninModeTableLayoutPanel);
            this.runningModePanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.runningModePanel.Location = new System.Drawing.Point(0, 0);
            this.runningModePanel.Name = "runningModePanel";
            this.runningModePanel.Size = new System.Drawing.Size(268, 42);
            this.runningModePanel.TabIndex = 1;
            // 
            // runninModeTableLayoutPanel
            // 
            this.runninModeTableLayoutPanel.ColumnCount = 3;
            this.runninModeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.runninModeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.runninModeTableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33F));
            this.runninModeTableLayoutPanel.Controls.Add(this.fileModeRadioButton, 0, 0);
            this.runninModeTableLayoutPanel.Controls.Add(this.directoryModeRadioButton, 1, 0);
            this.runninModeTableLayoutPanel.Controls.Add(this.asyncModeRadioButton, 2, 0);
            this.runninModeTableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runninModeTableLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.runninModeTableLayoutPanel.Name = "runninModeTableLayoutPanel";
            this.runninModeTableLayoutPanel.RowCount = 1;
            this.runninModeTableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.runninModeTableLayoutPanel.Size = new System.Drawing.Size(268, 42);
            this.runninModeTableLayoutPanel.TabIndex = 2;
            // 
            // directoryModeRadioButton
            // 
            this.directoryModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.directoryModeRadioButton.AutoSize = true;
            this.directoryModeRadioButton.Location = new System.Drawing.Point(64, 3);
            this.directoryModeRadioButton.Name = "directoryModeRadioButton";
            this.directoryModeRadioButton.Size = new System.Drawing.Size(67, 36);
            this.directoryModeRadioButton.TabIndex = 0;
            this.directoryModeRadioButton.TabStop = true;
            this.directoryModeRadioButton.Text = "Directory";
            this.directoryModeRadioButton.UseVisualStyleBackColor = true;
            this.directoryModeRadioButton.Click += new System.EventHandler(this.directoryModeRadioButton_Click);
            // 
            // fileModeRadioButton
            // 
            this.fileModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.fileModeRadioButton.AutoSize = true;
            this.fileModeRadioButton.Checked = true;
            this.fileModeRadioButton.Location = new System.Drawing.Point(142, 3);
            this.fileModeRadioButton.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.fileModeRadioButton.Name = "fileModeRadioButton";
            this.fileModeRadioButton.Size = new System.Drawing.Size(41, 36);
            this.fileModeRadioButton.TabIndex = 1;
            this.fileModeRadioButton.TabStop = true;
            this.fileModeRadioButton.Text = "File";
            this.fileModeRadioButton.UseVisualStyleBackColor = true;
            this.fileModeRadioButton.Click += new System.EventHandler(this.fileModeRadioButton_Click);
            // 
            // asyncModeRadioButton
            // 
            this.asyncModeRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) | System.Windows.Forms.AnchorStyles.Left)));
            this.asyncModeRadioButton.AutoSize = true;
            this.asyncModeRadioButton.Location = new System.Drawing.Point(142, 3);
            this.asyncModeRadioButton.Margin = new System.Windows.Forms.Padding(8, 3, 3, 3);
            this.asyncModeRadioButton.Name = "fileModeRadioButton";
            this.asyncModeRadioButton.Size = new System.Drawing.Size(41, 36);
            this.asyncModeRadioButton.TabIndex = 1;
            this.asyncModeRadioButton.TabStop = true;
            this.asyncModeRadioButton.Text = "Async";
            this.asyncModeRadioButton.UseVisualStyleBackColor = true;
            this.asyncModeRadioButton.Click += new System.EventHandler(this.asyncModeRadioButton_Click);
            // 
            // runningPanel
            // 
            this.runningPanel.Controls.Add(this.runButton);
            this.runningPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runningPanel.Location = new System.Drawing.Point(0, 42);
            this.runningPanel.Name = "runningPanel";
            this.runningPanel.Padding = new System.Windows.Forms.Padding(3, 3, 8, 3);
            this.runningPanel.Size = new System.Drawing.Size(268, 58);
            this.runningPanel.TabIndex = 2;
            // 
            // runButton
            // 
            this.runButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.runButton.Location = new System.Drawing.Point(3, 3);
            this.runButton.Name = "runButton";
            this.runButton.Padding = new System.Windows.Forms.Padding(3);
            this.runButton.Size = new System.Drawing.Size(257, 52);
            this.runButton.TabIndex = 1;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            // 
            // TransButtonControl
            // 
            this.Controls.Add(this.runningPanel);
            this.Controls.Add(this.runningModePanel);
            this.Name = "TransButton";
            this.Size = new System.Drawing.Size(268, 100);
            this.runningModePanel.ResumeLayout(false);
            this.runninModeTableLayoutPanel.ResumeLayout(false);
            this.runninModeTableLayoutPanel.PerformLayout();
            this.runningPanel.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel runningModePanel;
        private System.Windows.Forms.TableLayoutPanel runninModeTableLayoutPanel;
        private System.Windows.Forms.RadioButton directoryModeRadioButton;
        private System.Windows.Forms.RadioButton fileModeRadioButton;
        private System.Windows.Forms.RadioButton asyncModeRadioButton;
        private System.Windows.Forms.Panel runningPanel;
        private System.Windows.Forms.Button runButton;
    }
}
