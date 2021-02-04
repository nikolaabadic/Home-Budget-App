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
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;

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
        // GET: PaymentController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PaymentController/Details/5
        public ActionResult Details(int id,int accountID,int recipientID,int ownerAccountID)
        {
            List<Belonging> belongings = unitOfWork.Belonging.Search(b => b.PaymentID == id && b.AccountID==accountID && b.RecipientID==recipientID && b.OwnerID==ownerAccountID);
            PaymentDetailsModel model = new PaymentDetailsModel();
            model.Payment = unitOfWork.Payment.FindByID(id,accountID,recipientID);
            model.Payment.Categories = belongings;
            model.Categories = unitOfWork.Category.GetAll();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddCategory(BelongingModel request)
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
        [HttpPost]
        public ActionResult DeleteCategory(int id, int accountID, int recipientID, int categoryID, int ownerID)
        {
            Payment payment = unitOfWork.Payment.FindByID(id, accountID, recipientID);
            Belonging belonging = unitOfWork.Belonging.FindByID(categoryID, id, accountID, recipientID,ownerID);
            payment.Categories.Remove(belonging);
            unitOfWork.Commit();
            return RedirectToAction("Edit", "Payment", new { id, accountID, recipientID });

        }

        // GET: PaymentController/Create
        public ActionResult Create(int id)
        {
            List<Category> list = unitOfWork.Category.GetAll();
            List<SelectListItem> categories = list.Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryID.ToString() }).ToList();
            string accountNumber = unitOfWork.Account.Search(a => a.AccountID == id)[0].Number;
            PaymentCreateModel model = new PaymentCreateModel { Categories = categories, AccountNumber=accountNumber, AccountID=id};
            return View(model);
        }

        // POST: PaymentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PaymentCreateModel model)
        {
            int recipientID = unitOfWork.Account.Search(a => a.Number == model.RecipientAccountNumber)[0].AccountID;
            model.Payment.RecipientID = recipientID;
            try
            {
                unitOfWork.Payment.Add(model.Payment);
                unitOfWork.Commit();
                return RedirectToAction("Details", "Account",new { id = model.Payment.AccountID});
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return RedirectToAction("Create");
            }
        }

        // GET: PaymentController/Edit/5
        public ActionResult Edit(int id,int accountID, int recipientID, int ownerAccountID)
        {
            Payment payment = unitOfWork.Payment.FindByID(id, accountID, recipientID);
            payment.Categories = unitOfWork.Belonging.Search(b=> b.PaymentID ==id && b.AccountID==accountID && b.RecipientID==recipientID && b.OwnerID==ownerAccountID);
            List<Category> categories = unitOfWork.Category.GetAll();
            List<SelectListItem> list = categories.Select(c => new SelectListItem { Text = c.Name, Value = c.CategoryID.ToString() }).ToList();

            string accountNum = unitOfWork.Account.FindByID(accountID).Number;
            string recipientNum = unitOfWork.Account.FindByID(recipientID).Number;
            PaymentCreateModel model = new PaymentCreateModel { 
                AccountID = ownerAccountID,
                Payment = payment,
                Categories = list,
                CategoryList=categories,
                AccountNumber=accountNum,
                RecipientAccountNumber=recipientNum
            }; 
            return View(model);
        }

        // POST: PaymentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PaymentCreateModel model)
        {
            try
            {
                unitOfWork.Payment.UpdateCategoryList(model.Payment,model.Payment.Categories);
                unitOfWork.Commit();
                int id = model.AccountID;
                return RedirectToAction("Details", "Account", new {id});
            }
            catch
            {
                return View();
            }
        }

        // GET: PaymentController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PaymentController/Delete/5
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
