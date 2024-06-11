using ClosedXML.Excel;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using UMS.Areas.Faculty.Models;
using UMS.Areas.Student.Models;

namespace UMS.Areas.Faculty.Controllers
{
    [Area("Faculty")]
    [Route("Faculty/[controller]/[action]")]
    public class FacultyController : Controller
    {
        public IConfiguration _configuration;
        public FacultyController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult FacultyList()
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Faculty_SelectAll";
            SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
            dt.Load(sqlDataReader);
            return View(dt);
        }
        public IActionResult FacultyAddEdit(int? FacultyID)
        {
            FillCourseDDL();
            FillExperienceDDL();
            if (FacultyID != null)
            {
                string connectionstr = this._configuration.GetConnectionString("myconnectionString");
                DataTable dt = new DataTable();
                SqlConnection sqlConnection = new SqlConnection(connectionstr);
                sqlConnection.Open();
                SqlCommand ObjCmd = sqlConnection.CreateCommand();
                ObjCmd.CommandType = CommandType.StoredProcedure;
                ObjCmd.CommandText = "PR_Faculty_SelectByPK";
                ObjCmd.Parameters.AddWithValue("FacultyID", FacultyID);
                SqlDataReader sqlDataReader = ObjCmd.ExecuteReader();
                dt.Load(sqlDataReader);
                FacultyModel model = new FacultyModel();
                foreach (DataRow dr in dt.Rows)
                {

                    model.FacultyName = dr["FacultyName"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Experience = dr["Experience"].ToString();
                    model.PhoneNumber = dr["PhoneNumber"].ToString();
                    model.Location = dr["Location"].ToString();
                    model.CourseID = Convert.ToInt32(dr["CourseID"]);
                    model.Created = Convert.ToDateTime(dr["Created"]);
                    model.Modified = Convert.ToDateTime(dr["Modified"]);
                }
                return View("FacultyAddEdit", model);
            }

            return View("FacultyAddEdit");
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
        public IActionResult FacultySave(FacultyModel model)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            DataTable dt = new DataTable();
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            if (model.FacultyID == null || model.FacultyID == 0)
            {
                ObjCmd.CommandText = "PR_Faculty_Insert";
                model.Created = DateTime.Now;
                model.Modified = DateTime.Now;

            }
            else
            {
                ObjCmd.CommandText = "PR_Faculty_Update";
                ObjCmd.Parameters.AddWithValue("FacultyID", model.FacultyID);
                model.Modified = DateTime.Now;


            }
            ObjCmd.Parameters.AddWithValue("FacultyName", model.FacultyName);
            ObjCmd.Parameters.AddWithValue("ExperienceID", model.ExperienceID);
            ObjCmd.Parameters.AddWithValue("Location", model.Location);
            ObjCmd.Parameters.AddWithValue("PhoneNumber", model.PhoneNumber);
            ObjCmd.Parameters.AddWithValue("Email", model.Email);
            ObjCmd.Parameters.AddWithValue("CourseID", model.CourseID);
            ObjCmd.ExecuteNonQuery();
            if (Convert.ToBoolean(ObjCmd.ExecuteNonQuery()))
            {
                if (model.FacultyID == null)
                {
                    TempData["FacultyInsertMsg"] = "Record Inserted Successfully";
                }
                else
                {
                    TempData["FacultyInsertMsg"] = "Record Updated Successfully";
                }
            }
            return RedirectToAction("FacultyList");
        }
        public IActionResult FacultyDelete(int FacultyID)
        {
            string connectionstr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(connectionstr);
            sqlConnection.Open();
            SqlCommand ObjCmd = sqlConnection.CreateCommand();
            ObjCmd.CommandType = CommandType.StoredProcedure;
            ObjCmd.CommandText = "PR_Faculty_Delete";
            ObjCmd.Parameters.AddWithValue("FacultyID", FacultyID);
            ObjCmd.ExecuteNonQuery();
            return RedirectToAction("FacultyList");
        }
        public List<FacultyModel> GetFacultyModels()
        {
            List<FacultyModel> facultyModels = new List<FacultyModel>();
            string myconnStr = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection connection = new SqlConnection(myconnStr);
            connection.Open();
            SqlCommand cmd = connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Faculty_SelectAll";
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    FacultyModel FacultyModel = new FacultyModel
                    {
                        FacultyName = reader["FacultyName"].ToString(),
                        //Experience = reader["Experience"].ToString(),
                        Location = reader["Location"].ToString(),
                        PhoneNumber = reader["PhoneNumber"].ToString(),
                        Email = reader["Email"].ToString(),
                        CourseName = reader["CourseName"].ToString(),
                        Created = Convert.ToDateTime(reader["Created"]),
                        Modified = Convert.ToDateTime(reader["Modified"])
                    };
                    facultyModels.Add(FacultyModel);
                }
                return facultyModels;
            }
        }
        public IActionResult ExportFacultyToExcel()
        {

            List<FacultyModel> facultyModels = GetFacultyModels();
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Faculty");
                // Add headers
                worksheet.Cell(1, 1).Value = "Faculty Name";
                //worksheet.Cell(1, 2).Value = "Experience";
                worksheet.Cell(1, 3).Value = "Location";
                worksheet.Cell(1, 4).Value = "PhoneNumber";
                worksheet.Cell(1, 7).Value = "Email";
                worksheet.Cell(1, 9).Value = "Course Name";
                worksheet.Cell(1, 11).Value = "Created";
                worksheet.Cell(1, 12).Value = "Modified";
                int row = 2;
                foreach (var FacultyModel in facultyModels)
                {
                    worksheet.Cell(row, 1).Value = FacultyModel.FacultyName;
                    worksheet.Cell(row, 3).Value = FacultyModel.Experience;
                    worksheet.Cell(row, 3).Value = FacultyModel.Location;
                    worksheet.Cell(row, 6).Value = FacultyModel.PhoneNumber;
                    worksheet.Cell(row, 7).Value = FacultyModel.Email;
                    worksheet.Cell(row, 9).Value = FacultyModel.CourseName;
                    worksheet.Cell(row, 11).Value = FacultyModel.Created?.ToString("yyyy-MM-dd");
                    worksheet.Cell(row, 12).Value = FacultyModel.Modified?.ToString("yyyy-MM-dd");
                    row++;
                }
                var contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                var fileName = "FacultyData.xlsx";
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }
            }
        }
        /*public void FacultyDDL()
        {

            string str = this._configuration.GetConnectionString("myconnectionString");
            SqlConnection sqlConnection = new SqlConnection(str);
            sqlConnection.Open();

            SqlCommand cmd = sqlConnection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "PR_Faculty_Dropdown";

            SqlDataReader sqlDataReader = cmd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(sqlDataReader);

            List<FacultyDropDown> facultylist = new List<FacultyDropDown>();
            foreach (DataRow dr in dt.Rows)
            {
                FacultyDropDown tempcourse = new CourseDropDown();
                tempcourse.CourseID = Convert.ToInt32(dr["FacultyID"]);
                tempcourse.CourseName = dr["CourseName"].ToString();
                courselist.Add(tempcourse);
            }
            sqlConnection.Close();
            ViewBag.Courselists = courselist;
        }*/
        public void FillExperienceDDL()
        {
            string connectionString = this._configuration.GetConnectionString("myconnectionString");

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                sqlConnection.Open();

                using (SqlCommand cmd = sqlConnection.CreateCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "PR_Experience_Dropdown";

                    using (SqlDataReader sqlDataReader = cmd.ExecuteReader())
                    {
                        List<ExperienceDropDown> experiencelist = new List<ExperienceDropDown>();
                        while (sqlDataReader.Read())
                        {
                            ExperienceDropDown tempexperience = new ExperienceDropDown();
                            tempexperience.ExperienceID = Convert.ToInt32(sqlDataReader["ExperienceID"]);
                            tempexperience.Experience = sqlDataReader["Experience"].ToString();
                            experiencelist.Add(tempexperience);
                        }
                        ViewBag.Experiencelist = experiencelist;
                    }
                }
            }
        }

    }
}
