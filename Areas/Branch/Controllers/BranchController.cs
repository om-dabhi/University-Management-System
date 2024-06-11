using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using UMS.Areas.Branch.Models;
using UMS.Areas.Branch.Models;
using UMS.Areas.Student.Models;

namespace UMS.Areas.Branch.Controllers
{
    [Area("Branch")]
    [Route("Branch/[controller]/[action]")]
    public class BranchController : Controller
    {
        public readonly IConfiguration _configuration;
        public BranchController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult BranchList()
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand Objcmd = sqlConnection.CreateCommand();
            Objcmd.CommandType = CommandType.StoredProcedure;
            Objcmd.CommandText = "PR_Branch_SelectAll";
            SqlDataReader sqlDataReader = Objcmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt); 
        }
        public IActionResult BranchAddEdit(int? BranchID)
        {
            FillCourseDDL();
            if (BranchID != null)
            {
                string connectionstr = this._configuration.GetConnectionString("myconnectionString");
                DataTable dt = new DataTable();
                SqlConnection sqlConnection = new SqlConnection(connectionstr);
                sqlConnection.Open();
                SqlCommand ObjCmd = sqlConnection.CreateCommand();
                ObjCmd.CommandType = CommandType.StoredProcedure;
                ObjCmd.CommandText = "PR_Branch_SelectByPK";
                ObjCmd.Parameters.AddWithValue("BranchID", BranchID);
                SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
                dt.Load(sqlDataReader);
                BranchModel model = new BranchModel();
                foreach (DataRow dr in dt.Rows)
                {

                    model.BranchName = dr["BranchName"].ToString();
                    model.DeanName = dr["DeanName"].ToString();
                    model.BranchID = Convert.ToInt32(dr["BranchID"]);

                }
                return View("BranchAddEdit", model);
            }

            return View("BranchAddEdit");
        }
        public void FillCourseDDL()
        {

            string str = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(str);
            sqlConnection.Open();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Course_Dropdown";

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);

            List<CourseDropDown> courselist = new List<CourseDropDown>();
            foreach (DataRow dr in dt.Rows)
            {
                CourseDropDown tempcourse = new CourseDropDown();
                tempcourse.CourseID = Convert.ToInt32(dr["CourseID"]);
                tempcourse.CourseName = dr["CourseName"].ToString();
                courselist.Add(tempcourse);
            }
            sqlConnection.Close();
            ViewBag.Courselists = courselist;
        }
        public IActionResult BranchSave(BranchModel model)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (model.BranchID == null || model.BranchID == 0)
            {
                ObjCmd.CommandText = "PR_Branch_Insert";

            }
            else
            {
                ObjCmd.CommandText = "PR_Branch_Update";
                ObjCmd.Parameters.AddWithValue("BranchID", model.BranchID);

            }
            ObjCmd.Parameters.AddWithValue("BranchName", model.BranchName);
            ObjCmd.Parameters.AddWithValue("DeanName", model.DeanName);
            ObjCmd.Parameters.AddWithValue("CourseID", model.CourseID);
            ObjCmd.ExecuteNonQuery();
            int rowsAffected = ObjCmd.ExecuteNonQuery();
            if (rowsAffected > 0)
            {
                TempData["BranchInsertMsg"] = (model.BranchID == null || model.BranchID == 0)
                                               ? "Record Inserted Successfully"
                                               : "Record Updated Successfully";
            }
            else
            {
                // Handle case when no rows were affected (potential error scenario)
                TempData["BranchInsertMsg"] = "An error occurred while processing the request.";
            }
            return RedirectToAction("BranchList");
        }
        public IActionResult BranchDelete(int BranchID)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Branch_Delete";
            ObjCmd.Parameters.AddWithValue("BranchID", BranchID);
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("BranchList");
        }
    }
}
