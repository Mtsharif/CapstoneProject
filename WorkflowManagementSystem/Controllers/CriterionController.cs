///*
// * Description: This file represents the Criterion Controller class
// * Author: Mtsharif 
// * Date: 18/4/2018
// */

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
//    /// This controller is created based on the criterion domain and view model.
//    /// It is used to add criterions in the database as well as edit, list, and delete them.
//    /// </summary>
//    public class CriterionController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        /// <summary>
//        /// 
//        /// </summary>
//        /// <returns></returns>
//        // GET: Criterion
//        public ActionResult Index()
//        {
//            var criteria = db.Criteria.ToList();
//            var model = new List<CriterionViewModel>();

//            foreach (var item in criteria)
//            {
//                model.Add(new CriterionViewModel
//                {
//                    CriterionId = item.CriterionId,
//                    Name = item.Name,
//                    Description = item.Description,
//                });
//            }
//            return View(model);
//        }


//        // GET: Criterion/Create
//        public ActionResult Create()
//        {
//            return View();
//        }

//        // POST: Criterion/Create
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create(CriterionViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var criterion = new Criterion
//                {
//                    Name = model.Name,
//                    Description = model.Description,
//                };

//                db.Criteria.Add(criterion);
//                db.SaveChanges();

//                return RedirectToAction("Index");
//            }

//            return View(model);
//        }

//        // GET: Criterion/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Criterion criterion = db.Criteria.Find(id);

//            if (criterion == null)
//            {
//                return HttpNotFound();
//            }

//            CriterionViewModel model = new CriterionViewModel
//            {
//                CriterionId = criterion.CriterionId,
//                Name = criterion.Name,
//                Description = criterion.Description
//            };

//            return View(model);
//        }

//        // POST: Criterion/Edit/5
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit(int id, CriterionViewModel model)
//        {
//            if (ModelState.IsValid)
//            {
//                var criterion = db.Criteria.Find(id);
//                if (criterion == null)
//                {
//                    return HttpNotFound();
//                }

//                criterion.Name = model.Name;
//                criterion.Description = model.Description;

//                db.Entry(criterion).State = EntityState.Modified;
//                db.SaveChanges();

//                return RedirectToAction("Index");
//            }

//            return View();
//        }

//        // GET: Criterion/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Criterion criterion = db.Criteria.Find(id);

//            if (criterion == null)
//            {
//                return HttpNotFound();
//            }

//            var model = new CriterionViewModel
//            {
//                CriterionId = criterion.CriterionId,
//                Name = criterion.Name,
//                Description = criterion.Description
//            };

//            return View(model);
//        }

//        // POST: Criterion/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult Delete(int id)
//        {
//            Criterion criterion = db.Criteria.Find(id);

//            db.Criteria.Remove(criterion);
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
//    }
//}
