/*
 * Description: This file represents the Usher class
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
    /// This class represents the ushers hired in events.
    /// </summary>
    [Table("Usher")]
    public partial class Usher
    {
        public Usher()
        {
            UsherAppointeds = new HashSet<UsherAppointed>();
            UsherEvaluations = new HashSet<UsherEvaluation>();
            Languages = new HashSet<Language>();
        }

        public int UsherId { get; set; }
        

        [Required]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [StringLength(20)]
        public string MobileNumber { get; set; }

        [Column(TypeName = "date")]
        public DateTime? DateOfBirth { get; set; }

        public UsherGender Gender { get; set; }

        [StringLength(30)]
        public string Nationality { get; set; }

        [Required]
        [StringLength(20)]
        public string City { get; set; }

        public bool CarAvailability { get; set; }

        [StringLength(200)]
        public string MedicalCard { get; set; }

        public virtual ICollection<UsherAppointed> UsherAppointeds { get; set; }

        public virtual ICollection<UsherEvaluation> UsherEvaluations { get; set; }

        public virtual ICollection<Language> Languages { get; set; }

        public int? LanguageId { get; set; }
        public virtual Language Language { get; set; }
    }

    /// <summary>
    /// This enum limits the gender types to male and female.
    /// </summary>
    public enum UsherGender
    {
        Male,
        Female
    }
}
