using Models.Models.Solana;
using Solana.Unity.Rpc;
using Solana.Unity.Rpc.Models;

namespace SolanaTokenAnalyzer.Services
{
    public class TopHoldersCheckService(IRpcClient rpc)
    {
        public async Task<(SolHoldersInfo holdersInfo, string error)> GetTopHolders(string tokenAddress)
        {
            var retry = 0;
            var e = string.Empty;
            while (retry++ < 3)
            {
                var supply = await rpc.GetTokenSupplyAsync(tokenAddress);
                var totalSupply = 0d;
                if (supply.WasSuccessful && supply.Result.Value != null)
                {
                    totalSupply = supply.Result.Value.AmountDouble;
                    var topHolders = await rpc.GetTokenLargestAccountsAsync(tokenAddress);
                    if (topHolders.WasSuccessful && topHolders.Result.Value != null)
                    {
                        var holders = await InvestigateHolders(topHolders.Result.Value
                            .OrderByDescending(x => x.AmountDouble)
                            .Take(10)
                            .ToList(), totalSupply);
                        if (!string.IsNullOrEmpty(holders.error))
                        {
                            e = holders.error;
                            continue;
                        }
                        return (new SolHoldersInfo()
                        {
                            TotalTokenSupply = totalSupply,
                            Holders = holders.holders
                        }, string.Empty);
                    }
                    else
                    {
                        e = supply.Reason;
                        continue;
                    }
                }
                else
                {
                    e = supply.Reason;
                    continue;
                }
            }
            return (new SolHoldersInfo(), e);
        }

        private async Task<(List<SolHolder> holders, string error)> InvestigateHolders(List<LargeTokenAccount> tokenAccounts, double supply)
        {
            var holders = new List<SolHolder>();
            var e = string.Empty;
            foreach (var a in tokenAccounts)
            {
                var address = await rpc.GetTokenAccountInfoAsync(a.Address);
                if (!address.WasSuccessful || address.Result.Value == null)
                {
                    e = address.Reason;
                    break;
                }
                holders.Add(new SolHolder()
                {
                    Address = address.Result.Value.Data.Parsed.Info.Owner,
                    TokensAmount = a.AmountDouble,
                    SupplyPercentage = Math.Round(a.AmountDouble / supply * 100, 2)
                });
            }
            return (holders, e);    
        }
    }
}
