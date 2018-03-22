/*
 * Description: This file represents the Event Project controller class
 * Author: Mtsharif 
 * Date: 7/4/2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WorkflowManagementSystem.Models;
using WorkflowManagementSystem.ViewModels;

namespace WorkflowManagementSystem.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class EventProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: EventProject
        public ActionResult Index()
        {
            var eventprojects = db.EventProjects.ToList();

            var model = new List<EventProjectViewModel>();

            foreach (var item in eventprojects)
            {
                model.Add(new EventProjectViewModel
                {
                    Id = item.EventProjectId,
                    Name = item.Name,
                    EventType = item.EventType,
                    Brief = item.Brief,
                    Street = item.Street,
                    District = item.District,
                    City = item.City,
                    Status = item.Status,
                    Presentation = item.Presentation,
                    EventReportTemplate = item.EventReportTemplate,
                    EventReport = item.EventReport,
                    ThreeDModel = item.ThreeDModel,
                    DateCreated = item.DateCreated,
                });
            }
            return View(model);
        }

        // GET: EventProject/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: EventProject/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: EventProject/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventProject/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: EventProject/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: EventProject/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: EventProject/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
