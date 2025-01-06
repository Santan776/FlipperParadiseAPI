using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class RaydiumCLMMPoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetRaydiumCLMMPools(IRpcClient rpc, 
            LiquidityPoolsResponse.RaydiumClmm raydiumPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
             var pools = new List<SolLiquidityPool>();
            foreach (var p in raydiumPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.TokenVault0, p.TokenVault1);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.TokenMint0 == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.TokenMint0 == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.TokenMint0 == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.TokenMint1 == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.TokenMint1 == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.TokenMint1 == tokenAddress)
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
                    Dex = "Raydium CLMM",
                    PairAddress = p.Pubkey,
                    PercentageLocked = 0
                });
            }
            return pools;
        }
    }
}
