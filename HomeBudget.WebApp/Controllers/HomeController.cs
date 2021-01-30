using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HomeBudget.WebApp.Models;
using HomeBudget.Data.UnitOfWork;
using HomeBudget.Domain;
using Microsoft.AspNetCore.Http;

namespace HomeBudget.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            return View();
        }

        //Login
        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            try
            {
                User user = unitOfWork.User.GetByUsernameAndPinCode(new User { Username = model.Username, PINCode = model.PINCode });
                HttpContext.Session.SetInt32("userid", user.UserID);
                return RedirectToAction("Details","User");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, "Wrong credentials!");
                return View("Index");
            }
        }
    }
}
