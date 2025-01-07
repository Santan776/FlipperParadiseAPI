using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Solana.Unity.Rpc;
using System.IO.Compression;
using System.Net.Http.Headers;
using System.Text;
using SolanaTokenAnalyzer.Helpers.Liquidity;
using SolanaTokenAnalyzer.ResponseModels;
using Models.Models.Solana;

namespace SolanaTokenAnalyzer.Services
{
    public class LiquidityPoolsCheckerService(HttpClient httpClient, IRpcClient rpc)
    {
        public async Task<(List<SolLiquidityPool> pools, string error)> GetLiquidityPools(string tokenAddress)
        {
            var retry = 0;
            var e = string.Empty;
            while (retry++ < 3)
            {
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, $"https://defi.shyft.to/v0/pools/get_by_token?token={tokenAddress}");
                    request.Headers.Add("x-api-key", "gyYc_M66OTc462Do");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var response = await httpClient.SendAsync(request);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        e = response.StatusCode.ToString();
                        if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                            await Task.Delay(500);
                        continue;
                    }
                    var responseBytes = await response.Content.ReadAsByteArrayAsync();
                    var json = string.Empty;
                    using (var compressedStream = new MemoryStream(responseBytes))
                    using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
                    using (var reader = new StreamReader(gzipStream, Encoding.UTF8))
                    {
                        json = await reader.ReadToEndAsync();
                    }
                    var pools = await ReadPoolsDataFromJson(json, tokenAddress);
                    return (pools.pools, pools.error);
                }
                catch (Exception ex)
                {
                    e = ex.Message;
                }
            }
            return (new List<SolLiquidityPool>(), e);  
        }

        private async Task<(List<SolLiquidityPool> pools, string error)> ReadPoolsDataFromJson(string json, string tokenAddress)
        {
            var pools = new List<SolLiquidityPool>();
            var poolsData = JsonConvert.DeserializeObject<LiquidityPoolsResponse.Root>(json);
            if (poolsData == null || !poolsData.Success)
                return (pools, "Error fetching liquidity pools!");

            var prices = await GetTokensPrices(tokenAddress, Constants.WSOL_ADDRESS);
            if (!string.IsNullOrEmpty(prices.error))
                return (pools, prices.error);

            var tokenPrice = prices.tokenAPrice;
            var WSOLPrice = prices.tokenBPrice;

            if (poolsData.Result.Dexes.RaydiumAmm != null && poolsData.Result.Dexes.RaydiumAmm.Pools.Count > 0)
            {
                var raydiumAMMPools = await RaydiumAMMPoolsAnalyzer.GetRaydiumAMMPools(rpc, 
                    poolsData.Result.Dexes.RaydiumAmm, WSOLPrice, tokenPrice, tokenAddress);
                foreach(var p in raydiumAMMPools)
                    pools.Add(p);
            }
            if (poolsData.Result.Dexes.RaydiumClmm != null && poolsData.Result.Dexes.RaydiumClmm.Pools.Count > 0)
            {
                var raydiumCLMMPools = await RaydiumCLMMPoolsAnalyzer.GetRaydiumCLMMPools(rpc,
                    poolsData.Result.Dexes.RaydiumClmm, WSOLPrice, tokenPrice, tokenAddress);
                foreach (var p in raydiumCLMMPools)
                    pools.Add(p);
            }
            if (poolsData.Result.Dexes.RaydiumCpmm != null && poolsData.Result.Dexes.RaydiumCpmm.Pools.Count > 0)
            {
                var raydiumCPMMPools = await RaydiumCPMMPoolsAnalyzer.GetRaydiumCPMMPools(rpc,
                    poolsData.Result.Dexes.RaydiumCpmm, WSOLPrice, tokenPrice, tokenAddress);
                foreach (var p in raydiumCPMMPools)
                    pools.Add(p);
            }
            if (poolsData.Result.Dexes.Orca != null && poolsData.Result.Dexes.Orca.Pools.Count > 0)
            {
                var orcaPools = await OrcaWhirlpoolsAnalyzer.GetOrcaPools(rpc,
                    poolsData.Result.Dexes.Orca, WSOLPrice, tokenPrice, tokenAddress);
                foreach (var p in orcaPools)
                    pools.Add(p);
            }
            if (poolsData.Result.Dexes.Fluxbeam != null && poolsData.Result.Dexes.Fluxbeam.Pools.Count > 0)
            {
                var fluxbeamPools = await FluxbeamPoolsAnalyzer.GetFluxbeamPools(rpc,
                    poolsData.Result.Dexes.Fluxbeam, WSOLPrice, tokenPrice, tokenAddress);
                foreach (var p in fluxbeamPools)
                    pools.Add(p);
            }
            if (poolsData.Result.Dexes.MeteoraAmm != null && poolsData.Result.Dexes.MeteoraAmm.Pools.Count > 0)
            {
                var meteoraPools = await MeteoraPoolsAnalyzer.GetMeteoraPools(rpc,
                    poolsData.Result.Dexes.MeteoraAmm, WSOLPrice, tokenPrice, tokenAddress);
                foreach (var p in meteoraPools)
                    pools.Add(p);
            }
            return (pools.OrderByDescending(x => x.BalanceUSD).ToList(), string.Empty);
        }
        
        private async Task<(decimal tokenAPrice, decimal tokenBPrice, string error)> GetTokensPrices(string tokenAAddress, string tokenBAddress)
        {
            var retry = 0; 
            var e = string.Empty;  
            while (retry++ < 3)
            {
                var response = await httpClient.GetAsync($"https://api.jup.ag/price/v2?ids={tokenAAddress},{tokenBAddress}");
                if (response.StatusCode != System.Net.HttpStatusCode.OK)
                {
                    e = response.StatusCode.ToString();
                    if (response.StatusCode == System.Net.HttpStatusCode.TooManyRequests)
                        await Task.Delay(500);
                    continue;
                }
                var json = await response.Content.ReadAsStringAsync();
                var jObj = JObject.Parse(json);
                var tokenAPrice = jObj["data"]?[tokenAAddress]?["price"]?.Value<decimal>();
                if (tokenAPrice == null)
                {
                    e = "Error reading data json!";
                    continue;
                }
                var tokenBPrice = jObj["data"]?[tokenBAddress]?["price"]?.Value<decimal>();
                if (tokenBPrice == null)
                {
                    e = "Error reading data json!";
                    continue;
                }
                return (tokenAPrice.Value, tokenBPrice.Value, string.Empty);
            }
            return (0, 0, e);
        }
    }
}