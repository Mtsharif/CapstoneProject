/*
 * Description: This file represents the Criterion view model class
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
    /// Criterion view model is generated based on the criterion domain model to be used by the criterion controller.
    /// </summary>
    public class CriterionViewModel
    {
        public CriterionViewModel()
        {
            ClientSatisfactions = new List<ClientSatisfaction>();
            UsherEvaluations = new List<UsherEvaluation>();
        }

        [Key]
        public int CriterionId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DisplayFormat(NullDisplayText = "Description Not Available")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public List<ClientSatisfaction> ClientSatisfactions { get; set; }

        public List<UsherEvaluation> UsherEvaluations { get; set; }
    }
}