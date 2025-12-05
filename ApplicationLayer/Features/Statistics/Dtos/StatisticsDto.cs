using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Statistics.Dtos
{
    public class StatisticsDto
    {
        public int TotalCases { get; set; }
        public int OpenCases { get; set; }
        public int InProgressCases { get; set; }
        public int ClosedCases { get; set; }

        public Dictionary<string, int> CasesPerUser { get; set; } = new Dictionary<string, int>();

        public Dictionary<string, int> CasesPerClient { get; set; } = new Dictionary<string, int>();
        public List<string>? TopTags { get; set; } = new List<string>();
        public double? AverageResolutionTimeInDays { get; set; }
        public DateTime GeneratedAt { get; set; } = DateTime.UtcNow;
        }
}
