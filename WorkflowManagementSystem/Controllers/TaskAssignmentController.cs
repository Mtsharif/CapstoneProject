﻿/*
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
    /// This controller is generated based on the task assignemnt domain and view models.
    /// It allows tasks to be assigned to employees in a project.
    /// </summary>
    [Authorize(Roles = "Client Service Employee")]
    public class TaskAssignmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action retrieves a list of all tasks assigned
        /// </summary>
        /// <returns>Index view</returns>
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
                    TaskName = item.TaskName,
                    Description = item.Description,
                    Deadline = item.Deadline,
                    Status = item.Status,
                    Priority = item.Priority,
                    AssignmentDate = item.AssignmentDate,
                    IsCompleted = item.IsCompleted,
                    EventProject = item.EventProject.Name,
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
                TaskName = taskAssignment.TaskName,
                Description = taskAssignment.Description,
                Deadline = taskAssignment.Deadline,
                Status = taskAssignment.Status,
                Priority = taskAssignment.Priority,
                AssignmentDate = taskAssignment.AssignmentDate,
                IsCompleted = taskAssignment.IsCompleted,
                //EmployeeTask = taskAssignment.EmployeeTask.TaskName,
                EventProject = taskAssignment.EventProject.Name,
                AnyEmployee = taskAssignment.AnyEmployee.FullName,
                Employee = taskAssignment.Employee.FullName,
            };

            return View(model);
        }

        /// <summary>
        /// This action gets the task assignment create page
        /// </summary>
        /// <returns>Create view</returns>
        // GET: TaskAssignment/Create
        public ActionResult Create()
        {
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View();
        }

        /// <summary>
        /// This action enables the assignment of tasks to employees
        /// </summary>
        /// <param name="model">Task assignment model</param>
        /// <returns>Index view</returns>
        // POST: TaskAssignment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TaskAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taskAssignment = new TaskAssignment
                {
                    TaskName = model.TaskName,
                    Description = model.Description,
                    Deadline = model.Deadline,
                    Status = TaskAssignment.TaskStatus.Pending,
                    Priority = model.Priority,
                    AssignmentDate = DateTime.Now,
                    IsCompleted = model.IsCompleted,
                    EmployeeId = model.EmployeeId,
                    ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
                    EventProjectId = model.EventProjectId,
                };

                db.TaskAssignments.Add(taskAssignment);
                db.SaveChanges();

                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
                ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

                return RedirectToAction("Index");
                //return RedirectToAction("Details", new { id = taskAssignment.TaskAssignmentId });
            }
            else
            {
                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
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
