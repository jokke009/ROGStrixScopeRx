using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Controllers
{
    public class FadeService : IHostedService
    {
        private readonly ILogger<FadeService> _logger;

        private Timer _timer = null;

        private AppSettings _settings;

        private static byte _step = 1;
        private byte _brightness;
        private bool _countUp = true;

        public static byte Step { get => _step; set => _step = value; }

        public FadeService(ILogger<FadeService> logger, IOptions<AppSettings> settings)
        {
            _logger = logger;
            _settings = settings.Value;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //_timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(20));
            _timer = new Timer(Tick, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(100));
            return Task.CompletedTask;
        }

        private void Tick(object? state)
        {
            if(_countUp)
            {
                if (_brightness < 255)
                {
                    _brightness = (byte)(_brightness +  Step);
                    
                }
                else
                {
                    _countUp = false;
                }
            }
            else
            {
                if (_brightness > 0)               {
                    _brightness--;
                }
                else
                {
                    _countUp = true;
                }
            }

            _logger.LogInformation("ActionToBePerformed: " + _brightness);

            InternalDataPool.GlobalIterator = _brightness;
        }

        private void Cycle(object? state)
        {
            if (_countUp)
            {
                if (_brightness < 255)
                {
                    _brightness = (byte)(_brightness + Step);

                }
                else
                {
                    _countUp = false;
                }
            }
            else
            {
                if (_brightness > 0)
                {
                    _brightness--;
                }
                else
                {
                    _countUp = true;
                }
            }

            _logger.LogInformation("ActionToBePerformed: " + _brightness);

            InternalDataPool.GlobalIterator = _brightness;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
    }
}
