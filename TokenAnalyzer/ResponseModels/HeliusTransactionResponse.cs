using Newtonsoft.Json;

namespace SolanaTokenAnalyzer.ResponseModels
{
    public class HeliusTransactionResponse
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("fee")]
        public long Fee { get; set; }

        [JsonProperty("feePayer")]
        public string FeePayer { get; set; }

        [JsonProperty("signature")]
        public string Signature { get; set; }

        [JsonProperty("slot")]
        public long Slot { get; set; }

        [JsonProperty("timestamp")]
        public long Timestamp { get; set; }

        [JsonProperty("tokenTransfers")]
        public List<TokenTransfer> TokenTransfers { get; set; }

        [JsonProperty("nativeTransfers")]
        public List<NativeTransfer> NativeTransfers { get; set; }

        [JsonProperty("accountData")]
        public List<AccountDatum> AccountData { get; set; }

        [JsonProperty("transactionError")]
        public TransactionError TransactionError { get; set; }

        [JsonProperty("instructions")]
        public List<Instruction> Instructions { get; set; }

        [JsonProperty("events")]
        public Events Events { get; set; }
    }

    public class AccountDatum
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("nativeBalanceChange")]
        public object NativeBalanceChange { get; set; }

        [JsonProperty("tokenBalanceChanges")]
        public List<TokenBalanceChange> TokenBalanceChanges { get; set; }
    }

    public class Events
    {
        [JsonProperty("swap")]
        public Swap Swap { get; set; }

        [JsonProperty("setAuthority")]
        public List<SetAuthority> SetAuthority { get; set; }
    }

    public class InnerInstruction
    {
        [JsonProperty("accounts")]
        public List<string> Accounts { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("programId")]
        public string ProgramId { get; set; }
    }

    public class InnerSwap
    {
        [JsonProperty("tokenInputs")]
        public List<TokenInput> TokenInputs { get; set; }

        [JsonProperty("tokenOutputs")]
        public List<TokenOutput> TokenOutputs { get; set; }

        [JsonProperty("tokenFees")]
        public List<object> TokenFees { get; set; }

        [JsonProperty("nativeFees")]
        public List<object> NativeFees { get; set; }

        [JsonProperty("programInfo")]
        public ProgramInfo ProgramInfo { get; set; }
    }

    public class Instruction
    {
        [JsonProperty("accounts")]
        public List<string> Accounts { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        [JsonProperty("programId")]
        public string ProgramId { get; set; }

        [JsonProperty("innerInstructions")]
        public List<InnerInstruction> InnerInstructions { get; set; }
    }

    public class NativeOutput
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class NativeTransfer
    {
        [JsonProperty("fromUserAccount")]
        public string FromUserAccount { get; set; }

        [JsonProperty("toUserAccount")]
        public string ToUserAccount { get; set; }

        [JsonProperty("amount")]
        public object Amount { get; set; }
    }

    public class ProgramInfo
    {
        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("programName")]
        public string ProgramName { get; set; }

        [JsonProperty("instructionName")]
        public string InstructionName { get; set; }
    }

    public class RawTokenAmount
    {
        [JsonProperty("tokenAmount")]
        public string TokenAmount { get; set; }

        [JsonProperty("decimals")]
        public int Decimals { get; set; }
    }

    public class SetAuthority
    {
        [JsonProperty("account")]
        public string Account { get; set; }

        [JsonProperty("from")]
        public string From { get; set; }

        [JsonProperty("to")]
        public string To { get; set; }

        [JsonProperty("instructionIndex")]
        public int InstructionIndex { get; set; }

        [JsonProperty("innerInstructionIndex")]
        public int InnerInstructionIndex { get; set; }
    }

    public class Swap
    {
        [JsonProperty("nativeInput")]
        public object NativeInput { get; set; }

        [JsonProperty("nativeOutput")]
        public NativeOutput NativeOutput { get; set; }

        [JsonProperty("tokenInputs")]
        public List<TokenInput> TokenInputs { get; set; }

        [JsonProperty("tokenOutputs")]
        public List<object> TokenOutputs { get; set; }

        [JsonProperty("nativeFees")]
        public List<object> NativeFees { get; set; }

        [JsonProperty("tokenFees")]
        public List<object> TokenFees { get; set; }

        [JsonProperty("innerSwaps")]
        public List<InnerSwap> InnerSwaps { get; set; }
    }

    public class TokenBalanceChange
    {
        [JsonProperty("userAccount")]
        public string UserAccount { get; set; }

        [JsonProperty("tokenAccount")]
        public string TokenAccount { get; set; }

        [JsonProperty("rawTokenAmount")]
        public RawTokenAmount RawTokenAmount { get; set; }

        [JsonProperty("mint")]
        public string Mint { get; set; }
    }

    public class TokenInput
    {
        [JsonProperty("userAccount")]
        public string UserAccount { get; set; }

        [JsonProperty("tokenAccount")]
        public string TokenAccount { get; set; }

        [JsonProperty("rawTokenAmount")]
        public RawTokenAmount RawTokenAmount { get; set; }

        [JsonProperty("mint")]
        public string Mint { get; set; }

        [JsonProperty("fromTokenAccount")]
        public string FromTokenAccount { get; set; }

        [JsonProperty("toTokenAccount")]
        public string ToTokenAccount { get; set; }

        [JsonProperty("fromUserAccount")]
        public string FromUserAccount { get; set; }

        [JsonProperty("toUserAccount")]
        public string ToUserAccount { get; set; }

        [JsonProperty("tokenAmount")]
        public double TokenAmount { get; set; }

        [JsonProperty("tokenStandard")]
        public string TokenStandard { get; set; }
    }

    public class TokenOutput
    {
        [JsonProperty("fromTokenAccount")]
        public string FromTokenAccount { get; set; }

        [JsonProperty("toTokenAccount")]
        public string ToTokenAccount { get; set; }

        [JsonProperty("fromUserAccount")]
        public string FromUserAccount { get; set; }

        [JsonProperty("toUserAccount")]
        public string ToUserAccount { get; set; }

        [JsonProperty("tokenAmount")]
        public double TokenAmount { get; set; }

        [JsonProperty("mint")]
        public string Mint { get; set; }

        [JsonProperty("tokenStandard")]
        public string TokenStandard { get; set; }
    }

    public class TokenTransfer
    {
        [JsonProperty("fromTokenAccount")]
        public string FromTokenAccount { get; set; }

        [JsonProperty("toTokenAccount")]
        public string ToTokenAccount { get; set; }

        [JsonProperty("fromUserAccount")]
        public string FromUserAccount { get; set; }

        [JsonProperty("toUserAccount")]
        public string ToUserAccount { get; set; }

        [JsonProperty("tokenAmount")]
        public double TokenAmount { get; set; }

        [JsonProperty("mint")]
        public string Mint { get; set; }

        [JsonProperty("tokenStandard")]
        public string TokenStandard { get; set; }
    }

    public class TransactionError
    {
        [JsonProperty("InstructionError")]
        public List<object> InstructionError { get; set; }
    }
}
