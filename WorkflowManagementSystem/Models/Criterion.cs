/*
 * Description: This file represents the Criterion class
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
    /// The Criterion class refers to the rubric the projects and ushers will be evaluated by.
    /// </summary>
    [Table("Criterion")]
    public partial class Criterion
    {
        public Criterion()
        {
            ClientSatisfactions = new HashSet<ClientSatisfaction>();
            UsherEvaluations = new HashSet<UsherEvaluation>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CriterionId { get; set; }

        [Required]
        //[StringLength(50)]
        public CriterionName Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public virtual ICollection<ClientSatisfaction> ClientSatisfactions { get; set; }

        public virtual ICollection<UsherEvaluation> UsherEvaluations { get; set; }
    }

    /// <summary>
    /// This enum defines the criteria used for evaluation.
    /// </summary>
    public enum CriterionName
    {
        [Display(Name = "Quality of Work")]
        QualityOfWork,

        Communication,
        Performance,

        [Display(Name = "Overall Satisfactory Level")]
        OverallSatisfactoryLevel
    }
}
