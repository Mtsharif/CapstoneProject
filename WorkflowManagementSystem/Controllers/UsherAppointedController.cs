/*
 * Description: This file represents the Usher Appointed Controller class
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
    /// This controller is created based on the UsherAppointedViewModel and UsherAppointed classes.
    /// It allows the user to assign ushers to projects as well as edit and delete the assignment.
    /// </summary>
    [Authorize(Roles = "Production Employee")]
    public class UsherAppointedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action retrieves a list of appointed ushers.
        /// </summary>
        /// <returns>Index view</returns>
        // GET: UsherAppointed
        public ActionResult Index()
        {
            var usherAppointeds = db.UsherAppointeds.ToList();
            var model = new List<UsherAppointedViewModel>();

            foreach (var item in usherAppointeds)
            {
                model.Add(new UsherAppointedViewModel
                {
                    UsherAppointedId = item.UsherAppointedId,
                    DateAppointed = item.DateAppointed,
                    Usher = item.Usher.FullName,
                    EventProject = item.EventProject.Name,
                    Employee = item.Employee.FullName,
                });
            }

            return View(model);
        }

       /// <summary>
       /// This action retrieves the details of an usher assignment
       /// </summary>
       /// <param name="id">Usher appointed id</param>
       /// <returns>Details view</returns>
        // GET: UsherAppointed/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherAppointed usherAppointed = db.UsherAppointeds.Find(id);

            if (usherAppointed == null)
            {
                return HttpNotFound();
            }

            var model = new UsherAppointedViewModel
            {
                UsherAppointedId = usherAppointed.UsherAppointedId,
                DateAppointed = usherAppointed.DateAppointed,
                Usher = usherAppointed.Usher.FullName,
                EventProject = usherAppointed.EventProject.Name,
                Employee = usherAppointed.Employee.FullName,
            };

            return View(model);
        }

        /// <summary>
        /// This action obtains the usher assignment page 
        /// </summary>
        /// <returns>Usher appointed create view</returns>
        // GET: UsherAppointed/Create
        public ActionResult Create()
        {
            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View();
        }

        /// <summary>
        /// This action enables the user to assign ushers to projects
        /// </summary>
        /// <param name="model">Usher appointed model</param>
        /// <returns>Index view or create assignment view</returns>
        // POST: UsherAppointed/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsherAppointedViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usherAppointed = new UsherAppointed
                {                    
                    DateAppointed = DateTime.Now,
                    UsherId = model.UsherId,
                    EventProjectId = model.EventProjectId,
                    ProductionEmployeeId = User.Identity.GetUserId<int>(),
                };

                db.UsherAppointeds.Add(usherAppointed);
                db.SaveChanges();

                ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

                return RedirectToAction("Details", new { id = usherAppointed.UsherAppointedId });
            }
            else
            {
                ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
                return View();
            }
        }

        /// <summary>
        /// This action gets the usher appointed edit page
        /// </summary>
        /// <param name="id">Usher appointed id</param>
        /// <returns>Edit usher assignment view</returns>
        // GET: UsherAppointed/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherAppointed usherAppointed = db.UsherAppointeds.Find(id);

            if (usherAppointed == null)
            {
                return HttpNotFound();
            }

            var model = new UsherAppointedViewModel
            {
                UsherAppointedId = usherAppointed.UsherAppointedId,
                DateAppointed = DateTime.Now,
                Usher = usherAppointed.Usher.FullName,
                EventProject = usherAppointed.EventProject.Name,
                Employee = usherAppointed.Employee.FullName,
            };

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        /// <summary>
        /// This action enables an usher appointment to be edited
        /// </summary>
        /// <param name="model">Usher appointed model</param>
        /// <returns>Index view or edit view</returns>
        // POST: UsherAppointed/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsherAppointedViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsherAppointed usherAppointed = Mapper.Map<UsherAppointedViewModel, UsherAppointed>(model);
                db.Entry(usherAppointed).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        /// <summary>
        /// This action retrieves the deletion page
        /// </summary>
        /// <param name="id">Usher appointed id</param>
        /// <returns>Delete view or error view</returns>
        // GET: UsherAppointed/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherAppointed usherAppointed = db.UsherAppointeds.Find(id);

            if (usherAppointed == null)
            {
                return HttpNotFound();
            }

            UsherAppointedViewModel model = new UsherAppointedViewModel
            {
                EventProjectId = usherAppointed.EventProjectId,
                DateAppointed = usherAppointed.DateAppointed,
                Usher = usherAppointed.Usher.FullName,
                EventProject = usherAppointed.EventProject.Name,
                Employee = usherAppointed.Employee.FullName,
            };

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        /// <summary>
        /// This action allows the deletion of an usher assignment
        /// </summary>
        /// <param name="id">Usher appointed id</param>
        /// <returns>Index view</returns>
        // POST: UsherAppointed/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsherAppointed usherAppointed = db.UsherAppointeds.Find(id);
            db.UsherAppointeds.Remove(usherAppointed);
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
