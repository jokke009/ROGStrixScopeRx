using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using NAudio;
using NAudio.CoreAudioApi;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Model;

namespace ROGStrixScop.Library.Windows.Producers
{

    /// <summary>
    /// Producer class
    /// </summary>
    public class VolumeService :  IWinService 
    {

        const int MAXPNAMELEN = 32;
        public    float volume = 0;
        private readonly IDatapool _dataPool;

        private MMDeviceEnumerator enumerator;
        private MMDevice device;

        // CoreAudioDevice? _defaultPlaybackDevice = null;


        private readonly ILogger<VolumeService> _logger;

        public VolumeService(ILogger<VolumeService> logger, IDatapool data)
        {
            _logger = logger;
            _dataPool = data;
            //    _defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;

            enumerator = new MMDeviceEnumerator();
            device = enumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Console);

        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                SampleLevel();
                GetVolumeSetting();
                await Task.Delay(200);
                
            }

            //return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        public void GetVolumeSetting()
        {

            //       _defaultPlaybackDevice ??= new CoreAudioController().DefaultPlaybackDevice;
            //     double vol = _defaultPlaybackDevice.Volume;
            //      var test = _defaultPlaybackDevice.PeakValueChanged;
            var volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
            var volReporters = _dataPool.Reporters.Values.OfType<VolumeReporter>();
            foreach ( var volReporter in volReporters )
            {
                volReporter.RawValue = volume;
            }
           //_logger.LogInformation($"VolSetting: {volume}%");


        }
        public void SampleLevel()
        {

           // device.AudioEndpointVolume.MasterVolumeLevel
            var level = device.AudioMeterInformation.MasterPeakValue;
            //_logger.LogInformation($"level: {level}%");
            var levelReporters = _dataPool.Reporters.Values.OfType<LevelReporter>();
            foreach (var reporter in levelReporters)
            {
                reporter.RawValue = level;
            }

        }



    }
}
