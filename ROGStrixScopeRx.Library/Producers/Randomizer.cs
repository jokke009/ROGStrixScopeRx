using Microsoft.Extensions.Hosting;
using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Producers
{
    public class Randomizer : BackgroundService
    {
        private readonly IDatapool _datapool;

        Random random = new Random();
        public Randomizer(IDatapool datapool)
        {
            _datapool = datapool;


        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                RandomFrame();
                await Task.Delay(1000);
            }
        }

        private void RandomFrame()
        {
            foreach (var keym in _datapool.KeyColorDictionary)
            {
                // Generate random values for the color components (RGB)
                byte red = (byte)random.Next(256);
                byte green = (byte)random.Next(256);
                byte blue = (byte)random.Next(256);

                var col = Color.FromArgb(red, green, blue);

                _datapool.KeyColorDictionary[keym.Key] = col;
            }
        }
    }
}
