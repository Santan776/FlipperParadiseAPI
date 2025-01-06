using Newtonsoft.Json;

namespace SolanaTokenAnalyzer.ResponseModels
{
    public class HeliusMetadataResponse
    {
        [JsonProperty("jsonrpc")]
        public string Jsonrpc { get; set; }

        [JsonProperty("result")]
        public Result Result { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class Authority
    {
        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("scopes")]
        public List<string> Scopes { get; set; }
    }

    public class Compression
    {
        [JsonProperty("eligible")]
        public bool Eligible { get; set; }

        [JsonProperty("compressed")]
        public bool Compressed { get; set; }

        [JsonProperty("data_hash")]
        public string DataHash { get; set; }

        [JsonProperty("creator_hash")]
        public string CreatorHash { get; set; }

        [JsonProperty("asset_hash")]
        public string AssetHash { get; set; }

        [JsonProperty("tree")]
        public string Tree { get; set; }

        [JsonProperty("seq")]
        public int Seq { get; set; }

        [JsonProperty("leaf_id")]
        public int LeafId { get; set; }
    }

    public class Content
    {
        [JsonProperty("$schema")]
        public string Schema { get; set; }

        [JsonProperty("json_uri")]
        public string JsonUri { get; set; }

        [JsonProperty("files")]
        public List<File> Files { get; set; }

        [JsonProperty("metadata")]
        public Metadata Metadata { get; set; }

        [JsonProperty("links")]
        public Links Links { get; set; }
    }

    public class File
    {
        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("cdn_uri")]
        public string CdnUri { get; set; }

        [JsonProperty("mime")]
        public string Mime { get; set; }
    }

    public class Links
    {
        [JsonProperty("image")]
        public string Image { get; set; }
    }

    public class Metadata
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("token_standard")]
        public string TokenStandard { get; set; }
    }

    public class Ownership
    {
        [JsonProperty("frozen")]
        public bool Frozen { get; set; }

        [JsonProperty("delegated")]
        public bool Delegated { get; set; }

        [JsonProperty("delegate")]
        public object Delegate { get; set; }

        [JsonProperty("ownership_model")]
        public string OwnershipModel { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }
    }

    public class PriceInfo
    {
        [JsonProperty("price_per_token")]
        public double PricePerToken { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }
    }

    public class Result
    {
        [JsonProperty("interface")]
        public string Interface { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("content")]
        public Content Content { get; set; }

        [JsonProperty("authorities")]
        public List<Authority> Authorities { get; set; }

        [JsonProperty("compression")]
        public Compression Compression { get; set; }

        [JsonProperty("grouping")]
        public List<object> Grouping { get; set; }

        [JsonProperty("royalty")]
        public Royalty Royalty { get; set; }

        [JsonProperty("creators")]
        public List<object> Creators { get; set; }

        [JsonProperty("ownership")]
        public Ownership Ownership { get; set; }

        [JsonProperty("supply")]
        public object Supply { get; set; }

        [JsonProperty("mutable")]
        public bool Mutable { get; set; }

        [JsonProperty("burnt")]
        public bool Burnt { get; set; }

        [JsonProperty("token_info")]
        public TokenInfo TokenInfo { get; set; }
    }

    public class Royalty
    {
        [JsonProperty("royalty_model")]
        public string RoyaltyModel { get; set; }

        [JsonProperty("target")]
        public object Target { get; set; }

        [JsonProperty("percent")]
        public double Percent { get; set; }

        [JsonProperty("basis_points")]
        public double BasisPoints { get; set; }

        [JsonProperty("primary_sale_happened")]
        public bool PrimarySaleHappened { get; set; }

        [JsonProperty("locked")]
        public bool Locked { get; set; }
    }

    public class TokenInfo
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("supply")]
        public long Supply { get; set; }

        [JsonProperty("decimals")]
        public int Decimals { get; set; }

        [JsonProperty("token_program")]
        public string TokenProgram { get; set; }

        [JsonProperty("price_info")]
        public PriceInfo PriceInfo { get; set; }
    }


}
