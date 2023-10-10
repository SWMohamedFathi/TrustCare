using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TrustCare.Models;

namespace TrustCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ModelContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ModelContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AboutUs()
        {
            return View();
        }
        

          public IActionResult Testimonial()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ContactUs([Bind("ContactId,Name,Subject,Address,Phone,Email,MapLocation")] ContactU contactU)
        {
            if (ModelState.IsValid)
            {
                var account = _context.ContactUs;
                if (account == null)
                {


                    ViewBag.send = "Well Done!";


                }
                _context.Add(contactU);
                await _context.SaveChangesAsync();
                return RedirectToAction("ContactUs", "Home");

            }

         
            return View(contactU);
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}