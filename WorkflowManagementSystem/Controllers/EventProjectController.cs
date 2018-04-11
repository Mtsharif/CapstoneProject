/*
 * Description: This file represents the Event Project controller class
 * Author: Mtsharif 
 * Date: 4/4/2018
 */

using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.ViewModels;

namespace WorkflowManagementSystem.Controllers
{
    /// <summary>
    /// This controller is created based on the EventProject and EventProjectViewModel classes
    /// It allows the creation of projects as well as the listing, editing and deleting of projects 
    /// It also enables the creation of schedules of each project using partial views
    /// </summary>
    [Authorize(Roles = "Client Service Employee")]
    public class EventProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all event projects.
        /// </summary>
        /// <returns>Event project index view</returns>
        // GET: EventProject
        public ActionResult Index()
        {
            var eventprojects = db.EventProjects.ToList();
            var model = new List<EventProjectViewModel>();

            foreach (var item in eventprojects)
            {
                model.Add(new EventProjectViewModel
                {
                    EventProjectId = item.EventProjectId,
                    Name = item.Name,
                    EventType = item.EventType,
                    Brief = item.Brief,
                    Street = item.Street,
                    District = item.District,
                    City = item.City,
                    Status = item.Status,
                    DateCreated = item.DateCreated,
                    Employee = item.Employee.FullName,
                    Client = item.Client.FullName
                });
            }
          
            return View(model);
        }

        /// <summary>
        /// This action shows the details of a project
        /// </summary>
        /// <param name="id">The id of the selected project</param>
        /// <returns>Event project details view</returns>
        // GET: EventProject/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventProject eventProject = db.EventProjects.Find(id);
 
            if (eventProject == null)
            {
                return HttpNotFound();
            }

            var model = new EventProjectViewModel
            {
                EventProjectId = eventProject.EventProjectId,
                Name = eventProject.Name,
                EventType = eventProject.EventType,
                Brief = eventProject.Brief,
                Street = eventProject.Street,
                District = eventProject.District,
                City = eventProject.City,
                Status = eventProject.Status,
                DateCreated = eventProject.DateCreated,
                Employee = eventProject.Employee.FullName,
                Client = eventProject.Client.FullName,
                ProjectSchedules = eventProject.ProjectSchedules.ToList(),              
            };

