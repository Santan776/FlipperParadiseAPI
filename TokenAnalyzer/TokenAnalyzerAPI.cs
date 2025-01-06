using Models.Models.Solana;
using Solana.Unity.Rpc;
using SolanaTokenAnalyzer.Services;

namespace SolanaTokenAnalyzer
{
    public class TokenAnalyzerAPI
    {
        public async Task<(SolMetadata metadata, string error)> GetTokenMetadata(string tokenAddress, 
            HttpClient httpClient, IRpcClient rpc, string heliusApiKey)
        {
            string errors = string.Empty;
            var metadata = new SolMetadata();

            var metadataCheck = await new MetadataCheckService(httpClient, heliusApiKey).UpdateMetadata(tokenAddress, metadata);
            errors += metadataCheck.error;

            var contractCheck = await new ContractCheckService(rpc).UpdateContractData(tokenAddress, metadata);
            errors += contractCheck.error;

            var dexCheck = await new DexCheckService(httpClient).UpdateDexScreenerData(tokenAddress, metadata);
            errors += dexCheck.error;

            return (metadata, errors);
        }

        public async Task<(List<SolLiquidityPool> liquidityPools, string error)> GetTokenLiquidityPools(string tokenAddress,
            HttpClient httpClient, IRpcClient rpc)
        {
            var pools = await new LiquidityPoolsCheckerService(httpClient, rpc).GetLiquidityPools(tokenAddress);
            return pools;
        }

        public async Task<(SolDevInfo devInfo, string error)> GetTokenDevInfo(string tokenAddress, HttpClient httpClient, IRpcClient rpc, string heliusApiKey)
        {
            var devInfo = new SolDevInfo();
            var e = string.Empty;
            var devService = new DevInfoService(rpc, httpClient);
            var devAddress = await devService.FindDevAddress(tokenAddress);
            devInfo.DevAddress = devAddress.devAddress;
            e += devAddress.error;
            if (!string.IsNullOrEmpty(devAddress.devAddress))
            {
                var amount = await devService.GetDevHoldAmount(devAddress.devAddress, tokenAddress);
                devInfo.TokenAmount = amount.amount;
                e += amount.error;
                var linkedWallets = await devService.GetDevLinkedWallets(amount.ATA, tokenAddress, heliusApiKey);
                devInfo.LinkedWalletsAmount = linkedWallets.linkedWalletsAmount;
                devInfo.TokensSentToLinkedWallets = linkedWallets.sentToLinkedWallets;
                e += linkedWallets.error;
            }
            return (devInfo, e);
        }
    }
}