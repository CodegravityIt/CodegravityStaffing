using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class CountryMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public CountryMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }
        public List<cg_WorkRegion> getCountryDetails()
        {
            List<cg_WorkRegion> listCountry = new List<cg_WorkRegion>();
            try
            {
                listCountry = objGG_Dal.getCountryDetails();
            }
            catch (Exception ex)
            {
                throw;
            }
            return listCountry;
        }
    }
}
