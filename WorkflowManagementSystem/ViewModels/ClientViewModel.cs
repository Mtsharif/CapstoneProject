/*
 * Description: This file represents the Client View Model class
 * Author: Mtsharif 
 * Date: 7/4/2018
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
    /// The client view model is created based on the client domain model in order for the client controller to use it
    /// </summary>
    public class ClientViewModel
    {
        public ClientViewModel()
        {
            ClientSatisfactions = new List<ClientSatisfaction>();
            EventProjects = new List<EventProject>();
        }

        [Key]
        [Display(Name = "Client ID")]
        public int ClientId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [DisplayFormat(NullDisplayText = "Last Name Not Available")]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        // [Phone]
        [Display(Name = "Mobile Number")]
        public string MobileNumber { get; set; }

        [Display(Name = "Street")]
        [DisplayFormat(NullDisplayText = "Street Not Specified")]
        public string Street { get; set; }

        [Display(Name = "District")]
        [DisplayFormat(NullDisplayText = "District Not Specified")]
        public string District { get; set; }

        [Display(Name = "City")]
        [DisplayFormat(NullDisplayText = "City Not Specified")]
        public string City { get; set; }

        // List of client satisfactions done
        public List<ClientSatisfaction> ClientSatisfactions { get; set; }

        // List of events executed
        public List<EventProject> EventProjects { get; set; }
    }
}