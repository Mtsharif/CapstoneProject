/*
 * Description: This file represents the Item class
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
    /// This class contains  all items that could be used in an event and their unit costs.
    /// </summary>
    [Table("Item")]
    public partial class Item
    {
        public Item()
        {
            CostSheetItems = new HashSet<CostSheetItem>();
            CostVarianceItems = new HashSet<CostVarianceItem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ItemId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public decimal UnitCost { get; set; }

        // To show an item and its price
        [NotMapped]
        public string ItemPrice { get { return (Name + " " + UnitCost); } }

        public virtual ICollection<CostSheetItem> CostSheetItems { get; set; }

        public virtual ICollection<CostVarianceItem> CostVarianceItems { get; set; }
    }
}
