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
    public class PlacementMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        PlacementMaster ObjPM;//= new PlacementMaster();
        public PlacementMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjPM = new PlacementMaster(_configuration);

        }


        [HttpGet]
        public JsonResult Get()
        {
            //DataTable result = ObjCM.getMarketingDetails();
            return new JsonResult(ObjPM.getPlacementDetails());
        }
        [HttpPost]
        public JsonResult post(cg_Placement objPlacement)
        {
            bool result = ObjPM.AddNewPlacement(objPlacement);

            if (result)
                return new JsonResult("New placement Added Succfully");
            else
                return new JsonResult("Failed to add placement details ..");
        }
    }
}
