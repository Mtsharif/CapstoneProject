/*
 * Description: This file represents the ProjectSchedule class
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
    /// This class represents the schedule of an event. 
    /// </summary>
    [Table("ProjectSchedule")]
    public partial class ProjectSchedule
    {
        [Key]
        public int ScheduleId { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        //[Column(TypeName = "time")]
        //public TimeSpan StartTime { get; set; }
        public DateTime StartTime { get; set; }

        //[Column(TypeName = "time")]
        //public TimeSpan EndTime { get; set; }
        public DateTime EndTime { get; set; }

        public int EventProjectId { get; set; }

        public virtual EventProject EventProject { get; set; }
    }
}
