using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebApi_New.Models;
namespace WebApi_New.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeMasterController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        EmployeeMaster ObjEM;
        public EmployeeMasterController(IConfiguration configuration)
        {
            _configuration = configuration;
            ObjEM = new EmployeeMaster(_configuration);
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {

            return new JsonResult(ObjEM.GetEmployeeDetails());

        }
        [HttpPost]
        public JsonResult post(cg_Employees em)
        {
//            string Query = @"insert into [CG].[EmployeeMaster] values(
//'" + em.Emp_FirstName + @"',
//'" + em.Emp_LastName + @"',

//'" + em.Emp_Email + @"',
//'" + em.Emp_Phone + @"',
//'" + em.Emp_work_Region + @"',
//'" + em.Emp_IncentiveType + @"',
//'" + em.Emp_Status + @"',
//'" + em.Role_Id + @"'

//)";

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
                    table.Load(myReader);
                    myReader.Close();
                    mycon.Close();
                }
            }
            return new JsonResult("Added Succfully");
        }
        [HttpPut]
        public JsonResult put(cg_Employees em)
        {
            string Query = @"update [CG].[EmployeeMaster] set
       [Emp_LastName]='" + em.Emp_LastName + @"'
       [Emp_FirstName]='" + em.Emp_FirstName + @"'

      ,[Emp_Email]='" + em.Emp_Email + @"'
      ,[Emp_Phone]='" + em.Emp_Phone + @"'
      ,[Emp_work_Region]=" + em.Emp_work_Region + @"
      ,[Emp_IncentiveType]=" + em.Emp_IncentiveType + @"
      ,[Emp_Status]=" + em.Emp_Status + @"
      ,[Role_Id]=" + em.Role_Id + @" where 
       [Emp_Id]=" + em.Emp_Id + @" 
";

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
            return new JsonResult("updated Succfully");
        }

        [HttpDelete("{empId}")]
        public JsonResult delete(int empId)
        {
            string Query = @"delete from  [CG].[EmployeeMaster] where [Emp_Id]=" + empId + @" ";

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
            return new JsonResult("Deleted Succfully");
        }

    }
}
