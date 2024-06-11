using ClosedXML.Excel;
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
        public IActionResult StudentDetails(int ?StudentID)
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
                    model.DOB = Convert.ToDateTime( dr["DOB"]);
                    model.Address = dr["Address"].ToString();
                    model.PhoneNumber = dr["PhoneNumber"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.EnrollmentNo = dr["EnrollmentNo"].ToString();
                    model.Created = Convert.ToDateTime(dr["Created"]);
                    model.Modified = Convert.ToDateTime(dr["Modified"]);
                    model.IsOnRoll = Convert.ToBoolean(dr["IsOnRoll"]);
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
                model.Created = DateTime.Now; 
                model.Modified = DateTime.Now;

            }
            else
            {
                ObjCmd.CommandText = "PR_Student_Update";
                ObjCmd.Parameters.AddWithValue("StudentID", model.StudentID);
                model.Modified = DateTime.Now;


            }
            ObjCmd.Parameters.AddWithValue("FirstName", model.FirstName);
            ObjCmd.Parameters.AddWithValue("LastName", model.LastName);
            ObjCmd.Parameters.AddWithValue("Gender", model.Gender);
            ObjCmd.Parameters.AddWithValue("DOB", Convert.ToDateTime(model.DOB));
            ObjCmd.Parameters.AddWithValue("Address", model.Address);
            ObjCmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
            ObjCmd.Parameters.AddWithValue("Email", model.Email);
            ObjCmd.Parameters.AddWithValue("EnrollmentNo", model.EnrollmentNo);
            ObjCmd.Parameters.AddWithValue("CourseID", model.CourseID);
            ObjCmd.Parameters.AddWithValue("BranchID", model.BranchID);
            ObjCmd.Parameters.AddWithValue("IsOnRoll", Convert.ToBoolean(model.IsOnRoll));
            ObjCmd.ExecuteNonQuery();
            if (Convert.ToBoolean(ObjCmd.ExecuteNonQuery()))
            {
                if (model.StudentID == null)
                {
                    TempData["StudentInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["StudentInsertMsg"] = "Record Updated Successfully";
                }
            }

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
        public List<Studentmodel> GetStudentModels()
        {
            List<Studentmodel> studentModels = new List<Studentmodel>();
            string myconnStr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Student_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Studentmodel studentModel = new Studentmodel
                    {
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Gender = reader["Gender"].ToString(),
                        DOB = Convert.ToDateTime(reader["DOB"]),
                        Address = reader["Address"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        EnrollmentNo = reader["EnrollmentNo"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        BranchName = reader["BranchName"].ToString(),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"]),
                        IsOnRoll = Convert.ToBoolean(reader["IsOnRoll"])
                    };
                    studentModels.Add(studentModel);
                }
                return studentModels;
            }
        }
        public IActionResult ExportStudentsToExcel()
        {

            List<Studentmodel> studentModels = GetStudentModels();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Students");
                // Add headers
                worksheet.Cell(1, 1).Value = "First Name";
                worksheet.Cell(1, 2).Value = "Last Name";
                worksheet.Cell(1, 3).Value = "Gender";
                worksheet.Cell(1, 4).Value = "DOB";
                worksheet.Cell(1, 5).Value = "Address";
                worksheet.Cell(1, 6).Value = "PhoneNumber";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 8).Value = "EnrollmentNo";
                worksheet.Cell(1, 9).Value = "Course Name";
                worksheet.Cell(1, 10).Value = "Branch Name";
                worksheet.Cell(1, 11).Value = "Created";
                worksheet.Cell(1, 12).Value = "Modified";
                worksheet.Cell(1, 13).Value = "IsOnRoll";
                int row = 2;
                foreach (var studentModel in studentModels)
                {
                    worksheet.Cell(row, 1).Value = studentModel.FirstName;
                    worksheet.Cell(row, 3).Value = studentModel.LastName;
                    worksheet.Cell(row, 3).Value = studentModel.Gender;
                    worksheet.Cell(row, 4).Value = studentModel.DOB.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 5).Value = studentModel.Address;
                    worksheet.Cell(row, 6).Value = studentModel.PhoneNumber;
                    worksheet.Cell(row, 7).Value = studentModel.Email;
                    worksheet.Cell(row, 8).Value = studentModel.EnrollmentNo;
                    worksheet.Cell(row, 9).Value = studentModel.CourseName;
                    worksheet.Cell(row, 10).Value = studentModel.BranchName;
                    worksheet.Cell(row, 11).Value = studentModel.Created?.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 12).Value = studentModel.Modified?.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 13).Value = studentModel.IsOnRoll;
                    row++;
                }
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "StudentData.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }

    }
}
