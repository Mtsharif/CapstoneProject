/*
 * Description: This file represents the EmployeeTask class
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
    /// This class represents the tasks employees are required to complete.
    /// </summary>
    [Table("EmployeeTask")]
    public partial class EmployeeTask
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeTask()
        {
            TaskAssignments = new HashSet<TaskAssignment>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmployeeTaskId { get; set; }

        [Required]
        [StringLength(60)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime Deadline { get; set; }

        public TaskStatus Status { get; set; }

        public TaskPriority Priority { get; set; }

        public int EventProjectId { get; set; }

        public int ClientServiceEmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual EventProject EventProject { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<TaskAssignment> TaskAssignments { get; set; }
    }

    /// <summary>
    /// The TaskStatus enum indicates the possible status of a task.
    /// </summary>
    public enum TaskStatus
    {
        Pending,

        //[Display(Name = "In Progress")]
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

        Low,
    }
}
