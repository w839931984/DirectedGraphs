using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace 求有向图的强连通分量
{
    public partial class mainForm : Form
    {
        public static mainForm m_this;
        public static VertexNode[] vertexs;
        public delegate void ChildClose();

        public static int[] DFN;
        public static int[] LOW;
        public static int index = -1;
        public static Stack<VertexNode> stack = new Stack<VertexNode>();
        public static List<String> list = new List<String>();

        public mainForm()
        {
            m_this = this;
            InitializeComponent();
            
        }

        public static void closeForm()
        {
            m_this.Close();
        }

        public static void setVisible(Boolean b)
        {
            m_this.Visible = b;
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            new VertexNumForm().ShowDialog();
        }

        private void mainForm_Paint(object sender, PaintEventArgs e)
        {
            //这里开始绘制有向图
            Graphics g = e.Graphics;
            Pen p = new Pen(Color.Black, 2);
            Font font = new Font("宋体", 26);
            Brush brush = System.Drawing.Brushes.Black;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.FillRectangle(Brushes.White, this.ClientRectangle);
            AdjustableArrowCap lineCap = new AdjustableArrowCap(5, 10, true);
            p.StartCap = LineCap.Round;
            p.CustomEndCap = lineCap;

            //画出点
            this.paintVertex(g, font, brush);

            //画出箭头
            this.paintArrow(g, p);
            
            g.Dispose();
            p.Dispose();
        }

        private void paintArrow(Graphics g, Pen p)
        {
            for (int i = 0; i < vertexs.Length; i++)
            {
                if (vertexs[i].firstout != null)
                {
                    EdgeNode edgeNode = vertexs[i].firstout;
                    this.paintArrow(g, p, edgeNode);
                    while (edgeNode.taillink != null)
                    {
                        edgeNode = edgeNode.taillink;
                        this.paintArrow(g, p, edgeNode);
                    }
                }
            }
        }

        private void paintArrow(Graphics g, Pen p, EdgeNode edgeNode)
        {
            int tailvexNum = edgeNode.tailvex;
            int headvexNum = edgeNode.headvex;

            //尾结点
            VertexNode tailvex = vertexs[tailvexNum];
            //头结点
            VertexNode headvex = vertexs[headvexNum];
            
            int x1 = tailvex.posx;
            int y1 = tailvex.posy;
            int x2 = headvex.posx;
            int y2 = headvex.posy;

            //计算箭头合适的长度
            double len = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            int w = (int)(40 * Math.Abs(x1 - x2) / len);
            int h = (int)(40 * Math.Abs(y1 - y2) / len);
            int directionX = 0;
            int directionY = 0;

            if (x1 > x2)
            {
                x1 = x1 - w;
                x2 = x2 + w;
                //设置箭头平移方向
                directionX = 1;
            }
            else if (x1 < x2)
            {
                x1 = x1 + w;
                x2 = x2 - w;
                directionX = -1;
            }
            if (y1 > y2)
            {
                y1 = y1 - h;
                y2 = y2 + h;
                directionY = -1;
            }
            else if (y1 < y2)
            {
                y1 = y1 + h;
                y2 = y2 - h;
                directionY = 1;
            }

            //计算偏移量
            int movex;
            int movey;
            if (x1 == x2)
            {
                movex = directionY * 7;
            }
            else
            {
                movex = directionX * (int)(7 * Math.Abs(y1 - y2) / len);
            }
            if (y1 == y2)
            {
                movey = directionX * 7;
            }
            else 
            {
                movey = directionY * (int)(7 * Math.Abs(x1 - x2) / len);
            }
            
            x1 = x1 + movex;
            y1 = y1 + movey;
            x2 = x2 + movex;
            y2 = y2 + movey;

            //画线
            g.DrawLine(p, x1, y1, x2, y2);
        }

        private void paintVertex(Graphics g, Font font, Brush brush)
        {
            for (int i = 0; i < vertexs.Length; i++)
            {
                double d = 2 * Math.PI / vertexs.Length * i;
                double x = 300 * (1 + Math.Sin(d));
                double y = 300 * (1 - Math.Cos(d));
                Point point = new Point((int)(x + 0.5), (int)(y + 0.5));
                vertexs[i].posx = (int)(x + 0.5) + 24;
                vertexs[i].posy = (int)(y + 0.5) + 18;
                g.DrawString("V"+ i, font, brush, point);
            }
            int half = vertexs.Length / 2;
            this.Height = (int)(300 * (1 - Math.Cos(2 * Math.PI / vertexs.Length * half))) + 100;
        }

        private void mainForm_Shown(object sender, EventArgs e)
        {
            
        }

        private void DFS(VertexNode tailv)
        {
            //设置次序，LOW初值
            DFN[tailv.id] = LOW[tailv.id] = ++index;
            //MessageBox.Show(tailv.id + "");
            //进栈
            stack.Push(tailv);
            //设置访问状态
            tailv.visited = true;
            VertexNode headv = new VertexNode();
            for (EdgeNode e = tailv.firstout; e != null; e = e.taillink)
            {
                headv = vertexs[e.headvex];
                if (!headv.visited)
                {
                    DFS(headv);
                    LOW[tailv.id] = Math.Min(LOW[tailv.id], LOW[headv.id]);
                }
                else if (stack.Contains(headv))
                {
                    LOW[tailv.id] = Math.Min(LOW[tailv.id], DFN[headv.id]);
                }
            }
            if (DFN[tailv.id] == LOW[tailv.id])
            {
                headv = stack.Peek();
                //headv.visited = false;
                stack.Pop();
                String str = headv.id + "";
                while (tailv.id != headv.id)
                {
                    headv = stack.Peek();
                    //headv.visited = false;
                    stack.Pop();
                    str = str + "," + headv.id;
                }
                str = str + "组成一个强连通分量";
                list.Add(str);
            }
        }

        private void mainForm_MouseClick(object sender, MouseEventArgs e)
        {
            //求有向强连通分量
            for (int i = 0; i < vertexs.Length; i++)
            {
                if (!vertexs[i].visited)
                {
                    m_this.DFS(vertexs[i]);
                }
            }
            Label lbl = new Label();
            Label l = new Label();//声明一个label
            l.Location = new Point(700, 300);//设置位置
            l.AutoSize = true;//设置大小
            l.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            String str = "";
            foreach(String s in list)
            {
                str = str + "\n" + s;
            }
            l.Text = str;//设置Text值
            m_this.Controls.Add(l);//在当前窗体上添加这个label控件
        }
    }
}
