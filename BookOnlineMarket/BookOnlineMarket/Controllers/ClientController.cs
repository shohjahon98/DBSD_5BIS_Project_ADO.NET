using BookOnlineMarket.Models;
using BookOnlineMarket.Models.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookOnlineMarket.Controllers
{
    public class ClientController : Controller
    {
        ClientRepository _client = new ClientRepository();
        // GET: Client
        public ActionResult Index()
        {
            IEnumerable<Client> clients = _client.GetAllClient();
            return View(clients);
        }

        // GET: Client/Details/5
        public ActionResult Details(int id)
        {
            return View(_client.GetAllClient().Find(Client => Client.Id == id));
        }

        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Client/Create
        [HttpPost]
        public ActionResult Create(Client client)
        {
            try
            {
                // TODO: Add insert logic here
                _client.AddClient(client);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_client.GetAllClient().Find(Client => Client.Id == id));
        }

        // POST: Client/Edit/5
        [HttpPost]
        public ActionResult Edit(Client client)
        {
            try
            {
                // TODO: Add update logic here
                _client.UpdateClient(client);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Client/Delete/5
        public ActionResult Delete(int id)
        {
            _client.DeleteClient(id);
            return RedirectToAction("Index");
        }
       
    }
}
