/*
 * Description: This file represents the Event Project View Model class
 * Author: Mtsharif 
 * Date: 7/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// Event project view model is created based on the event project domain model to be used by the event project controller.
    /// </summary>
    public class EventProjectViewModel
    {
        public EventProjectViewModel()
        {
            ClientSatisfactions = new List<ClientSatisfaction>();
            CostSheets = new List<CostSheet>();
            Documents = new List<Document>();
            ProjectSchedules = new List<ProjectSchedule>();
            EmployeeTasks = new List<EmployeeTask>();
            UsherAppointeds = new List<UsherAppointed>();
        }

        [Key]
        [Display(Name = "Project ID")]
        public int EventProjectId { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Event Type")]
        public EventProjectType EventType { get; set; }

        [Required]
        [Display(Name = "Brief")]
        [DataType(DataType.MultilineText)]
        public string Brief { get; set; }

        [Display(Name = "Street")]
        [DisplayFormat(NullDisplayText = "Street Not Specified")]
        public string Street { get; set; }

        [Display(Name = "District")]
        [DisplayFormat(NullDisplayText = "District Not Specified")]
        public string District { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "City Not Specified")]
        public string City { get; set; }

        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "Creation Date Not Available")]
        public DateTime? DateCreated { get; set; }

       // The employee who created the event project
        public int ClientServiceEmployeeId { get; set; }

        [Display(Name = "Created By")]
        public string Employee { get; set; }

        // Event project client
        public int ClientId { get; set; }
        public string Client { get; set; }

        // TO DO: Event project does not have many client satisfactions!
        public List<ClientSatisfaction> ClientSatisfactions { get; set; }

        // List of cost sheets in an event project
        public List<CostSheet> CostSheets { get; set; }

        // List of documents in an event project
        public List<Document> Documents { get; set; }

        // List of schedules of event project
        public List<ProjectSchedule> ProjectSchedules { get; set; }

        // List of tasks to be done by employees for an event project
        public List<EmployeeTask> EmployeeTasks { get; set; }

        // List of ushers appointed to event projects
        public List<UsherAppointed> UsherAppointeds { get; set; }


        // Tabs Section

        // The presentation file as object (used to upload a file)
        //[Display(Name = "Presentation File")]
        //public HttpPostedFileBase PresentationFile { get; set; }

        //[Display(Name = "Presentation")]
        //[DisplayFormat(NullDisplayText = "Presentation Not Available")]
        //public string Presentation { get; set; }

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