using Models.Models.Solana;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.Helpers.Accounts;
using SolanaTokenAnalyzer.ResponseModels;

namespace SolanaTokenAnalyzer.Services
{
    public class DevInfoService(IRpcClient rpc, HttpClient httpClient)
    {
        public async Task<(string devAddress, string error)> FindDevAddress(string tokenAddress)
        {
            var origin = tokenAddress.Substring(tokenAddress.Length - 4);
            (string devAddress, string error) devAddress = (string.Empty, string.Empty);
            if (origin == "pump")
            {
                devAddress = await GetPumpFunTokenDev(tokenAddress);
            }
            else
            {
                devAddress = await GetMoonshotTokenDev(tokenAddress);    
            }
            if (!string.IsNullOrEmpty(devAddress.devAddress))
                return devAddress;
            return (string.Empty, "Can't find dev address: " + devAddress.error);
        }

        public async Task<(string ATA, double amount, string error)> GetDevHoldAmount(string devAddress, string tokenAddress)
        {
            var amount = await AccountsAnalyzer.GetAccountTokenAmount(rpc, devAddress, tokenAddress);
            return amount;
        }

        public async Task<(int linkedWalletsAmount, double sentToLinkedWallets, string error)> GetDevLinkedWallets(string devATA, string tokenAddress, string heliusApiKey)
        {
            var e = string.Empty;
            var retry = 0;
            while (retry++ < 3)
            {
                var signatures = await rpc.GetSignaturesForAddressAsync(devATA);
                var linkedWallets = new List<string>();
                var totalSentAmount = 0d;
                if (signatures.WasSuccessful)
                {
                    try
                    {
                        var signaturesChecked = 0;
                        while(signaturesChecked < signatures.Result.Count)
                        {
                            var body = new
                            {
                                transactions = signatures.Result.Select(x => x.Signature).Skip(signaturesChecked).Take(100).ToArray()
                            };
                            var jsonBody = JsonConvert.SerializeObject(body);
                            var response = await httpClient.PostAsync($"https://api.helius.xyz/v0/transactions?api-key={heliusApiKey}", new StringContent(jsonBody));
                            if (response.StatusCode != System.Net.HttpStatusCode.OK)
                            {
                                e = response.StatusCode.ToString();
                                continue;
                            }
                            var json = await response.Content.ReadAsStringAsync();
                            var transactions = JsonConvert.DeserializeObject<List<HeliusTransactionResponse>>(json);
                            totalSentAmount += GetLinkedWalletsFromTransactionsList(linkedWallets, transactions, tokenAddress, devATA);
                            signaturesChecked += 100;
                        }
                        return (linkedWallets.Distinct().Count(), totalSentAmount, string.Empty);
                    }
                    catch(Exception ex)
                    {
                        e = ex.Message;
                        continue;
                    }
                }
                else
                {
                    e = signatures.Reason;
                }
            }
            return (0, 0, e);
        }

        private double GetLinkedWalletsFromTransactionsList(List<string> linkedWallets, List<HeliusTransactionResponse> transactions, 
            string tokenAddress, string devATA)
        {
            if (transactions == null)
                return 0d;
            var amount = 0d; 
            foreach (var t in transactions)
            {  
                if (t.Type == "TRANSFER" && t.TokenTransfers.Count > 0 && t.TokenTransfers[0].FromTokenAccount == devATA)
                {
                    linkedWallets.Add(t.TokenTransfers[0].ToUserAccount);
                    amount += t.TokenTransfers[0].TokenAmount;
                }
            }         
            return amount;
        }

        public async Task<(List<SolDevPreviousCoin> devPreviousCoins, string error)> GetDevPreviousCoins(string devAddress, string tokenAddress)
        {
            var e = string.Empty;
            var retry = 0;
            while (retry++ < 3)
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://frontend-api.pump.fun/coins/user-created-coins/{devAddress}?limit=40&offset=0&includeNsfw=true");
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        e = response.StatusCode.ToString();
                        continue;
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    var coins = JsonConvert.DeserializeObject<List<PumpFunDevCoinsResponse>>(json);
                    if (coins != null)
                    {
                        var devPreviousCoins = new List<SolDevPreviousCoin>();
                        foreach (var coin in coins)
                        {
                            if (coin.Mint != tokenAddress)
                            {
                                devPreviousCoins.Add(new SolDevPreviousCoin
                                {
                                    Mint = coin.Mint,
                                    Name = coin.Name,
                                    Ticker = coin.Symbol,
                                    Image = coin.ImageUri,
                                    MarketCap = coin.MarketCap,
                                    NSFW = coin.Nsfw,
                                    Complete = coin.Complete
                                });
                            }
                        }
                        return (devPreviousCoins
                            .OrderByDescending(x => x.MarketCap)
                            .Take(10)
                            .ToList(), string.Empty);
                    }
                }
                catch (Exception ex)
                {
                    e = ex.Message;
                    continue;
                }
            }
            return (new List<SolDevPreviousCoin>(), e);
        }

        private async Task<(string devAddress, string error)> GetPumpFunTokenDev(string tokenAddress)
        {
            var e = string.Empty;
            var retry = 0;
            while (retry++ < 3)
            {
                try
                {
                    var response = await httpClient.GetAsync($"https://frontend-api.pump.fun/coins/{tokenAddress}");
                    if (response.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        e = response.StatusCode.ToString();
                        continue;
                    }
                    var json = await response.Content.ReadAsStringAsync();
                    var jObj = JObject.Parse(json);
                    var creator = jObj["creator"]?.Value<string>();
                    if (creator != null)
                        return (creator, string.Empty);
                    return (string.Empty, e);
                }
                catch (Exception ex)
                {
                    e = ex.Message;
                }
            }
            return (string.Empty, e);
        }

        private async Task<(string devAddress, string error)> GetMoonshotTokenDev(string tokenAddress)
        {
            var retry = 0;
            var e = string.Empty;
            while (retry++ < 3)
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
                    if (tokenInfo != null && tokenInfo.Pairs != null && tokenInfo.Pairs.Count > 0 && tokenInfo.Pairs[0].Moonshot != null)
                        return (tokenInfo.Pairs[0].Moonshot.Creator, string.Empty);
                    return (string.Empty, e);
                }
                catch(Exception ex)
                {
                    e = ex.Message;
                }         
            }
            return (string.Empty, e);
        }
    }
}
