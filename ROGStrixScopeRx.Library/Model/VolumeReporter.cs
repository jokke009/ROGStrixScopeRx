using ROGStrixScopeRx.Library.Defintions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Model
{
    public  class VolumeReporter : BaseReoprter
    {
        

        public VolumeReporter(byte keyBinding)
        {
            KeyBinding = keyBinding;
        }


    }
}
