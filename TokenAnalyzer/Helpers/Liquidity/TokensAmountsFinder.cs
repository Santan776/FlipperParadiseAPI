using Solana.Unity.Rpc;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class TokensAmountsFinder
    {
        public static async Task<(decimal amountBase, decimal amountQuote)> GetTokensAmounts(IRpcClient rpc, string baseVault, string quoteVault)
        {
            var retry = 0;
            var amountBase = 0m;
            var amountQuote = 0m;
            while (retry++ < 3)
            {
                var amountBaseResponse = await rpc.GetTokenAccountBalanceAsync(baseVault);
                var amountQuoteResponse = await rpc.GetTokenAccountBalanceAsync(quoteVault);
                if (amountBaseResponse.WasSuccessful && amountQuoteResponse.WasSuccessful)
                {
                    amountBase = amountBaseResponse.Result.Value.AmountDecimal;
                    amountQuote = amountQuoteResponse.Result.Value.AmountDecimal;
                    break;
                }
            }
            return (amountBase, amountQuote);
        }
    }
}
