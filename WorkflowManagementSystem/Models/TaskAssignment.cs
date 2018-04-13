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

        [Column(TypeName = "date")]
        public DateTime AssignmentDate { get; set; }

        //public int TaskId { get; set; }
        public int EmployeeTaskId { get; set; }

        public int EmployeeId { get; set; }

        public int ClientServiceEmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Employee AnyEmployee { get; set; }

        public virtual EmployeeTask EmployeeTask { get; set; }
    }
}
