using Newtonsoft.Json;

namespace SolanaTokenAnalyzer.ResponseModels
{
    public class PumpFunDevCoinsResponse
    {
        [JsonProperty("mint")]
        public string Mint { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("image_uri")]
        public string ImageUri { get; set; }

        [JsonProperty("metadata_uri")]
        public string MetadataUri { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("telegram")]
        public string Telegram { get; set; }

        [JsonProperty("bonding_curve")]
        public string BondingCurve { get; set; }

        [JsonProperty("associated_bonding_curve")]
        public string AssociatedBondingCurve { get; set; }

        [JsonProperty("creator")]
        public string Creator { get; set; }

        [JsonProperty("created_timestamp")]
        public long CreatedTimestamp { get; set; }

        [JsonProperty("raydium_pool")]
        public string RaydiumPool { get; set; }

        [JsonProperty("complete")]
        public bool Complete { get; set; }

        [JsonProperty("virtual_sol_reserves")]
        public double VirtualSolReserves { get; set; }

        [JsonProperty("virtual_token_reserves")]
        public double VirtualTokenReserves { get; set; }

        [JsonProperty("total_supply")]
        public double TotalSupply { get; set; }

        [JsonProperty("website")]
        public string Website { get; set; }

        [JsonProperty("show_name")]
        public bool ShowName { get; set; }

        [JsonProperty("king_of_the_hill_timestamp")]
        public long? KingOfTheHillTimestamp { get; set; }

        [JsonProperty("market_cap")]
        public double MarketCap { get; set; }

        [JsonProperty("reply_count")]
        public int ReplyCount { get; set; }

        [JsonProperty("last_reply")]
        public long LastReply { get; set; }

        [JsonProperty("nsfw")]
        public bool Nsfw { get; set; }

        [JsonProperty("market_id")]
        public string MarketId { get; set; }

        [JsonProperty("inverted")]
        public bool? Inverted { get; set; }

        [JsonProperty("is_currently_live")]
        public bool IsCurrentlyLive { get; set; }

        [JsonProperty("usd_market_cap")]
        public double UsdMarketCap { get; set; }
    }
}