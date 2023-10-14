using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustCare.Models;

namespace TrustCare.Controllers
{
    public class AdminController : Controller
    {

        private readonly ModelContext _context;
        public AdminController(ModelContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {

            ViewBag.UsersCount = _context.Users.Count();
            ViewBag.SubCount = _context.Subscriptions.Count();
            ViewBag.FirstName = HttpContext.Session.GetString("FirstName");
            ViewBag.ProfileImage = HttpContext.Session.GetString("ProfileImage");


            return View();
        }

        public IActionResult Profile(decimal UserId)
        {
       
            return View();
        }


    }
}
