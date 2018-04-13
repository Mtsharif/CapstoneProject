using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    public class TaskAssignmentViewModel
    {
        [Key]
        public int TaskAssignmentId { get; set; }

        [Display(Name = "Assignment Date")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime AssignmentDate { get; set; }

        // Task to be assigned 
        public int EmployeeTaskId { get; set; }

        [Display(Name = "Task")]
        public string EmployeeTask { get; set; }

        // Employee assigned to
        public int EmployeeId { get; set; }

        [Display(Name = "Assign To")]
        public string AnyEmployee { get; set; }

        // Employee who is assigning
        public int ClientServiceEmployeeId { get; set; }

        [Display(Name = "Assigned By")]
        public string Employee { get; set; }
    }
}