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
        Codegravity_Staffing_DAL objGG_Dal;
        public ConsultantMaster(IConfiguration configuration)
        {
            _configuration = configuration;
            objGG_Dal = new Codegravity_Staffing_DAL(_configuration);
        }
       

        public List<cg_Consultant> GetConsultDetails( )
        {
            List<cg_Consultant> listConsult = new List<cg_Consultant>();

            try
            {
                listConsult = objGG_Dal.getConsultDetails();
            }
            catch (Exception ex)
            {
            }
            return listConsult;

        }
        public DataTable AddConsultDetails(cg_Consultant Objcm)
        {
            DataTable dtConsult = new DataTable();

            try
            {

                string Query = @"insert into [CG].[ConsultantMaster] values(
                        '" + Objcm.Consult_First_Name + @"',
                        '" + Objcm.Consult_Email + @"',
                        '" + Objcm.Consult_Phone + @"',
                        '" + Objcm.Consult_Address + @"',
                        '" + Objcm.Consult_Technology + @"',
                        '" + Objcm.Consult_VisaStatus + @"',
                        " + Objcm.Consult_Status + @",
                        '" + Objcm.Consult_Last_Name + @"',
                        '" + Objcm.Consult_DOB + @"'
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
        public DataTable updateConsultDetails(cg_Consultant objCm)
        {
            DataTable dtConsult = new DataTable();

            try
            {
                string Query = @"update [CG].[ConsultantMaster] set
                               [Consult_First_Name]='" + objCm.Consult_First_Name + @"'
                              ,[Consult_Email]='" + objCm.Consult_Email + @"'
                              ,[Consult_Phone]='" + objCm.Consult_Phone + @"'
                              ,[Consult_Address]='" + objCm.Consult_Address + @"'
                              ,[Consult_Technology]='" + objCm.Consult_Technology + @"'
                              ,[Consult_VisaStatus]='" + objCm.Consult_VisaStatus + @"'
                              ,[Consult_Status] ='" + objCm.Consult_Status + @"'
                              ,[Consult_Status] ='" + objCm.Consult_Last_Name + @"'
                              ,[Consult_Status] ='" + objCm.Consult_DOB + @"'
                               where [Consult_Id]=" + objCm.Consult_Id + @" ";

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
