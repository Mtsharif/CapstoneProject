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
    /// 
    /// </summary>
    [Authorize(Roles = "Client Service Employee")]
    public class CostSheetController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: CostSheet
        public ActionResult Index()
        {
            var costSheets = db.CostSheets.ToList();
            var model = new List<CostSheetViewModel>();

            foreach (var item in costSheets)
            {
                model.Add(new CostSheetViewModel
                {
                    CostSheetId = item.CostSheetId,
                    Name = item.Name,
                    Status = item.Status,
                    ProductionEmployee = item.ProductionEmployee.FullName,
                    EventProject = item.EventProject.Name
                });
            }
            return View(model);
        }

        // GET: CostSheet/Details/5
        public ActionResult Details(int? id)
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
                CostSheetId = costSheet.EventProjectId,
                Name = costSheet.Name,
                Status = costSheet.Status,
                ProductionEmployee = costSheet.ProductionEmployee.FullName,
                EventProject = costSheet.EventProject.Name
            };

            return View(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        // GET: CostSheet/Create
        public ActionResult Create()
        {
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: CostSheet/Create
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Create(CostSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var costSheet = new CostSheet
                {
                    Name = model.Name,
                    Status = model.Status,
                    ProductionEmployeeId = User.Identity.GetUserId<int>(),
                    FinanceEmployeeId = User.Identity.GetUserId<int>(),
                    CEOEmployeeId = User.Identity.GetUserId<int>(),
                    EventProjectId = model.EventProjectId,                    
                };

                db.CostSheets.Add(costSheet);
                db.SaveChanges();

                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

                return View();
            }
        }

        // GET: CostSheet/Edit/5
        public ActionResult Edit(int? id)
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
                CostSheetId = costSheet.EventProjectId,
                Status = costSheet.Status,
                ProductionEmployee = costSheet.ProductionEmployee.FullName,
                EventProject = costSheet.EventProject.Name
            };

            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        // POST: CostSheet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CostSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                CostSheet costSheet = Mapper.Map<CostSheetViewModel, CostSheet>(model);
                db.Entry(costSheet).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        // GET: CostSheet/Delete/5
        public ActionResult Delete(int? id)
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

            CostSheetViewModel model = new CostSheetViewModel
            {
                CostSheetId = costSheet.EventProjectId,
                Name = costSheet.Name,
                Status = costSheet.Status,        
                ProductionEmployee = costSheet.ProductionEmployee.FullName,
                EventProject = costSheet.EventProject.Name
            };

            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        // POST: CostSheet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CostSheet costSheet = db.CostSheets.Find(id);
            db.CostSheets.Remove(costSheet);
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
