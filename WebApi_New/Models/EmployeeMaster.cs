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
        public bool AddEmployeeDetails(cg_Employees cg_emp)
        {
           bool Isemployeeadded=false;

            try
            {
                Isemployeeadded = objGG_Dal.AddEmployeeDetails(cg_emp);
            }
            catch (Exception ex)
            {
                Isemployeeadded = false;
            }
            return Isemployeeadded;

        }

        public List<cg_WorkRegion> GetworkRegionList()
        {
            List<cg_WorkRegion> listcountries = new List<cg_WorkRegion>();

            try
            {
               // listcountries = objGG_Dal.getCountyrDetails();
            }
            catch (Exception ex)
            {
            }
            return listcountries;

        }
    }
}
