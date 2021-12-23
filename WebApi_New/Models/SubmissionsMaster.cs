using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class SubmissionsMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public SubmissionsMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }
        public List<cg_Submissions> getSubmissionslist()
        {
            List<cg_Submissions> dtSubmissions = new List<cg_Submissions>();
            try
            {
               dtSubmissions = objGG_Dal.getSubmissionlist();
            }
            catch (Exception ex)
            {
            }
            return dtSubmissions;
        }

        public List<cg_Submissions> getSubmissionsdetails()
        {
            List<cg_Submissions> dtSubmissions = new List<cg_Submissions>();
            try
            {
                dtSubmissions = objGG_Dal.getSubmissionDetails();
            }
            catch (Exception ex)
            {
            }
            return dtSubmissions;
        }
        public bool AddNewSubmission(cg_Submissions Objsubmission)
        {
            bool Isrecordadded = false;

            try
            {
                Isrecordadded = objGG_Dal.AddNewsubmission(Objsubmission);
            }
            catch (Exception ex)
            {
                Isrecordadded = false;
            }
            return Isrecordadded;

        }
    }
}
