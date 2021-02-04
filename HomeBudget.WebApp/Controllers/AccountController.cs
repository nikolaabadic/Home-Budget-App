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
    
    public class AccountController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public AccountController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        // GET: AccountController
        [LogedInUser]
        public ActionResult Index()
        {
            return View();
        }

        // GET: AccountController/Details/5
        [LogedInUser]
        public ActionResult Details(int id)
        {
            Account account = unitOfWork.Account.FindByID(id);
            AccountDetailsModel model = new AccountDetailsModel();

            model.OwnerAccountID = id;
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
            model.CreditCards = account.CreditCards;           
            return View(model);
        }
        [AdminLoggedIn]
        [NotLoggedIn]
        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AdminLoggedIn]
        [NotLoggedIn]
        public ActionResult Create(AccountDetailsModel model)
        {
            try
            {
                int userID = unitOfWork.User.Search(u => u.Username == model.Username).UserID;
                Account account = new Account
                {
                    Currency = model.Currency,
                    AccountType=model.AccountType,
                    Number=model.Number,
                    Amount=model.Amount,
                    UserID=userID
                };
                unitOfWork.Account.Add(account);
                unitOfWork.Commit();
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AccountController/Edit/5
        [LogedInUser]
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogedInUser]
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
        [LogedInUser]
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [LogedInUser]
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
