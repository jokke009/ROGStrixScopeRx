using Microsoft.Extensions.Hosting;
using ROGStrixScopeRx.Library.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Generators
{
    public  interface IProducer 
    {
        void QueueInstruction(InstructionBase instr);

    }
}
