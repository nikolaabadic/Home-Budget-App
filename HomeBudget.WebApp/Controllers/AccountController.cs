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
    [LogedInUser]
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: AccountController
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        public ActionResult Details(int id)
        {
            Account account = unitOfWork.Account.FindByID(id);
            AccountDetailsModel model = new AccountDetailsModel();

            model.AccountID = id; ;
            model.AccountType = account.AccountType;
            model.Currency = account.Currency;
            model.Number = account.Number;
            model.Accounts = unitOfWork.Account.GetAll();

            double amount=0;
            List<Payment> paymentsFrom = unitOfWork.Payment.Search(p => p.RecipientID == account.AccountID);
            List<Payment> paymentsTo = unitOfWork.Payment.Search(p => p.AccountID == account.AccountID);

            foreach(var p in paymentsFrom)
            {
                amount += p.Amount;
            }
            foreach(var p in paymentsTo)
            {
                amount -= p.Amount;
            }
            model.Amount = amount;
            model.Payments = paymentsFrom.Concat(paymentsTo).ToList().OrderBy(p => p.DateTime).ToList();

            
            return View(model);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
