using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentJobs.Models
{
    public class JobPosting
    {
        public JobPosting()
        {
            Applications = new HashSet<Application>();
        }
        [Key]
        public int JobPostingId { get; set; }
        public int? UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? Users { get; set; }
        public int? SectorId { get; set; }
        [ForeignKey("SectorId")]
        public virtual Sector? Sectors { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [StringLength(30, ErrorMessage = "Title cannot exceed 30 characters.")]
        [MinLength(6, ErrorMessage = "Title cannot be less than 6 frames.")]
        public string JobPostingTitle { get; set; }
        [Required(ErrorMessage = "Job Posting Description Required")]
        [StringLength(600, ErrorMessage = "Job Posting Description cannot exceed 600 characters.")]
        [MinLength(10, ErrorMessage = "Job Posting Description cannot be less than 10 frames.")]
        public string JobPostingDescription { get; set; }
        public string? JobPostingImageUrl { get; set; }
        public DateTime JobPostingDate { get; set; }
        public DateTime JobPostingCreatedDate { get; set; }
        public bool JobPostingStatus { get; set; }
        public virtual ICollection<Application> Applications { get; set; }

    }
}
