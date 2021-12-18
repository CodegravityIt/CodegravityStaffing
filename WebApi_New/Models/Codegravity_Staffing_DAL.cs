using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_New.Models
{
    public class Codegravity_Staffing_DAL
    {
        public readonly IConfiguration _configuration;

        public Codegravity_Staffing_DAL(IConfiguration configuration)
        {
            _configuration = configuration;
        }



        public DataTable dynamicTableData(string SpNameToExecute)
        {

            DataTable dtDynamicData = new DataTable();
            try
            {
                string Query = SpNameToExecute;
                string sqlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataAdapter dbAdapater;
                using (SqlConnection dbConnection = new SqlConnection(sqlDatasource))
                {
                    dbConnection.Open();
                    using (SqlCommand dbcommand = new SqlCommand(Query, dbConnection))
                    {
                        dbcommand.CommandType = CommandType.StoredProcedure;
                        //dbcommand.Parameters.AddWithValue("paraname", SqlDbType.NVarChar).Value = "";
                        dbAdapater = new SqlDataAdapter(dbcommand);
                        dbAdapater.Fill(dtDynamicData);
                        dbConnection.Close();
                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
            return dtDynamicData;
        }
        public List<cg_Marketing> getMarketingDetails()
        {
            DataTable MarketingDetails = new DataTable();
            List<cg_Marketing> listMarketing = new List<cg_Marketing>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();
            try
            {
                MarketingDetails = dynamicTableData("[dbo].[Sp_Getmarketingdetails]");
                if (MarketingDetails != null && MarketingDetails.Rows.Count > 0)
                {

                    string strname = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(MarketingDetails.Rows[1]["Consult_id"])).FirstOrDefault().Emp_FirstName.ToString();
                    listMarketing = (from DataRow dr in MarketingDetails.Rows
                                     select new cg_Marketing()
                                     {
                                         Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                         Consult_Id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                                         Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString(),

                                         Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FirstName.ToString(),
                                         Marketing_Tech = dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),
                                         Is_Open_To_All = dr["Is_Open_To_All"] is DBNull ? "" : Convert.ToString(dr["Is_Open_To_All"]),
                                         Marketing_Start_Date = dr["Marketing_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_Start_Date"]).ToShortDateString(),
                                         Submitted_Vendor = dr["Submited_Vendor"] is DBNull ? "" : Convert.ToString(dr["Submited_Vendor"]),
                                         End_Client_Name = dr["End_Client_Name"] is DBNull ? "" : Convert.ToString(dr["End_Client_Name"]),
                                         Rate_confirmation = dr["Rate_confirmation"] is DBNull ? "" : Convert.ToString(dr["Rate_confirmation"]),
                                         Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                                         Assignment_date = dr["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(dr["Assignment_date"]).ToShortDateString(),
                                         Assignment_status = dr["Assignment_status"] is DBNull ? "" : Convert.ToString(dr["Assignment_status"]),
                                         Interview_Schedudule_Date = dr["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToString(dr["Interview_Schedudule_Date"]),
                                         Interview_Status = dr["Interview_Status"] is DBNull ? "" : Convert.ToString(dr["Interview_Status"]),
                                         Visa_Status = dr["Visa_Status"] is DBNull ? "" : Convert.ToString(dr["Visa_Status"]),
                                         Marketing_Status = dr["Marketing_Status"] is DBNull ? "" : Convert.ToString(dr["Marketing_Status"]),
                                         Marketing_End_Date = dr["Marketing_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_End_Date"]).ToShortDateString(),
                                         Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"])

                                     }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listMarketing;
        }

        public List<cg_Consultant> getConsultDetails()
        {
            DataTable dtConsult = new DataTable();
            List<cg_Consultant> listConsult = new List<cg_Consultant>();

            try
            {

                dtConsult = dynamicTableData("[dbo].[Sp_Getconsultantdetails]");

                if (dtConsult != null && dtConsult.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtConsult.Rows
                                   select new cg_Consultant()
                                   {
                                       Consult_Id = Convert.ToInt32(dr["Consult_Id"]),
                                       Consult_First_Name = dr["Consult_First_Name"].ToString(),
                                       Consult_Last_Name = dr["Consult_Last_Name"].ToString(),
                                       Consult_Email = dr["Consult_Email"].ToString(),
                                       Consult_Phone = dr["Consult_Phone"].ToString(),
                                       Consult_Address = dr["Consult_Address"].ToString(),
                                       Consult_Technology = dr["Consult_Technology"].ToString(),
                                       Consult_VisaStatus = dr["Consult_VisaStatus"].ToString(),
                                       Consult_Status = Convert.ToInt32(dr["Consult_Status"]),
                                       Consult_DOB = dr["Consult_DOB"].ToString(),
                                       Consult_Full_Name = dr["Consult_First_Name"].ToString() + " " + dr["Consult_Last_Name"].ToString()
                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listConsult;
        }

        public List<cg_Employees> getEmployeeDetails()
        {
            DataTable dtRecruiters = new DataTable();
            List<cg_Employees> listemployee = new List<cg_Employees>();
            List<cg_WorkRegion> listCountry = getCountryDetails();

            try
            {
                dtRecruiters = dynamicTableData("[dbo].[Sp_GetEmployeeDetails]");


                if (dtRecruiters != null && dtRecruiters.Rows.Count > 0)
                {
                    listemployee = (from DataRow dr in dtRecruiters.Rows
                                    select new cg_Employees()
                                    {
                                        Emp_Id = Convert.ToInt32(dr["Emp_Id"]),
                                        Emp_FirstName = dr["Emp_FirstName"].ToString() + " " + dr["Emp_LastName"].ToString(),
                                        Emp_Email = dr["Emp_Email"].ToString(),
                                        Emp_Phone = dr["Emp_Phone"].ToString(),
                                        //Emp_work_Region = listCountry.Where(p => p.CountryId == Convert.ToInt32(dr["Emp_work_Region"])).FirstOrDefault().CountyName.ToString(),//dr["Emp_work_Region"].ToString(),

                                        //Emp_IncentiveType = dr["Emp_IncentiveType"],
                                        //Emp_Status = dr["Emp_Status"].ToString(),
                                        //Role_Id = dr["Role_Id"].ToString()

                                    }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listemployee;
        }


        public List<cg_Placement> getPlacementDetails()
        {
            DataTable PlacementDetails = new DataTable();
            List<cg_Placement> listMarketing = new List<cg_Placement>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();
            try
            {

                PlacementDetails = dynamicTableData("[dbo].[Sp_Getplacementdetails]");


                if (PlacementDetails != null && PlacementDetails.Rows.Count > 0)
                {

                    string strname = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(PlacementDetails.Rows[1]["Consult_id"])).FirstOrDefault().Emp_FirstName.ToString();
                    listMarketing = (from DataRow dr in PlacementDetails.Rows
                                     select new cg_Placement()
                                     {
                                         Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                         Consult_id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                                         Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_First_Name.ToString() +
                                                        listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Last_Name.ToString(),
                                         Placed_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Placed_Sales_Recruiter"])).FirstOrDefault().Emp_FirstName.ToString(),
                                         Placed_Tech = dr["Placed_Tech"] is DBNull ? "" : Convert.ToString(dr["Placed_Tech"]),
                                         PO_Date = dr["PO_Date"] is DBNull ? "" : Convert.ToDateTime(dr["PO_Date"]).ToShortDateString(),
                                         Project_Start_Date = dr["Project_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Project_Start_Date"]).ToShortDateString(),
                                         Project_Duration = dr["Project_Duration"] is DBNull ? "" : Convert.ToString(dr["Project_Duration"]),
                                         Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                                         Consultant_Pay_Rate = dr["Consultant_Pay_Rate"] is DBNull ? "" : Convert.ToString(dr["Consultant_Pay_Rate"]),
                                         Vendor_Name = dr["Vendor_Name"] is DBNull ? "" : Convert.ToString(dr["Vendor_Name"]),
                                         Vendor_Address = dr["Vendor_Address"] is DBNull ? "" : Convert.ToString(dr["Vendor_Address"]),
                                         Client_Name = dr["Client_Name"] is DBNull ? "" : Convert.ToString(dr["Client_Name"]),
                                         Client_Address = dr["Client_Address"] is DBNull ? "" : Convert.ToString(dr["Client_Address"]),
                                         Project_End_Date = dr["Project_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Project_End_Date"]).ToShortDateString(),
                                         Visa_Type = dr["Visa_Type"] is DBNull ? "" : Convert.ToString(dr["Visa_Type"]),
                                         Project_Status = dr["Project_Status"] is DBNull ? "" : Convert.ToString(dr["Project_Status"]),
                                         Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"])

                                     }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listMarketing;
        }

        public List<cg_WorkRegion> getCountryDetails()
        {
            DataTable dtWorkregion = new DataTable();
            List<cg_WorkRegion> listConsult = new List<cg_WorkRegion>();

            try
            {
                dtWorkregion = dynamicTableData("[dbo].[Sp_Getcountrydetails]");


                if (dtWorkregion != null && dtWorkregion.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtWorkregion.Rows
                                   select new cg_WorkRegion()
                                   {
                                       CountryId = Convert.ToInt32(dr["CountryId"]),
                                       CountyName = dr["CountyName"].ToString(),
                                       Country_Description = dr["Country_Description"].ToString()


                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listConsult;
        }
        public List<cg_Technology> getTechnologyDetails()
        {
            DataTable dtTechnology = new DataTable();
            List<cg_Technology> listConsult = new List<cg_Technology>();

            try
            {
                dtTechnology = dynamicTableData("[dbo].[Sp_Gettechnologydetails]");


                if (dtTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtTechnology.Rows
                                   select new cg_Technology()
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       Technology_Name = dr["Technology_Name"].ToString(),
                                       Technology_Description = dr["Technology_Description"].ToString(),
                                       Notes = dr["Notes"].ToString()


                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listConsult;
        }

        public List<cg_VisaType> getVisatypeDetails()
        {
            DataTable dtvisatype = new DataTable();
            List<cg_VisaType> listConsult = new List<cg_VisaType>();

            try
            {
                dtvisatype = dynamicTableData("[dbo].[Sp_Getvisatypedetails]");


                if (dtvisatype != null && dtvisatype.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtvisatype.Rows
                                   select new cg_VisaType()
                                   {
                                       Id = Convert.ToInt32(dr["Id"]),
                                       Visa_Name = dr["Visa_Name"].ToString(),
                                       Visa_Description = dr["Visa_Description"].ToString(),
                                       Notes = dr["Notes"].ToString(),
                                       active = dr["active"].ToString()

                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listConsult;
        }

        public List<cg_Incentives> getIncentivetypeDetails()
        {
            DataTable dtincentivetype = new DataTable();
            List<cg_Incentives> listIncentives = new List<cg_Incentives>();

            try
            {
                dtincentivetype = dynamicTableData("[dbo].[Sp_Getincentivedetails]");


                if (dtincentivetype != null && dtincentivetype.Rows.Count > 0)
                {
                    listIncentives = (from DataRow dr in dtincentivetype.Rows
                                   select new cg_Incentives()
                                   {
                                       Incentive_Id = Convert.ToInt32(dr["Incentive_Id"]),
                                       Incentive_Amount = dr["Incentive_Amount"].ToString(),
                                       Incentive_Currency = dr["Incentive_Currency"].ToString(),
                                       Incentive_Type = dr["Incentive_Type"].ToString(),
                                       Incentive_Country = dr["Incentive_Country"].ToString(),
                                       Incentive_Description = dr["Incentive_Description"].ToString()

                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listIncentives;
        }

        public List<cg_Entitlement> getEntitlementDetails()
        {
            DataTable dtentitlement = new DataTable();
            List<cg_Entitlement> listEntitlement = new List<cg_Entitlement>();

            try
            {
                dtentitlement = dynamicTableData("[dbo].[Sp_Getentitlementdetails]");


                if (dtentitlement != null && dtentitlement.Rows.Count > 0)
                {
                    listEntitlement = (from DataRow dr in dtentitlement.Rows
                                      select new cg_Entitlement()
                                      {
                                          Entit_Id = Convert.ToInt32(dr["Entit_Id"]),
                                          Entit_Name = dr["Entit_Name"].ToString(),
                                          Entit_Desc = dr["Entit_Desc"].ToString(),
                                          Entit_status = dr["Entit_status"].ToString(),
                                          Notes = dr["Notes"].ToString()
                                      }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listEntitlement;
        }

    }
}
