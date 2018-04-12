using System.ComponentModel.DataAnnotations;
using System.Web;

namespace WorkflowManagementSystem.ViewModels
{
    public class ProjectPresentationViewModel
    {
        [Key]
        public int EventProjectId { get; set; }

        [Display(Name = "Presentation")]
        [DisplayFormat(NullDisplayText = "Presentation Not Available")]
        public string Presentation { get; set; }

        //The presentation file as object (used to upload a file)
        [Display(Name = "Presentation File")]
        public HttpPostedFileBase PresentationFile { get; set; }

    }
}