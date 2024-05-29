using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace StudentJobs.Models
{
    public class Sector
    {
        public Sector()
        {
            JobPostings = new HashSet<JobPosting>();
        }
        [Key]
        public int SectorId { get; set; }
        [Required(ErrorMessage = "Sector Name Required")]
        [StringLength(30, ErrorMessage = "Sector cannot exceed 30 characters.")]
        [MinLength(6, ErrorMessage = "Sector cannot be less than 6 frames.")]
        public string SectorFullName { get; set; }
        [Required(ErrorMessage = "Sector Description Required")]
        [StringLength(600, ErrorMessage = "Sector Description cannot exceed 600 characters.")]
        [MinLength(10, ErrorMessage = "Sector Description cannot be less than 10 frames.")]
        public string SectorDescription { get; set; }
        public string? SectorImageUrl { get; set; }
        public bool SectorStatus { get; set; }
        public virtual ICollection<JobPosting> JobPostings { get; set; }

    }
}
