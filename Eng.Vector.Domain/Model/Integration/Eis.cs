namespace Eng.Vector.Domain.Model.Integration
{
    /// <summary>
    /// [E]xternal [I]ntegration [S]ystem
    /// </summary>
    public class Eis
    {
        public string ID { get; set; }

        public TransferNetworkAccessInfo NetworkAccessInfo { get; set; }

        public TransferInput Input { get; set; }

        public TransferOutput Output { get; set; }
    }
}
