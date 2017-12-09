using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoTools
{
    public interface IManager
    {
        ICounter CreateCounter();
        IPlatformID CreatePlatformID();

        ICounter MainCounter(); // for the functor object
    }
}