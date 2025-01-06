using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Helpers.Liquidity
{
    public static class RaydiumCPMMPoolsAnalyzer
    {
        public static async Task<List<SolLiquidityPool>> GetRaydiumCPMMPools(IRpcClient rpc,
                LiquidityPoolsResponse.RaydiumCpmm raydiumPools, decimal WSOLPrice, decimal tokenPrice, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            foreach (var p in raydiumPools.Pools)
            {
                var liquidityUSD = 0m;
                var amounts = await TokensAmountsFinder.GetTokensAmounts(rpc, p.Token0Vault, p.Token1Vault);
                var amountBase = amounts.amountBase;
                var amountQuote = amounts.amountQuote;
                if (amountBase == 0 || amountQuote == 0)
                    continue;

                if (p.Token0Mint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountBase * WSOLPrice;
                }
                else if (p.Token0Mint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountBase;
                }
                else if (p.Token0Mint == tokenAddress)
                {
                    liquidityUSD += amountBase * tokenPrice;
                }
                else
                {
                    continue;
                }

                if (p.Token1Mint == Constants.WSOL_ADDRESS)
                {
                    liquidityUSD += amountQuote * WSOLPrice;
                }
                else if (p.Token1Mint == Constants.USDC_ADDRESS)
                {
                    liquidityUSD += amountQuote;
                }
                else if (p.Token1Mint == tokenAddress)
                {
                    liquidityUSD += amountQuote * tokenPrice;
                }
                else
                {
                    continue;
                }
                var burntAmount = await GetBurntLiquidity(rpc, p.LpMint, p.LpSupply);
                pools.Add(new SolLiquidityPool()
                {
                    BalanceUSD = liquidityUSD,
                    Dex = "Raydium CPMM",
                    PairAddress = p.Pubkey,
                    PercentageLocked = burntAmount
                });
            }
            return pools;
        }

        private static async Task<double> GetBurntLiquidity(IRpcClient rpc, string lpMint, long lpReserve)
        {
            var retry = 0;
            while (retry++ < 3)
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
