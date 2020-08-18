using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace LocalizeApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly IStringLocalizer _localize;

        public WeatherForecastController(IStringLocalizer localize)
        {
            _localize = localize;
        }
        
        [HttpGet]
        public string Test()
        {
            return _localize["Message"];
        }
    }
}