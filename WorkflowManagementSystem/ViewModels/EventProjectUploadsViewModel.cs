using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    public class EventProjectUploadsViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        //The presentation file as object (used to upload a file)
        //[Display(Name = "Presentation File")]
        //public HttpPostedFileBase PresentationFile { get; set; }

        [Display(Name = "Presentation")]
        [DisplayFormat(NullDisplayText = "Presentation Not Available")]
        public string Presentation { get; set; }

        //// The event report template file as object (used to upload a file)
        //[Display(Name = "Event Report Template File")]
        //public HttpPostedFileBase EventReportTemplateFile { get; set; }

        //[Display(Name = "Event Report Template")]
        //[DisplayFormat(NullDisplayText = "Event Report Template Not Available")]
        //public string EventReportTemplate { get; set; }

        // The event report file as object (used to upload a file)
        //[Display(Name = "Event Report File")]
        //public HttpPostedFileBase EventReportFile { get; set; }

        //[Display(Name = "Event Report")]
        //[DisplayFormat(NullDisplayText = "Event Report Not Available")]
        //public string EventReport { get; set; }

        //// The 3D model file as object (used to upload a file)
        //[Display(Name = "3D Model File")]
        //public HttpPostedFileBase ThreeDModelFile { get; set; }

        //[Display(Name = "3D Model")]
        //[DisplayFormat(NullDisplayText = "3D Model Not Available")]
        //public string ThreeDModel { get; set; }
    }
}
