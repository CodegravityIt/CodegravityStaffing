using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        public CountryMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjPM = new CountryMaster(_configuration);

        }


        [HttpGet]
        public JsonResult Get()
        {

            return new JsonResult(ObjPM.getCountryDetails());
        }
    }
}
