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
            _dataPool.Volume = device.AudioEndpointVolume.MasterVolumeLevelScalar;
           _logger.LogInformation($"VolSetting: {_dataPool.Volume}%");


        }
        public void SampleLevel()
        {

           // device.AudioEndpointVolume.MasterVolumeLevel
            var level = device.AudioMeterInformation.MasterPeakValue;
            //_logger.LogInformation($"level: {level}%");
            _dataPool.Level = level;
            
        }

        public void QueueInstruction(InstructionBase instr)
        {
            if (_dataPool.Bc != null)
            {
                _dataPool.Bc.Add(instr);
            }
        }

    }
}
