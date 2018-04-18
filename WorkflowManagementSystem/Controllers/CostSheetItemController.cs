///*
// * Description: This file represents the Cost Sheet Controller class
// * Author: Mtsharif 
// * Date: 18/4/2018
// */

//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using WorkflowManagementSystem.Models;
//using WorkflowManagementSystem.ViewModels;

//namespace WorkflowManagementSystem.Controllers
//{
//    /// <summary>
//    /// This controller is generated based on th cost sheet item view model and domain model classes.
//    /// It is used to add items to a cost sheet
//    /// </summary>
//    public class CostSheetItemController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

        
//        // GET: CostSheetItem
//        public ActionResult Index()
//        {
//            var costSheetItems = db.CostSheetItems.ToList();
//            var model = new List<CostSheetItemViewModel>();

//            foreach (var item in costSheetItems)
//            {
//                model.Add(new CostSheetItemViewModel
//                {
//                    ItemId = item.ItemId,
//                    Quantity = item.Quantity,
//                    Item = item.Item.Name + item.Item.UnitCost,
//                });
//            }
//            return View(model);
//        }

        
//        // GET: CostSheetItem/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            CostSheetItem costSheetItem = db.CostSheetItems.Find(id);

//            if (costSheetItem == null)
//            {
//                return HttpNotFound();
//            }

//            var model = new CostSheetItemViewModel
//            {
//                CostSheetId = costSheetItem.CostSheetId,
//                ItemId = costSheetItem.ItemId,
//                Quantity = costSheetItem.Quantity,
//                Item = costSheetItem.Item.Name + costSheetItem.Item.UnitCost,                
//                CostSheet = costSheetItem.CostSheet.Name
//            };

//            return View(model);
//        }

//        /// <summary>
//        /// This action retrives the create page to add items in a cost sheet
//        /// </summary>
//        /// <returns>Create view</returns>
//        // GET: CostSheetItem/Create
//        public ActionResult Create()
//        {
//            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", "UnitCost");
//            //ViewBag.ItemId = new SelectList(db.Items, "ItemId", "UnitCost");
//            ViewBag.CostSheetId = new SelectList(db.CostSheets, "CostSheetId", "Name");
//            return View();
//        }

//        /// <summary>
//        /// This action allows the addition of items to a cost sheet 
//        /// </summary>
//        /// <param name="model">Cost sheet item model</param>
//        /// <returns>Index view</returns>
//        // POST: CostSheetItem/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(CostSheetItemViewModel model)
//        {
//            if (ModelState.IsValid)
//            {                
//                    var costSheetItem = new CostSheetItem
//                    {
//                        Quantity = model.Quantity,
//                        ItemId = model.ItemId,
//                        CostSheetId = model.CostSheetId
//                    };

//                //var cost = db.CostSheetItems.Where(m => m.CostSheetId == model.CostSheetId && m.ItemId == model.ItemId);

//                    db.CostSheetItems.Add(costSheetItem);
//                    db.SaveChanges();                

//                ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name");
                
//                ViewBag.CostSheetId = new SelectList(db.CostSheets, "CostSheetId", "Name");

//                return RedirectToAction("Index");
//                //return RedirectToAction("Details", new { id = costSheetItem.CostSheetId, costSheetItem.ItemId });
//            }
//            else
//            {
//                ViewBag.ItemId = new SelectList(db.Items, "ItemId", "Name", "UnitCost");
//                //ViewBag.ItemId = new SelectList(db.Items, "ItemId", "UnitCost");
//                ViewBag.CostSheetId = new SelectList(db.CostSheets, "CostSheetId", "Name");

//                return View();
//            }
//        }

//        /// <summary>
//        /// This action retrieves the edit page
//        /// </summary>
//        /// <param name="id">Cost sheet item id</param>
//        /// <returns>Edit view</returns>
//        // GET: CostSheetItem/Edit/5
//        public ActionResult Edit(int id)
//        {
//            return View();
//        }


//        // POST: CostSheetItem/Edit/5
//        [HttpPost]
//        public ActionResult Edit(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add update logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }

//        // GET: CostSheetItem/Delete/5
//        public ActionResult Delete(int id)
//        {
//            return View();
//        }

//        // POST: CostSheetItem/Delete/5
//        [HttpPost]
//        public ActionResult Delete(int id, FormCollection collection)
//        {
//            try
//            {
//                // TODO: Add delete logic here

//                return RedirectToAction("Index");
//            }
//            catch
//            {
//                return View();
//            }
//        }
//    }
//}
