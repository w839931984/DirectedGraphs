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
    public partial class EdgeRelationForm : Form
    {
        private static int stepx = 80;
        private static int stepy = 40;

        private static VertexNode[] vertexs;

        private static Boolean userClose = true;

        private static EdgeRelationForm EdgeRelationForm_this;

        public static CheckBox[,] checkBoxes;

        public EdgeRelationForm(VertexNode[] vs)
        {
            InitializeComponent();
            EdgeRelationForm_this = this;
            vertexs = vs;
            createColLable(this, vertexs.Length);
            createRowLable(this, vertexs.Length);
            createCheckBox(this, vertexs.Length);
            createButton(this);
        }

        private static void createColLable(EdgeRelationForm e, int num)
        {
            int posx = 20;
            int posy = 40;
            for (int i = 0; i < num; i++)
            {
                Label l = new Label();//声明一个label
                l.Location = new Point(posx, posy);//设置位置
                l.AutoSize = true;//设置大小
                l.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                l.Text = "V" + i;//设置Text值
                e.Controls.Add(l);//在当前窗体上添加这个label控件
                posy += stepy;
            }
        }

        private static void createRowLable(EdgeRelationForm e, int num)
        {
            int posx = 70;
            int posy = 10;
            for (int i = 0; i < num; i++)
            {
                Label l = new Label();//声明一个label
                l.Location = new Point(posx, posy);//设置位置
                l.AutoSize = true;//设置大小
                l.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
                l.Text = "->V" + i;//设置Text值
                e.Controls.Add(l);//在当前窗体上添加这个label控件
                posx += stepx;
            }
        }

        private static void createCheckBox(EdgeRelationForm e, int num)
        {
            int posx = 80;
            int posy = 40;
            checkBoxes = new CheckBox[num,num];
            for (int i = 0; i < num; i++)
            {
                for (int j = 0; j < num; j++)
                {
                    checkBoxes[i, j] = new CheckBox();
                    checkBoxes[i, j].AutoSize = true;
                    checkBoxes[i, j].Location = new Point(posx, posy);
                    checkBoxes[i, j].UseVisualStyleBackColor = true;
                    if(i == j)
                    {
                        checkBoxes[i, j].Enabled = false;
                    }
                    e.Controls.Add(checkBoxes[i, j]);//在当前窗体上添加控件
                    posx += stepx;
                }
                posx = 80;
                posy += stepy;
            }
        }

        private static void createButton(EdgeRelationForm e)
        {
            Button btn = new Button();
            btn.AutoSize = true;
            btn.Text = "确定";
            btn.Location = new Point((e.Size.Width - btn.Width) / 2, e.Size.Height - btn.Height);
            btn.MouseClick += new MouseEventHandler(btn_click);
            e.Controls.Add(btn);
        }

        public static void btn_click(object sender, EventArgs e)
        {
            for (int i = 0; i < vertexs.Length; i++)
            {
                for (int j = 0; j < vertexs.Length; j++)
                {
                    if(checkBoxes[i, j].Checked)
                    {
                        EdgeNode edgeNode = new EdgeNode();

                        edgeNode.tailvex = i;
                        edgeNode.headvex = j;

                        //头插法
                        edgeNode.taillink = vertexs[i].firstout;
                        vertexs[i].firstout = edgeNode;

                        edgeNode.headlink = vertexs[j].firstin;
                        vertexs[j].firstin = edgeNode;
                    }
                }
            }
            mainForm.vertexs = vertexs;
            userClose = false;
            EdgeRelationForm_this.Close();
        }

        private void EdgeRelationForm_Shown(object sender, EventArgs e)
        {
            this.Size = new Size(this.Size.Width + 20, this.Size.Height);
        }

        private void EdgeRelationForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (userClose)
            {
                if (DialogResult.Yes == MessageBox.Show("是否退出程序?", "确认退出", MessageBoxButtons.YesNo))
                {
                    mainForm.closeForm();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
