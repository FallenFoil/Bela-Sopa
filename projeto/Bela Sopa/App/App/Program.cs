using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace BelaSopa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebHost.CreateDefaultBuilder(args);
            builder.UseStartup< Startup >().Build().Run();
        }
    }
}
