using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 求有向图的强连通分量
{
    class EdgeNode
    {
        private int tailvex;
        private int headvex;
        private EdgeNode headlink;
        private EdgeNode taillink;
    }
}
