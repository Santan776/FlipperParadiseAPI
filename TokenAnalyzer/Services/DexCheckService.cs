using Models.Models.Solana;
using Newtonsoft.Json;
using SolanaTokenAnalyzer.ResponseModels;
using Social = Models.Models.Solana.Social;

namespace SolanaTokenAnalyzer.Services
{
    public class DexCheckService(HttpClient httpClient)
    {
        public async Task<(SolMetadata metadata, string error)> UpdateDexScreenerData(string tokenAddress, SolMetadata metadata)
        {
            var retry = 0;
            var e = string.Empty;
            while(retry++ < 3)
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://api.dexscreener.com/latest/dex/tokens/{tokenAddress}");
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        e = response.StatusCode.ToString();
                        continue;
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    var tokenInfo = JsonConvert.DeserializeObject<DexScreenerResponse>(json);
                    if (tokenInfo != null && tokenInfo.Pairs != null && tokenInfo.Pairs.Count > 0)
                    {
                        if (tokenInfo.Pairs[0].Info != null)
                        {
                            metadata.Socials = GetSocials(tokenInfo);
                            metadata.HasProfileOnDexScreener = tokenInfo.Pairs[0].Info.ImageUrl != null || tokenInfo.Pairs[0].Info.Header != null;
                        }
                        return (metadata, string.Empty);
                    }
                    else
                    {
                        e = "Not listed on DexScreener yet!";
                    }
                }
                catch(Exception ex)
                {
                    e = ex.Message; 
                }
            }
            return (metadata, e);
        }

        private List<Social> GetSocials(DexScreenerResponse tokenInfo)
        {
            var socials = new List<Social>();
            foreach (var s in tokenInfo.Pairs[0].Info.Socials)
            {
                socials.Add(new Social()
                {
                    Label = s.Type,
                    Link = s.Url
                });
            }
            foreach (var w in tokenInfo.Pairs[0].Info.Websites)
            {
                socials.Add(new Social()
                {
                    Label = w.Label,
                    Link = w.Url
                });
            }
            return socials;
        }
    }
}
