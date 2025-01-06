using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class OrcaWhirlpoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetOrcaPools(IRpcClient rpc,
            LiquidityPoolsResponse.Orca orcaPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            foreach (var p in orcaPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.TokenVaultA, p.TokenVaultB);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.TokenMintA == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.TokenMintA == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.TokenMintA == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.TokenMintB == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.TokenMintB == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.TokenMintB == tokenAddress)
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
                    Dex = "Orca",
                    PairAddress = p.Pubkey,
                    PercentageLocked = 0
                });
            }
            return pools;
        }
    }
}