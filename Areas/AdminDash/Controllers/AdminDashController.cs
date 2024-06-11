using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace UMS.Areas.AdminDash.Controllers
{
    [Area("AdminDash")]
    [Route("AdminDash/[controller]/[action]")]
    public class AdminDashController : Controller
    {
        public readonly IConfiguration _configuration;
        public AdminDashController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AdminDash()
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Dash_Count";
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
    }
}
