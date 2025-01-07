namespace Models.Models.Solana
{
    public class SolHoldersInfo
    {
        public List<SolHolder> Holders { get; set; } = new List<SolHolder>();

        public double TotalTokenSupply { get; set; }
    }

    public class SolHolder
    {
        public string Address { get; set; } = string.Empty;

        public double TokensAmount { get; set; }

        public double SupplyPercentage { get; set; }
    }
}
