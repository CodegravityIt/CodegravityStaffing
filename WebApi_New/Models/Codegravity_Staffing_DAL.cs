﻿using Microsoft.Extensions.Configuration;
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
            // List<cg_Consultant> listConsultant = getConsultDetails(1);
            List<cg_Consultant> listConsultant = getConsultDetails();

            List<cg_Technology> listTech = getTechnologyDetails();
            try
            {
                MarketingDetails = dynamicTableData("[dbo].[Sp_Getmarketingdetails]");
                if (MarketingDetails != null && MarketingDetails.Rows.Count > 0)
                {

                    string strname = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(MarketingDetails.Rows[1]["Consult_id"])).FirstOrDefault().Emp_FirstName.ToString();

                    for (int i = 0; i < MarketingDetails.Rows.Count; i++)
                    {
                        cg_Marketing obj = new cg_Marketing();
                        obj.Id = Convert.ToInt32(MarketingDetails.Rows[i]["Id"]);
                        obj.Consult_Id = Convert.ToInt32(MarketingDetails.Rows[i]["Consult_Id"]);
                        //obj.Consult_Name = Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(MarketingDetails.Rows[i]["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString();
                        obj.Is_Open_To_All = Convert.ToString(MarketingDetails.Rows[i]["Is_Open_To_All"]);
                        obj.Marketing_Start_Date = Convert.ToString(MarketingDetails.Rows[i]["Marketing_Start_Date"]);
                        obj.Visa_Status = Convert.ToString(MarketingDetails.Rows[i]["Visa_Status"]);
                        obj.Marketing_Status = Convert.ToString(MarketingDetails.Rows[i]["Marketing_Status"]);
                        obj.Notes = Convert.ToString(MarketingDetails.Rows[i]["Notes"]);
                        obj.Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(MarketingDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString();
                        //obj.Marketing_Tech = listTech.Where(p => p.Technology_Name == MarketingDetails.Rows[i]["Marketing_Tech"]).Technology_Name.ToString();//dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),


                        listMarketing.Add(obj);
                    }
                    #region commented
                    //listMarketing = (from DataRow dr in MarketingDetails.Rows
                    //                 select new cg_Marketing()
                    //                 {
                    //                     Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                    //                     Consult_Id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                    //                     Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString(),
                    //                     Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString(),
                    //                     Marketing_Tech = dr["Marketing_Tech"] is DBNull ? "0" : listTech.Where(p => p.Id == Convert.ToInt32(dr["Marketing_Tech"])).FirstOrDefault().Technology_Name.ToString(),//dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),
                    //                     Is_Open_To_All = dr["Is_Open_To_All"] is DBNull ? "" : Convert.ToString(dr["Is_Open_To_All"]),
                    //                     Marketing_Start_Date = dr["Marketing_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_Start_Date"]).ToShortDateString(),
                    //                     Visa_Status = dr["Visa_Status"] is DBNull ? "" : Convert.ToString(dr["Visa_Status"]),
                    //                     Marketing_Status = dr["Marketing_Status"] is DBNull ? "" : Convert.ToString(dr["Marketing_Status"]),
                    //                     Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"])


                    //                     //Submitted_Vendor = dr["Submited_Vendor"] is DBNull ? "" : Convert.ToString(dr["Submited_Vendor"]),
                    //                     // End_Client_Name = dr["End_Client_Name"] is DBNull ? "" : Convert.ToString(dr["End_Client_Name"]),
                    //                     //Rate_confirmation = dr["Rate_confirmation"] is DBNull ? "" : Convert.ToString(dr["Rate_confirmation"]),
                    //                     //Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                    //                     //Assignment_date = dr["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(dr["Assignment_date"]).ToShortDateString(),
                    //                     //Assignment_status = dr["Assignment_status"] is DBNull ? "" : Convert.ToString(dr["Assignment_status"]),
                    //                     //Interview_Schedudule_Date = dr["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToString(dr["Interview_Schedudule_Date"]),
                    //                     //Interview_Status = dr["Interview_Status"] is DBNull ? "" : Convert.ToString(dr["Interview_Status"]),
                    //                     //Marketing_End_Date = dr["Marketing_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_End_Date"]).ToShortDateString(),

                    //                 }).ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listMarketing;
        }

        //public List<cg_Consultant> getConsultDetails( int Allorany)
        public List<cg_Consultant> getConsultDetails()

        {
            DataTable dtConsult = new DataTable();
            List<cg_Consultant> listConsult = new List<cg_Consultant>();

            try
            {

                // dtConsult = dynamicTableData("[dbo].[Sp_Getconsultantdetails]");
                string Query = @"[dbo].[Sp_Getconsultantdetails]";
                string sqlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataAdapter dbAdapater;
                using (SqlConnection dbConnection = new SqlConnection(sqlDatasource))
                {
                    dbConnection.Open();
                    using (SqlCommand dbcommand = new SqlCommand(Query, dbConnection))
                    {
                        dbcommand.CommandType = CommandType.StoredProcedure;
                        //dbcommand.Parameters.AddWithValue("@ConsultantStatusId", SqlDbType.Int).Value = 1;
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
                                        Emp_Id = dr["Emp_Id"] is DBNull ? 0 : Convert.ToInt32(dr["Emp_Id"]),
                                        Emp_FirstName = dr["Emp_FirstName"] is DBNull ? "" : dr["Emp_FirstName"].ToString(),
                                        Emp_LastName = dr["Emp_LastName"] is DBNull ? "" : dr["Emp_LastName"].ToString(),
                                        Emp_Email = dr["Emp_Email"] is DBNull ? "" : dr["Emp_Email"].ToString(),
                                        Emp_Phone = dr["Emp_Phone"] is DBNull ? "" : dr["Emp_Phone"].ToString(),
                                        Emp_work_Region = dr["Emp_work_Region"] is DBNull ? 0 : Convert.ToInt32(dr["Emp_work_Region"]),
                                        Emp_IncentiveType = dr["Emp_IncentiveType"] is DBNull ? 0 : Convert.ToInt32(dr["Emp_IncentiveType"]),
                                        Emp_Status = dr["Emp_Status"] is DBNull ? 0 : Convert.ToInt32(dr["Emp_Status"]),
                                        Role_Id = dr["Role_Id"] is DBNull ? 0 : Convert.ToInt32(dr["Role_Id"]),
                                        Emp_Country = listCountry.Where(p => p.CountryId == Convert.ToInt32(dr["Emp_work_Region"])).FirstOrDefault().CountyName.ToString(),
                                        Emp_FullName = dr["Emp_FirstName"].ToString() + " " + dr["Emp_LastName"].ToString()



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
            List<cg_Placement> listPlacement = new List<cg_Placement>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            // List<cg_Consultant> listConsultant = getConsultDetails(-1);
            List<cg_Consultant> listConsultant = getConsultDetails();

            try
            {

                PlacementDetails = dynamicTableData("[dbo].[Sp_Getplacementdetails]");


                if (PlacementDetails != null && PlacementDetails.Rows.Count > 0)
                {

                    listPlacement = (from DataRow dr in PlacementDetails.Rows
                                     select new cg_Placement()
                                     {
                                         Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                         Consult_id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                                         Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString(),
                                         Placed_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Placed_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString(),
                                         Placed_Tech = dr["Placed_Tech"] is DBNull ? "" : Convert.ToString(dr["Placed_Tech"]),
                                         PO_Date = dr["PO_Date"] is DBNull ? "" : Convert.ToDateTime(dr["PO_Date"]).ToString("MM/dd/yyyy"),
                                         Visa_Type = dr["Visa_Type"] is DBNull ? "" : Convert.ToString(dr["Visa_Type"]),
                                         Project_Start_Date = dr["Project_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Project_Start_Date"]).ToString("MM/dd/yyyy"),
                                         Project_Duration = dr["Project_Duration"] is DBNull ? "" : Convert.ToString(dr["Project_Duration"]),
                                         Project_End_Date = dr["Project_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Project_End_Date"]).ToString("MM/dd/yyyy"),
                                         Vendor_Name = dr["Vendor_Name"] is DBNull ? "" : Convert.ToString(dr["Vendor_Name"]),
                                         Vendor_SPOC_Name = dr["Vendor_SPOC_Name"] is DBNull ? "" : Convert.ToString(dr["Vendor_SPOC_Name"]),
                                         Vendor_SPOC_Email = dr["Vendor_SPOC_Email"] is DBNull ? "" : Convert.ToString(dr["Vendor_SPOC_Email"]),
                                         Vendor_SPOC_PhoneNumber = dr["Vendor_SPOC_PhoneNumber"] is DBNull ? "" : Convert.ToString(dr["Vendor_SPOC_PhoneNumber"]),
                                         Vendor_Address = dr["Vendor_Address"] is DBNull ? "" : Convert.ToString(dr["Vendor_Address"]),
                                         Client_Name = dr["Client_Name"] is DBNull ? "" : Convert.ToString(dr["Client_Name"]),
                                         Client_SPOC_Name = dr["Client_SPOC_Name"] is DBNull ? "" : Convert.ToString(dr["Client_SPOC_Name"]),
                                         Client_SPOC_Email = dr["Client_SPOC_Email"] is DBNull ? "" : Convert.ToString(dr["Client_SPOC_Email"]),
                                         Client_SPOC_PhoneNumber = dr["Client_SPOC_PhoneNumber"] is DBNull ? "" : Convert.ToString(dr["Client_SPOC_PhoneNumber"]),
                                         Client_Address = dr["Client_Address"] is DBNull ? "" : Convert.ToString(dr["Client_Address"]),
                                         Project_Type = dr["Project_Type"] is DBNull ? "" : Convert.ToString(dr["Project_Type"]).ToString(),
                                         Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                                         Consultant_Pay_Rate = dr["Consultant_Pay_Rate"] is DBNull ? "" : Convert.ToString(dr["Consultant_Pay_Rate"]),
                                         Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"]),
                                         Placement_Status = dr["Placement_Status"] is DBNull ? 0 : Convert.ToInt32(dr["Placement_Status"]),
                                         Created_Date = dr["Created_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Created_Date"]).ToString("MM/dd/yyyy"),
                                         Created_by = dr["Created_by"] is DBNull ? "" : Convert.ToString(dr["Created_by"]),
                                         Modified_Date = dr["Modified_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Modified_Date"]).ToString("MM/dd/yyyy"),
                                         Modified_by = dr["Modified_by"] is DBNull ? "" : Convert.ToString(dr["Modified_by"])
                                     }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listPlacement;
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

        public bool AddEmployeeDetails(cg_Employees em)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;

            try
            {
                String query = "INSERT INTO [CG].[EmployeeMaster] (Emp_FirstName,Emp_LastName,Emp_Email,Emp_Phone,Emp_work_Region,Emp_IncentiveType,Emp_Status,Role_Id) VALUES (@Emp_FirstName,@Emp_LastName,@Emp_Email,@Emp_Phone,@Emp_work_Region,@Emp_IncentiveType,@Emp_Status,@Role_Id)";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Emp_FirstName", em.Emp_FirstName);
                        mycommand.Parameters.AddWithValue("@Emp_LastName", em.Emp_LastName);
                        mycommand.Parameters.AddWithValue("@Emp_Email", em.Emp_Email);
                        mycommand.Parameters.AddWithValue("@Emp_Phone", em.Emp_Phone);

                        mycommand.Parameters.AddWithValue("@Emp_work_Region", em.Emp_work_Region);
                        mycommand.Parameters.AddWithValue("@Emp_IncentiveType", em.Emp_IncentiveType);
                        mycommand.Parameters.AddWithValue("@Emp_Status", em.Emp_Status);
                        mycommand.Parameters.AddWithValue("@Role_Id", em.Role_Id);
                        mycon.Open();
                        myReader = mycommand.ExecuteReader();
                        // table.Load(myReader.af);
                        if (myReader.RecordsAffected > 0)
                            result = true;
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return result;
        }

        public bool AddNewMarketingAssignment(cg_Marketing em)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;


            try
            {
                String query = "INSERT INTO [CG].[Consultant_Marketing] (Consult_id,Assigned_Sales_Recruiter,Marketing_Tech,Is_Open_To_All,Marketing_Start_Date,Marketing_End_Date,Visa_Status,Notes) VALUES " +
                                                                       "(@Consult_id,@Assigned_Sales_Recruiter,@Marketing_Tech,@Is_Open_To_All,@Marketing_Start_Date,@Marketing_End_Date,@Visa_Status,@Notes)";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consult_id", em.Consult_Id);
                        mycommand.Parameters.AddWithValue("@Assigned_Sales_Recruiter", em.Assigned_Sales_Recruiter);
                        mycommand.Parameters.AddWithValue("@Marketing_Tech", em.Marketing_Tech);
                        mycommand.Parameters.AddWithValue("@Is_Open_To_All", em.Is_Open_To_All);

                        mycommand.Parameters.AddWithValue("@Marketing_Start_Date", em.Marketing_Start_Date);
                        mycommand.Parameters.AddWithValue("@Marketing_End_Date", em.Marketing_End_Date);
                        mycommand.Parameters.AddWithValue("@Visa_Status", em.Visa_Status);
                        mycommand.Parameters.AddWithValue("@Notes", em.Notes);
                        mycon.Open();
                        myReader = mycommand.ExecuteReader();
                        // table.Load(myReader.af);
                        if (myReader.RecordsAffected > 0)
                        {
                            updateConsultantStatus(em.Consult_Id, 2);
                            result = true;
                        }
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return result;
        }

        public void updateConsultantStatus(int Consultant_id, int Status_id)
        {
            try
            {
                String query = @"update [CG].[ConsultantMaster] set [Consult_Status]=" + Status_id + "where [Consult_Id]=" + Consultant_id + ";";
                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                bool result = false;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consultant_id", Consultant_id);
                        mycommand.Parameters.AddWithValue("@Consult_Status", Status_id);

                        mycon.Open();
                        myReader = mycommand.ExecuteReader();
                        // table.Load(myReader.af);
                        if (myReader.RecordsAffected > 0)
                            result = true;
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                //throw;
            }
        }

        public bool AddNewplacementAssignment(cg_Placement em)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;


            try
            {
                String query = "INSERT INTO [CG].[Consultant_Placement] (Consult_id,Placed_Sales_Recruiter,Placed_Tech,PO_Date,Visa_Type,Project_Start_Date,Project_Duration,Project_End_Date," +
                    "Vendor_Name,Vendor_SPOC_Name,Vendor_SPOC_Email,Vendor_SPOC_PhoneNumber,Vendor_Address,Client_Name," +
                    "Client_SPOC_Name,Client_SPOC_Email,Client_SPOC_PhoneNumber,Client_Address,Project_Type,Bill_Rate,Consultant_Pay_Rate,Notes,Placement_Status,Created_Date,Created_by,Modified_Date,Modified_by)" +
                    " VALUES " +
                    "(@Consult_id,@Placed_Sales_Recruiter,@Placed_Tech,@PO_Date,@Visa_Type,@Project_Start_Date,@Project_Duration,@Project_End_Date,@Vendor_Name,@Vendor_SPOC_Name,@Vendor_SPOC_Email," +
                    "@Vendor_SPOC_PhoneNumber,@Vendor_Address,@Client_Name,@Client_SPOC_Name,@Client_SPOC_Email,@Client_SPOC_PhoneNumber," +
                    "@Client_Address,@Project_Type,@Bill_Rate,@Consultant_Pay_Rate,@Notes,@Placement_Status,@Created_Date,@Created_by,@Modified_Date,@Modified_by" +
                    ")";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consult_id", em.Consult_id);
                        mycommand.Parameters.AddWithValue("@Placed_Sales_Recruiter", em.Placed_Sales_Recruiter);
                        mycommand.Parameters.AddWithValue("@Placed_Tech", em.Placed_Tech);
                        mycommand.Parameters.AddWithValue("@PO_Date", Convert.ToDateTime(em.PO_Date).ToString("MM/dd/yyyy"));

                        mycommand.Parameters.AddWithValue("@Visa_Type", em.Visa_Type);
                        mycommand.Parameters.AddWithValue("@Project_Start_Date", Convert.ToDateTime(em.Project_Start_Date).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Project_Duration", em.Project_Duration+" months");

                        mycommand.Parameters.AddWithValue("@Project_End_Date", Convert.ToDateTime(em.Project_End_Date).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Vendor_Name", em.Vendor_Name);
                        mycommand.Parameters.AddWithValue("@Vendor_SPOC_Name", em.Vendor_SPOC_Name);

                        mycommand.Parameters.AddWithValue("@Vendor_SPOC_Email", em.Vendor_SPOC_Email);
                        mycommand.Parameters.AddWithValue("@Vendor_SPOC_PhoneNumber", em.Vendor_SPOC_PhoneNumber);
                        mycommand.Parameters.AddWithValue("@Vendor_Address", em.Vendor_Address);


                        mycommand.Parameters.AddWithValue("@Client_Name", em.Client_Name);
                        mycommand.Parameters.AddWithValue("@Client_SPOC_Name", em.Client_SPOC_Name);
                        mycommand.Parameters.AddWithValue("@Client_SPOC_Email", em.Client_SPOC_Email);


                        mycommand.Parameters.AddWithValue("@Client_SPOC_PhoneNumber", em.Client_SPOC_PhoneNumber);
                        mycommand.Parameters.AddWithValue("@Client_Address", em.Client_Address);
                        mycommand.Parameters.AddWithValue("@Project_Type", em.Project_Type);


                        mycommand.Parameters.AddWithValue("@Bill_Rate", "$"+em.Bill_Rate);
                        mycommand.Parameters.AddWithValue("@Consultant_Pay_Rate", "$" + em.Consultant_Pay_Rate);
                        mycommand.Parameters.AddWithValue("@Notes", em.Notes);
                        mycommand.Parameters.AddWithValue("@Placement_Status", em.Placement_Status);
                        mycommand.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Created_by", em.Created_by);

                        mycommand.Parameters.AddWithValue("@Modified_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Modified_by", em.Modified_by);

                        mycon.Open();
                        myReader = mycommand.ExecuteReader();
                        // table.Load(myReader.af);
                        if (myReader.RecordsAffected > 0)
                        {
                            //updateConsultantStatus(em.Consult_Id, 2);
                            result = true;
                        }
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return result;
        }

        public List<cg_Submissions> getSubmissionlist()
        {
            DataTable SubmissionDetails = new DataTable();
            List<cg_Submissions> listSubmissions = new List<cg_Submissions>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();

            List<cg_Technology> listTech = getTechnologyDetails();
            try
            {
                SubmissionDetails = dynamicTableData("[dbo].[Sp_GetsubmissionsList]");
                if (SubmissionDetails != null && SubmissionDetails.Rows.Count > 0)
                {


                    for (int i = 0; i < SubmissionDetails.Rows.Count; i++)
                    {
                        cg_Submissions obj = new cg_Submissions();
                        obj.Id = Convert.ToInt32(SubmissionDetails.Rows[i]["Id"]);
                        obj.Consult_id = Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"]);
                        obj.Recruiter_id = Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"]);
                        obj.NoOfSubmissions = Convert.ToInt32(SubmissionDetails.Rows[i]["NumberOfSubmissions"]);

                        obj.Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name;//Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Recruiter_Name = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"])).FirstOrDefault().Emp_FullName.ToString();

                        obj.Marketing_Tech = listTech.Where(p => p.Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Marketing_Tech"])).FirstOrDefault().Technology_Name;
                        obj.Vendor_Name = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Name"]);
                        obj.Vendor_POC_Name = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Name"]);
                        obj.Vendor_POC_Email = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Email"]);
                        obj.Vendor_POC_PhoneNumber = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"]);
                        obj.Vendor_Address = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Address"]);
                        obj.End_Client_Name = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Name"]);
                        obj.End_Client_POC_Name = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Name"]);
                        obj.End_Client_POC_Email = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Email"]);
                        obj.End_Client_POC_PhoneNumber = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"]);
                        obj.End_Client_Address = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Address"]);
                        obj.Rate_confirmation = Convert.ToString(SubmissionDetails.Rows[i]["Rate_confirmation"]);
                        obj.Bill_Rate = Convert.ToString(SubmissionDetails.Rows[i]["Bill_Rate"]);
                        obj.Assignment_date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Assignment_date"]).ToShortDateString();
                        obj.Assignment_done_by = Convert.ToString(SubmissionDetails.Rows[i]["Assignment_done_by"]);
                        obj.Assignment_status = Convert.ToString(SubmissionDetails.Rows[i]["Assignment_status"]);
                        obj.Interview_Schedudule_Date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Interview_Schedudule_Date"]).ToShortDateString();
                        obj.Interview_Status = Convert.ToString(SubmissionDetails.Rows[i]["Interview_Status"]);
                        obj.Created_date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Created_date"]).ToShortDateString();
                        obj.Created_by = Convert.ToString(SubmissionDetails.Rows[i]["Created_by"]);
                        obj.Modified_Date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Modified_Date"]).ToShortDateString();
                        obj.Modified_by = Convert.ToString(SubmissionDetails.Rows[i]["Modified_by"]);
                        obj.Notes = Convert.ToString(SubmissionDetails.Rows[i]["Notes"]);
                        obj.submission_status = Convert.ToString(SubmissionDetails.Rows[i]["submission_status"]);


                        listSubmissions.Add(obj);
                    }
                    #region commented
                    //listMarketing = (from DataRow dr in MarketingDetails.Rows
                    //                 select new cg_Marketing()
                    //                 {
                    //                     Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                    //                     Consult_Id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                    //                     Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString(),
                    //                     Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString(),
                    //                     Marketing_Tech = dr["Marketing_Tech"] is DBNull ? "0" : listTech.Where(p => p.Id == Convert.ToInt32(dr["Marketing_Tech"])).FirstOrDefault().Technology_Name.ToString(),//dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),
                    //                     Is_Open_To_All = dr["Is_Open_To_All"] is DBNull ? "" : Convert.ToString(dr["Is_Open_To_All"]),
                    //                     Marketing_Start_Date = dr["Marketing_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_Start_Date"]).ToShortDateString(),
                    //                     Visa_Status = dr["Visa_Status"] is DBNull ? "" : Convert.ToString(dr["Visa_Status"]),
                    //                     Marketing_Status = dr["Marketing_Status"] is DBNull ? "" : Convert.ToString(dr["Marketing_Status"]),
                    //                     Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"])


                    //                     //Submitted_Vendor = dr["Submited_Vendor"] is DBNull ? "" : Convert.ToString(dr["Submited_Vendor"]),
                    //                     // End_Client_Name = dr["End_Client_Name"] is DBNull ? "" : Convert.ToString(dr["End_Client_Name"]),
                    //                     //Rate_confirmation = dr["Rate_confirmation"] is DBNull ? "" : Convert.ToString(dr["Rate_confirmation"]),
                    //                     //Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                    //                     //Assignment_date = dr["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(dr["Assignment_date"]).ToShortDateString(),
                    //                     //Assignment_status = dr["Assignment_status"] is DBNull ? "" : Convert.ToString(dr["Assignment_status"]),
                    //                     //Interview_Schedudule_Date = dr["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToString(dr["Interview_Schedudule_Date"]),
                    //                     //Interview_Status = dr["Interview_Status"] is DBNull ? "" : Convert.ToString(dr["Interview_Status"]),
                    //                     //Marketing_End_Date = dr["Marketing_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_End_Date"]).ToShortDateString(),

                    //                 }).ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listSubmissions;
        }

        public List<cg_Submissions> getSubmissionDetails()
        {
            DataTable SubmissionDetails = new DataTable();
            List<cg_Submissions> listSubmissions = new List<cg_Submissions>();
            List<cg_Employees> listemployee = getEmployeeDetails();
            List<cg_Consultant> listConsultant = getConsultDetails();

            List<cg_Technology> listTech = getTechnologyDetails();
            try
            {
                SubmissionDetails = dynamicTableData("[dbo].[Sp_Getsubmissiondetailedinfo]");
                if (SubmissionDetails != null && SubmissionDetails.Rows.Count > 0)
                {


                    for (int i = 0; i < SubmissionDetails.Rows.Count; i++)
                    {
                        cg_Submissions obj = new cg_Submissions();
                        obj.Id = Convert.ToInt32(SubmissionDetails.Rows[i]["Id"]);
                        obj.Consult_id = Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"]);
                        obj.Recruiter_id = Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"]);
                        //obj.NoOfSubmissions=Convert.ToInt32(SubmissionDetails.Rows[i]["NumberOfSubmissions"]);

                        obj.Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name;//Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Recruiter_Name = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"])).FirstOrDefault().Emp_FullName.ToString();

                        obj.Marketing_Tech = listTech.Where(p => p.Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Marketing_Tech"])).FirstOrDefault().Technology_Name;
                        obj.Vendor_Name = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Name"]);
                        obj.Vendor_POC_Name = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Name"]);
                        obj.Vendor_POC_Email = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Email"]);
                        obj.Vendor_POC_PhoneNumber = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"]);
                        obj.Vendor_Address = Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Address"]);
                        obj.End_Client_Name = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Name"]);
                        obj.End_Client_POC_Name = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Name"]);
                        obj.End_Client_POC_Email = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Email"]);
                        obj.End_Client_POC_PhoneNumber = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"]);
                        obj.End_Client_Address = Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Address"]);
                        obj.Rate_confirmation = Convert.ToString(SubmissionDetails.Rows[i]["Rate_confirmation"]);
                        obj.Bill_Rate = Convert.ToString(SubmissionDetails.Rows[i]["Bill_Rate"]);
                        obj.Assignment_date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Assignment_date"]).ToString("MM/dd/yyyy");
                        obj.Assignment_done_by = Convert.ToString(SubmissionDetails.Rows[i]["Assignment_done_by"]);

                        obj.Assignment_status = Convert.ToString(SubmissionDetails.Rows[i]["Assignment_status"]);

                        obj.Interview_Schedudule_Date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Interview_Schedudule_Date"]).ToString("MM/dd/yyyy"); ;
                        obj.Interview_Status = Convert.ToString(SubmissionDetails.Rows[i]["Interview_Status"]);

                        obj.Created_date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Created_date"]).ToString("MM/dd/yyyy");

                        obj.Created_by = Convert.ToString(SubmissionDetails.Rows[i]["Created_by"]);

                        obj.Modified_Date = Convert.ToDateTime(SubmissionDetails.Rows[i]["Modified_Date"]).ToString("MM/dd/yyyy");

                        obj.Modified_by = Convert.ToString(SubmissionDetails.Rows[i]["Modified_by"]);

                        obj.Notes = Convert.ToString(SubmissionDetails.Rows[i]["Notes"]);
                        obj.submission_status = Convert.ToString(SubmissionDetails.Rows[i]["submission_status"]);


                        listSubmissions.Add(obj);
                    }
                    #region commented
                    //listMarketing = (from DataRow dr in MarketingDetails.Rows
                    //                 select new cg_Marketing()
                    //                 {
                    //                     Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                    //                     Consult_Id = dr["Consult_id"] is DBNull ? 0 : Convert.ToInt32(dr["Consult_id"]),
                    //                     Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString(),
                    //                     Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(dr["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString(),
                    //                     Marketing_Tech = dr["Marketing_Tech"] is DBNull ? "0" : listTech.Where(p => p.Id == Convert.ToInt32(dr["Marketing_Tech"])).FirstOrDefault().Technology_Name.ToString(),//dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),
                    //                     Is_Open_To_All = dr["Is_Open_To_All"] is DBNull ? "" : Convert.ToString(dr["Is_Open_To_All"]),
                    //                     Marketing_Start_Date = dr["Marketing_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_Start_Date"]).ToShortDateString(),
                    //                     Visa_Status = dr["Visa_Status"] is DBNull ? "" : Convert.ToString(dr["Visa_Status"]),
                    //                     Marketing_Status = dr["Marketing_Status"] is DBNull ? "" : Convert.ToString(dr["Marketing_Status"]),
                    //                     Notes = dr["Notes"] is DBNull ? "" : Convert.ToString(dr["Notes"])


                    //                     //Submitted_Vendor = dr["Submited_Vendor"] is DBNull ? "" : Convert.ToString(dr["Submited_Vendor"]),
                    //                     // End_Client_Name = dr["End_Client_Name"] is DBNull ? "" : Convert.ToString(dr["End_Client_Name"]),
                    //                     //Rate_confirmation = dr["Rate_confirmation"] is DBNull ? "" : Convert.ToString(dr["Rate_confirmation"]),
                    //                     //Bill_Rate = dr["Bill_Rate"] is DBNull ? "" : Convert.ToString(dr["Bill_Rate"]),
                    //                     //Assignment_date = dr["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(dr["Assignment_date"]).ToShortDateString(),
                    //                     //Assignment_status = dr["Assignment_status"] is DBNull ? "" : Convert.ToString(dr["Assignment_status"]),
                    //                     //Interview_Schedudule_Date = dr["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToString(dr["Interview_Schedudule_Date"]),
                    //                     //Interview_Status = dr["Interview_Status"] is DBNull ? "" : Convert.ToString(dr["Interview_Status"]),
                    //                     //Marketing_End_Date = dr["Marketing_End_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Marketing_End_Date"]).ToShortDateString(),

                    //                 }).ToList();
                    #endregion
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listSubmissions;
        }

        public bool AddNewsubmission(cg_Submissions em)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;


            try
            {
                String query = "INSERT INTO [CG].[Consultant_Placement] (Consult_id,Recruiter_id,Marketing_Tech,Vendor_Name,Vendor_POC_Name,Vendor_POC_Email,Vendor_POC_PhoneNumber,Vendor_Address," +
                    "Vendor_Name,Vendor_SPOC_Name,Vendor_SPOC_Email,Vendor_SPOC_PhoneNumber,Vendor_Address,Client_Name," +
                    "End_Client_Name,End_Client_POC_Name,End_Client_POC_Email,End_Client_POC_PhoneNumber,End_Client_Address,Rate_confirmation,Bill_Rate,Assignment_date,Assignment_status,Assignment_done_by," +
                    "Interview_Schedudule_Date,Interview_Status,submission_status,Created_by,Modified_by,Notes)" +
                    " VALUES " +
                    "(@Consult_id,@Recruiter_id,@Marketing_Tech,@Vendor_Name,@Vendor_POC_Name,@Vendor_POC_Email,@Vendor_POC_PhoneNumber,@Vendor_Address,@End_Client_Name,@End_Client_POC_Name,@End_Client_POC_Email,@End_Client_POC_PhoneNumber," +
                    "@End_Client_Address,@Rate_confirmation,@Bill_Rate,@Assignment_date,@Assignment_status,@Assignment_done_by,@Interview_Schedudule_Date,@Interview_Status,@submission_status,@Created_by," +
                    "@Modified_by,@Notes)";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consult_id", em.Consult_id);
                        mycommand.Parameters.AddWithValue("@Recruiter_id", em.Recruiter_id);
                        mycommand.Parameters.AddWithValue("@Marketing_Tech", em.Marketing_Tech);
                        mycommand.Parameters.AddWithValue("@Vendor_Name", em.Vendor_Name);

                        mycommand.Parameters.AddWithValue("@Vendor_POC_Name", em.Vendor_POC_Name);
                        mycommand.Parameters.AddWithValue("@Vendor_POC_Email", em.Vendor_POC_Email);
                        mycommand.Parameters.AddWithValue("@Vendor_POC_PhoneNumber", em.Vendor_POC_PhoneNumber);

                        mycommand.Parameters.AddWithValue("@Vendor_Address",em.Vendor_Address);
                        mycommand.Parameters.AddWithValue("@End_Client_Name", em.End_Client_Name);
    

                        mycommand.Parameters.AddWithValue("@End_Client_POC_Name", em.End_Client_POC_Name);
                        mycommand.Parameters.AddWithValue("@End_Client_POC_Email", em.End_Client_POC_Email);
                        mycommand.Parameters.AddWithValue("@End_Client_POC_PhoneNumber", em.End_Client_POC_PhoneNumber);


                        mycommand.Parameters.AddWithValue("@End_Client_POC_PhoneNumber", em.End_Client_POC_PhoneNumber);
                        mycommand.Parameters.AddWithValue("@End_Client_Address", em.End_Client_Address);
                        mycommand.Parameters.AddWithValue("@Rate_confirmation", em.Rate_confirmation);


                        mycommand.Parameters.AddWithValue("@Bill_Rate", "$" + em.Bill_Rate);
                        mycommand.Parameters.AddWithValue("@Assignment_date", Convert.ToDateTime(em.Assignment_date).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Assignment_status", em.Assignment_status);
                        mycommand.Parameters.AddWithValue("@Assignment_done_by", em.Assignment_done_by);

                        mycommand.Parameters.AddWithValue("@Interview_Schedudule_Date", Convert.ToDateTime(em.Interview_Schedudule_Date).ToString("MM/dd/yyyy"));

                        mycommand.Parameters.AddWithValue("@Interview_Status", em.Interview_Status);
                        mycommand.Parameters.AddWithValue("@submission_status", em.submission_status);

                        mycommand.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Created_by", em.Created_by);

                        mycommand.Parameters.AddWithValue("@Modified_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Modified_by", em.Modified_by);
                        mycommand.Parameters.AddWithValue("@Notes", em.Notes);

                        mycon.Open();
                        myReader = mycommand.ExecuteReader();
                        // table.Load(myReader.af);
                        if (myReader.RecordsAffected > 0)
                        {
                            //updateConsultantStatus(em.Consult_Id, 2);
                            result = true;
                        }
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                return false;
            }
            return result;
        }
    }
}
