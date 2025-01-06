using Models.Models.Solana;
using Newtonsoft.Json;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Services
{
    public class MetadataCheckService(HttpClient httpClient, string heliusApiKey)
    {
        public async Task<(SolMetadata metadata, string error)> UpdateMetadata(string tokenAddress, SolMetadata metadata)
        {
            var retry = 0;
            var e = string.Empty;
            while (retry++ < 3)
            {
                try
                {
                    var content = new StringContent($"{{\r\n      \"jsonrpc\": \"2.0\",\r\n      \"id\": \"test\",\r\n      \"method\": \"getAsset\",\r\n      \"params\": {{\r\n        \"id\": \"{tokenAddress}\"\r\n      }}\r\n    }}");
                    var response = await httpClient.PostAsync($"https://mainnet.helius-rpc.com/?api-key={heliusApiKey}", content);
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        e = response.StatusCode.ToString();
                        continue;
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    var tokenInfo = JsonConvert.DeserializeObject<HeliusMetadataResponse>(json);
                    if (tokenInfo == null || tokenInfo.Result == null)
                    {
                        e = "Can't deserialize helius response";
                        continue;
                    }
                    metadata.Name = tokenInfo.Result.Content.Metadata.Name;
                    metadata.Ticker = tokenInfo.Result.Content.Metadata.Symbol;
                    metadata.Description = tokenInfo.Result.Content.Metadata.Description;
                    metadata.IsMutable = tokenInfo.Result.Mutable;
                    metadata.ImageLink = tokenInfo.Result.Content.Links.Image;
                    return (metadata, string.Empty);
                }
                catch (Exception ex)
                {
                    e = ex.Message;
                    continue;
                }
            }
            return (metadata, e);
        }
    }
}