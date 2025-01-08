namespace Models.Models.Solana
{
    public class SolDevInfo
    {
        public string DevAddress { get; set; } = string.Empty;

        public double TokenAmount { get; set; }

        public int LinkedWalletsAmount { get; set; }

        public double TokensSentToLinkedWallets { get; set; }

        public List<SolDevPreviousCoin> DevPreviousCoins { get; set; } = new List<SolDevPreviousCoin>();
    }

    public class SolDevPreviousCoin
    {
        public string Mint { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string Ticker { get; set; } = string.Empty;

        public string Image { get; set; } = string.Empty;

        public double MarketCap { get; set; }

        public bool NSFW { get; set; }

        public bool Complete { get; set; }
    }
}