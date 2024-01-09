using Microsoft.AspNetCore.Mvc;

namespace pockencrypt.Controllers;

[ApiController]
[Route("api")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IConfiguration _configuration;
    private readonly IHostEnvironment _environment;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IConfiguration configuration, IHostEnvironment environment)
    {
        _logger = logger;
        _configuration = configuration;
        _environment = environment;
    }

    [HttpGet("show")]
    public IActionResult Get()
    {
        var config = _configuration.GetSection("Configuration").Get<Configuration>();
        var dataBaseDescrypted = "";
        var passwordDecrypted = "";

        if (_environment.IsProduction())
        {
            dataBaseDescrypted = ConfigurationSecret.Decrypt(config.Database);
            passwordDecrypted = ConfigurationSecret.Decrypt(config.Password);
        }
        
        return Ok(new { config, dataBaseDescrypted, passwordDecrypted});
    }
}
