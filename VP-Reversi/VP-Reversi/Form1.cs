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
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;
using System.Resources;

namespace VP_Reversi
{
    public partial class Form1 : Form
    {
        System.Windows.Forms.Timer timer;
        public Color colorp1;
        public Color colorp2;
        public Player p1;
        public Player p2;
        public Rvs rvs;
        public bool finished;
        public HighScore hscore;
        ToolTip t1;
        string FileName;
        SoundPlayer sp;
        public Form1()
        {
            InitializeComponent();
            sp = new SoundPlayer(Properties.Resources.clicksound);
            hscore = desHighScores();
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
            timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Tick += Timer_Tick;
            t1 = new ToolTip();
            FileName = null;
            this.Icon = Properties.Resources.reversiicon;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (finished == false)
            {
                if (p2.type == Type.Easy)
                {
                    Point p = rvs.generateRandom();
                    if (p.IsEmpty)
                    {
                        rvs.p2.canMove = false;
                        rvs.changeTurn();
                        move();
                        timer.Stop();
                        return;
                    }
                    rvs.changeValue(p.X, p.Y);
                    rvs.changeTurn();
                    sp.Play();
                    move();
                }
                if (p2.type == Type.Hard)
                {
                    //Rvs temp = DeepClone(rvs);
                    // Point p = temp.generateBestMove();
                    Point p = rvs.bestMove();
                    if (p.IsEmpty)
                    {
                        rvs.p2.canMove = false;
                        rvs.changeTurn();
                        move();
                        timer.Stop();
                        return;
                    }
                    rvs.changeValue(p.X, p.Y);
                        rvs.changeTurn();
                    sp.Play();
                    move();
                }
                
            }
            timer.Stop();
        }

        private HighScore desHighScores()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            HighScore temp = null;
            try
            {
                using (FileStream fs = File.OpenRead(path + "\\HighScore.hrvs"))
                {
                    IFormatter formatter = new BinaryFormatter();
                    temp = (HighScore)formatter.Deserialize(fs);
                }

                File.Delete(path + "\\HighScore.hrvs");

                return temp;
            }
            catch(FileNotFoundException)
            {
                return new HighScore();
            }
        }

