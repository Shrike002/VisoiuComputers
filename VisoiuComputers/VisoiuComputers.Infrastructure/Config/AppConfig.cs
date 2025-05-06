﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisoiuComputers.Infrastructure.Config.Models;

namespace VisoiuComputers.Infrastructure.Config
{
    public class AppConfig
    {
        public static bool ConsoleLogQueries = true;
        public static ConnectionStringsSettings? ConnectionStrings { get; set; }

        public static void Init(IConfiguration configuration)
        {
            Configure(configuration);
        }

        private static void Configure(IConfiguration configuration)
        {
            ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
        }
    }
}
