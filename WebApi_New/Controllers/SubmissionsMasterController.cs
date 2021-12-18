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
    public class SubmissionsMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        SubmissionsMaster ObjPM;//= new PlacementMaster();
        public SubmissionsMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjPM = new SubmissionsMaster(_configuration);

        }


        [HttpGet]
        public JsonResult Get()
        {
            //DataTable result = ObjCM.getMarketingDetails();
            return new JsonResult(ObjPM.getSubmissionsDetails());
        }
    }
}
