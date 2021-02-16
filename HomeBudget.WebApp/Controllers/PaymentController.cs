using HomeBudget.Data.UnitOfWork;
using HomeBudget.Domain;
using HomeBudget.WebApp.Filters;
using HomeBudget.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace HomeBudget.WebApp.Controllers
{
    [LogedInUser]
    public class PaymentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        public PaymentController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShowDetails(int id, int accountID, int recipientID, int ownerAccountID)
        {
            HttpContext.Session.SetInt32("paymentid", id);
            HttpContext.Session.SetInt32("recipientid", recipientID);
            HttpContext.Session.SetInt32("paymentaccountid", accountID);
            HttpContext.Session.SetInt32("owneraccountid", ownerAccountID);
            return RedirectToAction("Details");
        }

        public ActionResult Details()
        {
            int? id = HttpContext.Session.GetInt32("paymentid");
            int? recipientID = HttpContext.Session.GetInt32("recipientid");
            int? accountID = HttpContext.Session.GetInt32("paymentaccountid");
            int? ownerAccountID = HttpContext.Session.GetInt32("owneraccountid");

            if (id != null && recipientID != null && accountID != null && ownerAccountID != null)
            {
                List<Belonging> belongings = unitOfWork.Belonging.Search(b => b.PaymentID == (int)id && b.AccountID == (int)accountID && b.RecipientID == (int)recipientID && b.OwnerID == (int)ownerAccountID);
                PaymentDetailsModel model = new PaymentDetailsModel();
                model.Payment = unitOfWork.Payment.FindByID((int)id, (int)accountID, (int)recipientID);
                model.Payment.Categories = belongings;
                model.Categories = unitOfWork.Category.GetAll();
                model.AccountNumber = unitOfWork.Account.Search(a => a.AccountID == accountID)[0].Number;
                model.RecipientNumber = unitOfWork.Account.Search(a => a.AccountID == recipientID)[0].Number;
                return View(model);
            }
            return RedirectToAction("Details", "Account");
        }
        
        [HttpPost]
        public ActionResult AddCategory(BelongingModel request)
        {
            try
            {
                Category category = unitOfWork.Category.FindByID(request.CategoryID);
                BelongingModel model = new BelongingModel
                {
                    Num = request.Num,
                    CategoryID = request.CategoryID,
                    Name = category.Name,
                    OwnerID = request.OwnerID
                };
                unitOfWork.Commit();
                return PartialView("BelongingPartialView", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error adding categories!");
                return View("Edit");
            }
        }
        
        [HttpPost]
        public ActionResult AddCategoryEdit(BelongingEditModel request)
        {
            try
            {
                Category category = unitOfWork.Category.FindByID(request.CategoryID);
                BelongingModel model = new BelongingModel
                {
                    Num = request.Num,
                    CategoryID = request.CategoryID,
                    Name = category.Name,
                    OwnerID = request.OwnerID
                };
                Belonging b = new Belonging
                {
                    CategoryID = category.CategoryID,
                    PaymentID = request.PaymentID,
                    AccountID = request.AccountID,
                    RecipientID = request.RecipientID,
                    OwnerID = request.OwnerID
                };
                unitOfWork.Belonging.Add(b);
                unitOfWork.Commit();
                return PartialView("BelongingPartialView", model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, "Error adding categories!");
                return PartialView("ErrorPatrialView");
            }
        }
       
        [HttpDelete]
        public ActionResult DeleteCategory(int id, int accountID, int recipientID, int categoryID, int ownerID)
        {
            try
            {
                Payment payment = unitOfWork.Payment.FindByID(id, accountID, recipientID);
                Belonging belonging = unitOfWork.Belonging.FindByID(categoryID, id, accountID, recipientID, ownerID);
                payment.Categories.Remove(belonging);
                unitOfWork.Commit();
                return RedirectToAction("Details", "Account");
            } catch (Exception e)
            {
                return RedirectToAction("Details", "Account");
            }
        }

        public ActionResult Create()
        {
            List<Category> list = unitOfWork.Category.GetAll();
            List<SelectListItem> categories = list.Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryID.ToString() }).ToList();

            int? id = HttpContext.Session.GetInt32("accountid");
            if (id != null)
            {
                string accountNumber = unitOfWork.Account.Search(a => a.AccountID == (int)id)[0].Number;
                PaymentCreateModel model = new PaymentCreateModel { Categories = categories, AccountNumber = accountNumber, AccountID = (int)id};
                return View("Create",model);
            }
            return RedirectToAction("ShowDetails", "Account",new { id});
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentCreateModel model)
        {
            try
            {
                    try
                    {
                        Account recipient = unitOfWork.Account.Search(a => a.Number == model.RecipientAccountNumber)[0];
                        model.Payment.RecipientID = recipient.AccountID;
                                            
                        Account account = unitOfWork.Account.FindByID(model.Payment.AccountID);
                        if(account.Currency != recipient.Currency)
                        {
                            throw new FormatException();
                        }
                    byte[] amountByte = HttpContext.Session.Get("amount");
                    double amount = JsonSerializer.Deserialize<double>(amountByte);
                    if (model.Payment.Amount > amount)
                        {
                            throw new ApplicationException();
                        }
                    }
                    catch (FormatException)
                    {
                        throw new Exception("The recipient account you entered is in another currency!");
                    }
                    catch (ApplicationException)
                    {
                        throw new Exception("There is not enough money on the account for this payment!");
                    }
                    catch (Exception)
                    {
                        throw new Exception("Invalid recipient account number!");
                    }
                    

                if (model.Payment.Amount == 0)
                {
                    throw new Exception("Payment amount can't be 0!");
                }

                unitOfWork.Payment.Add(model.Payment);
                unitOfWork.Commit();
                return RedirectToAction("ShowDetails", "Account", new { id = model.Payment.AccountID });
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, 
                    ex.Message.Length > 100 ? "Error adding categories!" : ex.Message);
                int? id = HttpContext.Session.GetInt32("accountid");
                return View("Create", new PaymentCreateModel
                {
                    AccountID = model.Payment.AccountID,
                    AccountNumber = unitOfWork.Account.Search(a => a.AccountID == (int)id)[0].Number,
                    Categories = unitOfWork.Category.GetAll().Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryID.ToString() }).ToList(),
                    Payment = model.Payment
                });
            }
        }
        
        public ActionResult ShowEdit(int id, int accountID, int recipientID, int ownerAccountID)
        {
            HttpContext.Session.SetInt32("paymentid", id);
            HttpContext.Session.SetInt32("recipientid", recipientID);
            HttpContext.Session.SetInt32("paymentaccountid", accountID);
            HttpContext.Session.SetInt32("owneraccountid", ownerAccountID);
            return RedirectToAction("Edit");
        }
        public ActionResult Edit()
        {
            int? id = HttpContext.Session.GetInt32("paymentid");
            int? recipientID = HttpContext.Session.GetInt32("recipientid");
            int? accountID = HttpContext.Session.GetInt32("paymentaccountid");
            int? ownerAccountID = HttpContext.Session.GetInt32("owneraccountid");

            if (id != null && recipientID != null && accountID != null && ownerAccountID != null)
            {
                Payment payment = unitOfWork.Payment.FindByID((int)id, (int)accountID, (int)recipientID);
                payment.Categories = unitOfWork.Belonging.Search(b => b.PaymentID == (int)id && b.AccountID == (int)accountID && b.RecipientID == (int)recipientID && b.OwnerID == (int)ownerAccountID);
                List<Category> categories = unitOfWork.Category.GetAll();
                List<SelectListItem> list = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryID.ToString() }).ToList();

                string accountNum = unitOfWork.Account.FindByID((int)accountID).Number;
                string recipientNum = unitOfWork.Account.FindByID((int)recipientID).Number;
                PaymentCreateModel model = new PaymentCreateModel
                {
                    AccountID = (int)ownerAccountID,
                    Payment = payment,
                    Categories = list,
                    CategoryList = categories,
                    AccountNumber = accountNum,
                    RecipientAccountNumber = recipientNum
                };
                return View(model);
            }
            return RedirectToAction("ShowDetails", "Account", new { id = accountID});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentCreateModel model)
        {
            try
            {
                unitOfWork.Payment.UpdateCategoryList(model.Payment, model.Payment.Categories);
                unitOfWork.Commit();
                int id = model.AccountID;
                return RedirectToAction("ShowDetails", "Account", new { id });
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Error editing categories!");
                return RedirectToAction("Edit");
            }
        }
    }
}
