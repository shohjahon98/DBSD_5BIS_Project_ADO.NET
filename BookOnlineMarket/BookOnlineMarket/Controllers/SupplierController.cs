using BookOnlineMarket.Models;
using BookOnlineMarket.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookOnlineMarket.Controllers
{
    public class SupplierController : Controller
    {
        SupplierRepository _supplier = new SupplierRepository();
        // GET: Supplier
        public ActionResult Index()
        {
            IEnumerable<Supplier> suppliers = _supplier.GetAllSupplier();
            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            return View(_supplier.GetAllSupplier().Find(Supplier => Supplier.Id == id));
        }

        // GET: Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            try
            {
                // TODO: Add insert logic here
                _supplier.AddSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_supplier.GetAllSupplier().Find(Supplier => Supplier.Id == id));
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(Supplier supplier)
        {
            try
            {
                // TODO: Add update logic here
                _supplier.UpdateSupplier(supplier);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                _supplier.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
