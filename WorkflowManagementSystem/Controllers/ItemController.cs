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
    public class ItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

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

        // GET: Item/Details/5
        public ActionResult Details(int? id)
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

        // GET: Item/Create
        public ActionResult Create()
        {
            return View();
        }

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

        // POST: Item/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                //NOTE: to use the automapper, the property names of the model and viewmodel should be the same; otherwise use manual copy of the properties like in the employee controller.
                
                Item item = Mapper.Map<ItemViewModel, Item>(model);
                db.Entry(item).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }

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

        // POST: Item/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, FormCollection collection)
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
