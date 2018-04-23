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
    /// And the addition of presentations, documents, 3D models, and event reports as tab views
    /// </summary>
    [Authorize]
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
                UsherAppointeds = eventProject.UsherAppointeds.ToList(),
            };

            return PartialView(model);
        }

        /// <summary>
        /// This action retrieves the master details page.
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Master details view</returns>
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
        [Authorize(Roles = "Client Service Employee")]
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
        [Authorize(Roles = "Client Service Employee")]
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
        [Authorize(Roles = "Client Service Employee")]
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
        [Authorize(Roles = "Client Service Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, EventProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventProject = db.EventProjects.Find(id);
                if (eventProject == null)
                {
                    return HttpNotFound();
                }

                eventProject.Name = model.Name;
                eventProject.EventType = model.EventType;
                eventProject.Brief = model.Brief;
                eventProject.Street = model.Street;
                eventProject.District = model.District;
                eventProject.City = model.City;
                eventProject.Status = model.Status;
                eventProject.DateCreated = model.DateCreated;
                eventProject.ClientServiceEmployeeId = User.Identity.GetUserId<int>();
                eventProject.ClientId = model.ClientId;
                
                db.Entry(eventProject).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DetailsMaster", new { id = eventProject.EventProjectId });
            }

            ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.ClientId = new SelectList(db.Clients, "ClientId", "FullName");

            return View(model);
        }


        [Authorize(Roles = "Client Service Employee")]
        public ActionResult EditStatus(int? id)
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


        [Authorize(Roles = "Client Service Employee")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditStatus(int id, EventProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                var eventProject = db.EventProjects.Find(id);
                if (eventProject == null)
                {
                    return HttpNotFound();
                }

                eventProject.Name = model.Name;
                eventProject.EventType = model.EventType;
                eventProject.Brief = model.Brief;
                eventProject.Street = model.Street;
                eventProject.District = model.District;
                eventProject.City = model.City;
                eventProject.Status = model.Status;
                eventProject.DateCreated = model.DateCreated;
                eventProject.ClientServiceEmployeeId = User.Identity.GetUserId<int>();
                eventProject.ClientId = model.ClientId;

                db.Entry(eventProject).State = EntityState.Modified;
                db.SaveChanges();



                return RedirectToAction("DetailsMaster", new { id = eventProject.EventProjectId });
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
        [Authorize(Roles = "Client Service Employee")]
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
        [Authorize(Roles = "Client Service Employee")]
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
        /// This action shows the details of a client for a project
        /// </summary>
        /// <param name="id">client id</param>
        /// <returns>Client Details partial view</returns>
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

            var model = new ClientViewModel
            {
                ClientId = client.ClientId,
                FirstName = client.FirstName,
                LastName = client.LastName,
                Email = client.Email,
                MobileNumber = client.MobileNumber,
                Street = client.Street,
                District = client.District,
                City = client.City,
            };

            return PartialView(model);
        }

        /// <summary>
        /// This action retrieves the presentation partial view in the project details page
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>project presentation partial view</returns>
        [HttpGet]
        public ActionResult ProjectPresentationPartial(int? id/*, HttpPostedFileBase file*/)
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
            };

            //if (file.ContentLength > 0)
            //{
            //    var fileName = Path.GetFileName(file.FileName);
            //    var rand = Guid.NewGuid().ToString();
            //    var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
            //    file.SaveAs(path);
            //    project.Presentation = "~/Content/Uploads/" + rand + "-" + fileName;
            //}
            //return PartialView(model);

            ////HACK: ADD the path to presentation file path here
            //// PresentationFile = project.prese
            //if (model.PresentationFile != null && model.PresentationFile.ContentLength > 0)
            //{
            //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
            //    string filename = Path.GetFileName(model.PresentationFile.FileName);
            //    string ext = Path.GetExtension(filename).Substring(1);

            //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
            //    {
            //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
            //        return PartialView();
            //    }

            //    string appFolder = "~/Content/Uploads/";
            //    var rand = Guid.NewGuid().ToString();
            //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
            //    model.PresentationFile.SaveAs(path);
            //    project.Presentation = appFolder + rand + "-" + filename;
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
            //    return PartialView();
            //}

            return PartialView(model);
        }

        /// <summary>
        /// This action gets the presentation partial view of in a project's details page
        /// </summary>
        /// <param name="model">Project presentation model</param>
        /// <returns>Presentation partial view</returns>
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

                //    //HACK: Add the presentation file path here
                //    if (model.PresentationFile != null && model.PresentationFile.ContentLength > 0)
                //    {
                //        var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                //        string filename = Path.GetFileName(model.PresentationFile.FileName);
                //        string ext = Path.GetExtension(filename).Substring(1);

                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                //        return PartialView();
                //    }

                //    string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //    model.PresentationFile.SaveAs(path);
                //    project.Presentation = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}


                //try
                //{
                //if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                //{
                //    var presentation = System.Web.HttpContext.Current.Request.Files["HelpPresentation"];
                //    HttpPostedFileBase fileBase = new HttpPostedFileWrapper(presentation);
                //    var fileName = Path.GetFileName(fileBase.FileName);
                //    var rand = Guid.NewGuid().ToString();
                //    var path = Path.Combine(Server.MapPath("~/Content/Uploads/"), fileName);
                //    fileBase.SaveAs(path);
                //    project.Presentation = "~/Content/Uploads/" + rand + "-" + fileName;
                //    return Json("Presentation uploaded successfully");
                //}
                //else
                //{
                //    return Json("Presentatino was not uploaded");
                //}

                //catch (Exception)
                //{

                //    return Json("Error occured while uploading.");
                //}

                project.Presentation = model.Presentation;
                db.SaveChanges();
            }

            return PartialView(model);
        }

        /// <summary>
        /// This action gets the added presentation 
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Get presentation partial view</returns>
        public ActionResult ProjectGetPresentationPartial(int? id)
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
            };

            return View(model);

        }

        /// <summary>
        /// This action gets the schedule of a project
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>GetSchedules partial view</returns>
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

        /// <summary>
        /// This action enables the creation of the schedules
        /// </summary>
        /// <param name="model">Project schedule model</param>
        /// <returns>Schedule partial view </returns>
        [Authorize(Roles = "Client Service Employee")]
        [HttpPost]
        public ActionResult ProjectSchedulesPartial(ProjectScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Validating the date of birth
                if (model.EndTime < model.StartTime)
                {
                    ModelState.AddModelError("EndTime", "The ending time of an event cannot be before the starting time.");
                    ModelState.AddModelError(String.Empty, "Issue with the ending time");

                    return View(model);
                }
                else if (model.StartTime < DateTime.Now.TimeOfDay)
                {
                    ModelState.AddModelError("StartTime", "The starting time of an event cannot be before this time.");
                    ModelState.AddModelError(String.Empty, "Issue with the starting time");

                    return View(model);
                }

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

        /// <summary>
        /// This action retrieves the 3D model page/tab in the project details view
        /// </summary>
        /// <param name="id">Project id</param>
        /// <returns>3D model partial view</returns>
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
            //if (model.ThreeDModelFile != null && model.ThreeDModelFile.ContentLength > 0)
            //{
            //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
            //    string filename = Path.GetFileName(model.ThreeDModelFile.FileName);
            //    string ext = Path.GetExtension(filename).Substring(1);

            //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
            //    {
            //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
            //        return PartialView();
            //    }

            //    string appFolder = "~/Content/Uploads/";
            //    var rand = Guid.NewGuid().ToString();
            //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
            //    model.ThreeDModelFile.SaveAs(path);
            //    project.ThreeDModel = appFolder + rand + "-" + filename;
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
            //    return PartialView();
            //}
            return PartialView(model);
        }

        /// <summary>
        /// This action allows the addition of a 3D model
        /// </summary>
        /// <param name="model">Project 3DModel model</param>
        /// <returns>3D model partial view</returns>
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

                //if (model.ThreeDModelFile != null && model.ThreeDModelFile.ContentLength > 0)
                //{
                //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                //    string filename = Path.GetFileName(model.ThreeDModelFile.FileName);
                //    string ext = Path.GetExtension(filename).Substring(1);

                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                //        return PartialView();
                //    }

                //    string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //    model.ThreeDModelFile.SaveAs(path);
                //    project.ThreeDModel = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}
                project.ThreeDModel = model.ThreeDModel;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        /// <summary>
        /// This action retrieves the 3D model added
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Get 3D model partial view</returns>
        public ActionResult ProjectGet3DModelPartial(int? id)
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
            };

            //if (model.ThreeDModelFile != null && model.ThreeDModelFile.ContentLength > 0)
            //{
            //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
            //    string filename = Path.GetFileName(model.ThreeDModelFile.FileName);
            //    string ext = Path.GetExtension(filename).Substring(1);

            //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
            //    {
            //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
            //        return PartialView();
            //    }

            //    string appFolder = "~/Content/Uploads/";
            //    var rand = Guid.NewGuid().ToString();
            //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
            //    model.ThreeDModelFile.SaveAs(path);
            //    project.ThreeDModel = appFolder + rand + "-" + filename;
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
            //    return PartialView();
            //}

            return View(model);
        }

        /// <summary>
        /// This action gets the event report template partial view in the project details tab 
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Event report template partial view</returns>
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

            //if (model.EventReportTemplateFile != null && model.EventReportTemplateFile.ContentLength > 0)
            //{
            //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
            //    string filename = Path.GetFileName(model.EventReportTemplateFile.FileName);
            //    string ext = Path.GetExtension(filename).Substring(1);

            //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
            //    {
            //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
            //        return PartialView();
            //    }

            //    string appFolder = "~/Content/Uploads/";
            //    var rand = Guid.NewGuid().ToString();
            //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
            //    model.EventReportTemplateFile.SaveAs(path);
            //    project.EventReportTemplate = appFolder + rand + "-" + filename;
            //}
            //else
            //{
            //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
            //    return PartialView();
            //}

            return PartialView(model);
        }

        /// <summary>
        /// This action allows an event report template to be added.
        /// </summary>
        /// <param name="model">Event report template model</param>
        /// <returns>Event report template partial view</returns>
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

                //if (model.EventReportTemplateFile != null && model.EventReportTemplateFile.ContentLength > 0)
                //{
                //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                //    string filename = Path.GetFileName(model.EventReportTemplateFile.FileName);
                //    string ext = Path.GetExtension(filename).Substring(1);

                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                //        return PartialView();
                //    }

                //    string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //    model.EventReportTemplateFile.SaveAs(path);
                //    project.EventReportTemplate = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}
                project.EventReportTemplate = model.EventReportTemplate;
                db.SaveChanges();
            }

            // Failure: retrun the same model
            return PartialView(model);
        }

        /// <summary>
        /// This action gets the event report template added
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Getevent report template partial view</returns>
        public ActionResult ProjectGetEventReportTemplatePartial(int? id)
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
            };

            return View(model);
        }

        /// <summary>
        /// This action retrieves the event report page in the project details tab
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Event report partial view</returns>
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
                //HACK: ADD the path to event report file path here
            };

            return PartialView(model);
        }

        /// <summary>
        /// This action enables the addition of an event report
        /// </summary>
        /// <param name="model">Event report view model</param>
        /// <returns>Event report partial view</returns>
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

                //if (model.EventReportFile != null && model.EventReportFile.ContentLength > 0)
                //{
                //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                //    string filename = Path.GetFileName(model.EventReportFile.FileName);
                //    string ext = Path.GetExtension(filename).Substring(1);

                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                //        return PartialView();
                //    }

                //    string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //    model.EventReportFile.SaveAs(path);
                //    project.EventReport = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}
                project.EventReport = model.EventReport;
                db.SaveChanges();
            }

            return PartialView(model);
        }

        /// <summary>
        /// This action shows the event report added
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Get event report partial view</returns>
        public ActionResult ProjectGetEventReportPartial(int? id)
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
            };

            return View(model);
        }

        /// <summary>
        /// This action gets the document list of an event project.
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>GetDocument partial view</returns>
        public ActionResult GetDocumentPartial(int? id)
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

            var documents = project.Documents.ToList();

            var model = new List<DocumentViewModel>();

            foreach (var item in documents)
            {
                model.Add(new DocumentViewModel
                {
                    DocumentId = item.DocumentId,
                    Name = item.Name,
                    FilePath = item.FilePath,
                    Status = item.Status,
                    EventProject = item.EventProject.Name
                });
            }

            return View(model);
        }

        /// <summary>
        /// This action allows the documents to be added in the project details page
        /// </summary>
        /// <param name="model">Document view model</param>
        /// <returns>Document partial view</returns>
        [HttpPost]
        public ActionResult DocumentPartial(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                var document = new Document
                {
                    Name = model.Name,
                    Status = DocumentStatus.Pending,
                    FilePath = model.FilePath,
                    CEOFeedback = model.CEOFeedback,
                    EventProjectId = model.EventProjectId,
                };

                //if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
                //{
                //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
                //    string filename = Path.GetFileName(model.DocumentFile.FileName);
                //    string ext = Path.GetExtension(filename).Substring(1);

                //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                //    {
                //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                //        return PartialView();
                //    }

                //    string appFolder = "~/Content/Uploads/";
                //    var rand = Guid.NewGuid().ToString();
                //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);
                //    model.DocumentFile.SaveAs(path);

                //    document.FilePath = appFolder + rand + "-" + filename;
                //}
                //else
                //{
                //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                //    return PartialView();
                //}

                db.Documents.Add(document);
                db.SaveChanges();
            }

            return PartialView(model);
        }

        /// <summary>
        /// This action lists the tasks created of the event project.
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>Get Employee Tasks partial view</returns>
        //public ActionResult GetEmployeeTasksPartial(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    var project = db.EventProjects.Find(id);
        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    //var employeeTasks = project.EmployeeTasks.ToList();
        //    var taskAssignments = project.TaskAssignments.ToList();
        //    var model = new List<TaskAssignmentViewModel>();
        //    foreach (var item in taskAssignments)
        //    {
        //        model.Add(new TaskAssignmentViewModel
        //        {
        //            TaskAssignmentId = item.TaskAssignmentId,
        //            TaskName = item.TaskName,
        //            Description = item.Description,
        //            Deadline = item.Deadline,
        //            Status = item.Status,
        //            Priority = item.Priority,
        //            AssignmentDate = item.AssignmentDate,
        //            IsCompleted = item.IsCompleted,
        //            //EventProject = item.EventProject.Name,
        //            AnyEmployee = item.AnyEmployee.FullName,
        //            Employee = item.Employee.FullName,
        //        });
        //    }
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        //    ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");
        //    //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    return View(model);
        //}

        ///// <summary>
        ///// This action enables the addition of tasks in the project details page
        ///// </summary>
        ///// <param name="model">Employee Task view model</param>
        ///// <returns>Employee Task partial view</returns>
        //[Authorize(Roles = "Client Service Employee")]
        //[HttpPost]
        //public ActionResult EmployeeTaskPartial(TaskAssignment model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var project = db.EventProjects.Find(model.EventProjectId);

        //        if (project == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        var taskAssignment = new TaskAssignment
        //        {
        //            TaskName = model.TaskName,
        //            Description = model.Description,
        //            Deadline = model.Deadline,
        //            Status = TaskAssignment.TaskStatus.Pending,
        //            Priority = model.Priority,
        //            AssignmentDate = DateTime.Now,
        //            IsCompleted = model.IsCompleted,                  
        //            EmployeeId = model.EmployeeId, 
        //            ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
        //            EventProjectId = model.EventProjectId,
        //        };
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //        //var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        //        //ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

        //        //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //        ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //        db.TaskAssignments.Add(taskAssignment);
        //        db.SaveChanges();
        //    }
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        //    ViewBag.EmployeeId = new SelectList(list, "Id", "FullName");

        //    //ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    ViewBag.ClientServiceEmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //    return PartialView(model);
        //}


        public ActionResult UsherAppointedPartial()
        {        

            var eventprojects = db.EventProjects.ToList();
            var model = new List<EventProjectViewModel>();

            foreach (var item in eventprojects)
            {
                model.Add(new EventProjectViewModel
                {
                    EventProjectId = item.EventProjectId,
                    UsherAppointeds = item.UsherAppointeds.ToList()
                });
            }

            return PartialView(model);
        }

        // USHER APPOINTED 

        //public ActionResult GetUsherAppointedPartial(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var project = db.EventProjects.Find(id);

        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var usherAppointeds = project.UsherAppointeds.ToList();

        //    var model = new List<UsherAppointedViewModel>();

        //    foreach (var item in usherAppointeds)
        //    {
        //        model.Add(new UsherAppointedViewModel
        //        {
        //            UsherAppointedId = item.UsherAppointedId,
        //            DateAppointed = item.DateAppointed,
        //            Usher = item.Usher.FullName,
        //            Employee = item.Employee.FullName,
        //        });
        //    }

        //    ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
        //    return View(model);
        //}

        //[HttpPost]
        //public ActionResult UsherAppointedPartial(UsherAppointedViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var project = db.EventProjects.Find(model.EventProjectId);

        //        if (project == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        var usherAppointed = new UsherAppointed
        //        {
        //            DateAppointed = DateTime.Now,
        //            UsherId = model.UsherId,
        //            ProductionEmployeeId = User.Identity.GetUserId<int>(),
        //            EventProjectId = model.EventProjectId
        //        };

        //        ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");

        //        db.UsherAppointeds.Add(usherAppointed);
        //        db.SaveChanges();
        //    }

        //    return PartialView(model);
        //}

        public ActionResult GetCostSheetsPartial(int? id)
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

            var costSheets = project.CostSheets.ToList();

            var model = new List<CostSheetViewModel>();

            foreach (var item in costSheets)
            {
                model.Add(new CostSheetViewModel
                {
                    CostSheetId = item.CostSheetId,
                    CostSheetName = item.CostSheetName,
                    Status = item.Status,
                    ProductionEmployee = item.Employee.FullName,
                    //Employee = item.Employee.FullName,
                    //EventProject = item.EventProject.Name
                });
            }
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "ProductionEmployeeId", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return View(model);
        }

        /// <summary>
        /// This action enables the addition of tasks in the project details page
        /// </summary>
        /// <param name="model">Employee Task view model</param>
        /// <returns>Employee Task partial view</returns>
        //[Authorize(Roles = "Client Service Employee")]
        [HttpPost]
        public ActionResult CostSheetPartial(CostSheetViewModel model)
        {
            if (ModelState.IsValid)
            {
                var project = db.EventProjects.Find(model.EventProjectId);

                if (project == null)
                {
                    return HttpNotFound();
                }

                var costSheet = new CostSheet
                {
                    CostSheetName = model.CostSheetName,
                    Status = CostSheetStatus.Pending,
                    ProductionEmployeeId = User.Identity.GetUserId<int>(),
                    FinanceEmployeeId = User.Identity.GetUserId<int>(),
                    CEOEmployeeId = User.Identity.GetUserId<int>(),
                    EventProjectId = model.EventProjectId,
                };

                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "ProductionEmployeeId", "FullName");
                //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                //ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

                db.CostSheets.Add(costSheet);
                db.SaveChanges();
            }
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "ProductionEmployeeId", "FullName");
            //ViewBag.CEOEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.FinanceEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            //ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return PartialView(model);
        }






        //public ActionResult GetTaskAssignmentsPartial(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    var project = db.EventProjects.Find(id);

        //    if (project == null)
        //    {
        //        return HttpNotFound();
        //    }           

        //    var taskAssignments = project.TaskAssignments.ToList();

        //    var model = new List<TaskAssignmentViewModel>();

        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //    foreach (var item in taskAssignments)
        //    {
        //        model.Add(new TaskAssignmentViewModel
        //        {
        //            TaskAssignmentId = item.TaskAssignmentId,
        //            TaskName = item.TaskName,
        //            Description = item.Description,
        //            Deadline = item.Deadline,
        //            Status = item.Status,
        //            Priority = item.Priority,
        //            AssignmentDate = item.AssignmentDate,
        //            IsCompleted = item.IsCompleted,
        //            //EventProject = item.EventProject.Name,
        //            AnyEmployee = item.AnyEmployee.FullName,
        //            Employee = item.Employee.FullName,                    
        //        });
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");
        //    }
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //    return View(model);
        //}


        //[HttpPost]
        //public ActionResult TaskAssignmentPartial(TaskAssignmentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var project = db.EventProjects.Find(model.EventProjectId);

        //        if (project == null)
        //        {
        //            return HttpNotFound();
        //        }

        //        var taskAssignment = new TaskAssignment
        //        {
        //            TaskName = model.TaskName,
        //            Description = model.Description,
        //            Deadline = model.Deadline,
        //            Status = TaskAssignment.TaskStatus.Pending,
        //            Priority = model.Priority,
        //            AssignmentDate = DateTime.Now,
        //            IsCompleted = model.IsCompleted,
        //            EmployeeId = model.EmployeeId,
        //            ClientServiceEmployeeId = User.Identity.GetUserId<int>(),
        //            EventProjectId = model.EventProjectId,                    
        //        };

        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        //var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        //        ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //        db.TaskAssignments.Add(taskAssignment);
        //        db.SaveChanges();

        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //    }
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //    //var list = db.Employees.ToList().Select(e => new { e.Id, e.FullName });
        //    ViewBag.EmployeeId = new SelectList(db.Employees, "Id", "FullName");

        //    return PartialView(model);
        //}
    }
}
