/*
 * Description: This file represents the Project Event Report Template View Model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// This view model is created for a project's event report template. 
    /// It is created based on the event project view model to be used by the event project controller.
    /// </summary>
    public class ProjectEventReportTemplateViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        //// The event report template file as object (used to upload a file)
        //[Display(Name = "Event Report Template File")]
        //public HttpPostedFileBase EventReportTemplateFile { get; set; }

        [Display(Name = "Event Report Template")]
        [DisplayFormat(NullDisplayText = "Event Report Template Not Available")]
        public string EventReportTemplate { get; set; }
    }
}