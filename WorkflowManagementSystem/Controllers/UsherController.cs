/*
 * Description: This file represents the Usher Controller class
 * Author: Mtsharif 
 * Date: 20/3/2018
 */

using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
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
    /// The Usher controller is generated based on the Usher and Usher View Model classes.
    /// This controller manages ushers by creating new ushers as well as listing , editing and deleting them.
    /// </summary>
    public class UsherController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action lists all ushers present in the database. 
        /// </summary>
        /// <returns>The index view</returns>
        // GET: Usher
        public ActionResult Index()
        {
            var ushers = db.Ushers.ToList();
            var model = new List<UsherViewModel>();
            foreach (var item in ushers)
            {
                model.Add(new UsherViewModel
                {
                    UsherId = item.UsherId,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    MobileNumber = item.MobileNumber,
                    DateOfBirth = item.DateOfBirth,
                    Gender = item.Gender,
                    Nationality = item.Nationality,
                    City = item.City,
                    CarAvailability = item.CarAvailability,
                    MedicalCard = item.MedicalCard,
                });
            }
            return View(model);
        }

        /// <summary>
        /// This action shows the details of a selected usher.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The details view</returns>
        // GET: Usher/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usher usher = db.Ushers.Find(id);
            if (usher == null)
            {
                return HttpNotFound();
            }

            // UsherViewModel model = Mapper.Map<Usher, UsherViewModel>(usher);

            var model = new UsherViewModel
            {
                UsherId = usher.UsherId,
                FirstName = usher.FirstName,
                LastName = usher.LastName,
                MobileNumber = usher.MobileNumber,
                DateOfBirth = usher.DateOfBirth,
                Gender = usher.Gender,
                Nationality = usher.Nationality,
                City = usher.City,
                CarAvailability = usher.CarAvailability,
                MedicalCard = usher.MedicalCard,
                Language = usher.Language.Name,
            };  

            return View(model);
        }

        /// <summary>
        /// This action retrieves the create usher page
        /// </summary>
        /// <returns>The create usher view</returns>
        // GET: Usher/Create
        public ActionResult Create()
        {
            ViewBag.languageId = new SelectList(db.Languages, "Id", "Name");
            return View();
        }

        /// <summary>
        /// This action enables the creation of new ushers.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The index view</returns>
        // POST: Usher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsherViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Usher usher = Mapper.Map<UsherViewModel, Usher>(model);

                var usher = new Usher
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    MobileNumber = model.MobileNumber,
                    DateOfBirth = model.DateOfBirth,
                    Gender = model.Gender,
                    Nationality = model.Nationality,
                    City = model.City,
                    CarAvailability = model.CarAvailability,
                    //MedicalCard = model.MedicalCard,
                    LanguageId = model.LanguageId,
                };

                //TODO Remove invalid characters from the filename such as white spaces
                // check if the uploaded file is empty 
                if (model.MedicalCardFile != null && model.MedicalCardFile.ContentLength > 0)
                {
                    // Allowed extensions to be uploaded
                    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };

                    // using System.IO for Path class
                    // Get the file name without the path
                    string filename = Path.GetFileName(model.MedicalCardFile.FileName);

                    // Get the extension of the file
                    string ext = Path.GetExtension(filename).Substring(1);

                    // Check if the extension of the file is in the list of allowed extensions
                    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
                    {
                        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
                        return View();
                    }

                    // Set the application folder where to save the uploaded file
                    string appFolder = "~/Content/Uploads/";

                    // Generate a random string to add to the file name
                    // This is to avoid the files with the same names
                    var rand = Guid.NewGuid().ToString();

                    // Combine the application folder location with the file name
                    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

                    // Save the file in ~/Content/Uploads/filename.xyz
                    model.MedicalCardFile.SaveAs(path);

                    // Add the path to the course object
                    usher.MedicalCard = appFolder + rand + "-" + filename;

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
                    ViewBag.languageId = new SelectList(db.Languages, "Id", "Name");
                    return View();
                }

                db.Ushers.Add(usher);
                db.SaveChanges();

                return RedirectToAction("Index");

            }

            ViewBag.languageId = new SelectList(db.Languages, "Id", "Name");
            return View(model);
        }
        
        /// <summary>
        /// This action shows the information of a selected usher for editing.
        /// It checks if the id and usher are found in the database.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Error page or edit usher view</returns>
        // GET: Usher/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Usher usher = db.Ushers.Find(id);
            if (usher == null)
            {
                return HttpNotFound();
            }

            UsherViewModel model = Mapper.Map<Usher, UsherViewModel>(usher);

            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", usher.LanguageId);
            return View(model);
        }

        /// <summary>
        /// This action enables the admin to edit the usher's information and save it
        /// </summary>
        /// <param name="model"></param>
        /// <returns>Index view</returns>
        // POST: Usher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsherViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usher usher = Mapper.Map<UsherViewModel, Usher>(model);
                db.Entry(usher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.LanguageId = new SelectList(db.Languages, "Id", "Name", model.LanguageId);
            return View(model);
        }

        /// <summary>
        /// This action retrieves the information of a selected usher for deletion.
        /// It checks if the id and usher  exist.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Error page or usher view</returns>
        // GET: Usher/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usher usher = db.Ushers.Find(id);
            if (usher == null)
            {
                return HttpNotFound();
            }
            UsherViewModel model = Mapper.Map<UsherViewModel>(usher);

            return View(model);
        }

        /// <summary>
        /// This action enables the deletion of an usher by the admin.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Index view</returns>
        // POST: Usher/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usher usher = db.Ushers.Find(id);
            db.Ushers.Remove(usher);
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