            return View(model);         
        }

        /// <summary>
        /// This action retrieves the create event project page.
        /// </summary>
        /// <returns>Create event project view</returns>
        // GET: EventProject/Create
        public ActionResult Create()
        {
            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

            return View();
        }

        /// <summary>
        /// This action allows the user to create new event projects.
        /// </summary>
        /// <param name="model">The event project model</param>
        /// <returns>Event project index view or create model view</returns>
        // POST: EventProject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EventProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventProject = new EventProject
                {
                    Name = model.Name,
                    EventType = model.EventType,
                    Brief = model.Brief,
                    Street = model.Street,
                    District = model.District,
                    City = model.City,
                    Status = model.Status,
                    DateCreated = DateTime.Now,
                    ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
                    ClientId = model.ClientId,
                };

                db.EventProjects.Add(eventProject);
                db.SaveChanges();

                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");
                return View();
            }         
        }

        /// <summary>
        /// This action shows an event project's details for editing.
        /// </summary>
        /// <param name="id">The id of a selected event project</param>
        /// <returns>Error page or edit event project view</returns>
        // GET: EventProject/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventProject eventProject = db.EventProjects.Find(id);

            if (eventProject == null)
            {
                return HttpNotFound();
            }

            var model = new EventProjectViewModel
            {
                EventProjectId = eventProject.EventProjectId,
                Name = eventProject.Name,
                EventType = eventProject.EventType,
                Brief = eventProject.Brief,
                Street = eventProject.Street,
                District = eventProject.District,
                City = eventProject.City,
                Status = eventProject.Status,
                DateCreated = DateTime.Now,
                Employee = eventProject.Employee.FullName,
                Client = eventProject.Client.FullName,
            };

            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action enables the user to edit and save an event project.
        /// </summary>
        /// <param name="model">Event project model</param>
        /// <returns>Event project index view or edit model view</returns>
        // POST: EventProject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EventProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                EventProject eventProject = Mapper.Map<EventProjectViewModel, EventProject>(model);
                db.Entry(eventProject).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action retrieves a project's information to be deleted.
        /// </summary>
        /// <param name="id">The id of the selected event project</param>
        /// <returns>Error page or event project delete view</returns>
        // GET: EventProject/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventProject eventProject = db.EventProjects.Find(id);

            if (eventProject == null)
            {
                return HttpNotFound();
            }

            EventProjectViewModel model = new EventProjectViewModel
            {
                EventProjectId = eventProject.EventProjectId,
                Name = eventProject.Name,
                EventType = eventProject.EventType,
                Brief = eventProject.Brief,
                Street = eventProject.Street,
                District = eventProject.District,
                City = eventProject.City,
                Status = eventProject.Status,
                DateCreated = eventProject.DateCreated,
                Employee = eventProject.Employee.FullName,
                Client = eventProject.Client.FullName
            };

            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

            return View(model);
        }

        /// <summary>
        /// This action enables the deletion of an event project.
        /// </summary>
        /// <param name="id">The id of the project to be deleted</param>
        /// <returns>Event project index view</returns>
        // POST: EventProject/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EventProject eventProject = db.EventProjects.Find(id);
            db.EventProjects.Remove(eventProject);
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

        /// <summary>
        /// This action retrieves an event project's information to be able to add its schedules.
        /// </summary>
        /// <param name="id">The id of the selected event project</param>
        /// <returns>Error page or model view</returns>
        public ActionResult ProjectSchedules(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventProject eventProject = db.EventProjects.Find(id);

            if (eventProject == null)
            {
                return HttpNotFound();
            }

            EventProjectViewModel model = Mapper.Map<EventProject, EventProjectViewModel>(eventProject);          

            return View(model);
        }

        /// <summary>
        /// This action allows the creation of the project schedule in the event project view.
        /// </summary>
        /// <param name="model">Project schedule model</param>
        /// <returns>Add project schedule partial view</returns>
        [HttpPost]
        public ActionResult AddProjectSchedulePartial(ProjectScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var projectSchedule = new ProjectSchedule
                {
                    Date = model.Date,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    EventProjectId = model.EventProjectId,
                };

                db.ProjectSchedules.Add(projectSchedule);
                db.SaveChanges();

                return PartialView();
            }

            return PartialView(model);
        }

        /// <summary>
        /// This action retrieves the created project schedules' information.
        /// </summary>
        /// <param name="id">The id of the event project</param>
        /// <returns>Get project scheduels partial view</returns>
        public ActionResult GetProjectSchedulesPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var projectSchedules = db.ProjectSchedules.Where(p => p.EventProjectId == id).ToList();

            var model = new List<ProjectScheduleViewModel>();
            foreach (var projectSchedule in projectSchedules)
            {
                model.Add(new ProjectScheduleViewModel
                {
                    ScheduleId = projectSchedule.ScheduleId,
                    Date = projectSchedule.Date,
                    StartTime = projectSchedule.StartTime,
                    EndTime = projectSchedule.EndTime,
                    EventProjectId = projectSchedule.EventProjectId
                });
            }

            return PartialView(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult ClientDetailsPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Client client = db.Clients.Find(id);

            if (client == null)
            {
                return HttpNotFound();
            }

            ClientViewModel model = Mapper.Map<Client, ClientViewModel>(client);

            //return View(model);
            return PartialView(model);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ActionResult UploadPresentationPartial(EventProjectUploadsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventProject = new EventProject
                {
                    Presentation = model.Presentation
                };

                //if (model.PresentationFile != null && model.PresentationFile.ContentLength > 0)
                //{
                //    // Allowed extensions to be uploaded
                //    var extensions = new[] { "pdf", "pptx", "pptm", "ppt" };

                //    // Get the file name without the path
                //  string filename = Path.GetFileName(model.PresentationFile.FileName);

                //    // Get the extension of the file
                //   string ext = Path.GetExtension(filename).Substring(1);

                //   // Check if the extension of the file is in the list of allowed extensions
                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //         ModelState.AddModelError(string.Empty, "Accepted file are pdf, pptx, pptm, ppt documents");
                //         return PartialView();
                //    }

                //     string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //     string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //     model.PresentationFile.SaveAs(path);
                //    eventProject.Presentation = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}

                db.EventProjects.Add(eventProject);
                db.SaveChanges();

                return PartialView();
            }

            //return PartialView("UploadPresentationPartial", model);
            return PartialView(model);
        }


        public ActionResult PresentationDetailsPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EventProject eventProjectUploads = db.EventProjects.Find(id);

            if (eventProjectUploads == null)
            {
                return HttpNotFound();
            }

            var model = new EventProjectUploadsViewModel
            {
                EventProjectId = eventProjectUploads.EventProjectId,
                Presentation = eventProjectUploads.Presentation
            };

            return PartialView(model);
        }
    }
}
