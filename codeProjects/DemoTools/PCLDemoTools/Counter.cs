using System;
using System.Collections.Generic;
using System.Text;
using DemoTools;

namespace PCLDemoTools
{
    class Counter : DemoTools.Counter, DemoTools.ICounter
    {
        public int Value => Get();

        public void Add(ICounter other)
        {
            base.Add(other as Counter);
        }

        void ICounter.Add(int value)
        {
            base.Add(value);
        }
    }

    class MainCounter : DemoTools.ICounter
    {
        public void Reset()
        {
            DemoTools.DemoTools.MainCounter().Reset();
        }

        public void Add(int value)
        {
            DemoTools.DemoTools.MainCounter().Add(value);
        }

        public void Add(ICounter other)
        {
            DemoTools.DemoTools.MainCounter().Add(other.Value);
        }

        public int Value { get => DemoTools.DemoTools.MainCounter().Get(); }
    }
}
