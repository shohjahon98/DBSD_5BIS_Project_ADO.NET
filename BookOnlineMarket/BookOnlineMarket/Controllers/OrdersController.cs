using BookOnlineMarket.Models;
using BookOnlineMarket.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookOnlineMarket.Controllers
{
    public class OrdersController : Controller
    {
        OrdersRepository _orders = new OrdersRepository();
        // GET: Orders
        public ActionResult Index()
        {
            IEnumerable<Orders> orders = _orders.GetAllOrders();
            return View(orders);
        }

        // GET: Orders/Details/5
        public ActionResult Details(int id)
        {
            return View(_orders.GetAllOrders().Find(orders=>orders.Id==id));
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Orders/Create
        [HttpPost]
        public ActionResult Create(Orders orders)
        {
            try
            {
                // TODO: Add insert logic here
                _orders.AddOrders(orders);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_orders.GetAllOrders().Find(orders => orders.Id == id));
        }

        // POST: Orders/Edit/5
        [HttpPost]
        public ActionResult Edit(Orders orders)
        {
            try
            {
                
                orders.OrderDate = _orders.GetAllOrders().Find(order => order.Id == orders.Id).OrderDate;
                _orders.UpdateOrders(orders);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int id)
        {
            _orders.DeleteOrders(id);
            return RedirectToAction("Index");
        }
        

        
    }
}
