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

        public MarketingMaster(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Id { get; set; }
        public int Consult_Id { get; set; }

        public string Assigned_Sales_Recruiter { get; set; }
        public string Marketing_Tech { get; set; }
        public string Is_Open_To_All { get; set; }
        public DateTime Marketing_Start_Date { get; set; }
        public string Submitted_Vendor { get; set; }
        public string End_Client_Name { get; set; }
        public string Rate_confirmation { get; set; }
        public string Bill_Rate { get; set; }
        public DateTime Assignment_date { get; set; }
        public string Assignment_status { get; set; }
        public DateTime Interview_Schedudule_Date { get; set; }
        public string Interview_Status { get; set; }
        public string Visa_Status { get; set; }
        public string Marketing_Status { get; set; }

        public DateTime Marketing_End_Date { get; set; }
        public string Notes { get; set; }

        public DataTable GetConsultDetails()
        {
            DataTable dtConsult = new DataTable();

            try
            {

                string Query = @"SELECT *  FROM [CodeGravity].[CG].[Consultant_Marketing]";

                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    mycon.Open();
                    using (SqlCommand mycommand = new SqlCommand(Query, mycon))
                    {
                        myReader = mycommand.ExecuteReader();
                        dtConsult.Load(myReader);
                        myReader.Close();
                        mycon.Close();
                    }
                }
            }
            catch (Exception ex)
            {

                return null;
            }
            return dtConsult;
        }




    }
}
