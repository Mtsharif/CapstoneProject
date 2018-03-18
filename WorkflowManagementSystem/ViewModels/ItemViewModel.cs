﻿/*
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

        //NOTE: If you want to use automapper, the property names of model and viewmodel should be the same
        [Key]
        public int ItemId { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Display(Name = "Unit Cost")]
        [DataType(DataType.Currency)]
        public decimal UnitCost { get; set; }

        // List of cost sheet items
        public virtual List<CostSheetItem> CostSheetItems { get; set; }

        // List of cost variance items
        public virtual List<CostVarianceItem> CostVarianceItems { get; set; }
    }
}