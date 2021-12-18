using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class TechnologiesMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public TechnologiesMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }


        public List<cg_Technology> getTechnologiesDetails()
        {
            List<cg_Technology> dtTechnologies = new List<cg_Technology>();
            try
            {
                dtTechnologies = objGG_Dal.getTechnologyDetails();
            }
            catch (Exception ex)
            {
            }
            return dtTechnologies;
        }
    }
}
