using System.Threading.Tasks;

using Newtonsoft.Json.Linq;

using CodingExam.Models;

namespace CodingExam.HttpClients
{
    public interface IServiceClient
    {
        Task<ApiResponse> GetAsync(string host);
    }
}
