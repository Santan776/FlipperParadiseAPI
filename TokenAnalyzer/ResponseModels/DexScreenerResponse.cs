using Newtonsoft.Json;

namespace SolanaTokenAnalyzer.ResponseModels
{
    public class DexScreenerResponse
    {
        [JsonProperty("schemaVersion")]
        public string SchemaVersion { get; set; }

        [JsonProperty("pairs")]
        public List<Pair> Pairs { get; set; }
    }

    public partial class Pair
    {
        [JsonProperty("chainId")]
        public string ChainId { get; set; }

        [JsonProperty("dexId")]
        public string DexId { get; set; }

        [JsonProperty("url")]
        public Uri Url { get; set; }

        [JsonProperty("pairAddress")]
        public string PairAddress { get; set; }

        [JsonProperty("baseToken")]
        public EToken BaseToken { get; set; }

        [JsonProperty("quoteToken")]
        public EToken QuoteToken { get; set; }

        [JsonProperty("priceNative")]
        public string PriceNative { get; set; }

        [JsonProperty("priceUsd")]
        public string PriceUsd { get; set; }

        [JsonProperty("txns")]
        public Txns Txns { get; set; }

        [JsonProperty("volume")]
        public PriceChange Volume { get; set; }

        [JsonProperty("priceChange")]
        public PriceChange PriceChange { get; set; }

        [JsonProperty("liquidity")]
        public Liquidity Liquidity { get; set; }

        [JsonProperty("fdv")]
        public long Fdv { get; set; }

        [JsonProperty("marketCap")]
        public long MarketCap { get; set; }

        [JsonProperty("pairCreatedAt")]
        public long PairCreatedAt { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }

        [JsonProperty("moonshot")]
        public Moonshot Moonshot { get; set; }

        [JsonProperty("boosts")]
        public Boosts Boosts { get; set; }
    }

    public partial class EToken
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }
    }

    public partial class Boosts
    {
        [JsonProperty("active")]
        public long Active { get; set; }
    }

    public partial class Info
    {
        [JsonProperty("imageUrl")]
        public Uri ImageUrl { get; set; }

        [JsonProperty("header")]
        public Uri Header { get; set; }

        [JsonProperty("openGraph")]
        public Uri OpenGraph { get; set; }

        [JsonProperty("websites")]
        public List<Website> Websites { get; set; }

        [JsonProperty("socials")]
        public List<Social> Socials { get; set; }
    }

    public partial class Moonshot
    {
        [JsonProperty("creator")]
        public string Creator { get; set; }
    }

    public partial class Social
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;
    }

    public partial class Website
    {
        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;
    }

    public partial class Liquidity
    {
        [JsonProperty("usd")]
        public double Usd { get; set; }

        [JsonProperty("base")]
        public long Base { get; set; }

        [JsonProperty("quote")]
        public double Quote { get; set; }
    }

    public partial class PriceChange
    {
        [JsonProperty("m5")]
        public double M5 { get; set; }

        [JsonProperty("h1")]
        public double H1 { get; set; }

        [JsonProperty("h6")]
        public double H6 { get; set; }

        [JsonProperty("h24")]
        public double H24 { get; set; }
    }

    public partial class Txns
    {
        [JsonProperty("m5")]
        public H1 M5 { get; set; }

        [JsonProperty("h1")]
        public H1 H1 { get; set; }

        [JsonProperty("h6")]
        public H1 H6 { get; set; }

        [JsonProperty("h24")]
        public H1 H24 { get; set; }
    }

    public partial class H1
    {
        [JsonProperty("buys")]
        public long Buys { get; set; }

        [JsonProperty("sells")]
        public long Sells { get; set; }
    }
}