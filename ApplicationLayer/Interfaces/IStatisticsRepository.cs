using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Interfaces
{
    public interface IStatisticsRepository
    {
        Task<int> GetTotalCasesAsync();
        Task<int> GetOpenCasesAsync();
        Task<int> GetInProgressCasesAsync();
        Task<int> GetClosedCasesAsync();

        Task<Dictionary<string, int>> GetCasesPerUserAsync();
        Task<Dictionary<string, int>> GetCasesPerClientAsync();

        Task<List<string>> GetTopTagsAsync(int top = 5);

        Task<double> GetAverageClosingTimeAsync();


    }
}
