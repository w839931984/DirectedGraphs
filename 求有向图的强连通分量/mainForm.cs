using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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

        private void mainForm_Shown(object sender, EventArgs e)
        {
            this.Visible = false;
            new VertexNumForm().ShowDialog();

            //这里开始绘制有向图
        }

        public static void closeForm()
        {
            m_this.Close();
        }

        public static void setVisible(Boolean b)
        {
            m_this.Visible = b;
        }

    }
}
