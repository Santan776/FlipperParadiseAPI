namespace Models.Models.Solana
{
    public class SolLiquidityPool
    {
        public string Dex { get; set; } = string.Empty;

        public string PairAddress { get; set; } = string.Empty;

        public decimal BalanceUSD { get; set; }

        public double PercentageLocked { get; set; }
    }
}