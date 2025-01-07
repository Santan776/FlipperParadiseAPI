using Models.Models.Common;
using Models.Models.Solana;
using SolanaTokenAnalyzer;
using System.Net;

namespace FlipperParadiseAPI.Services
{
    public class SolanaTokensAnalyzerService(SolanaRPCConnection rpc, IConfiguration configuration)
    {
        public async Task<(SolMetadata metadata, string error)> GetTokenMetadata(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var result = await analyzer.GetTokenMetadata(tokenAddress, new HttpClient(), rpc.Connection1,
                configuration.GetSection("ApiKeys")["Helius"]);
            return result;
        }

        public async Task<(List<SolLiquidityPool> liquidityPools, string error)> GetTokenLiquidityPools(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var result = await analyzer.GetTokenLiquidityPools(tokenAddress, new HttpClient(), rpc.Connection1);
            return result;
        }

        public async Task<(SolDevInfo devInfo, string error)> GetTokenDevInfo(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var result = await analyzer.GetTokenDevInfo(tokenAddress, new HttpClient(), rpc.Connection1,
                configuration.GetSection("ApiKeys")["Helius"]);
            return result;
        }

        
    }
}