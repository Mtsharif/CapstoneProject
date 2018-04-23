/*
 * Description: This file represents the Cost Sheet Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using AutoMapper;
using Microsoft.AspNet.Identity;
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
    /// This controller is created based on the cost sheet view model and domain model classes 
    /// It allows the creation of a cost sheet as well as its editing, listing, and deletion. 
    /// </summary>
    [Authorize]
    public class CostSheetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action gets a page that includes a cost sheet's details
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <returns>cost sheet items view</returns>
        public ActionResult CostSheetItems(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CostSheet costSheet = db.CostSheets.Find(id);

            if (costSheet == null)
            {
                return HttpNotFound();
            }

            var model = new CostSheetViewModel
            {
                CostSheetId = costSheet.CostSheetId,
                CostSheetName = costSheet.CostSheetName,
                Status = costSheet.Status,
                ProductionEmployee = costSheet.ProductionEmployee.FullName,
            };

            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemPrice");

            return View(model);
        }

        /// <summary>
        /// This action gets the items added in a cost sheet
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <returns>get cost sheet items partial view</returns>
        public ActionResult GetCostSheetItemsPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var costSheetItems = db.CostSheetItems.Where(c => c.CostSheetId == id).ToList();

            var costPerItem = db.CostSheetItems.Where(p => p.CostSheetId == id).Select(p => p.Quantity * p.Item.UnitCost).ToList();
            var total = costPerItem.Sum();
            //var total = db.CostSheetItems.Where(p => p.CostSheetId == id).Select(p => p.Quantity * p.Item.UnitCost).Sum();

            var model = new List<CostSheetItemViewModel>();
            foreach (var costSheetItem in costSheetItems)
            {
                model.Add(new CostSheetItemViewModel
                {
                    CostSheetId = costSheetItem.CostSheetId,
                    ItemId = costSheetItem.ItemId,
                    Quantity = costSheetItem.Quantity,
                    Item = costSheetItem.Item.ItemPrice,                  
                });
            }
            ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemPrice");
            ViewBag.CostPerItem = costPerItem;
            ViewBag.Total = total;

            return PartialView(model);
        }

        /// <summary>
        /// This action enables the addition of items in a cost sheet
        /// </summary>
        /// <param name="model">cost sheet item view model</param>
        /// <returns>Add cost sheet item partial view</returns>
        [Authorize(Roles = "Production Employee")]
        [HttpPost]
        public ActionResult AddCostSheetItemPartial(CostSheetItemViewModel model)
        {

        if (ModelState.IsValid)
        {
            var costSheetItem = new CostSheetItem
            {
                Quantity = model.Quantity,
                CostSheetId = model.CostSheetId,
                ItemId = model.ItemId
            };

             ViewBag.ItemId = new SelectList(db.Items, "ItemId", "ItemPrice");

            db.CostSheetItems.Add(costSheetItem);
            db.SaveChanges();

            return PartialView();
        }

        return PartialView(model);
       
    }

        /// <summary>
        /// This action retrieves the finance approval page
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <returns>Add finance approval view</returns>
        // GET: CostSheet/AddFinanceApproval
        //[Authorize(Roles = "Finance Employee")]
        public ActionResult AddFinanceApproval(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            CostSheet costSheet = db.CostSheets.Find(id);

            if (costSheet == null)
            {
                return View("Error");
            }

            var model = new CostSheetViewModel
            {
                CostSheetId = costSheet.CostSheetId,
                CostSheetName = costSheet.CostSheetName,
                Status = costSheet.Status,
                FinanceEmployee = costSheet.FinanceEmployee.FullName,
                FinanceEmployeeDecision = costSheet.FinanceEmployeeDecision,
                FinanceEmployeeFeedback = costSheet.FinanceEmployeeFeedback,                
            };

            ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action allows finance employees to approve a cost sheet
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <param name="model"></param>
        /// <returns>cost sheet items view</returns>
        // POST: CostSheet/AddFinanceApproval
        //[Authorize(Roles = "Finance Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddFinanceApproval(int id, CostSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var costSheet = db.CostSheets.Find(id);

                if (costSheet == null)
                {
                    return HttpNotFound();
                }

                if (model.FinanceEmployeeDecision == CostSheetFinanceDecision.Approved)
                {
                    model.Status = CostSheetStatus.FinanceApproved;
                }
                else if (model.FinanceEmployeeDecision == CostSheetFinanceDecision.Rejected)
                {
                    model.Status = CostSheetStatus.FinanceRejected;
                }

                costSheet.FinanceEmployeeId = User.Identity.GetUserId<int>();
                costSheet.Status = model.Status;
                costSheet.FinanceEmployeeDecision = model.FinanceEmployeeDecision;
                costSheet.FinanceEmployeeFeedback = model.FinanceEmployeeFeedback;

                db.Entry(costSheet).State = EntityState.Modified;
                db.SaveChanges();
               
                return RedirectToAction("CostSheetItems", "CostSheet", new { id = costSheet.CostSheetId });
            }

            ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View();
        }

        /// <summary>
        /// Thi action gets the CEO approval page
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <returns>Add CEO Approval view</returns>
        // GET: CostSheet/AddCEOApproval
        //[Authorize(Roles = "CEO")]
        public ActionResult AddCEOApproval(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            CostSheet costSheet = db.CostSheets.Find(id);

            if (costSheet == null)
            {
                return View("Error");
            }

            var model = new CostSheetViewModel
            {
                CostSheetId = costSheet.CostSheetId,
                CostSheetName = costSheet.CostSheetName,
                Status = costSheet.Status,
                Employee = costSheet.Employee.FullName,
                CEODecision = costSheet.CEODecision,
                CEOFeedback = costSheet.CEOFeedback,
            };

            ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action enables the CEO to approve a cost sheet
        /// </summary>
        /// <param name="id">cost sheet id</param>
        /// <param name="model"></param>
        /// <returns>cost sheet items view</returns>
        // POST: CostSheet/AddCEOApproval
        //[Authorize(Roles = "CEO")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCEOApproval(int id, CostSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var costSheet = db.CostSheets.Find(id);

                if (costSheet == null)
                {
                    return HttpNotFound();
                }

                if (model.CEODecision == CostSheetCEODecision.Approved)
                {
                    model.Status = CostSheetStatus.CEOApproved;
                }
                else if (model.CEODecision == CostSheetCEODecision.Rejected)
                {
                    model.Status = CostSheetStatus.CEORejected;
                }

                costSheet.CEOEmployeeId = User.Identity.GetUserId<int>();
                costSheet.Status = model.Status;
                costSheet.CEODecision = model.CEODecision;
                costSheet.CEOFeedback = model.CEOFeedback;

                db.Entry(costSheet).State = EntityState.Modified;
                db.SaveChanges();               

                return RedirectToAction("CostSheetItems", "CostSheet", new { id = costSheet.CostSheetId });
            }

            ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View();
        }

        public ActionResult ApprovalDetailsMaster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            return View();
        }

        /// <summary>
        /// This action is for retrieving finance employee's approval's details
        /// </summary>
        /// <param name="id">document id</param>
        /// <returns>finance approval details view</returns>
        // GET: CostSheet/FinanceApprovalDetails
        //[Authorize(Roles = "Finance Employee, CEO, ProductionEmployee")]
        public ActionResult FinanceApprovalDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CostSheet costSheet = db.CostSheets.Find(id);

            if (costSheet == null)
            {
                return HttpNotFound();
            }

            var model = new CostSheetViewModel
            {
                CostSheetId = costSheet.CostSheetId,
                CostSheetName = costSheet.CostSheetName,
                Status = costSheet.Status,
                EventProject = costSheet.EventProject.Name,
                FinanceEmployee = costSheet.FinanceEmployee.FullName,
                FinanceEmployeeDecision = costSheet.FinanceEmployeeDecision,
                FinanceEmployeeFeedback = costSheet.FinanceEmployeeFeedback,
            };

            ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "FullName");

            return View(model);
        }


        /// <summary>
        /// This action is for retrieving CEO approval's details
        /// </summary>
        /// <param name="id">document id</param>
        /// <returns>finance approval details view</returns>
        // GET: CostSheet/CEOeApprovalDetails
        //[Authorize(Roles = "Finance Employee, CEO, ProductionEmployee")]
        public ActionResult CEOApprovalDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            CostSheet costSheet = db.CostSheets.Find(id);

            if (costSheet == null)
            {
                return HttpNotFound();
            }

            var model = new CostSheetViewModel
            {
                CostSheetId = costSheet.CostSheetId,
                CostSheetName = costSheet.CostSheetName,
                Status = costSheet.Status,
                EventProject = costSheet.EventProject.Name,
                Employee = costSheet.Employee.FullName,
                CEODecision = costSheet.CEODecision,
                CEOFeedback = costSheet.CEOFeedback,
            };

            ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "FullName");

            return View(model);
        }


        ///// <summary>
        ///// This action shows a list of all cost sheets created
        ///// </summary>
        ///// <returns>Index view</returns>
        //// GET: CostSheet
        //public ActionResult Index()
        //{
        //    var costSheets = db.CostSheets.ToList();
        //    var model = new List<CostSheetViewModel>();

        //    foreach (var item in costSheets)
        //    {
        //        model.Add(new CostSheetViewModel
        //        {
        //            CostSheetId = item.CostSheetId,
        //            Name = item.Name,
        //            Status = item.Status,
        //            ProductionEmployee = item.ProductionEmployee.FullName,
        //            EventProject = item.EventProject.Name
        //        });
        //    }
        //    return View(model);
        //}

        ///// <summary>
        ///// This action retrieves the details page of a cost sheet
        ///// </summary>
        ///// <param name="id">Cost sheet id</param>
        ///// <returns>Details view</returns>
        //// GET: CostSheet/Details/5
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CostSheet costSheet = db.CostSheets.Find(id);

        //    if (costSheet == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = new CostSheetViewModel
        //    {
        //        CostSheetId = costSheet.EventProjectId,
        //        Name = costSheet.Name,
        //        Status = costSheet.Status,
        //        ProductionEmployee = costSheet.ProductionEmployee.FullName,
        //        EventProject = costSheet.EventProject.Name
        //    };

        //    return View(model);
        //}

        ///// <summary>
        ///// This action retrieves the create cost sheet page
        ///// </summary>
        ///// <returns>Create view</returns>
        //// GET: CostSheet/Create
        //public ActionResult Create()
        //{
        //    ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View();
        //}

        ///// <summary>
        ///// This action enables the creation of a cost sheet
        ///// </summary>
        ///// <param name="model">Cost Sheet model</param>
        ///// <returns>Index view</returns>
        //// POST: CostSheet/Create
        //[ValidateAntiForgeryToken]
        //[HttpPost]
        //public ActionResult Create(CostSheetViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var costSheet = new CostSheet
        //        {
        //            Name = model.Name,
        //            Status = model.Status,
        //            ProductionEmployeeId = User.Identity.GetUserId<int>(),
        //            FinanceEmployeeId = User.Identity.GetUserId<int>(),
        //            CEOEmployeeId = User.Identity.GetUserId<int>(),
        //            EventProjectId = model.EventProjectId,
        //        };

        //        db.CostSheets.Add(costSheet);
        //        db.SaveChanges();

        //        ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //        return View();
        //    }
        //}

        ///// <summary>
        ///// This action retrieves the cost sheet edit page
        ///// </summary>
        ///// <param name="id">Cost sheet id</param>
        ///// <returns>Edit view</returns>
        //// GET: CostSheet/Edit/5
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CostSheet costSheet = db.CostSheets.Find(id);

        //    if (costSheet == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = new CostSheetViewModel
        //    {
        //        CostSheetId = costSheet.EventProjectId,
        //        Status = costSheet.Status,
        //        ProductionEmployee = costSheet.ProductionEmployee.FullName,
        //        EventProject = costSheet.EventProject.Name
        //    };

        //    ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View(model);
        //}

        ///// <summary>
        ///// This action allows the user to edit the cost sheet
        ///// </summary>
        ///// <param name="model">Cost sheet model</param>
        ///// <returns>Index view</returns>
        //// POST: CostSheet/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(CostSheetViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        CostSheet costSheet = Mapper.Map<CostSheetViewModel, CostSheet>(model);
        //        db.Entry(costSheet).State = EntityState.Modified;
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View(model);
        //}

        ///// <summary>
        ///// This action retrieves the cost sheet delete page
        ///// </summary>
        ///// <param name="id">Cost sheet id</param>
        ///// <returns>Delete view</returns>
        //// GET: CostSheet/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    CostSheet costSheet = db.CostSheets.Find(id);

        //    if (costSheet == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    CostSheetViewModel model = new CostSheetViewModel
        //    {
        //        CostSheetId = costSheet.EventProjectId,
        //        Name = costSheet.Name,
        //        Status = costSheet.Status,
        //        ProductionEmployee = costSheet.ProductionEmployee.FullName,
        //        EventProject = costSheet.EventProject.Name
        //    };

        //    ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View(model);
        //}

        ///// <summary>
        ///// This action enables the deletion of a cost sheet
        ///// </summary>
        ///// <param name="id">Cost sheet id</param>
        ///// <returns>Index view</returns>
        //// POST: CostSheet/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    CostSheet costSheet = db.CostSheets.Find(id);
        //    db.CostSheets.Remove(costSheet);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
