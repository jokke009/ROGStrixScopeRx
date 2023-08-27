using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Protocol.Messages;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Generators
{
    public class Flasher : BackgroundService
    {


        private int _onTime = 100;
        private int _offTime = 300;

        private readonly IDatapool _pool;
        private readonly ILogger _logger;
        //inject the pool service
        public Flasher(ILogger<USBService> logger, IDatapool pool)
        {
            _pool = pool;
            _logger = logger;
        }



        public async Task ScanTask(CancellationToken token)
        {


            while (true)
            {
                   // RxMessageSetLed msg = new RxMessageSetLed(((byte)ScopeRx.KEY_EN_ESCAPE), Color.White);

                    InstructionSetLed instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_F7, Color.White);
                    QueueInstruction(instr);
                    await Task.Delay(_onTime);

                    instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_F7, Color.Black);
                    QueueInstruction(instr);
                    await Task.Delay(_offTime);


                }
            }
        
        public void QueueInstruction(InstructionBase instr)
        {
            if (_pool.Bc != null)
            {
                _pool.Bc.Add(instr);
            }
        }


        public async Task StopAsync(CancellationToken cancellationToken)
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
           await Task.Run(async () =>
            {
                _logger.LogInformation("Background-Service started...");
                while (!stoppingToken.IsCancellationRequested)
                {


                    InstructionSetLed instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_F7, Color.White);
                    QueueInstruction(instr);
                     await Task.Delay(_onTime);
                    _logger.LogInformation("ON");

                    instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_F7, Color.Black);
                    QueueInstruction(instr);
                    await Task.Delay(_offTime);
                    _logger.LogInformation("OFF");

                }

            });
        }
    }
}
