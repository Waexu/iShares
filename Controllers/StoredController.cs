using Microsoft.AspNetCore.Mvc;

namespace ETF_API.Controllers
{
    [ApiController]
    [Route("stored")]
    public class StoredController : ControllerBase
    {
        [HttpGet]
        public List<string> Get()
        {
            return Logic.ETF.Store.ETFs.Get();
        }
    }
}