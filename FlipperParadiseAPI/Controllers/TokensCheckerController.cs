using FlipperParadiseAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlipperParadiseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokensCheckerController(TokenAnalyzerService tokenAnalyzer) : ControllerBase
    {
        [HttpGet("/token/metadata/{tokenAddress}")]
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

        [HttpGet("/token/liquidity/{tokenAddress}")]
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

        [HttpGet("/token/devinfo/{tokenAddress}")]
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
    }
}
