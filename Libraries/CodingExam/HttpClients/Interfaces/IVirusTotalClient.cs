using System.Threading.Tasks;

using CodingExam.Models;

namespace CodingExam.HttpClients
{
    public interface IVirusTotalClient
    {
        Task<ApiResponse> GetAsync(string host);
    }
}