        private void serHighScores(HighScore hs)
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            using (FileStream fs = File.Create(path + "\\HighScore.hrvs"))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fs, hs);
            }
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
            changePanel1();
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
            changePanel2();
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
            Invalidate(true);
            move();
            return;
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
                        sp.Play();
                        rvs.changeValue(point.X, point.Y);
                        // panel2.Invalidate(true);
                        rvs.changeTurn();
                        move();
                        return;
                    }
                }
            }
 
        }

        public void move()
        {
            if (finished == true) return;
            Invalidate(true);
            lblPrv.Text = p1.name + "\n" + " Coins: " + rvs.getFirst();
            lblVtor.Text = p2.name + "\n" + " Coins: " + rvs.getSecond();
            if  (rvs.p1.canMove == false && rvs.p2.canMove == false)
            {
                p1.canMove = true;
                p2.canMove = true;
                if (rvs.getFirst()>rvs.getSecond())
                {
                    finished = true;
                    if (rvs.p2.type==Type.Human)
                    {
                        DialogResult dr = MessageBox.Show("The game is finished. " + p1.name + " is the winner. Do you want your result to be added to the Highscore list?", "Reversi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            hscore.addItem(new RankedPlayers(rvs.p1.name, rvs.p2.name, rvs.getFirst(), rvs.getSecond()));
                            serHighScores(hscore);
                        }
                    }
                    else 
                    {
                       DialogResult dr= MessageBox.Show("The game is finished. " + p1.name + " is the winner. Do you want your result to be added to the Highscore list?", "Reversi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                       if (dr == DialogResult.Yes)
                        {
                            hscore.addItem(new RankedPlayers(rvs.p1.name,rvs.p2.name, rvs.getFirst(), rvs.getSecond()));
                            serHighScores(hscore);
                        }
                    }
                    return;
                }
                else if (rvs.getFirst() < rvs.getSecond())
                {
                    finished = true;
                    if (rvs.p2.type == Type.Human)
                    {
                        DialogResult dr = MessageBox.Show("The game is finished. " + p2.name + " is the winner. Do you want your result to be added to the Highscore list?", "Reversi", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dr == DialogResult.Yes)
                        {
                            hscore.addItem(new RankedPlayers(rvs.p2.name, rvs.p1.name, rvs.getSecond(), rvs.getFirst()));
                            serHighScores(hscore);
                        }
                    }
                    else
                    {
                        MessageBox.Show("The game is finished. " + p2.name + " is the winner.", "Reversi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return;
                }
                else
                {
                    MessageBox.Show("The game is finished. It's draw.","Reversi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    finished = true;
                    return;
                }
            }
            rvs.findPossibleMoves();
            if (rvs.turn==1)
            {
                if (rvs.noPossibleMoves())
                {
                    rvs.p1.canMove = false;
                    rvs.changeTurn();
                    move();
                    return;

                }
                else
                {
                    rvs.p1.canMove = true;
                    return;
                }

            }

            if (rvs.turn==2)
            {
                if (rvs.noPossibleMoves())
                {
                    rvs.p2.canMove = false;
                    rvs.changeTurn();
                    move();
                    return;
                }
                else
                {
                    rvs.p2.canMove = true;
                    if (rvs.p2.type != Type.Human)
                    {
                        if (rvs.possibleMoves.Count == 0)
                        {
                            rvs.p2.canMove = false;
                            rvs.changeTurn();
                            move();
                            return;
                        }
                        timer.Start();
                    }
                    return;
                }
            }
            Invalidate(true);
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            newGame();
        }

        public void newGame()
        {
            FileName = null;
            finished = false;
            int turn = new Random().Next(1, 3);
            rvs = new Rvs(turn);
            changePanel2();
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
            Invalidate(true);
            move();
            return;
        }

        private void btnHighScores_Click(object sender, EventArgs e)
        {
            if (hscore.list.Count==0)
            {
                MessageBox.Show("No highscores to show!","Empty list",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }
            changePanel3();
            listBox1.Items.Clear();
            hscore.list.Sort();
            hscore.list.Reverse();
            foreach (RankedPlayers p in hscore.list)
            {
                listBox1.Items.Add(p);
            }
            
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnReset_MouseHover(object sender, EventArgs e)
        {
            t1.Show("Start a game",btnReset);
        }

        private void btnSave_MouseHover(object sender, EventArgs e)
        {
            t1.Show("Save the current game", btnSave);
        }

        private void btnGoBack_MouseHover(object sender, EventArgs e)
        {
            t1.Show("Go back to the main menu", btnGoBack);
        }

        private void btnBack2_Click(object sender, EventArgs e)
        {
            changePanel1();
        }

        public void changePanel1()
        {
            this.Text = "Reversi";
            colorp1 = panelColor1.BackColor;
            colorp2 = panelColor2.BackColor;
            panel1.Visible = true;
            panel1.Enabled = true;
            panel2.Visible = false;
            panel2.Enabled = false;
            panel3.Visible = false;
            panel3.Enabled = false;
        }

        public void changePanel2()
        {
            this.Text = "Reversi - New game";
            panel1.Visible = false;
            panel1.Enabled = false;
            panel2.Visible = true;
            panel2.Enabled = true;
            panel3.Enabled = false;
            panel3.Visible = false;
        }



        public void changePanel3()
        {
            this.Text = "Reversi - Highscores";
            panel2.Visible = false;
            panel2.Enabled = false;
            panel1.Visible = false;
            panel1.Enabled = false;
            panel3.Enabled = true;
            panel3.Visible = true;
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            if ((rvs.getFirst() == 2 && rvs.getSecond() == 2) || (finished))
            {
                changePanel1();
                return;
            }
            DialogResult dr = MessageBox.Show("Do you want to save the game before going back?", "Back to main menu", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                saveGame();
                changePanel1();
            }
            else if (dr == DialogResult.No)
            {
                changePanel1();
            }
           
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            if (rvs.getFirst() == 2 && rvs.getSecond() == 2) return;
            if (finished==true)
            {
                newGame(); return;

            }
            DialogResult dr= MessageBox.Show("Do you want to save the game?", "Reset", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                saveGame();
                newGame();
            }
            else if(dr==DialogResult.No)
            {
                newGame();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (finished==false)
            saveGame();
        }

        public void saveGame()
        {
            if (FileName ==null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Reversi (*.rvs)|*.rvs";
                sfd.Title = "Save game";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    FileName = sfd.FileName;
                }
            }
            if (FileName!=null)
            {
                using (FileStream fs = new FileStream(FileName, FileMode.Create))
                {
                    IFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(fs, rvs);
                }
            }
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            loadGame();
        }

        public void loadGame()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter= "Reversi (*.rvs)|*.rvs";
            ofd.Title = "Load game";
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                FileName = ofd.FileName;
                try
                {
                    using (FileStream fs = new FileStream(FileName, FileMode.Open))
                    {
                        IFormatter formatter = new BinaryFormatter();
                        rvs = (Rvs)formatter.Deserialize(fs);
                        updateGame();
                        changePanel2();
                        this.Text = "Reversi - " + FileName;
                        Invalidate(true);
                    }
                }
                catch (Exception )
                {
                    MessageBox.Show("Could not read file: " + FileName);
                    FileName = null;
                    return;
                }
                
            }
        }

        public void updateGame()
        {
            p1 = rvs.p1;
            p2 = rvs.p2;
            colorp1 = rvs.p1.color;
            colorp2 = rvs.p2.color;
            finished = false;
            lblPrv.Text = "";
            lblPrv.ForeColor = colorp1;
            lblPrv.Text += p1.name + "\n" + " Coins: " + rvs.getFirst();
            lblVtor.Text = "";
            lblVtor.Text = p2.name + "\n" + " Coins: " + rvs.getSecond();
            lblVtor.ForeColor = colorp2;
            Invalidate(true);
            move();
            return;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (panel2.Enabled == true && panel2.Visible == true)
            {
                if ((rvs.getFirst() != 2 || rvs.getSecond() != 2) && finished==false)
                {
                    DialogResult dr = MessageBox.Show("Do you want to save the game before quiting?", "Exit game", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);
                    if (dr == DialogResult.Yes)
                    {
                        saveGame();
                    }
                    if (dr ==DialogResult.Cancel)
                    {
                        e.Cancel = true;
                        return;
                    }
                }
            }
            serHighScores(hscore);
        }

        public static Rvs DeepClone(Rvs obj)
        {
            using (var ms = new MemoryStream())
            {
                var formatter = new BinaryFormatter();
                formatter.Serialize(ms, obj);
                ms.Position = 0;

                return (Rvs)formatter.Deserialize(ms);
            }
        }
    }
}
