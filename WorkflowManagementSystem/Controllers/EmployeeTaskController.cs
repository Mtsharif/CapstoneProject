///*
// * Description: This file represents the Employee Task Controller class
// * Author: Mtsharif 
// * Date: 18/4/2018
// */

//using AutoMapper;
//using Microsoft.AspNet.Identity;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using WorkflowManagementSystem.Models;
//using WorkflowManagementSystem.ViewModels;

//namespace WorkflowManagementSystem.Controllers
//{
//    /// <summary>
//    /// This controller is created based on the employee task domain and view models. 
//    /// It enables the user to manage tasks 
//    /// </summary>
//    [Authorize(Roles = "Client Service Employee")]
//    public class EmployeeTaskController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        // GET: EmployeeTask
//        public ActionResult Index()
//        {
//            var employeeTasks = db.EmployeeTasks.ToList();
//            var model = new List<EmployeeTaskViewModel>();

//            foreach (var item in employeeTasks)
//            {
//                model.Add(new EmployeeTaskViewModel
//                {
//                    EmployeeTaskId = item.EmployeeTaskId,
//                    TaskName = item.TaskName,
//                    Description = item.Description,
//                    //Deadline = item.Deadline,
//                    //Status = item.Status,
//                    //Priority = item.Priority,
//                    EventProject = item.EventProject.Name,
//                    Employee = item.Employee.FullName,
//                });
//            }

//            return View(model);
//        }

//        // GET: EmployeeTask/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            //// ADDED
//            //// Pass Project ID to Partial views in the DetailsMaster view
//            //ViewBag.EmployeeTaskId = id;

//            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

//            if (employeeTask == null)
//            {
//                return HttpNotFound();
//            }

//            var model = new EmployeeTaskViewModel
//            {
//                EmployeeTaskId = employeeTask.EmployeeTaskId,
//                TaskName = employeeTask.TaskName,
//                Description = employeeTask.Description,
//                //Deadline = employeeTask.Deadline,
//                //Status = employeeTask.Status,
//                //Priority = employeeTask.Priority,
//                EventProject = employeeTask.EventProject.Name,
//                Employee = employeeTask.Employee.FullName,
//            };

//            return View(model);
//        }

//        /// <summary>
//        /// This action gets the task creation page
//        /// </summary>
//        /// <returns>Create view</returns>
//        // GET: EmployeeTask/Create
//        public ActionResult Create()
//        {
//            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
//            return View();
//        }

//        /// <summary>
//        /// This action allows the creation of tasks
//        /// </summary>
//        /// <param name="model">Employee task model</param>
//        /// <returns>Index view</returns>
//        // POST: EmployeeTask/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(EmployeeTaskViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var employeeTask = new EmployeeTask
//                {
//                    TaskName = model.TaskName,
//                    Description = model.Description,
//                    //Deadline = model.Deadline,
//                    //Status = model.Status,
//                    //Priority = model.Priority,
//                    EventProjectId = model.EventProjectId,
//                    ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
//                };

//                db.EmployeeTasks.Add(employeeTask);
//                db.SaveChanges();

//                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

//                return RedirectToAction("Details", new { id = employeeTask.EmployeeTaskId });
//            }
//            else
//            {
//                ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
//                return View();
//            }
//        }

//        /// <summary>
//        /// This action retrieves the edit task page
//        /// </summary>
//        /// <param name="id">Employee task id</param>
//        /// <returns>Edit view</returns>
//        // GET: EmployeeTask/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

//            if (employeeTask == null)
//            {
//                return HttpNotFound();
//            }

//            var model = new EmployeeTaskViewModel
//            {
//                EmployeeTaskId = employeeTask.EmployeeTaskId,
//                TaskName = employeeTask.TaskName,
//                Description = employeeTask.Description,
//                //Deadline = employeeTask.Deadline,
//                //Status = employeeTask.Status,
//                //Priority = employeeTask.Priority,
//                EventProject = employeeTask.EventProject.Name,
//                Employee = employeeTask.Employee.FullName,
//            };

