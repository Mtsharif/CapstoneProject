/*
 * Description: This file represents the EventProject class
 * Author: Mtsharif 
 * Date: 27/2/2018
 */

namespace WorkflowManagementSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    /// <summary>
    /// The EventProject class refers to all events executed by the organization.
    /// </summary>
    [Table("EventProject")]
    public partial class EventProject
    {
        public EventProject()
        {
            ClientSatisfactions = new HashSet<ClientSatisfaction>();
            CostSheets = new HashSet<CostSheet>();
            Documents = new HashSet<Document>();
            ProjectSchedules = new HashSet<ProjectSchedule>();
            EmployeeTasks = new HashSet<EmployeeTask>();
            UsherAppointeds = new HashSet<UsherAppointed>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EventProjectId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [Required]
        //[StringLength(30)]
        public EventProjectType EventType { get; set; }

        [Required]
        [StringLength(1000)]
        public string Brief { get; set; }

        [StringLength(25)]
        public string Street { get; set; }

        [StringLength(25)]
        public string District { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        public ProjectStatus Status { get; set; }

        [StringLength(200)]
        public string Presentation { get; set; }

        [StringLength(200)]
        public string EventReportTemplate { get; set; }

        [StringLength(200)]
        public string EventReport { get; set; }

        [StringLength(200)]
        public string ThreeDModel { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateCreated { get; set; }

        public int ClientServiceEmployeeId { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual ICollection<ClientSatisfaction> ClientSatisfactions { get; set; }

        public virtual ICollection<CostSheet> CostSheets { get; set; }

        public virtual ICollection<Document> Documents { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<ProjectSchedule> ProjectSchedules { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }

        public virtual ICollection<UsherAppointed> UsherAppointeds { get; set; }
    }

    /// <summary>
    /// This enum specifies the possible types of events.
    /// </summary>
    public enum EventProjectType
    {
        Conference,

        //[Display(Name = "Product Launch")]
        ProductLaunch,

        Seminar,

        Forum, 

        Exhibition,

        Meeting,

        //[Display(Name = "Business Dinner")]
        BusinessDinner,

        //[Display(Name = "Award Ceremony")]
        AwardCeremony,

        Wedding, 

        Birthday,

        //[Display(Name = "Organization Milestone")]
        OrganizationMilestone,

        Festival,   
    }

    public enum ProjectStatus
    {
        Pending,

        Approved, 

        Rejected,
    }
}
