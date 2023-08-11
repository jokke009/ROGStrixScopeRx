using AudioSwitcher.AudioApi.CoreAudio;
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

namespace ROGStrixScop.Library.Windows.Producers
{
    public class VolumeService :  IWinService 
    {

        const int MAXPNAMELEN = 32;
        public float leftVolume = 0;
        public    float rightVolume = 0;

        CoreAudioDevice? _defaultPlaybackDevice = null;


        private readonly ILogger<VolumeService> _logger;

        public VolumeService(ILogger<VolumeService> logger)
        {
            _logger = logger;

            _defaultPlaybackDevice = new CoreAudioController().DefaultPlaybackDevice;
        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {

                //GetVolume();
                InternalDataPool.Volume = GetVolumeSetting();
                await Task.Delay(200);
                
            }

            //return Task.CompletedTask;

        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }



        public double GetVolumeSetting()
        {

            _defaultPlaybackDevice ??= new CoreAudioController().DefaultPlaybackDevice;
            double vol = _defaultPlaybackDevice.Volume;
            //_defaultPlaybackDevice.
            // defaultPlaybackDevice.Volume = 80;

            _logger.LogInformation($"VolSetting: {vol}%");

            return vol;

        }
        public void GetVolume()
        {
            uint volume;


        }
    }
}
