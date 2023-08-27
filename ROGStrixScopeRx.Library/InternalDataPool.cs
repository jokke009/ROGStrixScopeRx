using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static ROGStrixScopeRx.Library.Services.USBService;

namespace ROGStrixScopeRx.Library
{
    public  class InternalDataPool : IDatapool
    {
        private readonly ILogger<InternalDataPool> _logger;
        static string test;
        static float _volume;
        static float _level;

        private readonly IConfiguration _configuration;

        private BlockingCollection<InstructionBase> _bc;
        public BlockingCollection<InstructionBase> Bc { get => _bc; set => _bc = value; }

        public AppSettings _appSettings { get; private set; }

        static byte globalIterator;
        public static byte GlobalIterator { get => globalIterator; set => globalIterator = value; }
        
        static int _tickRate;
        

        public static int TickRate { get => _tickRate; set => _tickRate = value; }
        public float Volume { get => _volume; set => _volume = value; }
        public float Level { get => _level; set => _level = value; }

        public InternalDataPool(ILogger<InternalDataPool> logger)
        {
            _logger = logger;
            Init();

        }

        private void Init()
        {
            Bc = new BlockingCollection<InstructionBase>();
            globalIterator = 0;
        }

        private void LoadAppSettings()
        {
            TickRate = _appSettings.TickRate;
        }

    }
}
