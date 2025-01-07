using Solana.Unity.Rpc;

namespace FlipperParadiseAPI.Services
{
    public class SolanaRPCConnection
    {
        public SolanaRPCConnection(IConfiguration config)
        {
            Connection1 = ClientFactory.GetClient(config.GetSection("SolanaRPCs").GetValue<string>("QuickNode"));
            Connection2 = ClientFactory.GetClient(config.GetSection("SolanaRPCs").GetValue<string>("Helius"));
        }

        public IRpcClient Connection1 { get; set; }
        public IRpcClient Connection2 { get; set; }
    }
}
