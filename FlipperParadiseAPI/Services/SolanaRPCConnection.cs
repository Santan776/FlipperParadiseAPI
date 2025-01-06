using Solana.Unity.Rpc;

namespace FlipperParadiseAPI.Services
{
    public class SolanaRPCConnection
    {
        public SolanaRPCConnection()
        {
            Connection = ClientFactory.GetClient("https://mainnet.helius-rpc.com/?api-key=b2c98adf-dc51-42bd-a0f7-de6d482f9e11");//("https://prettiest-cosmopolitan-firefly.solana-mainnet.quiknode.pro/17a9eec4debebaf43c6c6e73cfc7ca84df1ab3dc");
        }

        public IRpcClient Connection { get; set; }
    }
}
