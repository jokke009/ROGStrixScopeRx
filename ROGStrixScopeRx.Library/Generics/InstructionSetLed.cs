using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Generics
{
    public class InstructionSetLed :InstructionBase
    {
        private byte _key;
        private Color _color;

        public InstructionSetLed(byte key, Color color) 
        {
            Key = key; 
            Color = color;    
        }

        public byte Key { get => _key; set => _key = value; }
        public Color Color { get => _color; set => _color = value; }
    }
}
