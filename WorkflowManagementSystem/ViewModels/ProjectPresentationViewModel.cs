/*
 * Description: This file represents the Project Presentation View Model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// This view model is created for a project's presentation. 
    /// It is created based on the event project view model to be used by the event project controller.
    /// </summary>
    public class ProjectPresentationViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        ////The presentation file as object (used to upload a file)
        //[Display(Name = "Presentation File")]
        //public HttpPostedFileBase PresentationFile { get; set; }

        [Display(Name = "Presentation")]
        //[DisplayFormat(NullDisplayText = "Presentation Not Available")]
        public string Presentation { get; set; }       
    }
}