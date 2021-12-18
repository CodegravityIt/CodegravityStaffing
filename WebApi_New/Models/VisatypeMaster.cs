using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class VisatypeMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public VisatypeMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }

        public List<cg_VisaType> getVisatypeDetails()
        {
            List<cg_VisaType> dtConsult = new List<cg_VisaType>();
            try
            {
                dtConsult = objGG_Dal.getVisatypeDetails();
            }
            catch (Exception ex)
            {
            }
            return dtConsult;
        }
    }
}
