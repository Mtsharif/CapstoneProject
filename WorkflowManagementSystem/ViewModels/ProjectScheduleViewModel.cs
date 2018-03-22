using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    public class ProjectScheduleViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.DateTime )]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy hh:mm:ss}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Start Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan StartTime { get; set; }

        [Display(Name = "End Time")]
        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:hh:mm:ss}", ApplyFormatInEditMode = true)]
        public TimeSpan EndTime { get; set; }

        // Event project schedule
        public int EventProjectId { get; set; }
        public string EventProject  { get; set; }
    }
}