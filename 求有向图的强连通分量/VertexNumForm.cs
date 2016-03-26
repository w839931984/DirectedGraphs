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
    public partial class VertexNumForm : Form
    {
        private VertexNode[] vertexs;
        private Boolean userClose = true;

        public VertexNumForm()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, EventArgs e)
        {
            int num = (int) vertexNum.Value;
            vertexs = new VertexNode[num];
            for (int i = 0; i<num; i++ )
            {
                vertexs[i] = new VertexNode();
                vertexs[i].firstin = null;
                vertexs[i].firstout = null;
            }
            this.Visible = false;
            userClose = false;
            this.Close();
            new EdgeRelationForm(vertexs).ShowDialog();
        }

        private void VertexNumForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userClose)
            {
                mainForm.closeForm();
            }
        }

    }
}