//            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

//            return View(model);
//        }

//        /// <summary>
//        /// This action allows the user to edit a created task
//        /// </summary>
//        /// <param name="model">Employee task model</param>
//        /// <returns>Index view</returns>
//        // POST: EmployeeTask/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(EmployeeTaskViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                EmployeeTask employeeTask = Mapper.Map<EmployeeTaskViewModel, EmployeeTask>(model);
//                db.Entry(employeeTask).State = EntityState.Modified;
//                db.SaveChanges();

//                return RedirectToAction("Index");
//            }

//            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

//            return View(model);
//        }

//        /// <summary>
//        /// This action gets the task delete page
//        /// </summary>
//        /// <param name="id">Employee task id</param>
//        /// <returns>Delete view</returns>
//        // GET: EmployeeTask/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);

//            if (employeeTask == null)
//            {
//                return HttpNotFound();
//            }

//            EmployeeTaskViewModel model = new EmployeeTaskViewModel
//            {
//                EmployeeTaskId = employeeTask.EmployeeTaskId,
//                TaskName = employeeTask.TaskName,
//                Description = employeeTask.Description,
//                //Deadline = employeeTask.Deadline,
//                //Status = employeeTask.Status,
//                //Priority = employeeTask.Priority,
//                EventProject = employeeTask.EventProject.Name,
//                Employee = employeeTask.Employee.FullName,
//            };

//            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
//            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

//            return View(model);
//        }

//        /// <summary>
//        /// This action enables the deletion of a task
//        /// </summary>
//        /// <param name="id">Employee task id</param>
//        /// <returns>Index view</returns>
//        // POST: EmployeeTask/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            EmployeeTask employeeTask = db.EmployeeTasks.Find(id);
//            db.EmployeeTasks.Remove(employeeTask);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        //public ActionResult ProjectGetSchedulesPartial(int? id)
//        //{
//        //    if (id == null)
//        //    {
//        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//        //    }

//        //    var project = db.EventProjects.Find(id);

//        //    if (project == null)
//        //    {
//        //        return HttpNotFound();
//        //    }

//        //    var schedules = project.ProjectSchedules.ToList();

//        //    var model = new List<ProjectScheduleViewModel>();

//        //    foreach (var item in schedules)
//        //    {
//        //        model.Add(new ProjectScheduleViewModel
//        //        {
//        //            ScheduleId = item.ScheduleId,
//        //            ScheduleDate = item.Date,
//        //            StartTime = item.StartTime,
//        //            EndTime = item.EndTime
//        //        });
//        //    }

//        //    return View(model);
//        //}

//        ///// <summary>
//        ///// This action enables the creation of the schedules
//        ///// </summary>
//        ///// <param name="model">Project schedule model</param>
//        ///// <returns>Schedule partial view </returns>
//        //[HttpPost]
//        //public ActionResult TaskAssignmentPartial(TaskAssignmentViewModel model)
//        //{
//        //    if (ModelState.IsValid)
//        //    {
//        //        var task = db.EmployeeTasks.Find(model.EmployeeTaskId);

//        //        if (task == null)
//        //        {
//        //            return HttpNotFound();
//        //        }

//        //        var taskAssignment = new TaskAssignment
//        //        {
//        //            Deadline = model.Deadline,
//        //            Status = model.Status,
//        //            Priority = model.Priority,
//        //            AssignmentDate = DateTime.Now,
//        //            EmployeeTaskId = model.EmployeeTaskId,
//        //            EmployeeId = model.EmployeeId,
//        //            ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
//        //        };
//        //        db.TaskAssignments.Add(taskAssignment);
//        //        db.SaveChanges();
//        //    }

//        //    return PartialView(model);
//        //}
//    }
//}
