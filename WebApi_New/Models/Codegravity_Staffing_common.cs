using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class cg_Marketing
    {
        public int Id { get; set; }
        public int Consult_Id { get; set; }
        public string Consult_Name { get; set; }

        public string Assigned_Sales_Recruiter { get; set; }
        public string Marketing_Tech { get; set; }
        public string Is_Open_To_All { get; set; }
        public string  Marketing_Start_Date { get; set; }
        public string Submitted_Vendor { get; set; }
        public string End_Client_Name { get; set; }
        public string Rate_confirmation { get; set; }
        public string Bill_Rate { get; set; }
        public string Assignment_date { get; set; }
        public string Assignment_status { get; set; }
        public string Interview_Schedudule_Date { get; set; }
        public string Interview_Status { get; set; }
        public string Visa_Status { get; set; }
        public string Marketing_Status { get; set; }

        public string Marketing_End_Date { get; set; }
        public string Notes { get; set; }
    }

    public class cg_Consultant
    {
        public int Consult_Id { get; set; }
        public string Consult_Name { get; set; }
        public string Consult_Email { get; set; }
        public string Consult_Phone { get; set; }
        public string Consult_Address { get; set; }
        public string Consult_Technology { get; set; }
        public string Consult_VisaStatus { get; set; }
        public int Consult_Status { get; set; }
    }

    public class cg_Employees
    {
        public int Emp_Id { get; set; }
        public string Emp_Name { get; set; }
        public string Emp_Email { get; set; }
        public string Emp_Phone { get; set; }
        public string Emp_work_Region { get; set; }
        public string Emp_IncentiveType { get; set; }
        public string Emp_Status { get; set; }
        public string Role_Id { get; set; }

    }
}
