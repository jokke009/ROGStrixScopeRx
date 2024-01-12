using HidApi;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace ROGStrixScopeRx.Library.Services
{

    /// <summary>
    /// Consumer class
    /// </summary>
    public  class USBService : BackgroundService , ICommunicationService
{
        private readonly ILogger<USBService> _logger;
        private readonly IDatapool _data;

        private static CancellationTokenSource _cts;


        private byte _preLevel = 0;
        private byte _preVol = 0;

        State _state = new State();

        Device _device;

        private string _path = "";
        private int _bandwidthIn = 0;
        private int _bandwidthOut = 0;
        private Timer _timer;
        private int _bytesWrittenIn = 0;
        private int _bytesWrittenOut = 0;

        bool ICommunicationService.State { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int BandWidthIn
        {
            get
            {
                return _bandwidthIn;
            }
            set { _bandwidthIn = value;}
        }

        public int BandWidthOut
        {
            get
            {
                return _bandwidthOut;
            }
            set { _bandwidthOut = value; }
        }
        public record State
        {
            public bool IsConnected { get; set; }
        }

        public USBService(ILogger<USBService> logger, IDatapool data)
        {
            _logger = logger;
            _data = data;
            _state.IsConnected = false;
            _logger.LogDebug($"Constructor called, IsConnected: {_state.IsConnected}");

            //foreach (var deviceInfo in Hid.Enumerate())
            //{
            //    using var device = deviceInfo.ConnectToDevice();
            //    Console.WriteLine($"GetManufacturer: {device.GetManufacturer()} name: {device.GetProduct()}");
            //    Console.WriteLine($"GetDeviceInfo: {device.GetDeviceInfo()}");
            //    var test = device.GetReportDescriptor();
            //    var test2 = device.GetDeviceInfo();
            //    Console.WriteLine($"GetDeviceInfo: {device.GetDeviceInfo()}");
            //}


            var devicepathIds = Hid.Enumerate(0x0B05, 0x1951);

            _device = devicepathIds.FirstOrDefault(x=> x.InterfaceNumber == 1).ConnectToDevice();

            //foreach (var deviceInfo in Hid.Enumerate(0x0B05, 0x1951))
            //{
            //    using var device = deviceInfo.ConnectToDevice();
            //    Console.WriteLine($"GetManufacturer: {device.GetManufacturer()} name: {device.GetProduct()}");
            //    Console.WriteLine($"GetDeviceInfo: {device.GetDeviceInfo()}");
            //    var test = device.GetReportDescriptor();
            //    var test2 = device.GetDeviceInfo();
            //    Console.WriteLine($"GetDeviceInfo: {device.GetDeviceInfo()}");
            //}

            //_path = @"\\?\\HID#VID_0B05&PID_1951&MI_01#8&ca448dc&0&0000#{4d1e55b2-f16f-11cf-88cb-001111000030}";
            //_device.Write(;
            _cts = new CancellationTokenSource();
            

        }
        protected async override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _state.IsConnected = true;
            _timer = new Timer(CalculateBandwidth, null, TimeSpan.Zero, TimeSpan.FromSeconds(1));


            _logger.LogInformation("starting usb task");
            await Task.Delay(1000, stoppingToken);

            byte i = 0;
            await Splash();
            await ClearAllLeds();
            Task t1 = Task.Run(() => NonBlockingConsumer(_cts.Token));

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
               
                await Task.Delay(100, stoppingToken);
            }
        }
        public void NonBlockingConsumer(CancellationToken ct)
        {
            // IsCompleted == (IsAddingCompleted && Count == 0)
            var _bc = _data.Bc;
            while (!_bc.IsCompleted)
            {
                InstructionBase nextItem = null;
                try
                {
                    if (!_bc.TryTake(out nextItem, 0, ct))
                    {
                       // _logger.LogInformation(" Take Blocked");
                    }
                    else
                    {
                        // _logger.LogInformation(" Take:{0}", nextItem);
                        Write(nextItem);
                    }
                }

                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Taking canceled.");
                    break;
                }
                finally
                {
                    if(_data.Bc.BoundedCapacity==_data.Bc.Count)
                    {
                        _logger.LogWarning("Buffer full!");
                    }
                }

                // Slow down consumer just a little to cause
                // collection to fill up faster, and lead to "AddBlocked"
                Thread.Sleep(10);
            }

            _logger.LogInformation("\r\nNo more items to take.");
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

                _device.Write(test); // do a lock
                var test2 = _device.Read(64);

                //0       var temp =  _device.Read(65);
                _logger.LogInformation("info is: " + test2.ToString()) ;
            }
        }

        private void CalculateBandwidth(object state)
        {
            BandWidthOut = _bytesWrittenOut;
            string h = BandWidthOut.ToSize(Extensions.SizeUnits.KB);
            _logger.LogInformation("BandWidthOut " + h);
            _bytesWrittenOut = 0;
        }
        private void SetLed(ScopeRx key, Color color)
        {
            if (_state.IsConnected)
            {

                byte[] buf = new byte[65];
                buf[0] = 0x00;
                buf[1] = 0xC0;
                buf[2] = 0x81;
                buf[3] = 0x01;
                buf[4] = 0x00;

                buf[5] = (byte)key;
                buf[6] = color.R;
                buf[7] = color.G;
                buf[8] = color.B;

                //
                ReadOnlySpan<byte> test = new ReadOnlySpan<byte>(buf, 0, 65);

                //               var tes=  _device.GetReportDescriptor(65);
                //_device.SendFeatureReport(test);
                //_device.Write(test);
                // var info =  Hid.Enumerate(0x0B05, 0x1951);

                this.Write(test.ToArray());
                var test2 = _device.Read(65);

            }
        }

        private async Task ClearAllLeds()
        {
            await Task.Run(async () =>
            {
                foreach(ScopeRx value in Enum.GetValues(typeof(ScopeRx)))
                {
                    SetLed(value, Color.FromArgb(255, 0, 0, 0));
                    await Task.Delay(10);
                }
            });
        }

        private async Task Splash()
        {
            await Task.Run(async () =>
            {
                foreach (ScopeRx value in Enum.GetValues(typeof(ScopeRx)))
                {
                    SetLed(value, Color.FromArgb(255, 100, 100, 100));
                    await Task.Delay(10);
                }
            });
        }

        private void ClearResponses()
        {
            int result = 1;
            byte[] buf = new byte[65];
            while (result > 0)
            {
                result = _device.ReadTimeout(65, 0).Length;
            }
        }
        private void AwaitResponses(int milliseconds)
        {
            byte[] buf = new byte[65];
            var result = _device.ReadTimeout(65, milliseconds).Length;

        }


        public void Open()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public bool LoadSettings()
        {
            throw new NotImplementedException();
        }

        public void Write(InstructionBase instruct)
        {
            if (instruct != null)
            {
                RxMessageBase rx;
                switch (instruct)
                {
                    case InstructionSetLed setled:
                        rx = new RxMessageSetLed((byte)setled.Key, setled.Color);
                        break;
                    case InstructionSetAllLeds setallled:
                        SetAll(setallled);
                        return;

                        default: throw new NotImplementedException();
                }
                Write(rx);
            }
        }

        public void Write(RxMessageBase instruct)
        {
            ClearResponses();
            this.Write(instruct.OutBytes);
            var test2 = _device.Read(65);
        }

        private void Write(byte[] bytes)
        {
            _bytesWrittenOut += bytes.Length;
            _device.Write(bytes);
        }

        private void SetAll(InstructionSetAllLeds setallled)
        {
            RxMessageMultiLedFrame rx = new RxMessageMultiLedFrame(setallled.Ledlist);
            
            for (int i = 0; i < RxMessageMultiLedFrame.Frames.Count; i++)
            {
                ClearResponses();
                this.Write(RxMessageMultiLedFrame.Frames[i].OutBytes);
                AwaitResponses(10);
            }
            

        }

    }
}
