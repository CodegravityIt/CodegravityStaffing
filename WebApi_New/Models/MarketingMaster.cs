using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace WebApi_New.Models
{

    public class MarketingMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public MarketingMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }


        public List<cg_Marketing> getMarketingDetails()
        {
            List<cg_Marketing> dtConsult = new List<cg_Marketing>();
            try
            {
                dtConsult = objGG_Dal.getMarketingDetails();
            }
            catch (Exception ex)
            {
            }
            return dtConsult;
        }

        public List<cg_Technology> getTechnologyNames()
        {
            try
            {
                List<cg_Technology> dtConsult = new List<cg_Technology>();
                try
                {
                    dtConsult = objGG_Dal.getTechnologyDetails();
                }
                catch (Exception ex)
                {
                }
                return dtConsult;
            }
            catch (Exception)
            {

                //throw;
            }
            return null;
        }



        public bool AddNewMarketingAssignment(cg_Marketing Objmarketing)
        {
            bool Isrecordadded = false;

            try
            {
                Isrecordadded = objGG_Dal.AddNewMarketingAssignment(Objmarketing);
            }
            catch (Exception ex)
            {
                Isrecordadded = false;
            }
            return Isrecordadded;

        }



    }
}
