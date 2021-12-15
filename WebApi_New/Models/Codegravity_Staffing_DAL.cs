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

        public List<cg_Marketing> getMarketingDetails()
        {
            DataTable MarketingDetails = new DataTable();
            List<cg_Marketing> listMarketing = new List<cg_Marketing>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();
            try
            {
                string Query = @"[dbo].[Sp_Getmarketingdetails]";
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
                        dbAdapater.Fill(MarketingDetails);
                        dbConnection.Close();
                    }
                }
                if (MarketingDetails != null && MarketingDetails.Rows.Count > 0)
                {

                    string strname = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(MarketingDetails.Rows[1]["Consult_id"])).FirstOrDefault().Emp_Name.ToString();
                    listMarketing = (from DataRow dr in MarketingDetails.Rows
                                     select new cg_Marketing()
                                     {
                                         Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                         Consult_Id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                                         Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Name.ToString(),
                                         Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_Name.ToString(),
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
                string Query = @"[dbo].[Sp_Getconsultantdetails]";
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
                        dbAdapater.Fill(dtConsult);
                        dbConnection.Close();
                    }
                }
                if (dtConsult != null && dtConsult.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtConsult.Rows
                                   select new cg_Consultant()
                                   {
                                       Consult_Id = Convert.ToInt32(dr["Consult_Id"]),
                                       Consult_Name = dr["Consult_Name"].ToString(),
                                       Consult_Email = dr["Consult_Email"].ToString(),
                                       Consult_Phone = dr["Consult_Phone"].ToString(),
                                       Consult_Address = dr["Consult_Address"].ToString(),
                                       Consult_Technology = dr["Consult_Technology"].ToString(),
                                       Consult_VisaStatus = dr["Consult_VisaStatus"].ToString(),
                                       Consult_Status = Convert.ToInt32(dr["Consult_Status"])

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
            List<cg_Employees> listConsult = new List<cg_Employees>();

            try
            {
                string Query = @"[dbo].[Sp_GetEmployeeDetails]";
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
                        dbAdapater.Fill(dtRecruiters);
                        dbConnection.Close();
                    }
                }
                if (dtRecruiters != null && dtRecruiters.Rows.Count > 0)
                {
                    listConsult = (from DataRow dr in dtRecruiters.Rows
                                   select new cg_Employees()
                                   {
                                       Emp_Id = Convert.ToInt32(dr["Emp_Id"]),
                                       Emp_Name = dr["Emp_Name"].ToString(),
                                       Emp_Email = dr["Emp_Email"].ToString(),
                                       Emp_Phone = dr["Emp_Phone"].ToString(),
                                       Emp_work_Region = dr["Emp_work_Region"].ToString(),
                                       Emp_IncentiveType = dr["Emp_IncentiveType"].ToString(),
                                       Emp_Status = dr["Emp_Status"].ToString(),
                                       Role_Id = dr["Role_Id"].ToString()

                                   }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listConsult;
        }


        public List<cg_Placement> getPlacementDetails()
        {
            DataTable PlacementDetails = new DataTable();
            List<cg_Placement> listMarketing = new List<cg_Placement>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();
            try
            {
                string Query = @"[dbo].[Sp_Getplacementdetails]";
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
                        dbAdapater.Fill(PlacementDetails);
                        dbConnection.Close();
                    }
                }
                if (PlacementDetails != null && PlacementDetails.Rows.Count > 0)
                {

                    string strname = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(PlacementDetails.Rows[1]["Consult_id"])).FirstOrDefault().Emp_Name.ToString();
                    listMarketing = (from DataRow dr in PlacementDetails.Rows
                                     select new cg_Placement()
                                     {
                                         Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                         Consult_id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                                         Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Name.ToString(),
                                         Placed_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Placed_Sales_Recruiter"])).FirstOrDefault().Emp_Name.ToString(),
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
    }
}
