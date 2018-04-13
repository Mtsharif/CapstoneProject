/*
 * Description: This file represents the Task Assignment Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
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
    public class TaskAssignmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: TaskAssignment
        public ActionResult Index()
        {
            var taskAssignments = db.TaskAssignments.ToList();
            var model = new List<TaskAssignmentViewModel>();

            foreach (var item in taskAssignments)
            {
                model.Add(new TaskAssignmentViewModel
                {
                    TaskAssignmentId = item.TaskAssignmentId,
                    AssignmentDate = item.AssignmentDate,
                    EmployeeTask = item.EmployeeTask.Name,
                    AnyEmployee = item.AnyEmployee.FullName,
                    Employee = item.Employee.FullName,
                });
            }

            return View(model);
        }

        // GET: TaskAssignment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);

            if (taskAssignment == null)
            {
                return HttpNotFound();
            }

            var model = new TaskAssignmentViewModel
            {
                TaskAssignmentId = taskAssignment.TaskAssignmentId,
                AssignmentDate = taskAssignment.AssignmentDate,
                EmployeeTask = taskAssignment.EmployeeTask.Name,
                AnyEmployee = taskAssignment.AnyEmployee.FullName,
                Employee = taskAssignment.Employee.FullName,
            };

            return View(model);
        }

        // GET: TaskAssignment/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeTaskId = new SelectList(db.EmployeeTasks, "EmployeeTaskId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        // POST: TaskAssignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taskAssignment = new TaskAssignment
                {
                    AssignmentDate = DateTime.Now,
                    EmployeeTaskId = model.EmployeeTaskId,
                    EmployeeId = model.EmployeeId,
                    ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
                };

                db.TaskAssignments.Add(taskAssignment);
                db.SaveChanges();

                ViewBag.EmployeeTaskId = new SelectList(db.EmployeeTasks, "EmployeeTaskId", "Name");
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.EmployeeTaskId = new SelectList(db.EmployeeTasks, "EmployeeTaskId", "Name");
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                return View();
            }
        }

        // GET: TaskAssignment/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TaskAssignment/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: TaskAssignment/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TaskAssignment/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
