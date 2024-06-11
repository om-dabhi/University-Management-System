using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using UMS.Areas.Login.Models;
using System.Configuration;

namespace LoginController.Controllers
{
    public class LoginController : Controller
    { 

        public IConfiguration _configuration;
        public LoginController(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        public IActionResult SignInPage()
        {
            return View("Login");
        }

        [Area("Login")]
        [Route("Login[controller]/[action]")]
        public IActionResult Login(Loginmodel userLoginModel)
        {
            string ErrorMsg = string.Empty;
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Invalid Model State";
                return RedirectToAction("Index", "SEC_User");
            }

            if (string.IsNullOrEmpty(userLoginModel.UserName))
            {
                ErrorMsg += "User Name is Required";
            }
            if (string.IsNullOrEmpty(userLoginModel.Password))
            {
                ErrorMsg += "<br/>Password is Required";
            }

            if (!string.IsNullOrEmpty(ErrorMsg))
            {
                TempData["ErrorMessage"] = ErrorMsg;
                return RedirectToAction("Index", "SEC_User");
            }
            using (SqlConnection conn = new SqlConnection(_configuration.GetConnectionString("myconnectionString")))
            {
                conn.Open();
                using (SqlCommand objCmd = conn.CreateCommand())
                {
                    objCmd.CommandType = CommandType.StoredProcedure;
                    objCmd.CommandText = "PR_Login";
                    objCmd.Parameters.AddWithValue("@UserName", userLoginModel.UserName);
                    objCmd.Parameters.AddWithValue("@Password", userLoginModel.Password);

                    using (SqlDataReader objSDR = objCmd.ExecuteReader())
                    {
                        if (!objSDR.HasRows)
                        {
                            TempData["ErrorMessage"] = "Invalid User Name or Password";
                            return RedirectToAction("Index", "SEC_User");
                        }
                        else
                        {
                            DataTable dtLogin = new DataTable();
                            dtLogin.Load(objSDR);

                            foreach (DataRow dr in dtLogin.Rows)
                            {
                                HttpContext.Session.SetString("UserID", dr["UserID"].ToString());
                                HttpContext.Session.SetString("UserName", dr["UserName"].ToString());
                                HttpContext.Session.SetString("Password", dr["Password"].ToString());
                            }
                            return RedirectToAction("AdminDash", "AdminDash", new {Area= "AdminDash" });
                        }
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "SEC_User");
        }
    }
}
