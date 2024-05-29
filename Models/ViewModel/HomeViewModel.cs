namespace StudentJobs.Models.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Sector> Sectors { get; set; }
        public IEnumerable<JobPosting> JobPostings { get; set; }
        public IEnumerable<Scoring> Scorings { get; set; }
        public IEnumerable<JobPosting> Carousel { get; set; }
    }
}
