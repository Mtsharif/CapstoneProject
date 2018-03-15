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

        // GET: Usher
        public ActionResult Index()
        {
            var ushers = db.Ushers.ToList();
            var model = new List<UsherViewModel>();
            foreach (var item in ushers)
            {
                model.Add(new UsherViewModel
                {
                    Id = item.UsherId,
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

            UsherViewModel model = Mapper.Map<Usher, UsherViewModel>(usher);

            return View(model);
        }

        // GET: Usher/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Usher/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsherViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usher usher = Mapper.Map<UsherViewModel, Usher>(model);
                
                db.Ushers.Add(usher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(model);
        }
        
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

            return View(model);
        }

        // POST: Usher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, UsherViewModel model)
        public ActionResult Edit(UsherViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usher usher = Mapper.Map<UsherViewModel, Usher>(model);
               // db.Entry(usher).State = EntityState.Modified;
                db.Entry(usher).State = (usher.UsherId == 0 ? EntityState.Added : EntityState.Modified);

                db.SaveChanges();
                return RedirectToAction("Index");
            }
           
            return View(model);
        }

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
