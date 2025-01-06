namespace Models.Models.Solana
{
    public class SolDevInfo
    {
        public string DevAddress { get; set; } = string.Empty;

        public double TokenAmount { get; set; }

        public int LinkedWalletsAmount { get; set; }

        public double TokensSentToLinkedWallets { get; set; }
    }
}