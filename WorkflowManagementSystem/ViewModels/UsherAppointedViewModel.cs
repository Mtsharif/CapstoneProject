/*
* Description: This file represents the Usher Appointed View Model class
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
    /// This view model is created from the UsherAppointed domain model to be used by its controller
    /// </summary>
    public class UsherAppointedViewModel
    {
        [Key]
        public int UsherAppointedId { get; set; }

        [Display(Name = "Date Assigned")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true, NullDisplayText = "Creation Date Not Available")]
        public DateTime DateAppointed { get; set; }

        // Usher who is appointed
        public int UsherId { get; set; }

        [Display(Name = "Usher")]
        public string Usher { get; set; }
    
        // Event project appointed to
        public int EventProjectId { get; set; }

        [Display(Name = "Event Project")]
        public string EventProject { get; set; }

        // Employee who appointed the usher
        public int ProductionEmployeeId { get; set; }

        [Display(Name = "Assigned By")]
        public string Employee { get; set; }
    }
}