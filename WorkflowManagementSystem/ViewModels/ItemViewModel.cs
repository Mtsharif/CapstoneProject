/*
 * Description: This file represents the Item View Model class
 * Author: Mtsharif 
 * Date: 20/3/2018
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
    /// This Item View Model is generated based on the Item class to be used by the Item controller.
    /// </summary>
    public class ItemViewModel
    {
        public ItemViewModel()
        {
            CostSheetItems = new List<CostSheetItem>();
            CostVarianceItems = new List<CostVarianceItem>();
        }

        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Unit Cost")]
        public decimal UnitCost { get; set; }

        // List of cost sheet items
        public virtual List<CostSheetItem> CostSheetItems { get; set; }

        // List of cost variance items
        public virtual List<CostVarianceItem> CostVarianceItems { get; set; }
    }
}