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
    public class PerfmonService :  IWinService 
    {

        private PerformanceCounter _cpuCounter;
        private readonly IDatapool _dataPool;
        public PerformanceCounter CpuCounter { get => _cpuCounter; set => _cpuCounter = value; }
        private PerformanceCounter _memoryCounter;
        private PerformanceCounter _performanceCounter;
        private readonly ILogger<PerfmonService> _logger;

        private readonly IEnumerable<CpuReporter> _cpuReporters;

        public PerfmonService(ILogger<PerfmonService > logger, IDatapool data)
        {
            _cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
            _logger = logger;  
            _dataPool = data;
            _cpuReporters = _dataPool.Reporters.Values.OfType<CpuReporter>();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() => SomeInfinityProcess(cancellationToken));
            return Task.CompletedTask;
        }

        public void SomeInfinityProcess(CancellationToken cancellationToken)
        {
            for (; ; )
            {
                GetCpu();
                Thread.Sleep(1000);
                if (cancellationToken.IsCancellationRequested)
                    break;
            }
        }

        private void GetCpu()
        {
            float cpuUsage = CpuCounter.NextValue();
            _logger.LogInformation($"CPU Usage: {cpuUsage}%");

            if (_cpuReporters == null)
            {
                return;
            }
            else
            {
                foreach (var reporter in _cpuReporters)
                {
                    reporter.RawValue = cpuUsage;
                }
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
