using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library;
using ROGStrixScopeRx.Library.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ROGStrixScop.Library.Windows.Producers
{
    public class PerfmonService :  BackgroundService 
    {

        private List<PerformanceCounter> _cpuCounters;
        private readonly IDatapool _dataPool;
        public List<PerformanceCounter> CpuCounters { get => _cpuCounters; set => _cpuCounters = value; }
        private PerformanceCounter _memoryCounter;
        private PerformanceCounter _performanceCounter;
        private readonly ILogger<PerfmonService> _logger;

        private readonly IEnumerable<CpuReporter> _cpuReporters;

        private int _coreCount = 0;

        public PerfmonService(ILogger<PerfmonService > logger, IDatapool data)
        {

            


            //_cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _logger = logger;  
            _dataPool = data;
            _cpuReporters = _dataPool.Reporters.Values.OfType<CpuReporter>();

            _cpuCounters = new List<PerformanceCounter>();
            _coreCount = Environment.ProcessorCount;  // Get the number of CPU cores
            for (int i = 0; i < _coreCount; i++)
            {
                // Create a PerformanceCounter for each core
                using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", i.ToString()))
                {
                    CpuCounters.Add(cpuCounter);
                }
            }
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Task.Run(() => SomeInfinityProcess(cancellationToken));
            return Task.CompletedTask;
        }


        private async Task GetCpus()
        {

            if (_cpuReporters == null)
            {
                return;
            }
            else
            {
                int cpuIndex = 0;
                foreach (var reporter in _cpuReporters)
                {
                    reporter.RawValue = CpuCounters[cpuIndex].NextValue();
                    
                    cpuIndex++;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await GetCpus();
                await Task.Delay(100);

            }
        }
    }
}
