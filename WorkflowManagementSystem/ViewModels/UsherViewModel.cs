﻿/*
 * Description: This file represents the Usher View Model class
 * Author: Mtsharif 
 * Date: 20/3/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using WorkflowManagementSystem.Models;

namespace WorkflowManagementSystem.ViewModels
{
    /// <summary>
    /// The Usher View Model is created from the usher model and used by the usher controller.
    /// </summary>
    public class UsherViewModel
    {
        public UsherViewModel()
        {
            UsherAppointeds = new List<UsherAppointed>();
            UsherEvaluations = new List<UsherEvaluation>();
            UsherLanguages = new List<UsherLanguage>();
        }

        //NOTE: If you want to use automapper, the property names of model and viewmodel should be the same
        [Key]
        public int UsherId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Display(Name = "Gender")]
        public UsherGender Gender { get; set; }

        [Display(Name = "Nationality")]
        public string Nationality { get; set; }

        [Required]
        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "Car Availability")]
        public bool CarAvailability { get; set; }

        // The medical card file as object (used to upload a file)
        [Display(Name = "Medical Card File")]
        public HttpPostedFileBase MedicalCardFile  { get; set; }

        [Display(Name = "Medical Card")]
        public string MedicalCard { get; set; }

        // List of usher appointed
        public virtual List<UsherAppointed> UsherAppointeds { get; set; }

        // List of ushers evaluated
        public virtual List<UsherEvaluation> UsherEvaluations { get; set; }

        // List of usher languages
        [Display(Name = "Language")]
        public virtual List<UsherLanguage> UsherLanguages { get; set; }
    }
}