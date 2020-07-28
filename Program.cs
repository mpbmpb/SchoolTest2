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

//TODO REFACTOR!! edit-create dayviewmodels and seminarviewmodels -> 1 with overload constructor
//     make viewmodelBuilder & dbUpdater

//TODO make courseModel class & courseSeminar m2m class

//TODO make workshopModel class & workshopDay m2m class

//TODO make course class holds courseModel and list of all dates, times, places for days

//TODO make workshop class holds workshopModel and list of all dates, times, places for days

//TODO make calendar class that tracks all events

//TODO make student class

//TODO make teacher class

//TODO make enrollment class (m2m)

//TODO add login / roles