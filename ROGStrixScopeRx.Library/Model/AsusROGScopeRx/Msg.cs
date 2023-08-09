using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HidApi;


namespace ROGStrixScopeRx.Library.Model.AsusROGScopeRx
{
    public abstract class Msg
    {
        MemoryStream stream = new MemoryStream();
        BinaryWriter binaryWriter;



        public Msg() 
        {
             binaryWriter = new BinaryWriter(stream);
        }

        public void Write(byte[] buf)
        {
            ReadOnlySpan<byte> test = new ReadOnlySpan<byte>(buf, 0, 65);
        }
    }
}
