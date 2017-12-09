using System;
using System.Collections.Generic;
using System.Text;
using DemoTools;

namespace PCLDemoTools
{
    public class Manager : DemoTools.IManager
    {
        MainCounter mainCounter = new MainCounter();

        public ICounter CreateCounter()
        {
            return new Counter();
        }

        public IPlatformID CreatePlatformID()
        {
            return new PlatformID();
        }

        public ICounter MainCounter()
        {
            return mainCounter;
        }
    }
}
