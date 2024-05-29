using Microsoft.EntityFrameworkCore;

namespace StudentJobs.Models
{
    public class StudentJobsContext :DbContext
    {
        public StudentJobsContext() { }
        public StudentJobsContext(DbContextOptions<StudentJobsContext> options) : base(options) { }
        public DbSet<Application> Applications { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Scoring> Scorings { get; set; }
        public DbSet<Sector> Sectors { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(
            "Server=DESKTOP-FLT1JUM\\SQLEXPRESS;Database=StudentJobsDb;" +
            "TrustServerCertificate=True;Trusted_Connection=True;Encrypt=False"
               );
            }

            base.OnConfiguring(optionsBuilder);
        }



    }
}
