using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("downloaded/{ETF_Name}/dates")]
    public class DownloadedDatesController : ControllerBase
    {
        [HttpGet]
        public async Task<IEnumerable<string>> Post(string ETF_Name)
        {
            return await Logic.ETF.Downloaded.Dates.Get(ETF_Name);
        }
    }
}