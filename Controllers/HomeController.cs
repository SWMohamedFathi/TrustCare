using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Runtime.InteropServices;
using TrustCare.Models;

namespace TrustCare.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ModelContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public HomeController(ILogger<HomeController> logger, ModelContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            ViewBag.CurrentTime = DateTime.Now;

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

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult ContactUs()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([Bind("UserId,RoleId,ProfileImage,UserName,Password,Email,FirstName,LastName,Phone,Dateofbirth,ImageFile")] User user)
        {
            if (ModelState.IsValid)
            {

                if (user.ImageFile != null)
                {
                    string wwwRootPath = webHostEnvironment.WebRootPath;

                    string fileName = Guid.NewGuid().ToString() + user.ImageFile.FileName;

                    string path = Path.Combine(wwwRootPath + "/Images/" + fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await user.ImageFile.CopyToAsync(fileStream);
                    }

                    user.ProfileImage = fileName;
                }
                var account = _context.Users.Where(x => x.UserName == user.UserName && x.Email == user.Email).FirstOrDefault();
                if (account == null)
                {

                    user.RoleId = 2;
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Home");
                }

                else
                {
                    ViewBag.Error = "Email is already used, please try another  one.";
                }


            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "RoleId", "RoleId", user.RoleId);
            return View(user);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([Bind("UserName,Password")] User user)
        {

            var auth = _context.Users.Where(x => x.UserName == user.UserName && x.Password == user.Password).FirstOrDefault();

            if (auth != null)
            {
                var account = _context.Users.Where(x => x.UserName == user.UserName && x.Email == user.Email).FirstOrDefault();
                switch (auth.RoleId)
                {
                    case 1:
                        HttpContext.Session.SetString("FirstName", auth.FirstName);
                        HttpContext.Session.SetString("LastName", auth.LastName);
                        HttpContext.Session.SetString("UserName", auth.UserName);
                        HttpContext.Session.SetString("ProfileImage", auth.ProfileImage);

                        return RedirectToAction("Index", "Admin");
                    case 2:
                        //Var fname = int value 
                        HttpContext.Session.SetString("FirstName", auth.FirstName);
                        HttpContext.Session.SetString("LastName", auth.LastName);
                        HttpContext.Session.SetString("UserName", auth.UserName);
                        return RedirectToAction("Index", "Home");


                }
            }

            else
            {
                ViewBag.Error = "Wrong username or password.";
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    
    }
}