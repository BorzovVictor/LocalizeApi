## The .net core API project shows how to work with localization

### To configure
In `appsettings.json` we should to set db connection string with name 'LocalizationsDb' or rename it in the ServiceCollectionExtensions class
In `Startup` class you need to add two calls:
 * `services.AddLocalizationService(Configuration); `in the `ConfigureServices` method
 * `app.AddLocalizationApp();` in the `Configure` method

### How to use

Inject `IStringLocalizer` where you need and use with text

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

### How it works
Using indexers, we return from the database by the key the `Resource` object, from which we form an instance of the `LocalizedString` class.
`CreateStringLocalizer` method determines the data context, initializes the database and returns the `EFStringLocalizer` object.
And this object then will be used for localization.

### How to test
In the Postman call endpoint:
 * https://localhost:5001/weatherforecast
 * add to headers: [{"key":"Accept-Language","value":"en"}]
 ![alt text](https://github.com/BorzovVictor/LocalizeApi/blob/master/LocalizeApi/docs/Annotation.png)
 
### What to add
 * Work with cache (MemoryCache). We must get the data from the database and store it in memory. In the future, we should work with localized variables only from memory. We should rewrite follow methods to work with memory 
    * EFStringLocalizer.GetAllStrings
    * EFStringLocalizer.GetString
    
 The basic idea is to get the data from the database and store it in memory for further use 
