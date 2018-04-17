/*
 * Description: This file represents the Document View Model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// Document view model is generated based on the document domain model class.
    /// </summary>
    public class DocumentViewModel
    {
        [Key]
        public int DocumentId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "File Path")]
        public string FilePath { get; set; }

        // The file as object (used to upload a file)
        //[Required]
        [Display(Name = "Document File")]
        public HttpPostedFileBase DocumentFile { get; set; }

        [Display(Name = "Status")]
        public DocumentStatus Status { get; set; }

        [Display(Name = "Feedback")]
        [DisplayFormat(NullDisplayText = "Feedback is not available.")]
        public string CEOFeedback { get; set; }

        // Documents in an event project
        public int EventProjectId { get; set; }

        [Display(Name = "Project")]
        public string EventProject { get; set; }
    }
}