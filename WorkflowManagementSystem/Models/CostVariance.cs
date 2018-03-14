/*
 * Description: This file represents the CostVariance class
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
    /// The CostVariance represents the actual costs paid in a project against the expected.
    /// Thus, this class contains the analysis of the result, feedback, the date submitted and by whom.
    /// </summary>
    [Table("CostVariance")]
    public partial class CostVariance
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CostVariance()
        {
            CostVarianceItems = new HashSet<CostVarianceItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostVarianceId { get; set; }

        public CostVarianceAnalysis Analysis { get; set; }

        [StringLength(300)]
        public string Note { get; set; }

        [Column(TypeName = "date")]
        public DateTime DateSubmitted { get; set; }

        public int FinanceEmployeeId { get; set; }

        public int CostSheetId { get; set; }

        public virtual CostSheet CostSheet { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CostVarianceItem> CostVarianceItems { get; set; }

        public virtual Employee Employee { get; set; }
    }

    /// <summary>
    /// This enum specifies the analysis of a cost variance.
    /// </summary>
    public enum CostVarianceAnalysis
    {
        [Display(Name = "Within Budget")]
        WithinBudget,

        [Display(Name = "Over Budget")]
        OverBudget,
    }
}
