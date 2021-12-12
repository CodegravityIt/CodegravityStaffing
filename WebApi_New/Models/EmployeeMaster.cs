using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class EmployeeMaster
    {
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Phone { get; set; }
        public int Emp_work_Region { get; set; }
        public int Emp_IncentiveType { get; set; }
        public int Emp_Status { get; set; }
        public int Role_Id { get; set; }
    }
}
