using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
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

        private ConcurrentDictionary<int, BaseReoprter> _reporters;
        public ConcurrentDictionary<int, BaseReoprter> Reporters { get => _reporters; set => _reporters = value; }

        public ConcurrentDictionary<ScopeRx, Color> KeyColorDictionary { get => _keyColorDictionary; set => _keyColorDictionary = value; }
        public AppSettings _appSettings { get; private set; }

        static byte globalIterator;
        private ConcurrentDictionary<ScopeRx, Color> _keyColorDictionary;

        public static byte GlobalIterator { get => globalIterator; set => globalIterator = value; }
        
        

        
        public float Volume { get => _volume; set => _volume = value; }
        public float Level { get => _level; set => _level = value; }


        public InternalDataPool(ILogger<InternalDataPool> logger)
        {
            _logger = logger;

            KeyColorDictionary = new ConcurrentDictionary<ScopeRx, Color>();
            Init();
            PopulateKeyMap();

        }

        private void PopulateKeyMap()
        {
            foreach(var key in KeyMap.KeyMappings.Keys)
            {
                KeyColorDictionary.TryAdd(key, Color.Beige);
            }
        }

        private void Init()
        {
            Bc = new BlockingCollection<InstructionBase>(100);
            Reporters = new ConcurrentDictionary<int, BaseReoprter>();
            Reporters.TryAdd(1, new VolumeReporter((byte)ScopeRx.KEY_EN_F12));
            Reporters.TryAdd(2, new VolumeReporter((byte)ScopeRx.KEY_EN_F11));
            Reporters.TryAdd(3, new LevelReporter((byte)ScopeRx.KEY_EN_F9));
            Reporters.TryAdd(4, new CpuReporter((byte)ScopeRx.KEY_EN_F1));
            globalIterator = 0;
        }

        private void LoadAppSettings()
        {

        }

        /*
        public void Convert()
        {
            byte vol = (byte)(_data.Volume * 255);
            byte level = (byte)(_data.Level * 255);
            // _logger.LogInformation($"Vol is: {vol}");
            //SetLed(ScopeRx.KEY_EN_F12, Color.FromArgb(255, vol, 255- vol, 0));

            if (level != _preLevel)
            {
                if (level > 1)
                {
                    SetLed(ScopeRx.KEY_EN_F9, Color.FromArgb(255, level, 255 - level, 0));
                }
                else
                {
                    SetLed(ScopeRx.KEY_EN_F9, Color.Black);
                }
                _preLevel = level;
            }
            if (vol != _preVol)
            {
                if (vol > 1)
                {
                    SetLed(ScopeRx.KEY_EN_F12, Color.FromArgb(255, vol, 255 - vol, 0));
                }
                else
                {
                    SetLed(ScopeRx.KEY_EN_F12, Color.Black);
                }
                _preVol = vol;
            }

        }
        */


    }
}
