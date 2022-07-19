using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("download/{ETF_Name}")]
    public class DownloadController : ControllerBase
    {
        [HttpPost]
        public async Task<IEnumerable<Models.Download.DownloadItem>> Post(string ETF_Name)
        {
            return await Logic.ETF.Download.Execute(ETF_Name);
        }
    }
}