using Microsoft.AspNetCore.Hosting;

namespace CodingExam.Configurations
{
    public static class CodingExamEnvironment
    {
        private static IHostingEnvironment HostEnvironment;

        public static void Configure(IHostingEnvironment env)
        {
            HostEnvironment = env;
        }

        public static string Environment => HostEnvironment.EnvironmentName;

        public static bool IsDevelopment => HostEnvironment.IsDevelopment();        
    }
}
