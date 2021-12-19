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

            return new JsonResult(ObjCM.getMarketingDetails());
        }

        [HttpPost]
        public JsonResult post(cg_Marketing objMarket)
        {
            bool result = ObjCM.AddNewMarketingAssignment(objMarket);

            if (result)
                return new JsonResult("New market assignment Added Succfully");
            else
                return new JsonResult("Failed to add New market assignment ..");
        }

    }
}
