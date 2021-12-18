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
    public class EntitlementMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        EntitlementMaster ObjIM;
        public EntitlementMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjIM = new EntitlementMaster(_configuration);
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {

            return new JsonResult(ObjIM.GetEntitlementDetails());

        }
    }
}
