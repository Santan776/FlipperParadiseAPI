using Newtonsoft.Json;

namespace SolanaTokenAnalyzer.ResponseModels
{
    public class LiquidityPoolsResponse
    {
        public class Dexes
        {
            [JsonProperty("fluxbeam")]
            public Fluxbeam Fluxbeam { get; set; }

            [JsonProperty("meteoraAmm")]
            public MeteoraAmm MeteoraAmm { get; set; }

            [JsonProperty("orca")]
            public Orca Orca { get; set; }

            [JsonProperty("raydiumAmm")]
            public RaydiumAmm RaydiumAmm { get; set; }

            [JsonProperty("raydiumClmm")]
            public RaydiumClmm RaydiumClmm { get; set; }

            [JsonProperty("raydiumCpmm")]
            public RaydiumCpmm RaydiumCpmm { get; set; }
        }

        public class Fluxbeam
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class MeteoraAmm
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class Orca
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class Pool
        {

            [JsonProperty("tokenAccountA")]
            public string TokenAccountA { get; set; }

            [JsonProperty("TokenAccountB")]
            public string TokenAccountB { get; set; }

            [JsonProperty("mintA")]
            public string MintA { get; set; }

            [JsonProperty("mintB")]
            public string MintB { get; set; }

            [JsonProperty("pubkey")]
            public string Pubkey { get; set; }

            [JsonProperty("lpMint")]
            public string LpMint { get; set; }

            [JsonProperty("tokenAMint")]
            public string TokenAMint { get; set; }

            [JsonProperty("tokenBMint")]
            public string TokenBMint { get; set; }

            [JsonProperty("aVault")]
            public string AVault { get; set; }

            [JsonProperty("bVault")]
            public string BVault { get; set; }

            [JsonProperty("aVaultLp")]
            public string AVaultLp { get; set; }

            [JsonProperty("bVaultLp")]
            public string BVaultLp { get; set; }

            [JsonProperty("totalLockedLp")]
            public long TotalLockedLp { get; set; }

            [JsonProperty("baseDecimals")]
            public int BaseDecimals { get; set; }

            [JsonProperty("quoteDecimals")]
            public int QuoteDecimals { get; set; }

            [JsonProperty("baseMint")]
            public string BaseMint { get; set; }

            [JsonProperty("quoteMint")]
            public string QuoteMint { get; set; }

            [JsonProperty("reserved")]
            public long Reserved { get; set; }

            [JsonProperty("liquidity")]
            public long Liquidity { get; set; }

            [JsonProperty("tokenMintA")]
            public string TokenMintA { get; set; }

            [JsonProperty("tokenVaultA")]
            public string TokenVaultA { get; set; }

            [JsonProperty("tokenMintB")]
            public string TokenMintB { get; set; }

            [JsonProperty("tokenVaultB")]
            public string TokenVaultB { get; set; }

            public int BaseDecimal { get; set; }

            [JsonProperty("quoteDecimal")]
            public int QuoteDecimal { get; set; }

            [JsonProperty("baseVault")]
            public string BaseVault { get; set; }

            [JsonProperty("quoteVault")]
            public string QuoteVault { get; set; }

            [JsonProperty("lpVault")]
            public string LpVault { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("lpReserve")]
            public long LpReserve { get; set; }

            [JsonProperty("tokenMint0")]
            public string TokenMint0 { get; set; }

            [JsonProperty("tokenMint1")]
            public string TokenMint1 { get; set; }

            [JsonProperty("tokenVault0")]
            public string TokenVault0 { get; set; }

            [JsonProperty("tokenVault1")]
            public string TokenVault1 { get; set; }

            [JsonProperty("observationKey")]
            public string ObservationKey { get; set; }

            [JsonProperty("mintDecimals0")]
            public int MintDecimals0 { get; set; }

            [JsonProperty("mintDecimals1")]
            public int MintDecimals1 { get; set; }

            [JsonProperty("poolCreator")]
            public string PoolCreator { get; set; }

            [JsonProperty("token0Vault")]
            public string Token0Vault { get; set; }

            [JsonProperty("token1Vault")]
            public string Token1Vault { get; set; }

            [JsonProperty("token0Mint")]
            public string Token0Mint { get; set; }

            [JsonProperty("token1Mint")]
            public string Token1Mint { get; set; }

            [JsonProperty("token0Program")]
            public string Token0Program { get; set; }

            [JsonProperty("token1Program")]
            public string Token1Program { get; set; }

            [JsonProperty("lpMintDecimals")]
            public int LpMintDecimals { get; set; }

            [JsonProperty("mint0Decimals")]
            public int Mint0Decimals { get; set; }

            [JsonProperty("mint1Decimals")]
            public int Mint1Decimals { get; set; }

            [JsonProperty("lpSupply")]
            public long LpSupply { get; set; }
        }

        public class RaydiumAmm
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class RaydiumClmm
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class RaydiumCpmm
        {
            [JsonProperty("pools")]
            public List<Pool> Pools { get; set; }

            [JsonProperty("programId")]
            public string ProgramId { get; set; }
        }

        public class Result
        {
            [JsonProperty("page")]
            public int Page { get; set; }

            [JsonProperty("limit")]
            public int Limit { get; set; }

            [JsonProperty("dexes")]
            public Dexes Dexes { get; set; }
        }

        public class Root
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("result")]
            public Result Result { get; set; }
        }
    }
}