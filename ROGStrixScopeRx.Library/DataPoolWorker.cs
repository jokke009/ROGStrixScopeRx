using Microsoft.Extensions.Hosting;
using ROGStrixScopeRx.Library.Generics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library
{
    public class DataPoolWorker : BackgroundService
    {
        private readonly IDatapool _pool;

        static int _tickRate = 200;
        public static int TickRate { get => _tickRate; set => _tickRate = value; }
        public DataPoolWorker(IDatapool pool)
        {
            _pool = pool;
        }
        private void QueueInstruction(InstructionBase instr)
        {
            if (_pool.Bc != null)
            {
                _pool.Bc.Add(instr);
            }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                //foreach (var rep in _pool.Reporters.Values.Where(x => x.HasUpdate == true))
                //{
                //    var col = rep.Get8BitValue();
                //    InstructionSetLed instr = new InstructionSetLed(rep.KeyBinding, Color.FromArgb(col, 255 - col, 0));
                //    QueueInstruction(instr);
                //}
                 var leds = _pool.KeyColorDictionary.Select(kvp => new Tuple<byte,Color>((byte)kvp.Key, kvp.Value)).ToList();
                InstructionSetAllLeds instr = new InstructionSetAllLeds(leds);
                QueueInstruction(instr);
                await Task.Delay(TickRate);
            }


        }
    }
}
