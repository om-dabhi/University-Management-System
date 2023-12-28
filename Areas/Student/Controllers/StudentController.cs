using Microsoft.AspNetCore.Mvc;

namespace UMS.Areas.Student.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
