using HidApi;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Services
{
    public  class USBService : BackgroundService
{
        private readonly ILogger<USBService> _logger;

        State _state = new State();

        Device _device;

        private string _path = "";

        public record State
        {
            public bool IsConnected { get; set; }
        }

        public USBService(ILogger<USBService> logger)
        {
            _logger = logger;
            _state.IsConnected = false;
            _logger.LogDebug($"Constructor called, IsConnected: {_state.IsConnected}");

            _path = @"\\?\\HID#VID_0B05&PID_1951&MI_01#8&ca448dc&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}";
            _device = new Device(_path);

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _state.IsConnected = true;



            _logger.LogInformation("Hello World!" + Hid.Enumerate(0x0B05, 0x1951).ToString());
            await Task.Delay(1000, stoppingToken);

            byte i = 0;
            
            while (!stoppingToken.IsCancellationRequested)
            {
                /*
                if(i < 255)
                {
                    SetLed(i);``fgbv
                    _logger.LogInformation("Setting key: " + i);
                    await Task.Delay(200, stoppingToken);
                    i++;
                }
                else
                {
                    i = 0;
                }
                //using var device = deviceInfo.ConnectToDevice();
                //_logger.LogInformation("Hello World!" + device.GetProduct() + "v" + device.GetDeviceInfo().VendorId + "p" + device.GetDeviceInfo().ProductId );

                */
                SetLed(1, Color.FromArgb(255,InternalDataPool.GlobalIterator, InternalDataPool.GlobalIterator, InternalDataPool.GlobalIterator));
                await Task.Delay(200, stoppingToken);
            }
        }

        private void GetVersion()
        {
            if (_state.IsConnected) 
            {
                
                byte[] buf = new byte[64];
                buf[0] = 0x01;
                buf[1] = 0xC0;
                buf[2] = 0x78;
                buf[3] = 0x00;
                buf[4] = 0x00;
                
                buf[5] = 0x15;
                buf[6] = 0x00;
                buf[7] = 0x00;
                buf[8] = 0xFF;

                //
                ReadOnlySpan<byte> test = new ReadOnlySpan<byte>(buf, 0, 64);

                //               var tes=  _device.GetReportDescriptor(65);
                //_device.SendFeatureReport(test);
                //_device.Write(test);
                // var info =  Hid.Enumerate(0x0B05, 0x1951);

                _device.Write(test);
                var test2 = _device.Read(64);

                //0       var temp =  _device.Read(65);
                _logger.LogInformation("info is: " + test2.ToString()) ;
            }
        }

        private void SetLed(byte key, Color color)
        {
            if (_state.IsConnected)
            {

                byte[] buf = new byte[65];
                buf[0] = 0x00;
                buf[1] = 0xC0;
                buf[2] = 0x81;
                buf[3] = 0x01;
                buf[4] = 0x00;

                buf[5] = key;
                buf[6] = color.R;
                buf[7] = color.G;
                buf[8] = color.B;

                //
                ReadOnlySpan<byte> test = new ReadOnlySpan<byte>(buf, 0, 65);

                //               var tes=  _device.GetReportDescriptor(65);
                //_device.SendFeatureReport(test);
                //_device.Write(test);
                // var info =  Hid.Enumerate(0x0B05, 0x1951);

                _device.Write(test);
                var test2 = _device.Read(65);

            }
        }

        private void ClearResponses()
        {
            int result = 1;
            byte[] buf = new byte[65];
            while (result > 0)
            {
               // result = _device.ReadTimeout(65, 0);
            }


        }


    }
}
