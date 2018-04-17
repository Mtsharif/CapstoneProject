/*
 * Description: This file represents the Project Schedule View Model class
 * Author: Mtsharif 
 * Date: 7/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// The project schedule view model is based on the project schedule view model to be used by the event project controller.
    /// </summary>
    public class ProjectScheduleViewModel
    {
        [Key]
        //[Display(Name = "Schedule ID")]
        public int ScheduleId { get; set; }

        [Display(Name = "Schedule Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ScheduleDate { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        //[DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public TimeSpan EndTime { get; set; }

        // Event project schedule
        public int EventProjectId { get; set; }

        //public string EventProject  { get; set; }
    }
}