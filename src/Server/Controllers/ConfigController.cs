using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebApp.Shared;

namespace WebApp.Server.Controllers
{
	[ApiController]
    [Route("[controller]")]
    public class ConfigController : ControllerBase
    {
		private readonly AzureAdB2C _b2c;

		public ConfigController(IOptions<AzureAdB2C> b2c)
        {
            _b2c = b2c.Value;
        }

        [HttpGet]
        public ActionResult<AzureAdB2C> Get()
        {
			return _b2c;
        }
    }
}
