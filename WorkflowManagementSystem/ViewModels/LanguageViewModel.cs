/*
 * Description: This file represents the Language View Model class
 * Author: Mtsharif 
 * Date: 7/4/2018
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// This view model is created for the languages an ushers speaks.
    /// </summary>
    public class LanguageViewModel
    {
        public LanguageViewModel()
        {
            Ushers = new List<Usher>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        // List of ushers
        public virtual List<Usher> Ushers { get; set; }
    }
}