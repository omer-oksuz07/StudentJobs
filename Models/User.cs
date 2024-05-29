using System.ComponentModel.DataAnnotations;

namespace StudentJobs.Models
{
    public class User
    {
        public User()
        {
            JobPostings = new HashSet<JobPosting>();
            Applications = new HashSet<Application>();
            Scorings = new HashSet<Scoring>();
        }
        [Key]
        public int UserId { get; set; }
        [Required(ErrorMessage = "User Name Required")]
        [StringLength(30, ErrorMessage = "User Name cannot exceed 30 characters.")]
        [MinLength(6, ErrorMessage = "User Name cannot be less than 6 frames.")]
        public string UserFullName { get; set; }
        [StringLength(30, ErrorMessage = "Company Name cannot exceed 30 characters.")]
        [MinLength(6, ErrorMessage = "Company Name cannot be less than 6 frames.")]
        public string? UserCompanyName { get; set; }
        [Required(ErrorMessage = "User Mail Required")]
        [StringLength(30, ErrorMessage = "User Mail cannot exceed 30 characters.")]
        [MinLength(10, ErrorMessage = "User Mail cannot be less than 10 frames.")]
        public string UserEmail { get; set; }
        [Required(ErrorMessage = "User Password Required")]
        [StringLength(30, ErrorMessage = "User Password cannot exceed 30 characters.")]
        public string UserPassword { get; set; }
        public string? UserImageUrl { get; set; }
        [StringLength(600, ErrorMessage = "Company Address cannot exceed 600 characters.")]
        [MinLength(10, ErrorMessage = "Company Address cannot be less than 10 frames.")]
        public string? UserCompanyAddress { get; set; }
        [Required(ErrorMessage = "Telephone Required")]
        [StringLength(11, ErrorMessage = "Telephone must be 11 characters.")]
        public string UserTelephone { get; set; }
        [Required(ErrorMessage = "Role Required")]
        public string Role { get; set; }
        public DateTime? UserBirthDate { get; set; }
        public DateTime UserCreatedDate { get; set; }
        public DateTime? UserUpdateDate { get; set; }
        public bool UserStatus { get; set; }
        public virtual ICollection<JobPosting> JobPostings { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
        public virtual ICollection<Scoring> Scorings { get; set; }

    }
}
