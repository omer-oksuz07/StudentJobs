using System.Diagnostics;

namespace StudentJobs.Models.ViewModel
{
    public class DashboardViewModel
    {
        public int SectorCount { get; set; }
        public int JobPostingsCount { get; set; }
        public int ScoringsCount { get; set; }
        public IEnumerable<Application> Applications { get; set; }

    }
}
