using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using UMS.Areas.Course.Models;

namespace UMS.Areas.Course.Controllers
{
    [Area("Course")]
    [Route("Course/[controller]/[action]")]
    public class CourseController : Controller
    {
        public readonly IConfiguration _configuration;
        public CourseController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CourseList()
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Course_SelectAll";
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        public IActionResult CourseAddEdit(int? CourseID)
        {
            if (CourseID != null)
            {
                string connectionstr = this._configuration.GetConnectionString("myconnectionString");
                DataTable dt = new DataTable();
                SqlConnection sqlConnection = new SqlConnection(connectionstr);
                sqlConnection.Open();
                SqlCommand ObjCmd = sqlConnection.CreateCommand();
                ObjCmd.CommandType = CommandType.StoredProcedure;
                ObjCmd.CommandText = "PR_Course_SelectByPK";
                ObjCmd.Parameters.AddWithValue("CourseID", CourseID);
                SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
                dt.Load(sqlDataReader);
                CourseModel model = new CourseModel();
                foreach (DataRow dr in dt.Rows)
                {

                    model.CourseName = dr["CourseName"].ToString();
                    model.CourseID = Convert.ToInt32(dr["CourseID"]);

                }
                return View("CourseAddEdit", model);
            }

            return View("CourseAddEdit");
        }
        public IActionResult CourseSave(CourseModel model)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (model.CourseID == null || model.CourseID == 0)
            {
                ObjCmd.CommandText = "PR_Course_Insert";

            }
            else
            {
                ObjCmd.CommandText = "PR_Course_Update";
                ObjCmd.Parameters.AddWithValue("CourseID", model.CourseID);

            }
            ObjCmd.Parameters.AddWithValue("CourseName", model.CourseName);
            ObjCmd.ExecuteNonQuery();
            if (Convert.ToBoolean(ObjCmd.ExecuteNonQuery()))
            {
                if (model.CourseID == null)
                {
                    TempData["CourseInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["CourseInsertMsg"] = "Record Updated Successfully";
                }
            }
            return RedirectToAction("CourseList");
        }
        public IActionResult CourseDelete(int CourseID)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Course_Delete";
            ObjCmd.Parameters.AddWithValue("CourseID", CourseID);
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("CourseList");
        }
    }
}
