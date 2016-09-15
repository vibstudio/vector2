namespace Eng.Vector.Domain.Model.Integration
{
    public class TransferNetworkAccessInfo
    {
        public TransferNetworkCredentials Credentials { get; set; }

        public TransferNetworkAccessInfo()
        {
            Credentials = new TransferNetworkCredentials();
        }
    }
}