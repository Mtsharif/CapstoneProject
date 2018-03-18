/*
 * Description: This file represents the ClientSatisfaction class
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
    /// The ClientSatisfaction class refers to the clients' evaluation of the organization's services.
    /// Clients evaluate the execution of an event project through a set of criteria and the result is the client satisfaction.
    /// </summary>
    [Table("ClientSatisfaction")]
    public partial class ClientSatisfaction
    {
        public int ClientSatisfactionId { get; set; }

        public SatisfactionGrade GradeAttained { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateEvaluated { get; set; }

        [StringLength(300)]
        public string Comment { get; set; }

        public int CriterionId { get; set; }

        public int EventProjectId { get; set; }

        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        public virtual Criterion Criterion { get; set; }

        public virtual EventProject EventProject { get; set; }
    }

    /// <summary>
    /// This enum determines the set of grades used to evaluate each criterion.
    /// </summary>
    public enum SatisfactionGrade
    {
        Poor,
        Satisfactory,

        [Display(Name = "Very Good")]
        VeryGood, 

        Excellent,
    }
}
