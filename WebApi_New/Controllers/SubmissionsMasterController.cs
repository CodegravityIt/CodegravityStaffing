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
    [Route("api/[controller]/[action]")]
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
        [ActionName("")]
        public JsonResult Get()
        {
        
            return new JsonResult(ObjPM.getSubmissionslist());
        }

        [HttpGet]
        [ActionName("GetAll")]
        public JsonResult GetSubmissionDetails()
        {
         

            return new JsonResult(ObjPM.getSubmissionsdetails());


        }
        [HttpPost]
        [ActionName("add")]
        public JsonResult post(cg_Submissions objSubmission)
        {
            bool result = ObjPM.AddNewSubmission(objSubmission);

            if (result)
                return new JsonResult("New submission Added Succfully");
            else
                return new JsonResult("Failed to add New submission ..");
        }
    }
}
