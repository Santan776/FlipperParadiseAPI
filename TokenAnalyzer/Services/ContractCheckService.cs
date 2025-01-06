using Models.Models.Solana;
using Solana.Unity.Rpc;

namespace SolanaTokenAnalyzer.Services
{
    public class ContractCheckService(IRpcClient rpc)
    {
        public async Task<(SolMetadata metadata, string error)> UpdateContractData(string tokenAddress, SolMetadata metadata)
        {
            var retry = 0;
            var e = string.Empty;
            while (retry++ < 3)
            {
                var mint = await rpc.GetTokenMintInfoAsync(tokenAddress);
                if (mint.WasSuccessful && mint.Result.Value != null)
                {
                    metadata.MintAuthorityRevoked = mint.Result.Value.Data.Parsed.Info.MintAuthority == null;
                    metadata.FreezeAuthorityRevoked = mint.Result.Value.Data.Parsed.Info.FreezeAuthority == null;
                    return (metadata, string.Empty);
                }
                else
                {
                    e = mint.Reason;
                    continue;
                }
            }
            return (metadata, e);
        }
    }
}