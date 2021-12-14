using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebApi_New.Models;

namespace WebApi_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarketingMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        MarketingMaster ObjCM;
        public MarketingMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjCM = new MarketingMaster(_configuration);

        }

        [HttpGet]
        public JsonResult Get()
        {
            //DataTable result = ObjCM.getMarketingDetails();
            return new JsonResult(ObjCM.getMarketingDetails());
        }
    }
}
