using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ROGStrixScopeRx.Library.Services.USBService;

namespace ROGStrixScopeRx.Library
{
    public  class InternalDataPool 
    {
        private readonly ILogger<InternalDataPool> _logger;
        static string test;

        private readonly IConfiguration _configuration;

        
        

        public AppSettings _appSettings { get; private set; }

        static byte globalIterator;
        public static byte GlobalIterator { get => globalIterator; set => globalIterator = value; }
        
        static int _tickRate;
        public static int TickRate { get => _tickRate; set => _tickRate = value; }

        public InternalDataPool()
        {
            Init();

        }

        private void Init()
        {
            globalIterator = 0;
        }

        private void LoadAppSettings()
        {
            TickRate = _appSettings.TickRate;
        }

    }
}
