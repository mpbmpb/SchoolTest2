using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SchoolTest2.Models;

namespace SchoolTest2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}


//TODO refactor courseDesignController

//TODO add days and subjects to courseDesign views

//TODO make course class holds courseDesign and list of all dates, times, places for days

//TODO make calendar class that tracks all events

//TODO make student class

//TODO make teacher class

//TODO make enrollment class (m2m)

//TODO add login / roles