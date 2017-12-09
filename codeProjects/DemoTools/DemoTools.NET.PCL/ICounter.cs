using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTools
{
    public interface ICounter
    {
        void Reset();
        void Add(int value);
        void Add(ICounter other);

        int Value { get; }
    }
}
