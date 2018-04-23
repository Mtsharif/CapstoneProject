/*
 * Description: This file represents the Cost Sheet View Model class
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
    /// The cost sheet view model is created based on the cost sheet model to be used by the cost sheet controller.
    /// </summary>
    public class CostSheetViewModel
    {
        public CostSheetViewModel()
        {
            CostSheetItems = new List<CostSheetItem>();     
        }

        [Key]
        public int CostSheetId { get; set; }

        //[Required]
        [Display(Name = "Name")]
        public string CostSheetName { get; set; }

        [Display(Name = "Status")]
        public CostSheetStatus Status { get; set; }

        // Employee creating the cost sheet
        public int ProductionEmployeeId { get; set; }

        [Display(Name = "Created By")]
        public string ProductionEmployee { get; set; }

        // Cost sheet for an event project
        public int EventProjectId { get; set; }

        [Display(Name = "Project")]
        public string EventProject { get; set; }

        // Approvals

        // CEO Approval
        [Display(Name = "Decision")]
        public CostSheetCEODecision CEODecision { get; set; }

        [Display(Name = "Feedback")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Feedback is not available.")]
        public string CEOFeedback { get; set; }

        // CEO approving the cost sheet
        public int CEOEmployeeId { get; set; }

        [Display(Name = "Approved By")]
        public string Employee { get; set; }

        // Finance Approval
        [Display(Name = "Decision")]
        public CostSheetFinanceDecision FinanceEmployeeDecision { get; set; }

        [Display(Name = "Feedback")]
        [DataType(DataType.MultilineText)]
        [DisplayFormat(NullDisplayText = "Feedback is not available.")]
        public string FinanceEmployeeFeedback { get; set; }

        // Finance employee approving the cost sheet
        public int FinanceEmployeeId { get; set; }

        [Display(Name = "Approved By")]
        public string FinanceEmployee { get; set; }

        // The cost variance of a cost sheet
        //public string CostVariance { get; set; }

        // The list of items in a cost sheet
        public List<CostSheetItem> CostSheetItems { get; set; }
    }
}