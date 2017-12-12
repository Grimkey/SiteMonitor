using System;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace SiteMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            var configuration = builder.Build();

            var sitecheck = new SiteCheck();
            sitecheck.Validate(new string[]{"www.google.com", "foo.bar"});
        }
    }
}
