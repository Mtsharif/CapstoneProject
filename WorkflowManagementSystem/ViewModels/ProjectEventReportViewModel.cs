/*
 * Description: This file represents the Project Event Report View Model class
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
    /// This view model is created for a project's event report. 
    /// It is created based on the event project view model to be used by the event project controller.
    /// </summary>
    public class ProjectEventReportViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        //The event report file as object (used to upload a file)
        [Display(Name = "Event Report File")]
        public HttpPostedFileBase EventReportFile { get; set; }

        [Display(Name = "Event Report")]
        [DisplayFormat(NullDisplayText = "Event Report Not Available")]
        public string EventReport { get; set; }
    }
}