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
using System.IO;


namespace VP_Reversi
{
    public partial class Form1 : Form
    {
        public Color colorp1;
        public Color colorp2;
        public Player p1;
        public Player p2;
        public Rvs rvs;
        public bool finished;
        public Form1()
        {
            InitializeComponent();
            panel2.Enabled = false;
            this.DoubleBuffered = true;
            colorp1 = Color.Blue;
            colorp2 = Color.Red;
            p1 = new Player("Player1");
            p2 = new Player("Computer - Easy - 2");
            finished = true;
            base.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            UpdateStyles();
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
            ddl1.Enabled = false;
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
            int turn = new Random().Next(1, 3);
            rvs = new Rvs(turn);
            panel1.Visible = false;
            panel1.Enabled = false;
            panel2.Visible = true;
            panel2.Enabled = true;
            if (name1.Text.Trim().Length > 0)
            {
                p1.name = name1.Text;
            }
                if (ddl1.SelectedIndex == 0) p1.type = Type.Human;
                if (ddl1.SelectedIndex == 1) p1.type = Type.Easy;
                if (ddl1.SelectedIndex == 2) p1.type = Type.Hard;
                lblPrv.Text = "";
                lblPrv.ForeColor = colorp1;
                lblPrv.Text += p1.name + "\n" + " Coins: " + rvs.getFirst();
            

            if (name2.Text.Trim().Length > 0)
            {
                p2.name = name2.Text;
            }
                if (ddl2.SelectedIndex == 0) p2.type = Type.Human;
                if (ddl2.SelectedIndex == 1) p2.type = Type.Easy;
                if (ddl2.SelectedIndex == 2) p2.type = Type.Hard;
                lblVtor.Text = "";
                lblVtor.Text = p2.name + "\n" + " Coins: " + rvs.getSecond();
                lblVtor.ForeColor = colorp2;
            p1.color = colorp1;
            p2.color = colorp2;
            finished = false;
            rvs.p1 = p1;
            rvs.p2 = p2;
            panel2.Invalidate(true);
            move();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            rvs.draw(e.Graphics, colorp1, colorp2);
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            if (finished == true) return;
            if (!(rvs.turn == 2 && p2.type != Type.Human))
            {
                Point point = new Point(e.X, e.Y);
                point = rvs.checkPoint(point);
                if (point.X != 0 && point.Y != 0)
                {
                    if (rvs.isValid(point.X, point.Y))
                    {
                        rvs.changeValue(point.X, point.Y);
                       // panel2.Invalidate(true);
                        rvs.changeTurn();
                        move();
                    }
                }
            }
 
        }

        public void move()
        {
            if (finished == true) return;

            lblPrv.Text = p1.name + "\n" + " Coins: " + rvs.getFirst();
            lblVtor.Text = p2.name + "\n" + " Coins: " + rvs.getSecond();
            panel2.Invalidate(true);
            if  (p1.canMove == false && p2.canMove == false)
            {
                p1.canMove = true;
                p2.canMove = true;
                if (rvs.getFirst()>rvs.getSecond())
                {
                    MessageBox.Show("The game is finished. " + p1.name + " is the winner.");
                    finished = true;
                    panel2.Enabled = false;
                    return;
                }
                else if (rvs.getFirst() < rvs.getSecond())
                {
                    MessageBox.Show("The game is finished. " + p2.name + " is the winner.");
                    finished = true;
                    panel2.Enabled = false;
                    return;
                }
                else
                {
                    MessageBox.Show("The game is finished. It's draw.");
                    finished = true;
                    panel2.Enabled = false;
                    return;
                }
            }
            rvs.findPossibleMoves();
            if (rvs.turn==1)
            {
                if (rvs.noPossibleMoves())
                {
                    rvs.p1.canMove = false;
                }
                else
                {
                    rvs.p1.canMove = true;
                }

                if (rvs.p1.canMove==false)
                {
                    rvs.changeTurn();
                    move();
                }
                else
                {
                    panel2.Enabled = true;
                }
            }

            if (rvs.turn==2)
            {
                if (rvs.noPossibleMoves())
                {
                    rvs.p2.canMove = false;
                }
                else
                {
                    rvs.p2.canMove = true;
                }

                if (rvs.p2.canMove == false)
                {
                    rvs.changeTurn();
                    move();
                }
                else
                {
                    if (rvs.p2.type == Type.Human)
                    {
                        panel2.Enabled = true;
                    }
                    else if (rvs.p2.type==Type.Easy)
                    {
                        panel2.Enabled = false;
                        Point p = rvs.generateRandom();
                        rvs.changeValue(p.X, p.Y);
                        rvs.changeTurn();
                        move();
                    }
                }
            }
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newGame();
        }

        public void newGame()
        {

            // colorp1 = Color.Blue;
            //   colorp2 = Color.Red;
            //    p1 = new Player("Player1");
            //  p2 = new Player("Computer - Easy - 2");
            finished = false;
            int turn = new Random().Next(1, 3);
            rvs = new Rvs(turn);
            panel1.Visible = false;
            panel1.Enabled = false;
            panel2.Visible = true;
            panel2.Enabled = true;
          //  p1.type = Type.Human;
        //    p2.type = Type.Easy;
            lblPrv.Text = "";
            lblPrv.ForeColor = colorp1;
            lblPrv.Text += p1.name + "\n" + " Coins: " + rvs.getFirst();
            lblVtor.Text = "";
            lblVtor.Text = p2.name + "\n" + " Coins: " + rvs.getSecond();
            lblVtor.ForeColor = colorp2;
            p1.color = colorp1;
            p2.color = colorp2;
            rvs.p1 = p1;
            rvs.p2 = p2;
            panel2.Invalidate(true);
            move();
        }
    }
}
