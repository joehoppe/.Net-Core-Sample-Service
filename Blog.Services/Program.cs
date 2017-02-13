using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Blog.Services
{
    /// <summary>
    /// Program entry point
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program entry point
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public static void Main()
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}