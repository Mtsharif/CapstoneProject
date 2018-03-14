/*
 * Description: This file represents the Document class
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
    /// The Document class refers to the documents used in a project; which are mainly quotations and invoices.
    /// </summary>
    [Table("Document")]
    public partial class Document
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocumentId { get; set; }

        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public DocumentStatus Status { get; set; }

        [Required]
        [StringLength(200)]
        public string FilePath { get; set; }

        [StringLength(300)]
        public string CEOFeedback { get; set; }

        public int EventProjectId { get; set; }

        public virtual EventProject EventProject { get; set; }
    }

    /// <summary>
    /// This enum specifies the current status of a document.
    /// </summary>
    public enum DocumentStatus
    {
        Pending,

        Approved,

        Rejected,
    }
}
