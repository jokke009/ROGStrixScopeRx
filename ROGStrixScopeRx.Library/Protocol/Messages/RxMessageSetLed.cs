using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Protocol.Messages
{
    public sealed class RxMessageSetLed : RxMessageBase
    {
        private byte[] _outBytes;

        private RxMessageSetLed() : base()
        {
            _outBytes[1] = 0xC0;
            _outBytes[2] = 0x81;
            _outBytes[3] = 0x01;
            _outBytes[4] = 0x00;
        }

        public RxMessageSetLed(byte key, byte red, byte green, byte blue) : this()
        {
            _outBytes[5] = key;
            _outBytes[6] = red;
            _outBytes[7] = green;
            _outBytes[8] = blue;
        }


        public RxMessageSetLed(byte key, Color color)
        {
            
        }
    }
}
