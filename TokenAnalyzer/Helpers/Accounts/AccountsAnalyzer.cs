using Solana.Unity.Rpc;

namespace SolanaTokenAnalyzer.Helpers.Accounts
{
    public static class AccountsAnalyzer
    {
        public static async Task<(string ATA, double amount, string error)> GetAccountTokenAmount(IRpcClient rpc, string accountAddress, string tokenAddress)
        {
            var e = string.Empty;
            var retry = 0;
            while (retry++ < 3)
            {
                var ATAs = await rpc.GetTokenAccountsByOwnerAsync(accountAddress, tokenAddress);
                if (!ATAs.WasSuccessful)
                {
                    e = ATAs.Reason;
                    continue;
                }
                if (ATAs.Result.Value.Count == 0)
                {
                    return (string.Empty, 0, string.Empty);
                }
                return (ATAs.Result.Value[0].PublicKey, ATAs.Result.Value[0].Account.Data.Parsed.Info.TokenAmount.AmountDouble, string.Empty);
            }
            return (string.Empty, 0, e);
        }
    }
}
