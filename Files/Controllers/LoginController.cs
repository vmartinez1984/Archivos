using Files.Dtos.Inputs;
using Files.Helpers;
using Files.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Files.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public LoginController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(UserLogin userLogin)
        {
            try
            {
                User user;
                user = await _appDbContext.User.Where(x => x.UserName == userLogin.UserName && x.Password == userLogin.Password && x.IsActive).FirstOrDefaultAsync();
                if (user is null)
                {
                    ViewBag.Error ="El usuario y/o contraseña son incorrectos";

                    return View();
                }
                else
                {
                    HttpContext.Session.SetInt32(SessionUser.Id, user.Id);
                    HttpContext.Session.SetInt32(SessionUser.RoleId, user.RoleId);
                    HttpContext.Session.SetString(SessionUser.FullName, user.FullName);

                    return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public ActionResult LogOut()
        {
            HttpContext.Session.Clear();

            return RedirectToAction("Index");
        }
    }
}
