using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NLog;
using System;
using System.Linq;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        ILogger logger = LogManager.GetCurrentClassLogger();

        [Authorize(Policy = "Auth")]
        [Produces("application/json")]
        [HttpPost("GET_WeatherForecast")]
        public respObj GET_WeatherForecast()
        {
            try
            {

                using (DataModel.db dbContext = new DataModel.db(Connections.get()))
                {

                    var result = (from i in dbContext.activity select i).ToList();

                    return (new respObj { status = "OK", status_code = 200, obj = result });
                }

            }
            catch (Exception ex)
            {
                return (new respObj { status = "ERROR", status_code = 400, message = ex.Message });
            }
        }

    }
}
