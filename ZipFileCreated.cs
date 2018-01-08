using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributeLib
{
    public class ZipFileCreated : EventArgs
    {
        public long fileSize { get; set; }
        public string filePath { get; set; }
    }
}
