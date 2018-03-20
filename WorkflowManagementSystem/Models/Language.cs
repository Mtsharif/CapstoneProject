/*
 * Description: This file represents the Language class
 * Author: Mtsharif 
 * Date: 20/3/2018
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkflowManagementSystem.Models
{
    /// <summary>
    /// This class represents a set of languages
    /// </summary>
    [Table("Language")]
    public class Language
    {
        public Language()
        {
            Ushers = new HashSet<Usher>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Usher> Ushers { get; set; }

    }
}