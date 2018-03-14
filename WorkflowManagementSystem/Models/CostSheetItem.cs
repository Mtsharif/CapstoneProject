/*
 * Description: This file represents the CostSheetItem class
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
    /// The CostSheetItem class represents the items required for a project and the quantity.
    /// </summary>
    [Table("CostSheetItem")]
    public partial class CostSheetItem
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CostSheetId { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        public virtual CostSheet CostSheet { get; set; }

        public virtual Item Item { get; set; }
    }
}
