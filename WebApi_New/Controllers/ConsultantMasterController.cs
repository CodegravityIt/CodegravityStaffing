using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApi_New.Models;

namespace WebApi_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConsultantMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        ConsultantMaster ObjCM;
        public ConsultantMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjCM = new ConsultantMaster(_configuration);
           
        }
        [HttpGet]
        public JsonResult Get()
        {
           // DataTable result = ObjCM.GetConsultDetails();
            return new JsonResult(ObjCM.GetConsultDetails());
        }
        [HttpPost]
        public JsonResult post(cg_Consultant cm)
        {

            DataTable result = ObjCM.AddConsultDetails(cm);
            return new JsonResult("Added Succfully");
        }
        [HttpPut]
        public JsonResult put(cg_Consultant cm)
        {
            DataTable result = ObjCM.AddConsultDetails(cm);
            return new JsonResult("updated Succfully");
        }

        [HttpDelete("{ConsultId}")]
        public JsonResult delete(int ConsultId)
        {

            DataTable result = ObjCM.DeleteConsultDetails(ConsultId);
            return new JsonResult("Deleted Succfully");
        }
    }
}
