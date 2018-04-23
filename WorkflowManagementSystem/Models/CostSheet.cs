/*
 * Description: This file represents the CostSheet class
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
    /// The CostSheet contains all costs of all items required for a project. 
    /// This class consists of the items, the CostSheet status, as well as the approvals given.
    /// </summary>
    [Table("CostSheet")]
    public partial class CostSheet
    {
        public CostSheet()
        {
            CostSheetItems = new HashSet<CostSheetItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostSheetId { get; set; }

        //[Required]
        [StringLength(30)]
        public string CostSheetName { get; set; }

        public CostSheetStatus Status { get; set; }

        public CostSheetCEODecision CEODecision { get; set; }

        [StringLength(300)]
        public string CEOFeedback { get; set; }

        public CostSheetFinanceDecision FinanceEmployeeDecision { get; set; }

        [StringLength(300)]
        public string FinanceEmployeeFeedback { get; set; }

        public int ProductionEmployeeId { get; set; }

        public int EventProjectId { get; set; }

        public int CEOEmployeeId { get; set; }

        public int FinanceEmployeeId { get; set; }

        public virtual Employee Employee { get; set; }

        public virtual ICollection<CostSheetItem> CostSheetItems { get; set; }

        public virtual EventProject EventProject { get; set; }

        public virtual Employee FinanceEmployee { get; set; }

        public virtual Employee ProductionEmployee { get; set; }

        public virtual CostVariance CostVariance { get; set; }
    }

    /// <summary>
    /// This enum  indicates the possible status of a cost sheet.
    /// </summary>
    public enum CostSheetStatus
    {
        Pending,

        [Display(Name = "Finance Approved")]
        FinanceApproved,

        [Display(Name = "CEO Approved")]
        CEOApproved,

        [Display(Name = "Finance Rejected")]
        FinanceRejected,

        [Display(Name = "CEO Rejected")]
        CEORejected
    }

    /// <summary>
    /// This enum specifies the decision of the CEO regarding a cost sheet.
    /// </summary>
    public enum CostSheetCEODecision
    {
        Approved,
        Rejected
    }

    /// <summary>
    /// This enum determines the decision of the finance employee regarding a cost sheet.
    /// </summary>
    public enum CostSheetFinanceDecision
    {
        Approved,
        Rejected
    }

}
