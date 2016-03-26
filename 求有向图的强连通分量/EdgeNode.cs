using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 求有向图的强连通分量
{
    public class EdgeNode
    {
        public int tailvex;
        public int headvex;
        public EdgeNode headlink;
        public EdgeNode taillink;
    }
}
