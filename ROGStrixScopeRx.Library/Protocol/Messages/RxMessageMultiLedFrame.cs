using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library.Protocol.Messages
{
    public sealed class RxMessageMultiLedFrame : RxMessageBase
    {

        private int _packets;
        private static List<RxMessageMultiLedFrame> _frames = new List<RxMessageMultiLedFrame>();

        public int Packets { get => _packets; set => _packets = value; }
        public static List<RxMessageMultiLedFrame> Frames { get => _frames; set => _frames = value; }

        private RxMessageMultiLedFrame() : base()
        {
            _outBytes[1] = 0xC0;
            _outBytes[2] = 0x81;
            //byte 3 see public constructor
            _outBytes[4] = 0x00;
        }

        public RxMessageMultiLedFrame(List<Tuple<byte, Color>> ledlist) : this()
        {
            Frames = new List<RxMessageMultiLedFrame>();   
            Packets = (int)Math.Ceiling((float)ledlist.Count / 15);
            for (int i = 0; i < Packets; i++)
            {
                int remaining = ledlist.Count - i * 15;
                byte leds = (byte)((remaining > 0x0F) ? 0x0F : remaining);

                // Create a new instance of RxMessageMultiLedFrame for each packet
                RxMessageMultiLedFrame frame = new RxMessageMultiLedFrame();

                frame._outBytes[3] = leds;

                for (int j = 0; j < leds; j++)
                {
                    frame._outBytes[j * 4 + 5] = ledlist[i * 15 + j].Item1;
                    frame._outBytes[j * 4 + 6] = ledlist[i * 15 + j].Item2.R;
                    frame._outBytes[j * 4 + 7] = ledlist[i * 15 + j].Item2.G;
                    frame._outBytes[j * 4 + 8] = ledlist[i * 15 + j].Item2.B;
                }
                Frames.Add(frame);
            }
        }
    }
}
