using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    public class UsherLanguageViewModel
    {
        [Key]
        public int UsherId { get; set; }

        // public int Id { get; set; }

        [Key]
        //public string Language { get; set; }
        public Languages Language { get; set; }

        public enum Languages
        {
            English,
            Arabic
        }

        public string Usher { get; set; }

    }
}