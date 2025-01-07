using FlipperParadiseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlipperParadiseAPI.Controllers
{
    [ApiController]
    public class SolanaTokensCheckerController(SolanaTokensAnalyzerService tokenAnalyzer) : ControllerBase
    {
        [HttpGet("solana/token/metadata/{tokenAddress}")]
        public async Task<IActionResult> GetTokenMetadata(string tokenAddress)
        {
            var metadata = await tokenAnalyzer.GetTokenMetadata(tokenAddress);
            return Ok(new 
            { 
                success = string.IsNullOrEmpty(metadata.error),
                metadata.error,
                metadata.metadata
            });
        }

        [HttpGet("solana/token/liquidity/{tokenAddress}")]
        public async Task<IActionResult> GetTokenLiquidityPools(string tokenAddress)
        {
            var liquidity = await tokenAnalyzer.GetTokenLiquidityPools(tokenAddress);
            return Ok(new
            {
                success = string.IsNullOrEmpty(liquidity.error),
                liquidity.error,
                liquidity.liquidityPools
            });
        }

        [HttpGet("solana/token/devinfo/{tokenAddress}")]
        public async Task<IActionResult> GetTokenDevInfo(string tokenAddress)
        {
            var devInfo = await tokenAnalyzer.GetTokenDevInfo(tokenAddress);
            return Ok(new
            {
                success = string.IsNullOrEmpty(devInfo.error),
                devInfo.error,
                devInfo.devInfo
            });
        }

        [HttpGet("solana/token/topholders/{tokenAddress}")]
        public async Task<IActionResult> GetTokenTopHolders(string tokenAddress)
        {
            var topHolders = await tokenAnalyzer.GetTokenTopHolders(tokenAddress);
            return Ok(new
            {
                success = string.IsNullOrEmpty(topHolders.error),
                topHolders.error,
                topHolders.holdersInfo
            });
        }
    }
}
