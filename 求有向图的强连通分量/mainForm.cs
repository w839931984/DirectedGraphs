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
            VertexNode tailvex = vertexs[tailvexNum];
            VertexNode headvex = vertexs[headvexNum];
            int x1 = tailvex.posx;
            int y1 = tailvex.posy;
            int x2 = headvex.posx;
            int y2 = headvex.posy;

            double len = Math.Sqrt((x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2));
            int w = (int)(40 / len * Math.Abs(x1 - x2));
            int h = (int)(40 / len * Math.Abs(y1 - y2));

            if (x1 > x2)
            {
                x1 = x1 - w;
                x2 = x2 + w;
            }
            else if (x1 < x2)
            {
                x1 = x1 + w;
                x2 = x2 - w;
            }
            if (y1 > y2)
            {
                y1 = y1 - h;
                y2 = y2 + h;
            }
            else if (y1 < y2)
            {
                y1 = y1 + h;
                y2 = y2 - h;
            }
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

    }
}
