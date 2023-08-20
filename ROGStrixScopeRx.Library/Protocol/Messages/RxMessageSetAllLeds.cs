using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Protocol.Messages
{
    public abstract class MessageSetManyLeds: RxMessageBase
    {
        private byte[] _outBytes;
        protected const int FRAME_SIZE = 65;

        private byte _remaining;

        private MessageSetManyLeds() : base()
        {
            _outBytes[1] = 0xC0;
            _outBytes[2] = 0x81;
            _outBytes[3] = 0x01;
            
        }

        public MessageSetManyLeds(byte key, byte red, byte green, byte blue) : this()
        {
            _outBytes[4] = _remaining;
            _outBytes[5] = key;
            _outBytes[6] = red;
            _outBytes[7] = green;
            _outBytes[8] = blue;
        }


        public MessageSetManyLeds(byte key, Color color)
        {
            
        }
    }
}
