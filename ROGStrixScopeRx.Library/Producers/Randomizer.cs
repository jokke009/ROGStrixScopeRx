using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Producers
{
    internal class Randomizer : IProduceService
    {
        Random random = new Random();
        public Randomizer()
        {
           
            var keylist = new List<Tuple<byte, Color>>();
            foreach (var keym in KeyMap.KeyMappings)
            {
                // Generate random values for the color components (RGB)
                byte red = (byte)random.Next(256);
                byte green = (byte)random.Next(256);
                byte blue = (byte)random.Next(256);

                var col = Color.FromArgb(red, green, blue); 

                keylist.Add(new Tuple<byte, KeyColor>((byte)keym.Key, test));

            }
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
