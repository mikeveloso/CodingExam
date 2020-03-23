using System.Net;

using Newtonsoft.Json.Linq;

namespace CodingExam.Models
{
    public class ApiResponse
    {
        public bool IsSuccessStatusCode => ((int)StatusCode >= 200 && (int)StatusCode < 300);
            
        public HttpStatusCode StatusCode { get; set; }

        public string ErrorMessage { get; set; }

        public JObject Result { get; set; }        
    }
}
