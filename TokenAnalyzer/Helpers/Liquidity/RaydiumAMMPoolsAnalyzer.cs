using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class RaydiumAMMPoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetRaydiumAMMPools(IRpcClient rpc,
            LiquidityPoolsResponse.RaydiumAmm raydiumPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            foreach (var p in raydiumPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.BaseVault, p.QuoteVault);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.BaseMint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.BaseMint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.BaseMint == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.QuoteMint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.QuoteMint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.QuoteMint == tokenAddress)
                {
                    liquidityUSD += amountQuote * tokenPrice;
                }
                else
                {
                    continue;
                }
                var burntAmount = await GetBurntLiquidity(rpc, p.LpMint, p.LpReserve);
                pools.Add(new SolLiquidityPool()
                {
                    BalanceUSD = liquidityUSD,
                    Dex = "Raydium AMM",
                    PairAddress = p.Pubkey,
                    PercentageLocked = burntAmount
                });
            }
            return pools;
        }

        private static async Task<double> GetBurntLiquidity(IRpcClient rpc, string lpMint, long lpReserve)
        {
            var retry = 0;
            while(retry++ < 3)
            {
                var supply = await rpc.GetTokenSupplyAsync(lpMint);
                if (!supply.WasSuccessful)
                    continue;
                var reserve = lpReserve / Math.Pow(10, supply.Result.Value.Decimals);
                var burntAmount = (reserve - supply.Result.Value.AmountDouble) / reserve * 100;
                return burntAmount;
            }
            return 0;
        }
    }
}
