namespace Eng.Vector.Win.UI.Views
{
    partial class WindowsServiceView
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
            this.components = new System.ComponentModel.Container();
            this.windowsServiceDashboard = new Eng.Windows.Forms.WindowsServiceDashboard(this.components);
            this.SuspendLayout();
            // 
            // windowsServiceDashboard
            // 
            this.windowsServiceDashboard.Location = new System.Drawing.Point(-1, 0);
            this.windowsServiceDashboard.Name = "windowsServiceDashboard";
            this.windowsServiceDashboard.Size = new System.Drawing.Size(359, 108);
            this.windowsServiceDashboard.TabIndex = 0;
            // 
            // WindowsServiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.windowsServiceDashboard);
            this.Name = "WindowsServiceView";
            this.Size = new System.Drawing.Size(359, 108);
            this.ResumeLayout(false);

        }

        #endregion

        private Eng.Windows.Forms.WindowsServiceDashboard windowsServiceDashboard;


    }
}
