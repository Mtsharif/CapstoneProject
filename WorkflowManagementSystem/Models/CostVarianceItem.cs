/*
 * Description: This file represents the CostVarianceItem class
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
    /// This class represents the items used in a project, the actual cost paid, and the actual quantity.
    /// </summary>
    [Table("CostVarianceItem")]
    public partial class CostVarianceItem
    {
        [Key]
        [Column(Order = 0)]
       // [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostVarianceId { get; set; }

        [Key]
        [Column(Order = 1)]
       // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        public decimal ActualCost { get; set; }

        public int ActualQuantity { get; set; }

        public virtual CostVariance CostVariance { get; set; }

        public virtual Item Item { get; set; }
    }
}
