/*
 * Description: This file represents the Usher Evaluation view model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// This view model is created based on the Usher Evaluation domain model to be used by its controller.
    /// </summary>
    public class UsherEvaluationViewModel
    {
        [Key]
        public int UsherEvaluationId { get; set; }

        [Display(Name = "Grade Attained")]
        public EvaluationGrade GradeAttained { get; set; }

        [Display(Name = "Date Evaluated")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateGraded { get; set; }

        [Display(Name = "Comment")]
        [DisplayFormat(NullDisplayText = "Comment Not Available")]
        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        // The usher evaluated
        public int UsherId { get; set; }

        [Display(Name = "Usher")]
        public string Usher { get; set; }

        // The criterion used to evaluate the usher
        public int CriterionId { get; set; }

        [Display(Name = "Criterion")]
        public string Criterion { get; set; }

        // Employee who is evaluating
        public int ProductionEmployeeId { get; set; }

        [Display(Name = "Evaluated By")]
        public string Employee { get; set; }
    }
}