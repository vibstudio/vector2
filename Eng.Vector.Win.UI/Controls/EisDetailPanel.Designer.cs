namespace Eng.Vector.Win.UI.Controls
{
    partial class EisDetailPanel
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
            this.runningPanel = new System.Windows.Forms.Panel();
            this.fileListPanel = new System.Windows.Forms.Panel();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.fileListPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // runningPanel
            // 
            this.runningPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.runningPanel.Location = new System.Drawing.Point(0, 405);
            this.runningPanel.Name = "runningPanel";
            this.runningPanel.Size = new System.Drawing.Size(300, 90);
            this.runningPanel.TabIndex = 0;
            // 
            // fileListPanel
            // 
            this.fileListPanel.Controls.Add(this.fileListBox);
            this.fileListPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListPanel.Location = new System.Drawing.Point(0, 0);
            this.fileListPanel.Name = "fileListPanel";
            this.fileListPanel.Padding = new System.Windows.Forms.Padding(3, 8, 8, 1);
            this.fileListPanel.Size = new System.Drawing.Size(300, 405);
            this.fileListPanel.TabIndex = 1;
            // 
            // fileListBox
            // 
            this.fileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.HorizontalScrollbar = true;
            this.fileListBox.Location = new System.Drawing.Point(3, 8);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.Size = new System.Drawing.Size(289, 396);
            this.fileListBox.TabIndex = 0;
            // 
            // EisDetailPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fileListPanel);
            this.Controls.Add(this.runningPanel);
            this.Name = "EisDetailPanel";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.Size = new System.Drawing.Size(300, 500);
            this.fileListPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel runningPanel;
        private System.Windows.Forms.Panel fileListPanel;
        private System.Windows.Forms.ListBox fileListBox;
    }
}
