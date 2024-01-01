using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;

namespace UMS.Areas.Student.Controllers
{
    [Area("Student")]
    [Route("Student/[controller]/[action]")]
    public class StudentController : Controller
    {
        public readonly IConfiguration _configuration;
        public StudentController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult StudentList()
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Student_SelectAll";
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
    }
}
