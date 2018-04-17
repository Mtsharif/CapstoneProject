/*
 * Description: This file represents the TaskAssignment class
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
    /// This class represents the assignment of tasks to employees.
    /// </summary>
    [Table("TaskAssignment")]
    public partial class TaskAssignment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TaskAssignmentId { get; set; }

        [Required]
        [StringLength(60)]
        public string TaskName { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Column(TypeName = "date")]
        public DateTime AssignmentDate { get; set; }

        public DateTime Deadline { get; set; }

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public bool IsCompleted { get; set; }

        public int EventProjectId { get; set; }

        public int EmployeeId { get; set; }

        public int ClientServiceEmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee AnyEmployee { get; set; }

        public virtual EventProject EventProject { get; set; }

        /// <summary>
        /// The TaskStatus enum indicates the possible status of a task.
        /// </summary>
        public enum TaskStatus
        {
            Pending,

            [Display(Name = "In Progress")]
            InProgress,

            Completed,
            Overdue
        }

        /// <summary>
        /// This enum specifies the possible priorities of a task.
        /// </summary>
        public enum TaskPriority
        {
            High,
            Medium,
            Low
        }
    }
}
