using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROGStrixScop.Library.Windows.Producers
{
    public class BaseService
    {
        public BaseService() 
        { 
        
        }

        public bool IsSupported()
        {
            return ExtMethods.IsWindows();
        }
    }
}
