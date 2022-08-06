using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_New.Models;

namespace WebApi_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountryMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        CountryMaster ObjPM;//= new PlacementMaster();
        private readonly ILogger<CountryMasterController> _logger;
        public CountryMasterController(IConfiguration configuration, ILogger<CountryMasterController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            ObjPM = new CountryMaster(_configuration);

        }


        [HttpGet]
        public JsonResult Get()
        {
            try
            {
                return new JsonResult(ObjPM.getCountryDetails());
            }
            catch (Exception ex)
            {
                _logger.LogError("Country Details: " + ex.Message, ex);
                return null;
            }
        }
    }
}
