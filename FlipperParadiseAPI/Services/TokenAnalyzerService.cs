using Models.Models.Solana;
using SolanaTokenAnalyzer;
using System.Net;

namespace FlipperParadiseAPI.Services
{
    public class TokenAnalyzerService(SolanaRPCConnection rpc, IConfiguration configuration)
    {
        public async Task<(SolMetadata metadata, string error)> GetTokenMetadata(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var result = await analyzer.GetTokenMetadata(tokenAddress, new HttpClient(), rpc.Connection,
                configuration.GetSection("ApiKeys")["Helius"]);
            return result;
        }

        public async Task<(List<SolLiquidityPool> liquidityPools, string error)> GetTokenLiquidityPools(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var result = await analyzer.GetTokenLiquidityPools(tokenAddress, new HttpClient(), rpc.Connection);
            return result;
        }

        public async Task<(SolDevInfo devInfo, string error)> GetTokenDevInfo(string tokenAddress)
        {
            var analyzer = new TokenAnalyzerAPI();
            var proxy = new WebProxy("95.214.123.76", 8080);
            var handler = new HttpClientHandler()
            {
                Proxy = proxy,
                UseProxy = true
            };
            var result = await analyzer.GetTokenDevInfo(tokenAddress, new HttpClient(), rpc.Connection,
                configuration.GetSection("ApiKeys")["Helius"]);
            return result;
        }
    }
}