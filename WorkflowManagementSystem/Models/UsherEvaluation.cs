/*
 * Description: This file represents the UsherEvaluation class
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
    /// This class refers to the evaluation of ushers by the Production employee.
    /// </summary>
    [Table("UsherEvaluation")]
    public partial class UsherEvaluation
    {
        public int UsherEvaluationId { get; set; }

        public EvaluationGrade GradeAttained { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateGraded { get; set; }

        [StringLength(200)]
        public string Comment { get; set; }

        public int UsherId { get; set; }

        public int CriterionId { get; set; }

        public int ProductionEmployeeId { get; set; }

        public virtual Criterion Criterion { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual Usher Usher { get; set; }
    }

    /// <summary>
    /// This enum determines the set of grades used to evaluate each criterion.
    /// </summary>
    public enum EvaluationGrade
    {
        Poor,

        Satisfactory,

        //[Display(Name = "Very Good")]
        VeryGood,

        Excellent,
    }
}
