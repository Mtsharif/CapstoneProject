/*
 * Description: This file represents the Usher Evaluation Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using AutoMapper;
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
    /// 
    /// </summary>
    public class UsherEvaluationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: UsherEvaluation
        public ActionResult Index()
        {
            var usherEvaluations = db.UsherEvaluations.ToList();
            var model = new List<UsherEvaluationViewModel>();

            foreach (var item in usherEvaluations)
            {
                model.Add(new UsherEvaluationViewModel
                {
                    UsherEvaluationId = item.UsherEvaluationId,
                    GradeAttained = item.GradeAttained,
                    DateGraded = item.DateGraded,
                    Comment = item.Comment,
                    Usher = item.Usher.FullName,
                    Criterion = item.Criterion.Name,
                    Employee = item.Employee.FullName
                });
            }
            return View(model);
        }

        // GET: UsherEvaluation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherEvaluation usherEvaluation = db.UsherEvaluations.Find(id);

            if (usherEvaluation == null)
            {
                return HttpNotFound();
            }

            var model = new UsherEvaluationViewModel
            {
                UsherEvaluationId = usherEvaluation.UsherEvaluationId,
                DateGraded = usherEvaluation.DateGraded,
                Comment = usherEvaluation.Comment,
                Usher = usherEvaluation.Usher.FullName,
                Employee = usherEvaluation.Employee.FullName,
                Criterion = usherEvaluation.Criterion.Name
            };

            return View(model);
        }

        // GET: UsherEvaluation/Create
        public ActionResult Evaluate()
        {
            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");
            return View();
        }

        // POST: UsherEvaluation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Evaluate(UsherEvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var usherEvaluation = new UsherEvaluation
                {
                    GradeAttained = model.GradeAttained,
                    DateGraded = DateTime.Now,
                    Comment = model.Comment,
                    UsherId = model.UsherId,
                    CriterionId = model.CriterionId,
                    ProductionEmployeeId = User.Identity.GetUserId<int>(),
                };

                db.UsherEvaluations.Add(usherEvaluation);
                db.SaveChanges();

                ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");

                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
                ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
                ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");
                return View();
            }
        }

        // GET: UsherEvaluation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherEvaluation usherEvaluation = db.UsherEvaluations.Find(id);

            if (usherEvaluation == null)
            {
                return HttpNotFound();
            }

            var model = new UsherEvaluationViewModel
            {
                UsherEvaluationId = usherEvaluation.UsherEvaluationId,
                GradeAttained = usherEvaluation.GradeAttained,
                DateGraded = DateTime.Now,
                Comment = usherEvaluation.Comment,
                Usher = usherEvaluation.Usher.FullName,
                Employee = usherEvaluation.Employee.FullName,
                Criterion = usherEvaluation.Criterion.Name
            };

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");

            return View(model);
        }

        // POST: UsherEvaluation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UsherEvaluationViewModel model)
        {
            if (ModelState.IsValid)
            {
                UsherEvaluation usherEvaluation = Mapper.Map<UsherEvaluationViewModel, UsherEvaluation>(model);
                db.Entry(usherEvaluation).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");

            return View(model);
        }

        // GET: UsherEvaluation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UsherEvaluation usherEvaluation = db.UsherEvaluations.Find(id);

            if (usherEvaluation == null)
            {
                return HttpNotFound();
            }

            UsherEvaluationViewModel model = new UsherEvaluationViewModel
            {
                UsherEvaluationId = usherEvaluation.UsherEvaluationId,
                GradeAttained = usherEvaluation.GradeAttained,
                DateGraded = DateTime.Now,
                Comment = usherEvaluation.Comment,
                Usher = usherEvaluation.Usher.FullName,
                Employee = usherEvaluation.Employee.FullName,
                Criterion = usherEvaluation.Criterion.Name
            };

            ViewBag.UsherId = new SelectList(db.Ushers, "UsherId", "FullName");
            ViewBag.ProductionEmployeeId = new SelectList(db.Employees, "Id", "FullName");
            ViewBag.CriterionId = new SelectList(db.Criteria, "CriterionId", "Name");

            return View(model);
        }

        // POST: UsherEvaluation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UsherEvaluation usherEvaluation = db.UsherEvaluations.Find(id);
            db.UsherEvaluations.Remove(usherEvaluation);
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
