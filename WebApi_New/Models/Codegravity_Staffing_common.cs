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
        public string Marketing_Start_Date { get; set; }
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

        public string Consult_First_Name { get; set; }
        public string Consult_Last_Name { get; set; }

        public string Consult_Email { get; set; }
        public string Consult_Phone { get; set; }
        public string Consult_Address { get; set; }
        public string Consult_Technology { get; set; }
        public string Consult_VisaStatus { get; set; }
        public int Consult_Status { get; set; }

        public string Consult_DOB { get; set; }
        public string Consult_Full_Name { get; set; }

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

    public class cg_Placement
    {
        public int Id { get; set; }
        public int Consult_id { get; set; }
        public string Consult_Name { get; set; }

        public string Placed_Sales_Recruiter { get; set; }
        public string Placed_Tech { get; set; }
        public string Is_Open_To_All { get; set; }
        public string PO_Date { get; set; }
        public string Project_Start_Date { get; set; }
        public string Project_Duration { get; set; }
        public string Bill_Rate { get; set; }
        public string Consultant_Pay_Rate { get; set; }
        public string Vendor_Name { get; set; }
        public string Vendor_Address { get; set; }
        public string Client_Name { get; set; }
        public string Client_Address { get; set; }
        public string Project_End_Date { get; set; }
        public string Visa_Type { get; set; }

        public string Project_Status { get; set; }
        public string Notes { get; set; }
    }

    public class cg_WorkRegion
    {

        public int CountryId { get; set; }

        public string CountyName { get; set; }

        public string Country_Description { get; set; }

    }
    public class cg_Technology
    {

        public int Id { get; set; }

        public string Technology_Name { get; set; }

        public string Technology_Description { get; set; }
        public string Notes { get; set; }

    }
    public class cg_VisaType
    {

        public int Id { get; set; }

        public string Visa_Name { get; set; }

        public string Visa_Description { get; set; }
        public string Notes { get; set; }
        public string active { get; set; }


    }


    public class cg_Submissions
    {

        public int Id { get; set; }

        public string Technology_Name { get; set; }

        public string Technology_Description { get; set; }
        public string Notes { get; set; }

    }
    public class cg_County
    {

        public int CountryId { get; set; }

        public string CountyName { get; set; }

        public string Country_Description { get; set; }
        public string Notes { get; set; }

    }

    public class cg_Incentives
    {

        public int Incentive_Id { get; set; }

        public string Incentive_Amount { get; set; }

        public string Incentive_Currency { get; set; }
        public string Incentive_Type
        { get; set; }

        public string Incentive_Country
        { get; set; }
        public string Incentive_Description { get; set; }

    }

    public class cg_Entitlement
    {
        
        public int Entit_Id { get; set; }

        public string Entit_Name { get; set; }

        public string Entit_Desc { get; set; }
        public string Entit_status { get; set; }

        public string Notes  { get; set; }
        
    }
}
