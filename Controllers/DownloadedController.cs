
using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("downloaded")]
    public class DownloadedController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return await Logic.ETF.Downloaded.List.Get();
        }

    }
}
