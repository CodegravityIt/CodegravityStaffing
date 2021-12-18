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
    public class TechnologiesMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        TechnologiesMaster ObjTM;
        public TechnologiesMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjTM = new TechnologiesMaster(_configuration);

        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(ObjTM.getTechnologiesDetails());
        }
    }
}
