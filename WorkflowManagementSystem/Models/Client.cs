/*
 * Description: This file represents the Client class
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
    /// The Client class applies to all people using the organization's services 
    /// </summary>
    [Table("Client")]
    public partial class Client
    {
        public Client()
        {
            ClientSatisfactions = new HashSet<ClientSatisfaction>();
            EventProjects = new HashSet<EventProject>();
        }

        public int ClientId { get; set; }

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(80)]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [StringLength(25)]
        public string Street { get; set; }

        [StringLength(25)]
        public string District { get; set; }

        [StringLength(25)]
        public string City { get; set; }

        // To get the fullname (first name and last name) of a client in a dropdown list.
        [NotMapped]
        public string FullName { get { return (FirstName + " " + LastName); } }

        public virtual ICollection<ClientSatisfaction> ClientSatisfactions { get; set; }

        public virtual ICollection<EventProject> EventProjects { get; set; }
    }
}
