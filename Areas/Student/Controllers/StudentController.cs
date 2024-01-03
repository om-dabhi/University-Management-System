using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using UMS.Areas.Student.Models;

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
        public IActionResult StudentAddEdit(int? StudentID)
        {
            FillCourseDDL();
            FillBranchDDL();
            if (StudentID != null)
            {
                string connectionstr = this._configuration.GetConnectionString("myconnectionString");
                DataTable dt = new DataTable();
                SqlConnection sqlConnection = new SqlConnection(connectionstr);
                sqlConnection.Open();
                SqlCommand ObjCmd = sqlConnection.CreateCommand();
                ObjCmd.CommandType = CommandType.StoredProcedure;
                ObjCmd.CommandText = "PR_Student_SelectByPK";
                ObjCmd.Parameters.AddWithValue("StudentID", StudentID);
                SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
                dt.Load(sqlDataReader);
                Studentmodel model = new Studentmodel();
                foreach (DataRow dr in dt.Rows)
                {

                    model.FirstName = dr["FirstName"].ToString();
                    model.LastName = dr["LastName"].ToString();
                    model.Gender = dr["Gender"].ToString();
                    model.DOB = dr["DOB"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.PhoneNumber = dr["PhoneNumber"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.EnrollmentNo = dr["EnrollmentNo"].ToString();
                    model.BranchID = Convert.ToInt32(dr["BranchID"]);
                    model.CourseID = Convert.ToInt32(dr["CourseID"]);

                }
                return View("StudentAddEdit", model);
            }

            return View("StudentAddEdit");
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
        public void FillBranchDDL()
        {

            string str = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(str);
            sqlConnection.Open();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Branch_Dropdown";

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);

            List<BranchDropDown> branchlist = new List<BranchDropDown>();
            foreach (DataRow dr in dt.Rows)
            {
                BranchDropDown tempbranch = new BranchDropDown();
                tempbranch.BranchID = Convert.ToInt32(dr["BranchID"]);
                tempbranch.BranchName = dr["BranchName"].ToString();
                branchlist.Add(tempbranch);
            }
            sqlConnection.Close();
            ViewBag.Branchlists = branchlist;
        }
        public IActionResult StudentSave(Studentmodel model)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (model.StudentID == null)
            {
                ObjCmd.CommandText = "PR_Student_Insert";

            }
            else
            {
                ObjCmd.CommandText = "PR_Student_Update";
                ObjCmd.Parameters.AddWithValue("StudentID", model.StudentID);


            }
            ObjCmd.Parameters.AddWithValue("FirstName", model.FirstName);
            ObjCmd.Parameters.AddWithValue("LastName", model.LastName);
            ObjCmd.Parameters.AddWithValue("Gender", model.Gender);
            ObjCmd.Parameters.AddWithValue("DOB", model.DOB);
            ObjCmd.Parameters.AddWithValue("Address", model.Address);
            ObjCmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
            ObjCmd.Parameters.AddWithValue("Email", model.Email);
            ObjCmd.Parameters.AddWithValue("EnrollmentNo", model.EnrollmentNo);
            ObjCmd.Parameters.AddWithValue("CourseID", model.CourseID);
            ObjCmd.Parameters.AddWithValue("BranchID", model.BranchID);
            ObjCmd.Parameters.AddWithValue("IsOnRoll", model.IsOnRoll);
            ObjCmd.ExecuteNonQuery();

            return RedirectToAction("StudentList");
        }
        public IActionResult StudentDelete(int StudentID)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Student_Delete";
            ObjCmd.Parameters.AddWithValue("StudentID", StudentID);
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("StudentList");
        }
    }
}
