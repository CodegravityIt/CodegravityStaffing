using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace WebApi_New.Models
{

    public class ConsultantMaster
    {
        public readonly IConfiguration _configuration;

        public ConsultantMaster(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public int Consult_Id { get; set; }
        public string Consult_Name { get; set; }
        public string Consult_Email { get; set; }
        public string Consult_Phone { get; set; }
        public string Consult_Address { get; set; }
        public string Consult_Technology { get; set; }
        public string Consult_VisaStatus { get; set; }
        public int Consult_Status { get; set; }

        public DataTable GetConsultDetails()
        {
            DataTable dtConsult = new DataTable();

            try
            {

                string Query = @"SELECT *  FROM [CodeGravity].[CG].[ConsultantMaster]";

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
        public DataTable AddConsultDetails(ConsultantMaster Objcm)
        {
            DataTable dtConsult = new DataTable();

            try
            {

                string Query = @"insert into [CG].[ConsultantMaster] values(
                        '" + Objcm.Consult_Name + @"',
                        '" + Objcm.Consult_Email + @"',
                        '" + Objcm.Consult_Phone + @"',
                        '" + Objcm.Consult_Address + @"',
                        '" + Objcm.Consult_Technology + @"',
                        '" + Objcm.Consult_VisaStatus + @"',
                        " + Objcm.Consult_Status + @"
                         )";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    mycon.Open();
                    using (SqlCommand mycommand = new SqlCommand(Query, mycon))
                    {
                        myReader = mycommand.ExecuteReader();
                        table.Load(myReader);
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

        public DataTable DeleteConsultDetails(int ConsultId)
        {
            DataTable dtConsult = new DataTable();

            try
            {

                string Query = @"delete from  [CG].[ConsultantMaster] where [Consult_Id]=" + ConsultId + @" ";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    mycon.Open();
                    using (SqlCommand mycommand = new SqlCommand(Query, mycon))
                    {
                        myReader = mycommand.ExecuteReader();
                        table.Load(myReader);
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
        public DataTable updateConsultDetails(ConsultantMaster objCm)
        {
            DataTable dtConsult = new DataTable();

            try
            {
                string Query = @"update [CG].[ConsultantMaster] set
                               [Consult_Name]='" + objCm.Consult_Name + @"'
                              ,[Consult_Email]='" + objCm.Consult_Email + @"'
                              ,[Consult_Phone]='" + objCm.Consult_Phone + @"'
                              ,[Consult_Address]='" + objCm.Consult_Address + @"'
                              ,[Consult_Technology]='" + objCm.Consult_Technology + @"'
                              ,[Consult_VisaStatus]='" + objCm.Consult_VisaStatus + @"'
                              ,[Consult_Status] ='" + objCm.Consult_Status + @"'where 
                               [Consult_Id]=" + objCm.Consult_Id + @" ";

                DataTable table = new DataTable();
                string SQlDatasource = _configuration.GetConnectionString("CodeGravityDB");
                SqlDataReader myReader;
                using (SqlConnection mycon = new SqlConnection(SQlDatasource))
                {
                    mycon.Open();
                    using (SqlCommand mycommand = new SqlCommand(Query, mycon))
                    {
                        myReader = mycommand.ExecuteReader();
                        table.Load(myReader);
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
