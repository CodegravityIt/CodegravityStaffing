using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class EntitlementMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public EntitlementMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }
        public List<cg_Entitlement> GetEntitlementDetails()
        {
            List<cg_Entitlement> listEntitlement = new List<cg_Entitlement>();

            try
            {
                listEntitlement = objGG_Dal.getEntitlementDetails();
            }
            catch (Exception ex)
            {
            }
            return listEntitlement;

        }
    }
}
