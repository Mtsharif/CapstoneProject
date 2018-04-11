/*
 * Description: This file represents the Document Controller class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using AutoMapper;
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
    /// 
    /// </summary>
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Document
        public ActionResult Index()
        {
            var documents = db.Documents.ToList();
            var model = new List<DocumentViewModel>();
            foreach (var item in documents)
            {
                model.Add(new DocumentViewModel
                {
                   DocumentId = item.DocumentId,
                   Name = item.Name,
                   FilePath = item.FilePath,
                   Status = item.Status,
                   CEOFeedback = item.CEOFeedback,
                   EventProject = item.EventProject.Name
                });
            }
            return View(model);
        }

        // GET: Document/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            var model = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Name = document.Name,
                FilePath = document.FilePath,
                Status = document.Status,
                CEOFeedback = document.CEOFeedback,
                EventProject = document.EventProject.Name
            };

            return View(model);
        }

        // GET: Document/Create
        public ActionResult AddDocument()
        {
            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        // POST: Document/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public ActionResult AddDocument(DocumentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var document = new Document
        //        {
        //            Name = model.Name,
        //            Status = model.Status,
        //            FilePath = model.FilePath, 
        //            CEOFeedback = model.CEOFeedback,
        //            EventProjectId = model.EventProjectId
        //        };

        //        ////TODO Remove invalid characters from the filename such as white spaces
        //        //// check if the uploaded file is empty 
        //        //if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
        //        //{
        //        //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };

        //        //    string filename = Path.GetFileName(model.DocumentFile.FileName);

        //        //    string ext = Path.GetExtension(filename).Substring(1);

        //        //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
        //        //    {
        //        //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
        //        //        return View();
        //        //    }

        //        //    string appFolder = "~/Content/Uploads/";
        //        //    var rand = Guid.NewGuid().ToString();
        //        //    string path = Path.Combine(Server.MapPath(appFolder), rand + "-" + filename);

        //        //    model.DocumentFile.SaveAs(path);
        //        //    document.FilePath = appFolder + rand + "-" + filename;
        //        //}
        //        //else
        //        //{
        //        //    ModelState.AddModelError(string.Empty, "Empty files are not accepted");
        //        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        //    return View();
        //        //}

        //        db.Documents.Add(document);
        //        db.SaveChanges();

        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        return RedirectToAction("DocumentListPartial", new { id = document.DocumentId });
        //    }
        //    else
        //    {
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        return View();
        //    }
        //}

        // GET: Document/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            var model = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Name = document.Name,
                FilePath = document.FilePath,
                Status = document.Status,
                CEOFeedback = document.CEOFeedback,
                EventProject = document.EventProject.Name,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        // POST: Document/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Document document = Mapper.Map<DocumentViewModel, Document>(model);

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return View(model);
        }

        // GET: Document/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return HttpNotFound();
            }

            var model = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Name = document.Name,
                FilePath = document.FilePath,
                CEOFeedback = document.CEOFeedback,
                Status = document.Status,                
                EventProject = document.EventProject.Name,
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

            return View(model);
        }

        // POST: Document/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Document document = db.Documents.Find(id);
            db.Documents.Remove(document);
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

        //[HttpPost]
        //public ActionResult AddApprovalPartial(DocumentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var document = new Document
        //        {
        //            Status = model.Status,
        //            CEOFeedback = model.CEOFeedback,
        //        };

        //        db.Documents.Add(document);
        //        db.SaveChanges();

        //        return PartialView();
        //    }

        //    return PartialView(model);
        //}

        [HttpPost]
        public ActionResult AddDocumentPartial(DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = new Document
                {
                    Name = model.Name,
                    Status = model.Status,
                    FilePath = model.FilePath,
                    CEOFeedback = model.CEOFeedback,
                    EventProjectId = model.EventProjectId,
                };

                db.Documents.Add(document);
                db.SaveChanges();

                return PartialView();
            }

            return PartialView(model);
        }

        public PartialViewResult DocumentListPartial(int id)
        {
            var documents = db.Documents.Where(d => d.DocumentId == id).ToList();
            var model = new List<DocumentViewModel>();
            foreach (var item in documents)
            {
                model.Add(new DocumentViewModel
                {
                    DocumentId = item.DocumentId,
                    Name = item.Name,
                    FilePath = item.FilePath,
                    Status = item.Status,
                    CEOFeedback = item.CEOFeedback,
                    EventProject = item.EventProject.Name
                });
            }

            return PartialView(model);
        }


    }
}
