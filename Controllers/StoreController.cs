using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("stored/{ETF_Name}")]
    public class StoreController : ControllerBase
    {
        [HttpPost]
        public async Task<int> Post(string ETF_Name)
        {
            return await Logic.ETF.Store.Execute(ETF_Name);
        }
    }
}