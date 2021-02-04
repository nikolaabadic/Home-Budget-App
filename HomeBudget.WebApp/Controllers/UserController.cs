using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeBudget.Data.UnitOfWork;
using HomeBudget.Domain;
using HomeBudget.WebApp.Filters;
using HomeBudget.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeBudget.WebApp.Controllers
{
    
    public class UserController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public UserController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        [LogedInUser]
        // GET: UserController
        public ActionResult Index()
        {
            return View("Login");
        }
        [LogedInUser]
        public ActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
        [AdminLoggedIn]
        [NotLoggedIn]
        // GET: AdminController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdminController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminLoggedIn]
        [NotLoggedIn]
        public ActionResult Create(User user)
        {
            try
            {
                unitOfWork.User.Add(user);
                unitOfWork.Commit();
                return View();

            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Details/5
        [LogedInUser]
        public ActionResult Details()
        {
            int? id = HttpContext.Session.GetInt32("userid");
            User user=null;
            UserDetailsModel model = new UserDetailsModel();
            if (id != null)
            {
                user = unitOfWork.User.FindByID((int)id);
            } 
            if(user != null)
            {
                model.UserID = user.UserID;
                model.Name = user.Name;
                model.Surname = user.Surname;
                model.Accounts = unitOfWork.Account.Search(a => a.UserID == user.UserID);
            } 
            return View(model);
        }

        // GET: UserController/Edit/5
        [LogedInUser]
        public ActionResult Edit(int id)
        {
            User model = unitOfWork.User.FindByID(id);
            return View(model);
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogedInUser]
        public ActionResult Edit([FromForm]User user)
        {
            if (ModelState.IsValid)
            {
                
            }
            return View();
        }
    }
}
