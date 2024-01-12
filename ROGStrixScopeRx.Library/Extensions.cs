using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScopeRx.Library
{
    internal static class Extensions
    {
        
            public enum SizeUnits
        {
            Byte, KB, MB, GB, TB, PB, EB, ZB, YB
        }

        public static string ToSize(this int value, SizeUnits unit)
        {
            return (value / (double)Math.Pow(1024, (int)unit)).ToString("0.00");
        }
    }
}
