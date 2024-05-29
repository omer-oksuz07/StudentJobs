using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentJobs.Models
{
    public class Application
    {
        [Key]
        public int ApplicationId { get; set; }
        public int? JobPostingId { get; set; }
        [ForeignKey("JobPostingId")]
        public virtual JobPosting? JobPostings { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? Users { get; set; }
        [Required(ErrorMessage = "Cv Required")]
        [StringLength(600, ErrorMessage = "Cv cannot exceed 600 characters.")]
        [MinLength(10, ErrorMessage = "Cv cannot be less than 10 frames.")]
        public string Cv { get; set; }
        public bool ApplicationStatus { get; set; }
    }
}
