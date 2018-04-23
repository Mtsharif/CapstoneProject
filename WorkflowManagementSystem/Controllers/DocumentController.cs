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
    /// This controller is created based on the document view model and domain model classes.
    /// </summary>
    [Authorize]
    public class DocumentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        /// <summary>
        /// This action is for getting the approval page
        /// </summary>
        /// <param name="id">document id</param>
        /// <returns>Add approval view</returns>
        // GET: Document/AddApproval
        [Authorize(Roles = "CEO")]
        public ActionResult AddApproval(int? id)
        {
            if (id == null)
            {
                return View("Error");
            }
            Document document = db.Documents.Find(id);

            if (document == null)
            {
                return View("Error");
            }

            var model = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Name = document.Name,
                Status = document.Status,
                FilePath = document.FilePath,
                //CEOFeedback = document.CEOFeedback
            };

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return View(model);
        }

        /// <summary>
        /// This action allows the addition of an approval for a document
        /// </summary>
        /// <param name="id">document id</param>
        /// <param name="model"></param>
        /// <returns>Details master view</returns>
        // POST: Document/AddApproval
        [Authorize(Roles = "CEO")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddApproval(int id, DocumentViewModel model)
        {
            if (ModelState.IsValid)
            {
                var document = db.Documents.Find(id);

                if (document == null)
                {
                    return HttpNotFound();
                }

                document.Status = model.Status;
                document.CEOFeedback = model.CEOFeedback;

                db.Entry(document).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("DetailsMaster", "EventProject", new { id = document.EventProjectId });
            }

            ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
            return View();
        }

        /// <summary>
        /// This action is for retrieving an approval's details
        /// </summary>
        /// <param name="id">document id</param>
        /// <returns>details view</returns>
        // GET: Document/ApprovalDetails
        [Authorize(Roles = "Finance Employee, CEO")]
        public ActionResult ApprovalDetails(int? id)
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
                Status = document.Status,
                CEOFeedback = document.CEOFeedback,
                EventProject = document.EventProject.Name
            };

            return View(model);
        }

        //// GET: Document
        //[Authorize(Roles = "Finance Employee")]
        //public ActionResult Index()
        //{
        //    var documents = db.Documents.ToList();
        //    var model = new List<DocumentViewModel>();
        //    foreach (var item in documents)
        //    {
        //        model.Add(new DocumentViewModel
        //        {
        //            DocumentId = item.DocumentId,
        //            Name = item.Name,
        //            FilePath = item.FilePath,
        //            Status = item.Status,
        //            CEOFeedback = item.CEOFeedback,
        //            EventProject = item.EventProject.Name
        //        });
        //    }
        //    return View(model);
        //}

        //// GET: Document/Details/5
        //[Authorize(Roles = "Finance Employee")]
        //public ActionResult Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Document document = db.Documents.Find(id);

        //    if (document == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = new DocumentViewModel
        //    {
        //        DocumentId = document.DocumentId,
        //        Name = document.Name,
        //        FilePath = document.FilePath,
        //        Status = document.Status,
        //        CEOFeedback = document.CEOFeedback,
        //        EventProject = document.EventProject.Name
        //    };

        //    return View(model);
        //}

        //// GET: Document/Create
        //[Authorize(Roles = "Finance Employee")]
        //public ActionResult AddDocument()
        //{
        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //    return View();
        //}

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="model"></param>
        ///// <returns></returns>
        //// POST: Document/Create
        //[Authorize(Roles = "Finance Employee")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult AddDocument(DocumentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var document = new Document
        //        {
        //            Name = model.Name,
        //            Status = DocumentStatus.Pending,
        //            FilePath = model.FilePath,
        //            CEOFeedback = model.CEOFeedback,
        //            EventProjectId = model.EventProjectId
        //        };

        //        //// To upload file
        //        //if (model.DocumentFile != null && model.DocumentFile.ContentLength > 0)
        //        //{
        //        //    var extensions = new[] { "pdf", "docx", "doc", "jpg", "jpeg", "png" };
        //        //    string filename = Path.GetFileName(model.DocumentFile.FileName);
        //        //    string ext = Path.GetExtension(filename).Substring(1);

        //        //    if (!extensions.Contains(ext, StringComparer.OrdinalIgnoreCase))
        //        //    {
        //        //        ModelState.AddModelError(string.Empty, "Accepted file are pdf, docx, doc, jpg, jpeg, and png documents");
        //        //        return PartialView();
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
        //        //    return PartialView();
        //        //}

        //        db.Documents.Add(document);
        //        db.SaveChanges();

        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        return RedirectToAction("Index");
        //    }
        //    else
        //    {
        //        ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //        return View();
        //    }
        //}

        //// GET: Document/Edit/5
        //[Authorize(Roles = "Finance Employee")]
        //public ActionResult Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Document document = db.Documents.Find(id);

        //    if (document == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = new DocumentViewModel
        //    {
        //        DocumentId = document.DocumentId,
        //        Name = document.Name,
        //        FilePath = document.FilePath,
        //        Status = document.Status,
        //        CEOFeedback = document.CEOFeedback,
        //        EventProject = document.EventProject.Name,
        //    };

        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View(model);
        //}

        //// POST: Document/Edit/5
        //[Authorize(Roles = "Finance Employee")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(DocumentViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        Document document = Mapper.Map<DocumentViewModel, Document>(model);

        //        db.Entry(document).State = EntityState.Modified;
        //        db.SaveChanges();

        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");
        //    return View(model);
        //}

        //// GET: Document/Delete/5
        //[Authorize(Roles = "Finance Employee")]
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }

        //    Document document = db.Documents.Find(id);

        //    if (document == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    var model = new DocumentViewModel
        //    {
        //        DocumentId = document.DocumentId,
        //        Name = document.Name,
        //        FilePath = document.FilePath,
        //        CEOFeedback = document.CEOFeedback,
        //        Status = document.Status,
        //        EventProject = document.EventProject.Name,
        //    };

        //    ViewBag.EventProjectId = new SelectList(db.EventProjects, "EventProjectId", "Name");

        //    return View(model);
        //}

        //// POST: Document/Delete/5
        //[Authorize(Roles = "Finance Employee")]
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Document document = db.Documents.Find(id);
        //    db.Documents.Remove(document);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

    }
}
