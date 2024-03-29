﻿using Microsoft.AspNetCore.Http;
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
    public class IncentiveMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        IncentiveMaster ObjIM;
        public IncentiveMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjIM = new IncentiveMaster(_configuration);
            _configuration = configuration;
        }
        [HttpGet]
        [ActionName("GetAll")]
        public JsonResult Get()
        {

            return new JsonResult(ObjIM.GetIncentiveDetails());

          
        }
        ////  [HttpGet("{ReportDate}")]
        //[HttpGet]
        //[ActionName("GetincentiveReportsByDate")]
        //public JsonResult GetincentiveReportsByDate(DateTime ReportDate)
        //{

        //    return new JsonResult(ObjIM.GetincentiveReportsByDate(ReportDate));


        //}


        [HttpGet("{ReportDate}")]
        [ActionName("GetincentiveReportsByDate")]
        public JsonResult GetincentiveReportsByDate(string ReportDate)
        {

            return new JsonResult(ObjIM.GetincentiveReportsByDate(ReportDate));


        }
        [HttpGet]
        [ActionName("")]
        public JsonResult GetincentiveDetails()
        {
            //return new JsonResult("called new method");


            return new JsonResult(ObjIM.GetIncentivetypeDetails());


        }

        [HttpPost]
        [ActionName("")]
        public JsonResult post(cg_Incentivedetils em)
        {
            //          

            bool result = ObjIM.AddNewIncentivedetails(em);

            if (result)
                return new JsonResult("Employee details Added Succfully");
            else
                return new JsonResult("Failed to add Employee details..");
        }

    }
}
