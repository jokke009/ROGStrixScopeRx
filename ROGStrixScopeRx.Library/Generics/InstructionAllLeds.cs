using ROGStrixScopeRx.Library.Defintions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public InstructionSetAllLeds(Color color)
        {
            _ledlist = new List<Tuple<byte,Color>>();
            foreach (byte i in Enum.GetValues(typeof(ScopeRx)))
            {
                _ledlist.Add(Tuple.Create(i, color));
            }
        }
        public List<Tuple<byte, Color>> Ledlist { get => _ledlist; }
    }
}
