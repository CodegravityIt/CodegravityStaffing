using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class PlacementMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public PlacementMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }
        public List<cg_Placement> getPlacementDetails()
        {
            List<cg_Placement> dtConsult = new List<cg_Placement>();
            try
            {
                dtConsult = objGG_Dal.getPlacementDetails();
            }
            catch (Exception ex)
            {
            }
            return dtConsult;
        }

        public bool AddNewPlacement(cg_Placement Objmarketing)
        {
            bool Isrecordadded = false;

            try
            {
                Isrecordadded = objGG_Dal.AddNewplacementAssignment(Objmarketing);
            }
            catch (Exception ex)
            {
                Isrecordadded = false;
            }
            return Isrecordadded;

        }

    }
}
