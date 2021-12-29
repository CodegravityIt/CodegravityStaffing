using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
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
            List<cg_Consultant> listConsultant = getConsultDetails(0);


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
                        obj.Recruiter_Id = Convert.ToInt32(MarketingDetails.Rows[i]["Assigned_Sales_Recruiter"]);
                        //obj.Consult_Name = Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Assigned_Sales_Recruiter = listemployee.Where(p => p.Emp_Id == Convert.ToInt32(MarketingDetails.Rows[i]["Assigned_Sales_Recruiter"])).FirstOrDefault().Emp_FullName.ToString();
                        obj.Is_Open_To_All = Convert.ToString(MarketingDetails.Rows[i]["Is_Open_To_All"]);
                        obj.Marketing_Start_Date = Convert.ToString(MarketingDetails.Rows[i]["Marketing_Start_Date"]);
                        obj.Visa_Status = Convert.ToString(MarketingDetails.Rows[i]["Visa_Status"]);
                        obj.Marketing_Status = Convert.ToString(MarketingDetails.Rows[i]["Marketing_Status"]);
                        obj.Notes = Convert.ToString(MarketingDetails.Rows[i]["Notes"]);
                        obj.Consult_Name = listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(MarketingDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name.ToString();
                        obj.Marketing_Tech = listTech.Where(p => p.Id == Convert.ToInt32(MarketingDetails.Rows[i]["Marketing_Tech"])).FirstOrDefault().Technology_Name;//dr["Marketing_Tech"] is DBNull ? "" : Convert.ToString(dr["Marketing_Tech"]),


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
        public List<cg_Consultant> getConsultDetails(int ConsultantStatusId)

        {
            DataTable dtConsult = new DataTable();
            List<cg_Consultant> listConsult = new List<cg_Consultant>();
            List<cg_Technology> listtech = getTechnologyDetails();
            List<cg_MarketingStatus> listMarketingStatus = getMarketingstatusmaster();


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
                        dbcommand.Parameters.AddWithValue("@ConsultantStatusId", SqlDbType.Int).Value = ConsultantStatusId;
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
                                       Consult_Technology = listtech.Where(p=>p.Id==Convert.ToInt32(dr["Consult_Technology"])).FirstOrDefault().Technology_Name ,//dr["Consult_Technology"].ToString(),
                                       Consult_VisaStatus = dr["Consult_VisaStatus"].ToString(),
                                       Consult_Status = listMarketingStatus.Where(p=>p.Id==Convert.ToInt32(dr["Consult_Status"])).FirstOrDefault().Status_Name,
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
            List<cg_Consultant> listConsultant = getConsultDetails(0);

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
            List<cg_Technology> listTechnology = new List<cg_Technology>();

            try
            {
                dtTechnology = dynamicTableData("[dbo].[Sp_Gettechnologydetails]");


                if (dtTechnology != null && dtTechnology.Rows.Count > 0)
                {
                    listTechnology = (from DataRow dr in dtTechnology.Rows
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
            return listTechnology;
        }

        public List<cg_MarketingStatus> getMarketingstatusmaster()
        {
            DataTable dtMarketingstatus = new DataTable();
            List<cg_MarketingStatus> listMarketingstatus  = new List<cg_MarketingStatus>();

            try
            {
                dtMarketingstatus = dynamicTableData("[dbo].[Sp_GetMarketingstatusmaster]");


                if (dtMarketingstatus != null && dtMarketingstatus.Rows.Count > 0)
                {
                    listMarketingstatus = (from DataRow dr in dtMarketingstatus.Rows
                                      select new cg_MarketingStatus()
                                      {
                                          Id = Convert.ToInt32(dr["Id"]),
                                          Status_Name = dr["Status_Name"].ToString(),
                                          Status_Description = dr["Status_Description"].ToString(),
                                          Notes = dr["Notes"].ToString(),
                                          active = Convert.ToInt32(dr["active"])

                                      }).ToList();

                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listMarketingstatus;
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
                dtincentivetype = dynamicTableData("[dbo].[Sp_Getincentivemaster]");


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
                        mycommand.Parameters.AddWithValue("@PO_Date", DateTime.ParseExact(em.PO_Date, "MM/dd/yyyy", CultureInfo.InvariantCulture)); 

                        mycommand.Parameters.AddWithValue("@Visa_Type", em.Visa_Type);
                        mycommand.Parameters.AddWithValue("@Project_Start_Date", DateTime.ParseExact(em.Project_Start_Date, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                        mycommand.Parameters.AddWithValue("@Project_Duration", em.Project_Duration + " months");

                        mycommand.Parameters.AddWithValue("@Project_End_Date", DateTime.ParseExact(em.Project_End_Date, "MM/dd/yyyy", CultureInfo.InvariantCulture));
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


                        mycommand.Parameters.AddWithValue("@Bill_Rate", "$" + em.Bill_Rate);
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
                            AddIncentivedetailsFromplacement(em);
                            updateConsultantStatus(em.Consult_id, 3);
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
            List<cg_Consultant> listConsultant = getConsultDetails(0);

            List<cg_Technology> listTech = getTechnologyDetails();
            try
            {
                SubmissionDetails = dynamicTableData("[dbo].[Sp_GetsubmissionsList]");
                if (SubmissionDetails != null && SubmissionDetails.Rows.Count > 0)
                {


                    for (int i = 0; i < SubmissionDetails.Rows.Count; i++)
                    {
                        cg_Submissions obj = new cg_Submissions();
                        obj.Id = SubmissionDetails.Rows[i]["Id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Id"]);
                        obj.Consult_id = SubmissionDetails.Rows[i]["Consult_id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"]);
                        obj.Recruiter_id = SubmissionDetails.Rows[i]["Recruiter_id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"]);
                        obj.NoOfSubmissions = SubmissionDetails.Rows[i]["NumberOfSubmissions"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["NumberOfSubmissions"]);

                        obj.Consult_Name = SubmissionDetails.Rows[i]["Consult_id"] is DBNull ? "" : listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name;//Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Recruiter_Name = SubmissionDetails.Rows[i]["Recruiter_id"] is DBNull ? "" : listemployee.Where(p => p.Emp_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"])).FirstOrDefault().Emp_FullName.ToString();

                        obj.Marketing_Tech = SubmissionDetails.Rows[i]["Marketing_Tech"] is DBNull ? "" : listTech.Where(p => p.Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Marketing_Tech"])).FirstOrDefault().Technology_Name;
                        obj.Vendor_Name = SubmissionDetails.Rows[i]["Vendor_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Name"]);
                        obj.Vendor_POC_Name = SubmissionDetails.Rows[i]["Vendor_POC_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Name"]);
                        obj.Vendor_POC_Email = SubmissionDetails.Rows[i]["Vendor_POC_Email"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Email"]);
                        obj.Vendor_POC_PhoneNumber = SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"]);
                        obj.Vendor_Address = SubmissionDetails.Rows[i]["Vendor_Address"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Address"]);
                        obj.End_Client_Name = SubmissionDetails.Rows[i]["End_Client_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Name"]);
                        obj.End_Client_POC_Name = SubmissionDetails.Rows[i]["End_Client_POC_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Name"]);
                        obj.End_Client_POC_Email = SubmissionDetails.Rows[i]["End_Client_POC_Email"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Email"]);
                        obj.End_Client_POC_PhoneNumber = SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"]);
                        obj.End_Client_Address = SubmissionDetails.Rows[i]["End_Client_Address"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Address"]);
                        obj.Rate_confirmation = SubmissionDetails.Rows[i]["Rate_confirmation"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Rate_confirmation"]);
                        obj.Bill_Rate = SubmissionDetails.Rows[i]["Bill_Rate"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Bill_Rate"]);
                        obj.Assignment_date = SubmissionDetails.Rows[i]["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Assignment_date"]).ToShortDateString();
                        obj.Assignment_done_by = SubmissionDetails.Rows[i]["Assignment_done_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Assignment_done_by"]);
                        obj.Assignment_status = SubmissionDetails.Rows[i]["Assignment_status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Assignment_status"]);
                        obj.Interview_Schedudule_Date = SubmissionDetails.Rows[i]["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Interview_Schedudule_Date"]).ToShortDateString();
                        obj.Interview_Status = SubmissionDetails.Rows[i]["Interview_Status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Interview_Status"]);
                        obj.Created_date = SubmissionDetails.Rows[i]["Created_date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Created_date"]).ToShortDateString();
                        obj.Created_by = SubmissionDetails.Rows[i]["Created_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Created_by"]);
                        obj.Modified_Date = SubmissionDetails.Rows[i]["Modified_Date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Modified_Date"]).ToShortDateString();
                        obj.Modified_by = SubmissionDetails.Rows[i]["Modified_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Modified_by"]);
                        obj.Notes = SubmissionDetails.Rows[i]["Notes"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Notes"]);
                        obj.submission_status = SubmissionDetails.Rows[i]["submission_status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["submission_status"]);


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
            List<cg_Consultant> listConsultant = getConsultDetails(0);

            List<cg_Technology> listTech = getTechnologyDetails();
            try
            {
                SubmissionDetails = dynamicTableData("[dbo].[Sp_Getsubmissiondetailedinfo]");
                if (SubmissionDetails != null && SubmissionDetails.Rows.Count > 0)
                {


                    for (int i = 0; i < SubmissionDetails.Rows.Count; i++)
                    {
                        cg_Submissions obj = new cg_Submissions();
                        obj.Id = SubmissionDetails.Rows[i]["Id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Id"]);
                        obj.Consult_id = SubmissionDetails.Rows[i]["Consult_id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"]);
                        obj.Recruiter_id = SubmissionDetails.Rows[i]["Recruiter_id"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"]);
                        //obj.NoOfSubmissions = SubmissionDetails.Rows[i]["NumberOfSubmissions"] is DBNull ? 0 : Convert.ToInt32(SubmissionDetails.Rows[i]["NumberOfSubmissions"]);

                        obj.Consult_Name = SubmissionDetails.Rows[i]["Consult_id"] is DBNull ? "" : listConsultant.Where(p => p.Consult_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Consult_id"])).FirstOrDefault().Consult_Full_Name;//Convert.ToString(MarketingDetails.Rows[i]["Consult_Name"]);
                        obj.Recruiter_Name = SubmissionDetails.Rows[i]["Recruiter_id"] is DBNull ? "" : listemployee.Where(p => p.Emp_Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Recruiter_id"])).FirstOrDefault().Emp_FullName.ToString();

                        obj.Marketing_Tech = SubmissionDetails.Rows[i]["Marketing_Tech"] is DBNull ? "" : listTech.Where(p => p.Id == Convert.ToInt32(SubmissionDetails.Rows[i]["Marketing_Tech"])).FirstOrDefault().Technology_Name;
                        obj.Vendor_Name = SubmissionDetails.Rows[i]["Vendor_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Name"]);
                        obj.Vendor_POC_Name = SubmissionDetails.Rows[i]["Vendor_POC_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Name"]);
                        obj.Vendor_POC_Email = SubmissionDetails.Rows[i]["Vendor_POC_Email"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_Email"]);
                        obj.Vendor_POC_PhoneNumber = SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_POC_PhoneNumber"]);
                        obj.Vendor_Address = SubmissionDetails.Rows[i]["Vendor_Address"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Vendor_Address"]);
                        obj.End_Client_Name = SubmissionDetails.Rows[i]["End_Client_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Name"]);
                        obj.End_Client_POC_Name = SubmissionDetails.Rows[i]["End_Client_POC_Name"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Name"]);
                        obj.End_Client_POC_Email = SubmissionDetails.Rows[i]["End_Client_POC_Email"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_Email"]);
                        obj.End_Client_POC_PhoneNumber = SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_POC_PhoneNumber"]);
                        obj.End_Client_Address = SubmissionDetails.Rows[i]["End_Client_Address"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["End_Client_Address"]);
                        obj.Rate_confirmation = SubmissionDetails.Rows[i]["Rate_confirmation"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Rate_confirmation"]);
                        obj.Bill_Rate = SubmissionDetails.Rows[i]["Bill_Rate"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Bill_Rate"]);
                        obj.Assignment_date = SubmissionDetails.Rows[i]["Assignment_date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Assignment_date"]).ToShortDateString();
                        obj.Assignment_done_by = SubmissionDetails.Rows[i]["Assignment_done_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Assignment_done_by"]);
                        obj.Assignment_status = SubmissionDetails.Rows[i]["Assignment_status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Assignment_status"]);
                        obj.Interview_Schedudule_Date = SubmissionDetails.Rows[i]["Interview_Schedudule_Date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Interview_Schedudule_Date"]).ToShortDateString();
                        obj.Interview_Status = SubmissionDetails.Rows[i]["Interview_Status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Interview_Status"]);
                        obj.Created_date = SubmissionDetails.Rows[i]["Created_date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Created_date"]).ToShortDateString();
                        obj.Created_by = SubmissionDetails.Rows[i]["Created_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Created_by"]);
                        obj.Modified_Date = SubmissionDetails.Rows[i]["Modified_Date"] is DBNull ? "" : Convert.ToDateTime(SubmissionDetails.Rows[i]["Modified_Date"]).ToShortDateString();
                        obj.Modified_by = SubmissionDetails.Rows[i]["Modified_by"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Modified_by"]);
                        obj.Notes = SubmissionDetails.Rows[i]["Notes"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["Notes"]);
                        obj.submission_status = SubmissionDetails.Rows[i]["submission_status"] is DBNull ? "" : Convert.ToString(SubmissionDetails.Rows[i]["submission_status"]);



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
                String query = "INSERT INTO [CG].[Consult_Marketing_Submission] (Consult_id,Recruiter_id,Marketing_Tech,Vendor_Name,Vendor_POC_Name,Vendor_POC_Email,Vendor_POC_PhoneNumber,Vendor_Address," +

                    "End_Client_Name,End_Client_POC_Name,End_Client_POC_Email,End_Client_POC_PhoneNumber,End_Client_Address,Rate_confirmation,Bill_Rate,Assignment_date,Assignment_status,Assignment_done_by," +
                    "Interview_Schedudule_Date,Interview_Status,submission_status,Created_by,Modified_by,Notes)" +
                    " VALUES " +
                    "(@Consult_id,@Recruiter_id,@Marketing_Tech,@Vendor_Name,@Vendor_POC_Name,@Vendor_POC_Email,@Vendor_POC_PhoneNumber,@Vendor_Address,@End_Client_Name,@End_Client_POC_Name,@End_Client_POC_Email,@End_Client_POC_PhoneNumber," +
                    "@End_Client_Address,@Rate_confirmation,@Bill_Rate,@Assignment_date,@Assignment_status,@Assignment_done_by,@Interview_Schedudule_Date,@Interview_Status,@submission_status,@Created_by," +
                    "@Modified_by,@Notes)";

                DataTable table = new DataTable();
                em.Created_date = DateTime.Now.ToShortDateString();
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

                        mycommand.Parameters.AddWithValue("@Vendor_Address", em.Vendor_Address);
                        mycommand.Parameters.AddWithValue("@End_Client_Name", em.End_Client_Name);


                        mycommand.Parameters.AddWithValue("@End_Client_POC_Name", em.End_Client_POC_Name);
                        mycommand.Parameters.AddWithValue("@End_Client_POC_Email", em.End_Client_POC_Email);
                        mycommand.Parameters.AddWithValue("@End_Client_POC_PhoneNumber", em.End_Client_POC_PhoneNumber);



                        mycommand.Parameters.AddWithValue("@End_Client_Address", em.End_Client_Address);
                        mycommand.Parameters.AddWithValue("@Rate_confirmation", em.Rate_confirmation);


                        mycommand.Parameters.AddWithValue("@Bill_Rate", "$" + em.Bill_Rate);
                        mycommand.Parameters.AddWithValue("@Assignment_date", DateTime.ParseExact(em.Assignment_date, "MM/dd/yyyy", CultureInfo.InvariantCulture));
                        mycommand.Parameters.AddWithValue("@Assignment_status", em.Assignment_status);
                        mycommand.Parameters.AddWithValue("@Assignment_done_by", em.Assignment_done_by);

                        ;

                        mycommand.Parameters.AddWithValue("@Interview_Schedudule_Date", DateTime.ParseExact(em.Interview_Schedudule_Date, "MM/dd/yyyy", CultureInfo.InvariantCulture));

                        mycommand.Parameters.AddWithValue("@Interview_Status", em.Interview_Status);
                        mycommand.Parameters.AddWithValue("@submission_status", em.submission_status);

                        mycommand.Parameters.AddWithValue("@Created_Date", em.Created_date);
                        mycommand.Parameters.AddWithValue("@Created_by", em.Recruiter_id);

                        mycommand.Parameters.AddWithValue("@Modified_Date", DateTime.Now.ToString());
                        mycommand.Parameters.AddWithValue("@Modified_by", em.Recruiter_id);
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


        public bool AddIncentivedetailsFromApi(cg_Incentivedetils em)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;


            try
            {
                String query = "INSERT INTO [CG].[[EmployeeIncentiveDetails]] (Consultant_Id,Recruiter_Id,Project_Start_Date,IncentiveType,Term1_IncentivePeriod,Term1_IncentivepayableDate,Term1_IncentiveAmount,Is_Term1_IncentivePaid,Term2_IncentivePeriod," +
                    "Term2_IncentivePayableDate,Term2_IncentiveAmount,Is_Term2_IncentivePaid,Term3_IncentivePeriod,Term3_IncentivePayableDate,Term3_IncentiveAmount," +
                    "Is_Term3_IncentivePaid,Term4_IncentivePeriod,Term4_IncentivePayableDate,Term4_IncentiveAmount,Is_Term4_IncentivePaid,Comments,Notes1,Notes2,Created_Date,Created_by,Modified_Date,Modified_by,Incentive_Status)" +
                    " VALUES " +
                    "(@Consultant_Id,@Recruiter_Id,@Project_Start_Date,@IncentiveType,@Term1_IncentivePeriod,@Term1_IncentivepayableDate,@Term1_IncentiveAmount,@Is_Term1_IncentivePaid,@Term2_IncentivePeriod,@Term2_IncentivePayableDate,@Term2_IncentiveAmount,@Is_Term2_IncentivePaid," +
                    "@Term3_IncentivePeriod,@Term3_IncentivePayableDate,@Term3_IncentiveAmount,@Is_Term3_IncentivePaid,@Term4_IncentivePeriod,@Term4_IncentivePayableDate," +
                    "@Term4_IncentiveAmount,@Is_Term4_IncentivePaid,@Comments,@Notes1,@Notes2,@Placement_Status,@Created_Date,@Created_by,@Modified_Date,@Modified_by,@Incentive_Status" +
                    ")";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consult_id", em.Consultant_Id);
                        mycommand.Parameters.AddWithValue("@Recruiter_Id", em.Recruiter_Id);
                        mycommand.Parameters.AddWithValue("@Project_Start_Date", em.Project_Start_Date);
                        mycommand.Parameters.AddWithValue("@IncentiveType", em.IncentiveType);

                        mycommand.Parameters.AddWithValue("@Term1_IncentivePeriod", em.Term1_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term1_IncentivepayableDate", Convert.ToDateTime(em.Term1_IncentivepayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term1_IncentiveAmount", Convert.ToDecimal(em.Term1_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term1_IncentivePaid", em.Is_Term1_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term2_IncentivePeriod", em.Term2_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term2_IncentivePayableDate", Convert.ToDateTime(em.Term2_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term2_IncentiveAmount", Convert.ToDecimal(em.Term2_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term2_IncentivePaid", em.Is_Term2_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term3_IncentivePeriod", em.Term3_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term3_IncentivePayableDate", Convert.ToDateTime(em.Term3_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term3_IncentiveAmount", Convert.ToDecimal(em.Term3_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term3_IncentivePaid", em.Is_Term3_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term4_IncentivePeriod", em.Term4_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term4_IncentivePayableDate", Convert.ToDateTime(em.Term4_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term4_IncentiveAmount", Convert.ToDecimal(em.Term4_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term4_IncentivePaid", em.Is_Term4_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Comments", em.Comments);
                        mycommand.Parameters.AddWithValue("@Notes1", em.Notes1);
                        mycommand.Parameters.AddWithValue("@Notes2", em.Notes2);
                        mycommand.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Created_by", em.Created_by);


                        mycommand.Parameters.AddWithValue("@Modified_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Modified_by", em.Modified_by);
                        mycommand.Parameters.AddWithValue("@Incentive_Status", em.Incentive_Status);

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

        public bool AddIncentivedetailsFromplacement(cg_Placement placement)
        {
            DataTable dtRecruiters = new DataTable();
            bool result = false;
            List<cg_Employees> listemp = getEmployeeDetails();
            int employeIncentivetype = listemp.Where(p => p.Emp_Id == Convert.ToInt32(placement.Placed_Sales_Recruiter)).FirstOrDefault().Emp_IncentiveType;
            cg_Incentivedetils CalculatedIncentives = calculateIncetives(placement.Project_Start_Date, employeIncentivetype);

            cg_Incentivedetils em = new cg_Incentivedetils();
            try
            {
                String query = "INSERT INTO [CG].[[EmployeeIncentiveDetails]] (Consultant_Id,Recruiter_Id,Project_Start_Date,IncentiveType,Term1_IncentivePeriod,Term1_IncentivepayableDate,Term1_IncentiveAmount,Is_Term1_IncentivePaid,Term2_IncentivePeriod," +
                    "Term2_IncentivePayableDate,Term2_IncentiveAmount,Is_Term2_IncentivePaid,Term3_IncentivePeriod,Term3_IncentivePayableDate,Term3_IncentiveAmount," +
                    "Is_Term3_IncentivePaid,Term4_IncentivePeriod,Term4_IncentivePayableDate,Term4_IncentiveAmount,Is_Term4_IncentivePaid,Comments,Notes1,Notes2,Created_Date,Created_by,Modified_Date,Modified_by,Incentive_Status)" +
                    " VALUES " +
                    "(@Consultant_Id,@Recruiter_Id,@Project_Start_Date,@IncentiveType,@Term1_IncentivePeriod,@Term1_IncentivepayableDate,@Term1_IncentiveAmount,@Is_Term1_IncentivePaid,@Term2_IncentivePeriod,@Term2_IncentivePayableDate,@Term2_IncentiveAmount,@Is_Term2_IncentivePaid," +
                    "@Term3_IncentivePeriod,@Term3_IncentivePayableDate,@Term3_IncentiveAmount,@Is_Term3_IncentivePaid,@Term4_IncentivePeriod,@Term4_IncentivePayableDate," +
                    "@Term4_IncentiveAmount,@Is_Term4_IncentivePaid,@Comments,@Notes1,@Notes2,@Created_Date,@Created_by,@Modified_Date,@Modified_by,@Incentive_Status" +
                    ")";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    using (SqlCommand mycommand = new SqlCommand(query, mycon))
                    {
                        mycommand.Parameters.AddWithValue("@Consultant_Id", placement.Consult_id);
                        mycommand.Parameters.AddWithValue("@Recruiter_Id", placement.Placed_Sales_Recruiter);
                        mycommand.Parameters.AddWithValue("@Project_Start_Date", placement.Project_Start_Date);
                        mycommand.Parameters.AddWithValue("@IncentiveType", employeIncentivetype);


                        mycommand.Parameters.AddWithValue("@Term1_IncentivePeriod", CalculatedIncentives.Term1_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term1_IncentivepayableDate", Convert.ToDateTime(CalculatedIncentives.Term1_IncentivepayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term1_IncentiveAmount", Convert.ToDecimal(CalculatedIncentives.Term1_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term1_IncentivePaid", CalculatedIncentives.Is_Term1_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term2_IncentivePeriod", CalculatedIncentives.Term2_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term2_IncentivePayableDate", Convert.ToDateTime(CalculatedIncentives.Term2_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term2_IncentiveAmount", Convert.ToDecimal(CalculatedIncentives.Term2_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term2_IncentivePaid", CalculatedIncentives.Is_Term2_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term3_IncentivePeriod", CalculatedIncentives.Term3_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term3_IncentivePayableDate", Convert.ToDateTime(CalculatedIncentives.Term3_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term3_IncentiveAmount", Convert.ToDecimal(CalculatedIncentives.Term3_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term3_IncentivePaid", CalculatedIncentives.Is_Term3_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Term4_IncentivePeriod", CalculatedIncentives.Term4_IncentivePeriod);
                        mycommand.Parameters.AddWithValue("@Term4_IncentivePayableDate", Convert.ToDateTime(CalculatedIncentives.Term4_IncentivePayableDate).ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Term4_IncentiveAmount", Convert.ToDecimal(CalculatedIncentives.Term4_IncentiveAmount));
                        mycommand.Parameters.AddWithValue("@Is_Term4_IncentivePaid", CalculatedIncentives.Is_Term4_IncentivePaid);


                        mycommand.Parameters.AddWithValue("@Comments", "''");
                        mycommand.Parameters.AddWithValue("@Notes1", placement.Notes);
                        mycommand.Parameters.AddWithValue("@Notes2", "''");
                        mycommand.Parameters.AddWithValue("@Created_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Created_by", em.Created_by);


                        mycommand.Parameters.AddWithValue("@Modified_Date", DateTime.Now.ToString("MM/dd/yyyy"));
                        mycommand.Parameters.AddWithValue("@Modified_by", em.Modified_by);
                        mycommand.Parameters.AddWithValue("@Incentive_Status", "1");

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

        public cg_Incentivedetils calculateIncetives(string Project_Start_Date, int Incentivetype)
        {
            cg_Incentivedetils objIncentives = new cg_Incentivedetils();
            List<cg_Incentives> incentiveTypeList = getIncentivetypeDetails();

            try
            {
                DateTime dtprojstartdate = DateTime.ParseExact(Project_Start_Date, "MM/dd/yyyy", CultureInfo.InvariantCulture);


                DateTime term1 = dtprojstartdate.AddMonths(3);
                string Term1_IncentivePeriod = String.Format("{0: d MMMM  yyyy}", dtprojstartdate) + "--" + String.Format("{0: d MMMM  yyyy}", term1);
                DateTime Term1_Incentivemonth = term1.AddMonths(1).Date;
                string Term1_IncentivepayableDate = new DateTime(Term1_Incentivemonth.Year, Term1_Incentivemonth.Month, 1).ToString("MM/dd/yyyy");

                objIncentives.Term1_IncentiveAmount = incentiveTypeList.Where(p => p.Incentive_Id == Incentivetype).FirstOrDefault().Incentive_Amount;
                objIncentives.Term1_IncentivepayableDate = Term1_IncentivepayableDate;
                objIncentives.Term1_IncentivePeriod = Term1_IncentivePeriod;
                objIncentives.Is_Term1_IncentivePaid = 0;


                DateTime term2 = dtprojstartdate.AddMonths(6);
                string Term2_IncentivePeriod = String.Format("{0: d MMMM  yyyy}", term1) + "--" + String.Format("{0: d MMMM  yyyy}", term2);
                DateTime Term2_Incentivemonth = term2.AddMonths(1).Date;
                string Term2_IncentivepayableDate = new DateTime(Term2_Incentivemonth.Year, Term2_Incentivemonth.Month, 1).ToString("MM/dd/yyyy");

                objIncentives.Term2_IncentiveAmount = incentiveTypeList.Where(p => p.Incentive_Id == Incentivetype).FirstOrDefault().Incentive_Amount;
                objIncentives.Term2_IncentivePayableDate = Term2_IncentivepayableDate;
                objIncentives.Term2_IncentivePeriod = Term2_IncentivePeriod;
                objIncentives.Is_Term2_IncentivePaid = 0;


                DateTime term3 = dtprojstartdate.AddMonths(9);
                string Term3_IncentivePeriod = String.Format("{0: d MMMM  yyyy}", term2) + "--" + String.Format("{0: d MMMM  yyyy}", term3);
                DateTime Term3_Incentivemonth = term3.AddMonths(1).Date;
                string Term3_IncentivepayableDate = new DateTime(Term3_Incentivemonth.Year, Term3_Incentivemonth.Month, 1).ToString("MM/dd/yyyy");

                objIncentives.Term3_IncentiveAmount = incentiveTypeList.Where(p => p.Incentive_Id == Incentivetype).FirstOrDefault().Incentive_Amount;
                objIncentives.Term3_IncentivePayableDate = Term3_IncentivepayableDate;
                objIncentives.Term3_IncentivePeriod = Term3_IncentivePeriod;
                objIncentives.Is_Term3_IncentivePaid = 0;




                DateTime term4 = dtprojstartdate.AddMonths(12);

                string Term4_IncentivePeriod = String.Format("{0: d MMMM  yyyy}", term3) + "--" + String.Format("{0: d MMMM  yyyy}", term4);
                DateTime Term4_Incentivemonth = term4.AddMonths(1).Date;
                string Term4_IncentivepayableDate = new DateTime(Term4_Incentivemonth.Year, Term4_Incentivemonth.Month, 1).ToString("MM/dd/yyyy");

                objIncentives.Term4_IncentiveAmount = incentiveTypeList.Where(p => p.Incentive_Id == Incentivetype).FirstOrDefault().Incentive_Amount;
                objIncentives.Term4_IncentivePayableDate = Term4_IncentivepayableDate;
                objIncentives.Term4_IncentivePeriod = Term4_IncentivePeriod;
                objIncentives.Is_Term4_IncentivePaid = 0;
            }
            catch (Exception ex)
            {

                //throw;
            }

            return objIncentives;

        }


        public List<cg_Incentivedetils> getIncentivedetails()
        {
            DataTable dtincentivedetails = new DataTable();

            List<cg_Incentivedetils> listIncentivesdetails = new List<cg_Incentivedetils>();
            List<cg_Employees> listemp = getEmployeeDetails();
            List<cg_Consultant> listconsultant = getConsultDetails(0);


            try
            {
                dtincentivedetails = dynamicTableData("[dbo].[Sp_Getincentivedetails]");


                if (dtincentivedetails != null && dtincentivedetails.Rows.Count > 0)
                {

                    listIncentivesdetails = (from DataRow dr in dtincentivedetails.Rows
                                             select new cg_Incentivedetils()
                                             {
                                                 Id = dr["Id"] is DBNull ? 0 : Convert.ToInt32(dr["Id"]),
                                                 Consultant_Id = dr["Consultant_Id"] is DBNull ? 0 : Convert.ToInt32(dr["Consultant_Id"]),
                                                 Consultant_Name = listconsultant.Where(p => p.Consult_Id == Convert.ToInt32(dr["Consultant_Id"])).FirstOrDefault().Consult_Full_Name,
                                                 Recruiter_Id = dr["Recruiter_Id"] is DBNull ? 0 : Convert.ToInt32(dr["Recruiter_Id"]),
                                                 Recruiter_Name = listemp.Where(p => p.Emp_Id == Convert.ToInt32(dr["Recruiter_Id"])).FirstOrDefault().Emp_FullName,
                                                 IncentiveType = dr["IncentiveType"] is DBNull ? "" : Convert.ToString(dr["IncentiveType"]),
                                                 Project_Start_Date = dr["Project_Start_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Project_Start_Date"]).ToString("MM/dd/yyyy"),

                                                 Term1_IncentivePeriod = dr["Term1_IncentivePeriod"] is DBNull ? "" : Convert.ToString(dr["Term1_IncentivePeriod"]),
                                                 Term1_IncentivepayableDate = dr["Term1_IncentivepayableDate"] is DBNull ? "" : Convert.ToDateTime(dr["Term1_IncentivepayableDate"]).ToString("MM/dd/yyyy"),
                                                 Term1_IncentiveAmount = dr["Term1_IncentiveAmount"] is DBNull ? "" : Convert.ToString(dr["Term1_IncentiveAmount"]),
                                                 Is_Term1_IncentivePaid = dr["Is_Term1_IncentivePaid"] is DBNull ? 0 : Convert.ToInt32(dr["Is_Term1_IncentivePaid"]),

                                                 Term2_IncentivePeriod = dr["Term2_IncentivePeriod"] is DBNull ? "" : Convert.ToString(dr["Term2_IncentivePeriod"]),
                                                 Term2_IncentivePayableDate = dr["Term2_IncentivePayableDate"] is DBNull ? "" : Convert.ToDateTime(dr["Term2_IncentivePayableDate"]).ToString("MM/dd/yyyy"),
                                                 Term2_IncentiveAmount = dr["Term2_IncentiveAmount"] is DBNull ? "" : Convert.ToString(dr["Term2_IncentiveAmount"]),
                                                 Is_Term2_IncentivePaid = dr["Is_Term2_IncentivePaid"] is DBNull ? 0 : Convert.ToInt32(dr["Is_Term2_IncentivePaid"]),

                                                 Term3_IncentivePeriod = dr["Term3_IncentivePeriod"] is DBNull ? "" : Convert.ToString(dr["Term3_IncentivePeriod"]),
                                                 Term3_IncentivePayableDate = dr["Term3_IncentivePayableDate"] is DBNull ? "" : Convert.ToDateTime(dr["Term3_IncentivePayableDate"]).ToString("MM/dd/yyyy"),
                                                 Term3_IncentiveAmount = dr["Term3_IncentiveAmount"] is DBNull ? "" : Convert.ToString(dr["Term3_IncentiveAmount"]),
                                                 Is_Term3_IncentivePaid = dr["Is_Term3_IncentivePaid"] is DBNull ? 0 : Convert.ToInt32(dr["Is_Term3_IncentivePaid"]),

                                                 Term4_IncentivePeriod = dr["Term4_IncentivePeriod"] is DBNull ? "" : Convert.ToString(dr["Term4_IncentivePeriod"]),
                                                 Term4_IncentivePayableDate = dr["Term4_IncentivePayableDate"] is DBNull ? "" : Convert.ToDateTime(dr["Term4_IncentivePayableDate"]).ToString("MM/dd/yyyy"),
                                                 Term4_IncentiveAmount = dr["Term4_IncentiveAmount"] is DBNull ? "" : Convert.ToString(dr["Term4_IncentiveAmount"]).ToString(),
                                                 Is_Term4_IncentivePaid = dr["Is_Term4_IncentivePaid"] is DBNull ? 0 : Convert.ToInt32(dr["Is_Term4_IncentivePaid"]),

                                                 Comments = dr["Comments"] is DBNull ? "" : Convert.ToString(dr["Comments"]),
                                                 Notes1 = dr["Notes1"] is DBNull ? "" : Convert.ToString(dr["Notes1"]),
                                                 Notes2 = dr["Notes2"] is DBNull ? "" : Convert.ToString(dr["Notes2"]),

                                                 Created_Date = dr["Created_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Created_Date"]).ToString("MM/dd/yyyy"),
                                                 Created_by = dr["Created_by"] is DBNull ? "" : Convert.ToString(dr["Created_by"]),
                                                 Modified_Date = dr["Modified_Date"] is DBNull ? "" : Convert.ToDateTime(dr["Modified_Date"]).ToString("MM/dd/yyyy"),
                                                 Modified_by = dr["Modified_by"] is DBNull ? "" : Convert.ToString(dr["Modified_by"]),

                                                 Incentive_Status = dr["Incentive_Status"] is DBNull ? "" : Convert.ToString(dr["Incentive_Status"])

                                             }).ToList();


                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return listIncentivesdetails;
        }

    }
}
