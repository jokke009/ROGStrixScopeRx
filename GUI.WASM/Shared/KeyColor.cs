using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ROGStrixScopeRx.Library.Defintions;

namespace GUI.WASM.Shared
{
    public class KeyColor
    {
        private ScopeRx _key;

        private Color _color;

        public KeyColor()
        {
            
        }

        public string GetHex()
        {
            return _color.IsEmpty ? string.Empty : ColorTranslator.ToHtml(_color);
        }

    }
}
