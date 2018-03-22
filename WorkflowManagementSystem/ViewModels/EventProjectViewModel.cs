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

        public int Id { get; set; }

        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Display(Name = "Event Type")]
        public EventProjectType EventType { get; set; }

        [Display(Name = "Brief")]
        public string Brief { get; set; }

        [Display(Name = "Street")]
        public string Street { get; set; }

        [Display(Name = "District")]
        public string District { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; }

        [Display(Name = "Presentation")]
        public string Presentation { get; set; }

        [Display(Name = "Event Report Template")]
        public string EventReportTemplate { get; set; }

        [Display(Name = "Event Report")]
        public string EventReport { get; set; }

        [Display(Name = "3D Model")]
        public string ThreeDModel { get; set; }

        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateCreated { get; set; }

        // The employee who created the event project
        public int ClientServiceEmployeeId { get; set; }

        // Event project client
        public int ClientId { get; set; }
        public string Client { get; set; }

        // TO DO: Event project does not have many client satisfactions!
        public List<ClientSatisfaction> ClientSatisfactions { get; set; }

        // List of cost sheets in an event project
        public List<CostSheet> CostSheets { get; set; }

        // List of documents in an event project
        public List<Document> Documents { get; set; }

        public string Employee { get; set; }

        // List of schedules of event project
        public List<ProjectSchedule> ProjectSchedules { get; set; }

        // List of tasks to be done by employees for an event project
        public List<EmployeeTask> EmployeeTasks { get; set; }

        // List of ushers appointed to event projects
        public List<UsherAppointed> UsherAppointeds { get; set; }
    }
}