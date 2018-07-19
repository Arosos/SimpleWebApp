using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleWebApp.Areas.Identity.Data;
using SimpleWebApp.Models;

[assembly: HostingStartup(typeof(SimpleWebApp.Areas.Identity.IdentityHostingStartup))]
namespace SimpleWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SimpleWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SimpleWebAppContextConnection")));

                services.AddDefaultIdentity<SimpleWebAppUser>()
                    .AddEntityFrameworkStores<SimpleWebAppContext>();
            });
        }
    }
}