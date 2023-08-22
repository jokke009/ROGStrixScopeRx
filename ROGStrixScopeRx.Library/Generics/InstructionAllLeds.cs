using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Generics
{
    public class InstructionSetAllLeds :InstructionBase
    {
        private List<Tuple<byte, Color>> _ledlist;
        private Color _color;

        public InstructionSetAllLeds(List<Tuple<byte,Color>> ledlist) 
        {
            _ledlist = ledlist; 
        }

        public List<Tuple<byte, Color>> Ledlist { get => _ledlist; }
    }
}
