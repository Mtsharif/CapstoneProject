/*
 * Description: This file represents the Item Controller class
 * Author: Mtsharif 
 * Date: 20/3/2018
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
    /// The Item controller is generated based on the Item domain model and Item View Model classes.
    /// This controller manages items by allowing the admin to add new items, list them, and edit and delete them.
    /// Only the admin is allowed to access this controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action demonstrates the list of all items available. 
        /// </summary>
        /// <returns>Item index view</returns>
        // GET: Item
        public ActionResult Index()
        {
            var items = db.Items.ToList();
            var model = new List<ItemViewModel>();
            foreach (var item in items)
            {
                model.Add(new ItemViewModel
                {
                    ItemId = item.ItemId,
                    Name = item.Name,
                    Description = item.Description,
                    UnitCost = item.UnitCost,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action retrieves the create new item page
        /// </summary>
        /// <returns>Create item view</returns>
        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// This action allows the admin to create new items and save them.
        /// </summary>
        /// <param name="model">Item model</param>
        /// <returns>Item index view or create model view</returns>
        // POST: Item/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                Item item = Mapper.Map<ItemViewModel, Item>(model);

                db.Items.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// This action retrieves an item's information to edit them.
        /// It checks if the id and item are available in the database.
        /// </summary>
        /// <param name="id">The id of the selected item</param>
        /// <returns>Error page or edit item view</returns>
        // GET: Item/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            ItemViewModel model = Mapper.Map<Item, ItemViewModel>(item);

            return View(model);
        }

        /// <summary>
        /// This action allows changes to be made to the item and saves the changes.
        /// </summary>
        /// <param name="model">Item model</param>
        /// <returns>Item index view or edit model view</returns>
        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {          
                Item item = Mapper.Map<ItemViewModel, Item>(model);
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(model);
        }

        /// <summary>
        /// This action retrieves an item to be deleted by the admin.
        /// It checks if the id and item are available in the database.
        /// </summary>
        /// <param name="id">The id of the chosen item</param>
        /// <returns>Error page or item delete view</returns>
        // GET: Item/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Item item = db.Items.Find(id);

            if (item == null)
            {
                return HttpNotFound();
            }

            ItemViewModel model = Mapper.Map<ItemViewModel>(item);

            return View(model);
        }

        /// <summary>
        /// This action allows the admin to delete an item
        /// </summary>
        /// <param name="id">The id of the item to be deleted</param>
        /// <returns>Item index view</returns>
        // POST: Item/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            Item item = db.Items.Find(id);

            db.Items.Remove(item);
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
