using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Protocol.Messages
{
    public sealed class RxMessageSetManyLeds : RxMessageBase
    {

        private byte _remaining;
        private int _packets;
        private List<RxMessageSetManyLeds> _frames;

        public int Packets { get => _packets; set => _packets = value; }
        public List<RxMessageSetManyLeds> Frames { get => _frames; set => _frames = value; }

        private RxMessageSetManyLeds() : base()
        {
            _outBytes[1] = 0xC0;
            _outBytes[2] = 0x81;
            //byte 3 see public constructor
            _outBytes[4] = 0x00;
        }

        public RxMessageSetManyLeds(byte key, byte red, byte green, byte blue) : this()
        {
            _outBytes[4] = _remaining;
            _outBytes[5] = key;
            _outBytes[6] = red;
            _outBytes[7] = green;
            _outBytes[8] = blue;
        }

        /// <summary>
        /// Legacy method that allows to set a single led, but all leds are sent whilst meaintaining state. 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="color"></param>
        public RxMessageSetManyLeds(byte key, Color color)
        {
         //   _key = key;
           // _color = color;
        }

        public RxMessageSetManyLeds(List<Tuple<byte, Color>> ledlist)
        {
            Packets = (int)Math.Ceiling((float)ledlist.Count / 15);
            for (int i = 0; i < Packets; i++)
            {
                int remaining = ledlist.Count - i * 15;
                byte leds = (byte)((remaining > 0x0F) ? 0x0F : remaining);
                _outBytes[3] = leds;

                for (int j = 0; j < leds; j++)
                {
                    _outBytes[j * 4 + 5] = ledlist[i * 15 + j].Item1;
                    _outBytes[j * 4 + 6] = ledlist[i * 15 + j].Item2.R;
                    _outBytes[j * 4 + 7] = ledlist[i * 15 + j].Item2.G;
                    _outBytes[j * 4 + 8] = ledlist[i * 15 + j].Item2.B;
                }

                this.Frames.Add(this);
            }
        }

        public void GetFrames()
        {

        }
    }
}
