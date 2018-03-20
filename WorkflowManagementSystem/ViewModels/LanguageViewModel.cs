using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
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