/*
 * Description: This file represents the Client Controller class
 * Author: Mtsharif 
 * Date: 4/4/2018
 */

using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.ViewModels;

namespace WorkflowManagementSystem.Controllers
{
    /// <summary>
    /// This client controller is generated based on the Client and ClientViewModel classes 
    /// This controller enables client service employees to add, edit, list, and delete clients.
    /// </summary>
    public class ClientController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action shows a list of all the clients in the database.
        /// </summary>
        /// <returns>Client index view</returns>
        // GET: Client
        public ActionResult Index()
        {
            var clients = db.Clients.ToList();
            var model = new List<ClientViewModel>();

            foreach (var item in clients)
            {
                model.Add(new ClientViewModel
                {
                    ClientId = item.ClientId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Email = item.Email,
                    MobileNumber = item.MobileNumber,
                    Street = item.Street,
                    District = item.District,
                    City = item.City,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action demonstrates the details of a chosen client
        /// </summary>
        /// <param name="id">The id of the chosen client</param>
        /// <returns>Client details view</returns>
        // GET: Client/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            ClientViewModel model = Mapper.Map<Client, ClientViewModel>(client);

            return View(model);
        }

        /// <summary>
        /// This action obtains the create client page.
        /// </summary>
        /// <returns>Create client view</returns>
        // GET: Client/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action allows the creation of new clients.
        /// </summary>
        /// <param name="model">The client model</param>
        /// <returns>Client index view or create model view</returns>
        // POST: Client/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = Mapper.Map<ClientViewModel, Client>(model);

                db.Clients.Add(client);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);

        }

        /// <summary>
        /// This action displays a specific client's information for editing.
        /// </summary>
        /// <param name="id">The id of the chosen client</param>
        /// <returns>Error page or edit client view</returns>
        // GET: Client/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            ClientViewModel model = Mapper.Map<Client, ClientViewModel>(client);

            return View(model);
        }

        /// <summary>
        /// This action allows the editing and saving of a client's information.
        /// </summary>
        /// <param name="model">Client model</param>
        /// <returns>Client index view or edit model view</returns>
        // POST: Client/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ClientViewModel model)
        {
            if (ModelState.IsValid)
            {
                Client client = Mapper.Map<ClientViewModel, Client>(model);

                db.Entry(client).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// This action displays a chosen client's information for deletion.
        /// </summary>
        /// <param name="id">The id of a chosen client</param>
        /// <returns>Error page or client delete view</returns>
        // GET: Client/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            ClientViewModel model = Mapper.Map<ClientViewModel>(client);

            return View(model);
        }

        /// <summary>
        /// This action allows the user to delete a client.
        /// </summary>
        /// <param name="id">The id of the client to be deleted</param>
        /// <returns>Client index view</returns>
        // POST: Client/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Client client = db.Clients.Find(id);

            db.Clients.Remove(client);

            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
