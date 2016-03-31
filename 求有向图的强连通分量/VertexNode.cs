using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 求有向图的强连通分量
{
    public class VertexNode
    {
        public EdgeNode firstin;
        public EdgeNode firstout;
        public int id;
        public int posx;
        public int posy;
        public Boolean visited = false;
    }
}
