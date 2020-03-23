using System.Threading.Tasks;
using CodingExam.Common;
using CodingExam.Models;

namespace CodingExam.HttpClients
{
    public interface IApiClient
    {
        Task<ApiResponse> GetAsync(string host,ApiEndPointType endpoint);
    }
}
