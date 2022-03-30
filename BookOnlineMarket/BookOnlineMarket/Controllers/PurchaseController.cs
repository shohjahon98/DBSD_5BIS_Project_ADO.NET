using BookOnlineMarket.Models;
using BookOnlineMarket.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookOnlineMarket.Controllers
{
    public class PurchaseController : Controller
    {
        PurchaseRepository _purchase = new PurchaseRepository();
        // GET: Purchase
        public ActionResult Index()
        {
            IEnumerable<Purchase> purchase = _purchase.GetAllPurchase();
            return View(purchase);
        }

        // GET: Purchase/Details/5
        public ActionResult Details(int id)
        {
            return View(_purchase.GetAllPurchase().Find(purchase=>purchase.Id==id));
        }

        // GET: Purchase/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Purchase/Create
        [HttpPost]
        public ActionResult Create(Purchase purchase)
        {
            try
            {
                // TODO: Add insert logic here
                _purchase.AddPurchase(purchase);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Purchase/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_purchase.GetAllPurchase().Find(purchase => purchase.Id == id));
        }

        // POST: Purchase/Edit/5
        [HttpPost]
        public ActionResult Edit(Purchase purchase)
        {
            try
            {
                purchase.PurchaseDate = _purchase.GetAllPurchase().Find(purchas => purchas.Id == purchase.Id).PurchaseDate;
                _purchase.UpdatePurchase(purchase);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Delete(int id)
        {
            try
            {
                // TODO: Add delete logic here
                _purchase.DeletePurchase(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
