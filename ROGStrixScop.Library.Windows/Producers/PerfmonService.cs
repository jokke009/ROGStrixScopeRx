﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ROGStrixScop.Library.Windows.Producers
{
    public class PerfmonService : BaseService, IWinService 
    {

        private PerformanceCounter _cpuCounter;
        public PerformanceCounter CpuCounter { get => _cpuCounter; set => _cpuCounter = value; }
        private PerformanceCounter _memoryCounter;
        private PerformanceCounter _performanceCounter;

        public PerfmonService(ILogger<PerfmonService > logger)
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
        }



        public Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                float cpuUsage = CpuCounter.NextValue();
                Task.Delay(1000);
                Console.WriteLine($"CPU Usage: {cpuUsage}%");
            }
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
