using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using ROGStrixScopeRx.Library.Model;
using ROGStrixScopeRx.Library.Services;
using System;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ROGStrixScopeRx.Library.Services.USBService;

namespace ROGStrixScopeRx.Library
{
    public class ExternalDataPool
    {
        public ExternalDataPool()
        {
            using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("testmap", 10000))
            {
            }
        }
    }
}
