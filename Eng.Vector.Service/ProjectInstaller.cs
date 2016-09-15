namespace Eng.Vector.Service
{
    #region Namespaces

    using System.ComponentModel;
    using System.Configuration.Install;

    #endregion

    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}