using System;
using System.Collections.Generic;
using System.Text;

namespace PCLDemoTools
{
    class PlatformID : DemoTools.PlatformID, DemoTools.IPlatformID
    {
        public string Value => Get();
    }
}
