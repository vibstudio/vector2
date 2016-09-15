namespace Eng.Vector.Domain.Model.Integration
{
    public class TransferNetworkCredentials
    {
        public string UserName { get; set; }

        public string Domain { get; set; }

        public string Password { get; set; }

        public bool ExecuteImpersonation
        {
            get
            {
                // Perchè possa eseguita l'impersonazione in sicurezza devono essere specificati tutti i parametri di accesso
                return !string.IsNullOrEmpty(UserName)
                       &&
                       !string.IsNullOrEmpty(Domain)
                       &&
                       !string.IsNullOrEmpty(Password);
            }
        }

        public override string ToString()
        {
            return string.Format("Domain: {0}, UserName: {1}, Password: {2}", Domain, UserName, Password);
        }
    }
}