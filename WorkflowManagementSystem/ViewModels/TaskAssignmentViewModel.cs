/*
 * Description: This file represents the Task Assignment view model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;
using static WorkflowManagementSystem.Models.TaskAssignment;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// This view model is created based on the Task Assignment domain model to be used by its controller. 
    /// </summary>
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

        [Display(Name = "Deadline")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "Deadline Not Available")]
        public DateTime Deadline { get; set; }

        [Display(Name = "Status")]
        public TaskStatus Status { get; set; }

        [Display(Name = "Priority")]
        public TaskPriority Priority { get; set; }

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