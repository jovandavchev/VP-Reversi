using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VP_Reversi
{
    public partial class Form1 : Form
    {
        public Color colorp1;
        public Color colorp2;
        public Player p1;
        public Player p2;
        public Rvs rvs;
        public Form1()
        {
            InitializeComponent();
            panel2.Enabled = false;
            this.DoubleBuffered = true;
            colorp1 = Color.Blue;
            colorp2 = Color.Red;
            p1 = new Player("Player1");
            p2 = new Player("Computer - Easy - 2");
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            UpdateStart();
        }

        public void UpdateStart()
        {
            ddl1.Text = ddl1.Items[0].ToString();
            ddl2.Text = ddl2.Items[1].ToString();
            name2.Text= ddl2.Items[1].ToString();
            name2.Text += " - 2";
            name2.Enabled = false;
            name1.Text = "Player 1";
        }

        private void ddl2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl2.SelectedIndex==0)
            {
                name2.Text = "Player 2";
                name2.Enabled = true;
            }
            else
            {
                name2.Text = ddl2.SelectedItem.ToString();
                name2.Text += " - 2";
                name2.Enabled = false;
            }
        }

        private void ddl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl1.SelectedIndex == 0)
            {
                name1.Text = "Player 1";
                name1.Enabled = true;
            }
            else
            {
                name1.Text = ddl1.SelectedItem.ToString();
                name1.Text += " - 1";
                name1.Enabled = false;
            }
        }

        private void color1_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog()==DialogResult.OK)
            {
                if (colorp2 != cd.Color)
                {
                    colorp1 = cd.Color;
                    panelColor1.BackColor = cd.Color;
                }
            }
        }

        private void color2_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();

            if (cd.ShowDialog() == DialogResult.OK)
            {
                if (colorp1 != cd.Color)
                {
                    colorp2 = cd.Color;
                    panelColor2.BackColor = cd.Color;
                }
            }
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            rvs = new Rvs();
            panel1.Visible = false;
            panel1.Enabled = false;
            panel2.Visible = true;
            panel2.Enabled = true;
            if (name1.Text.Trim().Length > 0)
            {
                p1.name = name1.Text;
                if (ddl1.SelectedIndex == 0) p1.type = Type.Human;
                if (ddl1.SelectedIndex == 1) p1.type = Type.Easy;
                if (ddl1.SelectedIndex == 2) p1.type = Type.Hard;
                lblPrv.Text = "";
                lblPrv.ForeColor = colorp1;
                lblPrv.Text += p1.name + "\n" + " Coins: " + p1.rvs.getFirst();
            }

            if (name2.Text.Trim().Length != 0)
            {
                p2.name = name2.Text;
                if (ddl2.SelectedIndex == 0) p2.type = Type.Human;
                if (ddl2.SelectedIndex == 1) p2.type = Type.Easy;
                if (ddl2.SelectedIndex == 2) p2.type = Type.Hard;
                lblVtor.Text = "";
                lblVtor.Text = p2.name + "\n" + " Coins: " + p2.rvs.getSecond();
                lblVtor.ForeColor = Color.Red;
            }
            
            panel2.Invalidate(true);
            //move();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            rvs.draw(e.Graphics, colorp1, colorp2);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
          /*  if (rvs.turn==1)
            {
                if (p1.type != Type.Human) return;
            }
            if (rvs.turn==2)
            {
                if (p2.type != Type.Human) return;
            } */
            Point point = new Point(e.X, e.Y);
            point = rvs.checkPoint(point);
            if (point.X != 0 && point.Y != 0)
            {
                if (rvs.isValid(point.X, point.Y))
                {
                    rvs.changeValue(point.X, point.Y);
                    panel2.Invalidate(true);
                    rvs.changeTurn();
                    p1.rvs = rvs;
                    p2.rvs = rvs;
                    lblPrv.Text = p1.name + "\n" + " Coins: " + p1.rvs.getFirst();
                    lblVtor.Text = p2.name + "\n" + " Coins: " + p2.rvs.getSecond();
                  //  move();
                }
            }
 
        }

   /*     public void move()
        {
            bool bl = false;
            if (rvs.turn==1)
            {
                if (p1.type != Type.Human) bl = true;
                p1.move();
                rvs = p1.rvs;
            }
            else
            {
                if (p2.type != Type.Human) bl = true;
                p2.move();
                rvs = p2.rvs;
            }
            panel2.Invalidate(true);
            p1.rvs = rvs;
            p2.rvs = rvs;
            lblPrv.Text = p1.name + "\n" + " Coins: " + p1.rvs.getFirst();
            lblVtor.Text = p2.name + "\n" + " Coins: " + p2.rvs.getSecond();
            if (bl) move();
        } */
    }
}
