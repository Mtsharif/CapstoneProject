/*
 * Description: This file represents the Project 3D model View Model class
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
    /// This view model is created for a project's 3D model. 
    /// It is created based on the event project view model to be used by the event project controller.
    /// </summary>
    public class Project3DModelViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        // The 3D model file as object (used to upload a file)
        [Display(Name = "3D Model File")]
        public HttpPostedFileBase ThreeDModelFile { get; set; }

        [Display(Name = "3D Model")]
        //[DisplayFormat(NullDisplayText = "3D Model Not Available")]
        public string ThreeDModel { get; set; }
    }
}