using ROGStrixScopeRx.Library.Generics;
using ROGStrixScopeRx.Library.Protocol.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library
{
    public interface ICommunicationService
    {
        public void Open();
        public void Close();
        public bool LoadSettings();

        
        public void Write(InstructionBase instruct); // generic write (better to use this vor uncoupling
        public void Write(RxMessageBase instruct); // direct raw right (for debugging purposesn

        public bool State
        {
            get;
            set;
        }

        public int BandWidth
        {
            get;
            set;
        }

    }
}
