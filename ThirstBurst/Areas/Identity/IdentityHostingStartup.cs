using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(ThirstBurst.Areas.Identity.IdentityHostingStartup))]
namespace ThirstBurst.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}
