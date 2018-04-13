/*
 * Description: This file represents the Employee Task Controller class
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
    public class EmployeeTaskController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EmployeeTask
        public ActionResult Index()
        {
            var employeeTasks = db.EmployeeTasks.ToList();
            var model = new List<EmployeeTaskViewModel>();

            foreach (var item in employeeTasks)
            {
                model.Add(new EmployeeTaskViewModel
                {
                    EmployeeTaskId = item.EmployeeTaskId,
                    Name = item.Name,
                    Description = item.Description,
                    Deadline = item.Deadline,
                    Status = item.Status,
                    Priority = item.Priority,
                    EventProject = item.EventProject.Name,
                    Employee = item.Employee.FullName,
                });
            }

            return View(model);
        }

        // GET: EmployeeTask/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

            if (employeeTask == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeTaskViewModel
            {
                EmployeeTaskId = employeeTask.EmployeeTaskId,
                Name = employeeTask.Name,
                Description = employeeTask.Description,
                Deadline = employeeTask.Deadline,
                Status = employeeTask.Status,
                Priority = employeeTask.Priority,
                EventProject = employeeTask.EventProject.Name,
                Employee = employeeTask.Employee.FullName,
            };

            return View(model);
        }

        // GET: EmployeeTask/Create
        public ActionResult Create()
        {
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        // POST: EmployeeTask/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employeeTask = new EmployeeTask
                {
                    Name = model.Name,
                    Description = model.Description,
                    Deadline = model.Deadline,
                    Status = model.Status,
                    Priority = model.Priority,
                    EventProjectId = model.EventProjectId,
                    ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
                };

                db.EmployeeTasks.Add(employeeTask);
                db.SaveChanges();

                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                return View();
            }
        }

        // GET: EmployeeTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

            if (employeeTask == null)
            {
                return HttpNotFound();
            }

            var model = new EmployeeTaskViewModel
            {
                EmployeeTaskId = employeeTask.EmployeeTaskId,
                Name = employeeTask.Name,
                Description = employeeTask.Description,
                Deadline = employeeTask.Deadline,
                Status = employeeTask.Status,
                Priority = employeeTask.Priority,
                EventProject = employeeTask.EventProject.Name,
                Employee = employeeTask.Employee.FullName,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        // POST: EmployeeTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeTaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                EmployeeTask employeeTask = Mapper.Map<EmployeeTaskViewModel, EmployeeTask>(model);
                db.Entry(employeeTask).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        // GET: EmployeeTask/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

            if (employeeTask == null)
            {
                return HttpNotFound();
            }

            EmployeeTaskViewModel model = new EmployeeTaskViewModel
            {
                EmployeeTaskId = employeeTask.EmployeeTaskId,
                Name = employeeTask.Name,
                Description = employeeTask.Description,
                Deadline = employeeTask.Deadline,
                Status = employeeTask.Status,
                Priority = employeeTask.Priority,
                EventProject = employeeTask.EventProject.Name,
                Employee = employeeTask.Employee.FullName,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        // POST: EmployeeTask/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);
            db.EmployeeTasks.Remove(employeeTask);
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
