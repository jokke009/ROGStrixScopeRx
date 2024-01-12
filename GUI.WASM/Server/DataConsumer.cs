using GUI.WASM.Shared;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.SignalR;
using ROGStrixScopeRx.Library;
using ROGStrixScopeRx.Library.Defintions;
using System;
using System.Drawing;

namespace GUI.WASM.Server
{
    public class DataConsumer : BackgroundService
    {
        private readonly ILogger<DataConsumer> _logger;
        private readonly IDatapool _datapool;
        private readonly IHubContext<DataHub> _hub;

        private readonly int _tickRate = 50;
        public int TickRate => _tickRate;
        private KeyColor toggle = new KeyColor();
        public DataConsumer(ILogger<DataConsumer> logger, IHubContext<DataHub> hubContext, IDatapool datapool)
        {
             _datapool = datapool;
            _logger = logger;
            _hub = hubContext;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                if (_hub != null)
                {
                   // _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                    // push block updates
                    //await _hub.Clients.All.SendAsync("DataMessage", 0x14, toggle);
                    // pick a random color from the system defined colors and assign it to toggle



                    // await SendSingle();
                    await SendMulti();

                    //await _hub.Clients.All.SendAsync("DataMessage", "tester","tester2");
                    await Task.Delay(TickRate, stoppingToken);
                }
            }

        }

        private async Task SendSingle()
        {
            Random random = new Random();
            foreach (var keym in KeyMap.KeyMappings)
            {
                // Generate random values for the color components (RGB)
                byte red = (byte)random.Next(256);
                byte green = (byte)random.Next(256);
                byte blue = (byte)random.Next(256);

                KeyColor test = new()
                {
                    Red = red,
                    Green = green,
                    Blue = blue,
                };

                await _hub.Clients.All.SendAsync("KeyMessage", (byte)keym.Key, test);
            }
        }

        //private async Task SendMulti()
        //{
        //    Random random = new Random();
        //    var keylist = new List<Tuple<byte, KeyColor>>();
        //    foreach (var keym in KeyMap.KeyMappings)
        //    {
        //        // Generate random values for the color components (RGB)
        //        byte red = (byte)random.Next(256);
        //        byte green = (byte)random.Next(256);
        //        byte blue = (byte)random.Next(256);

        //        KeyColor test = new()
        //        {
        //            Red = red,
        //            Green = green,
        //            Blue = blue,
        //        };

        //        keylist.Add(new Tuple<byte, KeyColor>((byte)keym.Key, test));

        //    }


        //    await _hub.Clients.All.SendAsync("MultiKeyMessage", keylist);
        //}

        private async Task SendMulti()
        {
            Random random = new Random();
            var keylist = new List<Tuple<byte, KeyColor>>();
            foreach (var keym in _datapool.KeyColorDictionary)
            {
                // Generate random values for the color components (RGB)
                byte red = keym.Value.R;
                byte green = keym.Value.G;
                byte blue = keym.Value.B;

                KeyColor test = new()
                {
                    Red = red,
                    Green = green,
                    Blue = blue,
                };

                keylist.Add(new Tuple<byte, KeyColor>((byte)keym.Key, test));

            }


            await _hub.Clients.All.SendAsync("MultiKeyMessage", keylist);
        }
    }

}
