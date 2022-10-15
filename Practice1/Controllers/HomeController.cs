using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Practice1.Models;
using System.Diagnostics;

namespace Practice1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([Bind("Login,Password")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(user);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
            }

            return View(user);
        }

        public IActionResult Login()
        {

            return View();
        }
        public IActionResult Registr()
        {
            return View();
        }

        public void RespError404()
        {
            Response.StatusCode = 404;
            Response.WriteAsync("Not Found");
        }

        public void RespError500()
        {
            Response.StatusCode = 500;
            Response.WriteAsync("Internal Server Error");
        }

        public void RespError400()
        {
            Response.StatusCode=400;
            Response.WriteAsync("Bad Request");
        }

        public void Req()
        {
            string Host = Request.Headers["Host"].ToString();
            string AcceptLanguage = Request.Headers["Accept-Language"].ToString();
            string Cookie = Request.Headers["Cookie"].ToString();

            string list = $"Host: {Host} \n Accept-Language: {AcceptLanguage} \n Cookie: {Cookie}";
            Response.WriteAsync(list);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}