using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace StudentJobs.Models
{
    public class Scoring
    {
        [Key]
        public int ScoringId { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User? Users { get; set; }
        [Required(ErrorMessage = "Title Required")]
        [StringLength(30, ErrorMessage = "Title cannot exceed 30 characters.")]
        [MinLength(6, ErrorMessage = "Title cannot be less than 6 frames.")]
        public string ScoringTitle { get; set; }
        [Required(ErrorMessage = "Job Posting Description Required")]
        [StringLength(600, ErrorMessage = "Job Posting Description cannot exceed 600 characters.")]
        [MinLength(10, ErrorMessage = "Job Posting Description cannot be less than 10 frames.")]
        public string ScoringDescription { get; set; }
        public int Score { get; set; }
        public DateTime ScoringCreatedDate { get; set; }
        public bool ScoringStatus { get; set; }
    }
}
