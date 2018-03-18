/*
 * Description: This file represents the Employee class
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
    /// The Employee class refers to all people working for the organization.
    /// </summary>
    [Table("Employee")]
    public partial class Employee : ApplicationUser
    {
        public Employee()
        {
            CostSheets = new HashSet<CostSheet>();
            ConfirmCostSheets = new HashSet<CostSheet>();
            SubmitCostSheets = new HashSet<CostSheet>();
            CostVariances = new HashSet<CostVariance>();
            EventProjects = new HashSet<EventProject>();
            EmployeeTasks = new HashSet<EmployeeTask>();
            TaskAssignments = new HashSet<TaskAssignment>();
            AssignedTasks = new HashSet<TaskAssignment>();
            UsherAppointeds = new HashSet<UsherAppointed>();
            UsherEvaluations = new HashSet<UsherEvaluation>();
        }

        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        //public int EmployeeId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        //[StringLength(20)]
        //public string MobileNumber { get; set; }

        //[Required]
        //[StringLength(80)]
        //public string Email { get; set; }

        //[StringLength(50)]
        public EmployeeJobTitle JobTitle { get; set; }

        //[StringLength(20)]
        public Department Department { get; set; }

        //[Required]
        //[StringLength(15)]
        //public string Password { get; set; }

        public EmployeeType EmployeeType { get; set; }

        public virtual ICollection<CostSheet> CostSheets { get; set; }

        public virtual ICollection<CostSheet> ConfirmCostSheets { get; set; }

        public virtual ICollection<CostSheet> SubmitCostSheets { get; set; }

        public virtual ICollection<CostVariance> CostVariances { get; set; }

        public virtual ICollection<EventProject> EventProjects { get; set; }

        public virtual ICollection<EmployeeTask> EmployeeTasks { get; set; }

        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }

        public virtual ICollection<TaskAssignment> AssignedTasks { get; set; }

        public virtual ICollection<UsherAppointed> UsherAppointeds { get; set; }

        public virtual ICollection<UsherEvaluation> UsherEvaluations { get; set; }
    }

    public enum EmployeeJobTitle
    {
        CEO,
        Director,
        Accountant,
        Designer,

        [Display(Name = "Event Planner")]
        EventPlanner,

        Assistant,

        [Display(Name = "Client Account Executive")]
        ClientAccountExecutive
    }

    /// <summary>
    /// EmployeeDepartment is an enum specifiying the types of departments in the organization. 
    /// </summary>
    public enum Department
    {
        [Display(Name = "Client Service")]
        ClientService, 

        Production,
        Finance,
        Creative
    }

    /// <summary>
    /// UserType is an enum specifying the types of employees/users of the application.
    /// </summary>
    public enum EmployeeType
    {
        [Display(Name = "Client Service")]
        ClientService,

        Production,
        Finance, 
        Creative, 
        CEO,

        [Display(Name = "Event Planner")]
        EventPlanner, 

        Administrator
    }
}