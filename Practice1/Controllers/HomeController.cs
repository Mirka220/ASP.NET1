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
            List<User> users = (from m in _dbContext.Users select m).ToList();

            return View(users);
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

        [HttpPost, ActionName("UpdateUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateUser(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(s => s.Id == Id);


            if (await TryUpdateModelAsync<User>(
                user, "", u => u.Login, u => u.Password))
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
                }
            }

            return View(user);
        }


        public async Task<IActionResult> UpdateUser(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == Id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> DetailsOfUser(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        public async Task<IActionResult> DeleteUser(Guid id, bool? Savechangeserror = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _dbContext.Users.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            if (Savechangeserror.GetValueOrDefault())
            {
                ViewData["DeleteError"] = "Delete failed, please try again later ... ";
            }

            return View(user);
        }

        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeleteUser(Guid id)
        {
            var user = await _dbContext.Users.FindAsync(id);

            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(DeleteUser), new { id = id, Savechangeserror = true });
            }
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

        public async Task<IActionResult> FullInfo()
        {
            return PartialView("FullInfo");
        }
    }
}