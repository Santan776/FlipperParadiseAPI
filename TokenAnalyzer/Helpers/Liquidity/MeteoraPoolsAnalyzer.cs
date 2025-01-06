using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class MeteoraPoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetMeteoraPools(IRpcClient rpc,
                LiquidityPoolsResponse.MeteoraAmm meteoraPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            foreach (var p in meteoraPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.AVaultLp, p.BVaultLp);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.TokenAMint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.TokenAMint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.TokenAMint == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.TokenBMint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.TokenBMint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.TokenBMint == tokenAddress)
                {
                    liquidityUSD += amountQuote * tokenPrice;
                }
                else
                {
                    continue;
                }
                var burntAmount = await GetBurntLiquidity(rpc, p.LpMint, p.TotalLockedLp);
                pools.Add(new SolLiquidityPool()
                {
                    BalanceUSD = liquidityUSD,
                    Dex = "Meteora",
                    PairAddress = p.Pubkey,
                    PercentageLocked = burntAmount
                });
            }
            return pools;
        }

        private static async Task<double> GetBurntLiquidity(IRpcClient rpc, string lpMint, long lpLocked)
        {
            var retry = 0;
            while (retry++ < 3)
            {
                var supply = await rpc.GetTokenSupplyAsync(lpMint);
                if (!supply.WasSuccessful)
                    continue;
                var lpLockedDouble = lpLocked / Math.Pow(10, supply.Result.Value.Decimals);
                var burntAmount = lpLockedDouble / supply.Result.Value.AmountDouble * 100;
                return burntAmount;
            }
            return 0;
        }
    }
}