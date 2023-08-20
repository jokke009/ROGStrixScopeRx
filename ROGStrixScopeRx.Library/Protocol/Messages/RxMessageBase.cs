using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Protocol.Messages
{
    public abstract class RxMessageBase
    {
        private byte[] _outBytes;
        private const int _frame_size = 65;

        public static int FRAME_SIZE => _frame_size; // used to get the frame size for calculations

        public byte[] OutBytes { get => _outBytes; set => _outBytes = value; }

        public RxMessageBase()
        {
            _outBytes = new byte[_frame_size]; //every message has a fixed Frame size
            _outBytes[0] = 0x00; // the first byte is and always must be 0x00;
        }
    }
}
