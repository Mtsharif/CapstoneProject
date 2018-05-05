/*
 * Description: This file represents the Task Assignment Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

//using Microsoft.AspNet.Identity;
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
    /// This controller is generated based on the task assignemnt domain and view models.
    /// It allows tasks to be assigned to employees in a project.
    /// </summary>
    [Authorize]
    public class TaskAssignmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action retrieves a list of all tasks assigned to the logged in user 
        /// </summary>
        /// <returns>Index view</returns>
        // GET: TaskAssignment
        public ActionResult Index()
        {
            //var taskAssignments = db.TaskAssignments.ToList();
            var loginId = User.Identity.GetUserId<int>();
            var taskAssignments = db.TaskAssignments.Where(p => p.EmployeeId == loginId).ToList();
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

        /// <summary>
        /// Retrieves a list of the tasks that are assigned by the logged in user to edit and delete the tasks
        /// </summary>
        /// <returns>All tasks index view</returns>
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult AllTasksIndex()
        {
            var loginId = User.Identity.GetUserId<int>();
            var taskAssignments = db.TaskAssignments.Where(p => p.ClientServiceEmployeeId == loginId).ToList();
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

        /// <summary>
        /// This action gets the a task assignment's details
        /// </summary>
        /// <param name="id">task assignment id</param>
        /// <returns>details view</returns>
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
        // GET: TaskAssignment/Assign
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult Assign(int? id)
        {            
            var model = new TaskAssignmentViewModel();
            model.EventProjectId = id ?? default(int);

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            return View(model);
        }

        /// <summary>
        /// This action enables the assignment of tasks to employees
        /// </summary>
        /// <param name="model">Task assignment model</param>
        /// <returns>Index view</returns>
        // POST: TaskAssignment/Assign
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult Assign(TaskAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Deadline < DateTime.Now.AddHours(2) && model.Deadline < DateTime.Today)
                {
                    ModelState.AddModelError("Deadline", "The deadline should not be older day and less than 2 hours from now.");

                    ModelState.AddModelError(String.Empty, "Issue with the deadline");

                    return View(model);
                }

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

                return RedirectToAction("AllTasksIndex");
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

        /// <summary>
        /// This action retrieves the task assignment edit page
        /// </summary>
        /// <param name="id">task aassignment id</param>
        /// <returns>edit view</returns>
        // GET: TaskAssignment/Edit/5
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult Edit(int? id)
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
                EventProject = taskAssignment.EventProject.Name,
                AnyEmployee = taskAssignment.AnyEmployee.FullName,
                Employee = taskAssignment.Employee.FullName,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action allows the user to edit an assigned task
        /// </summary>
        /// <param name="id">task assingment id</param>
        /// <param name="model">task assignment view model</param>
        /// <returns>Index view</returns>
        // POST: TaskAssignment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult Edit(int id, TaskAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taskAssignment = db.TaskAssignments.Find(id);
                if (taskAssignment == null)
                {
                    return HttpNotFound();
                }

                taskAssignment.TaskName = model.TaskName;
                taskAssignment.Description = model.Description;
                taskAssignment.Deadline = model.Deadline;
                taskAssignment.Status = TaskAssignment.TaskStatus.Pending;
                taskAssignment.Priority = model.Priority;
                taskAssignment.AssignmentDate = DateTime.Now;
                taskAssignment.IsCompleted = model.IsCompleted;
                taskAssignment.EmployeeId = model.EmployeeId;
                taskAssignment.ClientServiceEmployeeId = User.Identity.GetUserId<int>();
                taskAssignment.EventProjectId = model.EventProjectId;

                db.Entry(taskAssignment).State = EntityState.Modified;
                db.SaveChanges();
                          
                return RedirectToAction("AllTasksIndex");
            }
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View();
        }

        /// <summary>
        /// Retrieves the edit completion page
        /// </summary>
        /// <param name="id">task assignment id</param>
        /// <returns>edit completion view</returns>
        public ActionResult EditCompletion(int? id)
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
                AssignmentDate = DateTime.Now,
                IsCompleted = taskAssignment.IsCompleted,
                EventProject = taskAssignment.EventProject.Name,
                AnyEmployee = taskAssignment.AnyEmployee.FullName,
                Employee = taskAssignment.Employee.FullName,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action allows the user to edit the completion of a task
        /// </summary>
        /// <param name="id">task assingment id</param>
        /// <param name="model">task assignment view model</param>
        /// <returns>index view</returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditCompletion(int id, TaskAssignmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var taskAssignment = db.TaskAssignments.Find(id);
                if (taskAssignment == null)
                {
                    return HttpNotFound();
                }

                //if (taskAssignment.IsCompleted == true && taskAssignment.Deadline < DateTime.Today)
                //{
                //    taskAssignment.Status = TaskAssignment.TaskStatus.Completed;
                //}
                //else if (taskAssignment.Deadline > DateTime.Today)
                //{
                //    taskAssignment.Status = TaskAssignment.TaskStatus.Overdue;
                //}
                //else if (taskAssignment.IsCompleted == false && taskAssignment.Deadline < DateTime.Today)
                //{
                //    taskAssignment.Status = TaskAssignment.TaskStatus.Pending;
                //}
                //if (taskAssignment.IsCompleted == true)
                //{
                //    taskAssignment.Status = TaskAssignment.TaskStatus.Completed;
                //}

                taskAssignment.TaskName = model.TaskName;
                taskAssignment.Description = model.Description;
                taskAssignment.Deadline = model.Deadline;
                taskAssignment.Status = model.Status;
                taskAssignment.Priority = model.Priority;
                taskAssignment.AssignmentDate = DateTime.Now;
                taskAssignment.IsCompleted = model.IsCompleted;
                taskAssignment.EmployeeId = model.EmployeeId;
                taskAssignment.ClientServiceEmployeeId = User.Identity.GetUserId<int>();
                taskAssignment.EventProjectId = model.EventProjectId;

                db.Entry(taskAssignment).State = EntityState.Modified;
                db.SaveChanges();             

                return RedirectToAction("Index");
            }
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View();
        }

        /// <summary>
        /// This action retrieves the deletion page of an assignment 
        /// </summary>
        /// <param name="id">Task assignment id</param>
        /// <returns>Delte view</returns>
        // GET: TaskAssignment/Delete/5
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult Delete(int? id)
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

            TaskAssignmentViewModel model = new TaskAssignmentViewModel
            {
                TaskAssignmentId = taskAssignment.TaskAssignmentId,
                TaskName = taskAssignment.TaskName,
                Description = taskAssignment.Description,
                Deadline = taskAssignment.Deadline,
                Status = taskAssignment.Status,
                Priority = taskAssignment.Priority,
                AssignmentDate = taskAssignment.AssignmentDate,
                IsCompleted = taskAssignment.IsCompleted,
                EventProject = taskAssignment.EventProject.Name,
                AnyEmployee = taskAssignment.AnyEmployee.FullName,
                Employee = taskAssignment.Employee.FullName,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action allows the deletion of a task assignment 
        /// </summary>
        /// <param name="id">task assignment id</param>
        /// <returns>Index view</returns>
        // POST: TaskAssignment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Client Service Employee")]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskAssignment taskAssignment = db.TaskAssignments.Find(id);
            db.TaskAssignments.Remove(taskAssignment);
            db.SaveChanges();
            return RedirectToAction("AllTasksIndex");
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
