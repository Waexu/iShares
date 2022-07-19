using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("stored/{ETF_Name}/dates")]
    public class StoredDatesController : ControllerBase
    {
        [HttpGet]
        public List<string> Get(string ETF_Name)
        {
            return Logic.ETF.Store.Dates.Get(ETF_Name);
        }
    }
}