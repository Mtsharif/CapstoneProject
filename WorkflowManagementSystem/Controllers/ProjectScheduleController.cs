/*
 * Description: This file represents the Project Schedule Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using AutoMapper;
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
    /// This controller is generated from the Project Schedule view model and domain model classes
    /// </summary>
    public class ProjectScheduleController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action retrieves the schedule edit page
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <returns>Edit view</returns>
        // GET: ProjectSchedule/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectSchedule projectSchedule = db.ProjectSchedules.Find(id);

            if (projectSchedule == null)
            {
                return HttpNotFound();
            }

            var model = new ProjectScheduleViewModel
            {
                ScheduleId = projectSchedule.ScheduleId,
                ScheduleDate = projectSchedule.Date,
                StartTime = projectSchedule.StartTime,
                EndTime = projectSchedule.EndTime,
                EventProjectId = projectSchedule.EventProjectId
            };

            return View(model);
        }

        /// <summary>
        /// This action enables a schedule to be edited
        /// </summary>
        /// <param name="model">Project schedule model</param>
        /// <returns>Index view</returns>
        // POST: ProjectSchedule/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ProjectScheduleViewModel model)
        {
            if (ModelState.IsValid)
            {
                ProjectSchedule projectSchedule = Mapper.Map<ProjectScheduleViewModel, ProjectSchedule>(model);
                db.Entry(projectSchedule).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        /// <summary>
        /// This action retrieves the delete page of a schedule
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <returns>Delete view</returns>
        // GET: ProjectSchedule/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ProjectSchedule projectSchedule = db.ProjectSchedules.Find(id);

            if (projectSchedule == null)
            {
                return HttpNotFound();
            }

            ProjectScheduleViewModel model = new ProjectScheduleViewModel
            {
                ScheduleId = projectSchedule.ScheduleId,
                ScheduleDate = projectSchedule.Date,
                StartTime = projectSchedule.StartTime,
                EndTime = projectSchedule.EndTime,
            };

            return View(model);
        }

        /// <summary>
        /// This action allows the deletion of a schedule
        /// </summary>
        /// <param name="id">Schedule id</param>
        /// <returns>Index view</returns>
        // POST: ProjectSchedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id, ProjectScheduleViewModel model)
        {
            //var projectSchedule = db.ProjectSchedules.Find(model.ScheduleId);

            ProjectSchedule projectSchedule = db.ProjectSchedules.Find(id);
            db.ProjectSchedules.Remove(projectSchedule);
            db.SaveChanges();
            return RedirectToAction("DetailsMaster","EventProject", new { id = model.EventProjectId });
            //return RedirectToAction("DetailsMaster", "EventProject", new { projectId = model.EventProjectId });
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
