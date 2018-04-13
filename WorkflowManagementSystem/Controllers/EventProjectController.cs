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
        public ActionResult ProjectDetailsPartial(int? id)
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

            return PartialView(model);
        }

        // Returns the master details view
        public ActionResult DetailsMaster(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Pass Project ID to Partial views in the DetailsMaster view
            ViewBag.ProjectId = id;

            return View();
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
                    ScheduleDate = projectSchedule.Date,
                    StartTime = projectSchedule.StartTime,
                    EndTime = projectSchedule.EndTime,
                    //EventProjectId = projectSchedule.EventProjectId
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

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ProjectPresentationPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.EventProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectPresentationViewModel
            {
                EventProjectId = project.EventProjectId,
                Presentation = project.Presentation,

                //HACK: ADD the path to presentation file path here
                //PresentationFile = project.PresentationFilePath,
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ProjectPresentationPartial(ProjectPresentationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                //HACK: Add the presentation file path here
                if (model.PresentationFile != null && model.PresentationFile.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                    string filename = Path.GetFileName(model.PresentationFile.FileName);
                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                        return PartialView();
                    }

                    string appFolder = "~/Content/Uploads/";
                    var rand = Guid.NewGuid().ToString();
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                    model.PresentationFile.SaveAs(path);
                    project.Presentation = appFolder + rand + "-" + filename;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                    return PartialView();
                }
                project.Presentation = model.Presentation;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        // Get the schedules of the project with id
        public ActionResult ProjectGetSchedulesPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.EventProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var schedules = project.ProjectSchedules.ToList();

            var model = new List<ProjectScheduleViewModel>();

            foreach (var item in schedules)
            {
                model.Add(new ProjectScheduleViewModel
                {
                    ScheduleId = item.ScheduleId,
                    ScheduleDate = item.Date,
                    StartTime = item.StartTime,
                    EndTime = item.EndTime
                });
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult ProjectSchedulesPartial(ProjectScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                var schedule = new ProjectSchedule
                {
                    Date = model.ScheduleDate,
                    StartTime = model.StartTime,
                    EndTime = model.EndTime,
                    EventProjectId = model.EventProjectId
                };
                db.ProjectSchedules.Add(schedule);
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Project3DModelPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.EventProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new Project3DModelViewModel
            {
                EventProjectId = project.EventProjectId,
                ThreeDModel = project.ThreeDModel,

                //HACK: ADD the path to 3D model file path here
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult Project3DModelPartial(Project3DModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                if (model.ThreeDModelFile != null && model.ThreeDModelFile.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                    string filename = Path.GetFileName(model.ThreeDModelFile.FileName);
                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                        return PartialView();
                    }

                    string appFolder = "~/Content/Uploads/";
                    var rand = Guid.NewGuid().ToString();
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                    model.ThreeDModelFile.SaveAs(path);
                    project.ThreeDModel = appFolder + rand + "-" + filename;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                    return PartialView();
                }
                project.ThreeDModel = model.ThreeDModel;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ProjectEventReportTemplatePartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.EventProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectEventReportTemplateViewModel
            {
                EventProjectId = project.EventProjectId,
                EventReportTemplate = project.EventReportTemplate,
                //HACK: ADD the path to event report template file path here
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ProjectEventReportTemplatePartial(ProjectEventReportTemplateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                if (model.EventReportTemplateFile != null && model.EventReportTemplateFile.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                    string filename = Path.GetFileName(model.EventReportTemplateFile.FileName);
                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                        return PartialView();
                    }

                    string appFolder = "~/Content/Uploads/";
                    var rand = Guid.NewGuid().ToString();
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                    model.EventReportTemplateFile.SaveAs(path);
                    project.EventReportTemplate = appFolder + rand + "-" + filename;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                    return PartialView();
                }
                project.EventReportTemplate = model.EventReportTemplate;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        [HttpGet]
        public ActionResult ProjectEventReportPartial(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var project = db.EventProjects.Find(id);

            if (project == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectEventReportViewModel
            {
                EventProjectId = project.EventProjectId,
                EventReport = project.EventReport,
                //HACK: ADD the path to event report template file path here
            };

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult ProjectEventReportPartial(ProjectEventReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                if (model.EventReportFile != null && model.EventReportFile.ContentLength > 0)
                {
                    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                    string filename = Path.GetFileName(model.EventReportFile.FileName);
                    string ext = Path.GetExtension(filename).Substring(1);

                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                        return PartialView();
                    }

                    string appFolder = "~/Content/Uploads/";
                    var rand = Guid.NewGuid().ToString();
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                    model.EventReportFile.SaveAs(path);
                    project.EventReport = appFolder + rand + "-" + filename;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                    return PartialView();
                }
                project.EventReport = model.EventReport;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }
    }
}
