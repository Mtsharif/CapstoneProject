/*
 * Description: This file represents the Employee View Model class
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
    /// Employee view model is generated from the employee model to be used by the employee controller.
    /// </summary>
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Job Title")]
        public EmployeeJobTitle JobTitle { get; set; }

        [Display(Name = "Department")]
        public EmployeeDepartment Department { get; set; }

        [Display(Name = "Employee Type")]
        public UserType EmployeeType { get; set; }

        // To display the list of roles
        public string Roles { get; set; }
    }
}