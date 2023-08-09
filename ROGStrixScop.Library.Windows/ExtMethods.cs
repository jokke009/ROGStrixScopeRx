using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScop.Library.Windows
{
    public static class ExtMethods
        {
        public static bool IsWindows()
        {
            int p = (int)Environment.OSVersion.Platform;
            return p == 2 || p == 3 || p == 128;
        }
    }

}
