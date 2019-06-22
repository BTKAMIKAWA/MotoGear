using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace MotoGear.DataAccess.InMemory
{
    public class InMemoryRepo<Ty>
    {
        ObjectCache cache = MemoryCache.Default;
        List<Ty> items;
        string className;

    }
}
