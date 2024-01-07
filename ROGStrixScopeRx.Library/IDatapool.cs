using Microsoft.Extensions.Hosting;
using ROGStrixScopeRx.Library.Defintions;
using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library
{
    public interface IDatapool
    {
        public float Volume { get; set; }
        public float Level { get; set; }
        public BlockingCollection<InstructionBase> Bc { get; set; }

        public ConcurrentDictionary<int, BaseReoprter> Reporters { get; set; }

        public ConcurrentDictionary<ScopeRx, Color> KeyColorDictionary { get; set; }
    }
}
