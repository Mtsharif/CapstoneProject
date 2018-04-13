/*
 * Description: This file represents the Cost Sheet View Model class
 * Author: Mtsharif 
 * Date: 18/4/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// 
    /// </summary>
    public class CostSheetItemViewModel
    {
        [Key]
        [Column(Order = 0)]
        public int CostSheetId { get; set; }

        [Key]
        [Column(Order = 1)]
        public int ItemId { get; set; }

        [Display(Name = "Quantity")]
        public int Quantity { get; set; }

        public string CostSheet { get; set; }
        public string Item { get; set; }
    }
}