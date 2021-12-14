using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class EmployeeMaster
    {
        public readonly IConfiguration _configuration;
        Codegravity_Staffing_DAL objGG_Dal;
        public EmployeeMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }


        public List<cg_Employees> GetEmployeeDetails()
        {
            List<cg_Employees> listEmployee = new List<cg_Employees>();

            try
            {
                listEmployee = objGG_Dal.getEmployeeDetails();
            }
            catch (Exception ex)
            {
            }
            return listEmployee;

        }
    }
}
