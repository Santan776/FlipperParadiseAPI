using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class FluxbeamPoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetFluxbeamPools(IRpcClient rpc,
            LiquidityPoolsResponse.Fluxbeam fluxbeamPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            foreach (var p in fluxbeamPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.TokenAccountA, p.TokenAccountB);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.MintA == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.MintA == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.MintA == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.MintB == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.MintB == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.MintB == tokenAddress)
                {
                    liquidityUSD += amountQuote * tokenPrice;
                }
                else
                {
                    continue;
                }
                pools.Add(new SolLiquidityPool()
                {
                    BalanceUSD = liquidityUSD,
                    Dex = "Fluxbeam",
                    PairAddress = p.Pubkey,
                    PercentageLocked = 0
                });
            }
            return pools;
        }
    }
}