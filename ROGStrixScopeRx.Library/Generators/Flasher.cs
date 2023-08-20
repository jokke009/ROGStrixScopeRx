using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Generators
{
    public class Flasher : IProducer
    {


        private int _onTime = 1000;
        private int _offTime = 1000;

        private readonly IDatapool _pool;
        //inject the pool service
        public Flasher(IDatapool pool)
        {
            _pool = pool;
        }



        public async Task ScanTask(CancellationToken token)
        {
            Random rnd = new Random();

            while (true)
            {
                   // RxMessageSetLed msg = new RxMessageSetLed(((byte)ScopeRx.KEY_EN_ESCAPE), Color.White);

                    InstructionSetLed instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_ESCAPE, Color.White);
                    QueueInstruction(instr);
                    await Task.Delay(_onTime);

                    instr = new InstructionSetLed((byte)ScopeRx.KEY_EN_ESCAPE, Color.Black);
                    QueueInstruction(instr);
                    await Task.Delay(_offTime);


                }
            }
        
        public void QueueInstruction(InstructionBase instr)
        {
            _pool.
        }
    }
}
