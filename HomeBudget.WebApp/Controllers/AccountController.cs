using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
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
        [AdminNotLoggedIn]
        public ActionResult Index()
        {
            return View();
        }
        [LogedInUser]
        [AdminNotLoggedIn]
        public ActionResult ShowDetails(int id)
        {
            HttpContext.Session.SetInt32("accountid", id);
            return RedirectToAction("Details");
        }


        // GET: AccountController/Details/5
        [LogedInUser]
        [AdminNotLoggedIn]
        public ActionResult Details()
        {
            try
            {
                int? id = HttpContext.Session.GetInt32("accountid");
                if (id != null)
                {
                    Account account = unitOfWork.Account.FindByID((int)id);
                    AccountDetailsModel model = new AccountDetailsModel();

                    model.OwnerAccountID = (int)id;
                    model.AccountType = account.AccountType;
                    model.Currency = account.Currency;
                    model.Number = account.Number;
                    model.Accounts = unitOfWork.Account.GetAll();

                    double amount = 0;
                    List<Payment> paymentsFrom = unitOfWork.Payment.Search(p => p.RecipientID == account.AccountID);
                    List<Payment> paymentsTo = unitOfWork.Payment.Search(p => p.AccountID == account.AccountID);

                    List<ChartCategory> chartCategoriesIncome = new List<ChartCategory>();
                    List<ChartCategory> chartCategoriesExpense = new List<ChartCategory>();

                    List<Category> categories = unitOfWork.Category.GetAll();

                    foreach(var c in categories)
                    {
                        foreach(var p in paymentsFrom)
                        {
                            try
                            {
                                List<Belonging> belongings = unitOfWork.Belonging.Search(b => 
                                    b.OwnerID == (int)id && p.PaymentID == b.PaymentID && b.CategoryID == c.CategoryID);
                                foreach(var b in belongings)
                                {
                                    chartCategoriesIncome.Add(new ChartCategory { CategoryID = b.CategoryID, 
                                        Amount = p.Amount, Name = c.Name });
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }
                    foreach (var c in categories)
                    {
                        foreach (var p in paymentsTo)
                        {
                            try
                            {
                                List<Belonging> belongings = unitOfWork.Belonging.Search(
                                    b => b.OwnerID == (int)id && p.PaymentID == b.PaymentID && 
                                    b.CategoryID == c.CategoryID);
                                foreach (var b in belongings)
                                {
                                    chartCategoriesExpense.Add(new ChartCategory { CategoryID = b.CategoryID, 
                                        Amount = p.Amount, Name = c.Name });
                                }
                            }
                            catch (Exception e)
                            {
                                throw e;
                            }
                        }
                    }

                    List<string> categoryNamesIncome = chartCategoriesIncome.Select(cc => cc.Name).Distinct().ToList();
                    List<ChartCategory> incomeFinal = new List<ChartCategory>();
                    foreach(var c in categoryNamesIncome)
                    {
                        ChartCategory cc = new ChartCategory();
                        cc.Name = c;
                        cc.Amount = 0;
                        foreach(var p in chartCategoriesIncome)
                        {
                            if (p.Name == c)
                            {
                                cc.Amount += p.Amount;
                            }
                        }
                        incomeFinal.Add(cc);
                    }


                    List<string> categoryNamesExpense = chartCategoriesExpense.Select(cc => cc.Name).Distinct().ToList();
                    List<ChartCategory> expenseFinal = new List<ChartCategory>();
                    foreach (var c in categoryNamesExpense)
                    {
                        ChartCategory cc = new ChartCategory();
                        cc.Name = c;
                        cc.Amount = 0;
                        foreach (var p in chartCategoriesExpense)
                        {
                            if (p.Name == c)
                            {
                                cc.Amount += p.Amount;
                            }
                        }
                        expenseFinal.Add(cc);
                    }
                    model.IncomeLabels = categoryNamesIncome;
                    model.ExpenseLabels = categoryNamesExpense;
                    model.IncomeCategory = incomeFinal.Select(c=>c.Amount).ToList();
                    model.ExpenseCategory = expenseFinal.Select(c => c.Amount).ToList();

                    foreach (var p in paymentsFrom)
                    {
                        amount += p.Amount;
                    }
                    foreach (var p in paymentsTo)
                    {
                        amount -= p.Amount;
                    }

                    model.Amount = amount;
                    model.Payments = paymentsFrom.Concat(paymentsTo).ToList().OrderBy(p => p.DateTime).Reverse().ToList();
                    model.CreditCards = account.CreditCards;

                    HttpContext.Session.Set("amount", JsonSerializer.SerializeToUtf8Bytes(amount));
                    return View(model);
                }
                return RedirectToAction("Details", "User");
            } catch (Exception e)
            {
                return RedirectToAction("Details", "User");
            }
            
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
            if (!ModelState.IsValid)
            {
                if(model.Number==null || model.Username == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid fields!");
                    return View();
                }
            }
            try
            {
                try
                {
                    Account accountDB = unitOfWork.Account.Search(a => a.Number == model.Number)[0];
                    ModelState.AddModelError(string.Empty, "Account number must be unique!");
                    return View();
                } catch (Exception e)
                {
                    int userID = unitOfWork.User.Search(u => u.Username == model.Username).UserID;
                    Account account = new Account
                    {
                        Currency = model.Currency,
                        AccountType = model.AccountType,
                        Number = model.Number,
                        Amount = model.Amount,
                        UserID = userID
                    };
                    unitOfWork.Account.Add(account);
                    unitOfWork.Commit();
                    return RedirectToAction("Options", "Admin");
                }
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "No user found!");
                return View();
            }
        }
    }
}
