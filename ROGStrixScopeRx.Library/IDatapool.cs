using ROGStrixScopeRx.Library.Generics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library
{
    public interface IDatapool
    {
        public float Volume { get; set; }
        public float Level { get; set; }

        private static BlockingCollection<InstructionBase> _bc;
    }
}
