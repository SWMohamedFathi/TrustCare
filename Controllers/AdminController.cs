using Microsoft.AspNetCore.Mvc;

namespace TrustCare.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");

            return View();
        }
    }
}
