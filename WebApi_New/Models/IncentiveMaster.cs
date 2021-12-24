using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class IncentiveMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public IncentiveMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }

        public List<cg_Incentives> GetIncentivetypeDetails()
        {
            List<cg_Incentives> listEmployee = new List<cg_Incentives>();

            try
            {
                listEmployee = objGG_Dal.getIncentivetypeDetails();
            }
            catch (Exception ex)
            {
            }
            return listEmployee;

        }

        public List<cg_Incentivedetils> GetIncentiveDetails() {

            List<cg_Incentivedetils> listIncentiveDetails = new List<cg_Incentivedetils>();

            try
            {
                listIncentiveDetails = objGG_Dal.getIncentivedetails();
            }
            catch (Exception ex)
            {
            }
            return listIncentiveDetails;
            }
    }
}
