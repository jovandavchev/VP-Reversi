namespace VP_Reversi
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.newToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.saveToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panelColor2 = new System.Windows.Forms.Panel();
            this.color2 = new System.Windows.Forms.Button();
            this.name2 = new System.Windows.Forms.TextBox();
            this.ddl2 = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panelColor1 = new System.Windows.Forms.Panel();
            this.color1 = new System.Windows.Forms.Button();
            this.name1 = new System.Windows.Forms.TextBox();
            this.ddl1 = new System.Windows.Forms.ComboBox();
            this.btnHighScores = new System.Windows.Forms.Button();
            this.btnLoadGame = new System.Windows.Forms.Button();
            this.btnNewGame = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblVtor = new System.Windows.Forms.Label();
            this.lblPrv = new System.Windows.Forms.Label();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripButton,
            this.openToolStripButton,
            this.saveToolStripButton,
            this.toolStripSeparator6,
            this.helpToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(434, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStrip1_ItemClicked);
            // 
            // newToolStripButton
            // 
            this.newToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripButton.Image")));
            this.newToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newToolStripButton.Name = "newToolStripButton";
            this.newToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.newToolStripButton.Text = "&New";
            this.newToolStripButton.Click += new System.EventHandler(this.newToolStripButton_Click);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(56, 22);
            this.openToolStripButton.Text = "&Open";
            // 
            // saveToolStripButton
            // 
            this.saveToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("saveToolStripButton.Image")));
            this.saveToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.saveToolStripButton.Name = "saveToolStripButton";
            this.saveToolStripButton.Size = new System.Drawing.Size(51, 22);
            this.saveToolStripButton.Text = "&Save";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("helpToolStripButton.Image")));
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(52, 22);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.btnHighScores);
            this.panel1.Controls.Add(this.btnLoadGame);
            this.panel1.Controls.Add(this.btnNewGame);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 42);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(410, 334);
            this.panel1.TabIndex = 3;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.panelColor2);
            this.groupBox2.Controls.Add(this.color2);
            this.groupBox2.Controls.Add(this.name2);
            this.groupBox2.Controls.Add(this.ddl2);
            this.groupBox2.Location = new System.Drawing.Point(196, 239);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(189, 92);
            this.groupBox2.TabIndex = 5;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Player 2";
            // 
            // panelColor2
            // 
            this.panelColor2.BackColor = System.Drawing.Color.Red;
            this.panelColor2.Location = new System.Drawing.Point(6, 71);
            this.panelColor2.Name = "panelColor2";
            this.panelColor2.Size = new System.Drawing.Size(177, 10);
            this.panelColor2.TabIndex = 4;
            // 
            // color2
            // 
            this.color2.Location = new System.Drawing.Point(137, 45);
            this.color2.Name = "color2";
            this.color2.Size = new System.Drawing.Size(46, 20);
            this.color2.TabIndex = 2;
            this.color2.Text = "Color";
            this.color2.UseVisualStyleBackColor = true;
            this.color2.Click += new System.EventHandler(this.color2_Click);
            // 
            // name2
            // 
            this.name2.Location = new System.Drawing.Point(6, 45);
            this.name2.Name = "name2";
            this.name2.Size = new System.Drawing.Size(125, 20);
            this.name2.TabIndex = 1;
            // 
            // ddl2
            // 
            this.ddl2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl2.FormattingEnabled = true;
            this.ddl2.Items.AddRange(new object[] {
            "Human",
            "Computer - Easy",
            "Computer - Hard"});
            this.ddl2.Location = new System.Drawing.Point(6, 18);
            this.ddl2.Name = "ddl2";
            this.ddl2.Size = new System.Drawing.Size(177, 21);
            this.ddl2.TabIndex = 0;
            this.ddl2.SelectedIndexChanged += new System.EventHandler(this.ddl2_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panelColor1);
            this.groupBox1.Controls.Add(this.color1);
            this.groupBox1.Controls.Add(this.name1);
            this.groupBox1.Controls.Add(this.ddl1);
            this.groupBox1.Location = new System.Drawing.Point(196, 148);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(189, 92);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Player 1";
            // 
            // panelColor1
            // 
            this.panelColor1.BackColor = System.Drawing.Color.Blue;
            this.panelColor1.Location = new System.Drawing.Point(6, 70);
            this.panelColor1.Name = "panelColor1";
            this.panelColor1.Size = new System.Drawing.Size(177, 10);
            this.panelColor1.TabIndex = 3;
            // 
            // color1
            // 
            this.color1.Location = new System.Drawing.Point(137, 45);
            this.color1.Name = "color1";
            this.color1.Size = new System.Drawing.Size(46, 20);
            this.color1.TabIndex = 2;
            this.color1.Text = "Color";
            this.color1.UseVisualStyleBackColor = true;
            this.color1.Click += new System.EventHandler(this.color1_Click);
            // 
            // name1
            // 
            this.name1.Location = new System.Drawing.Point(6, 45);
            this.name1.Name = "name1";
            this.name1.Size = new System.Drawing.Size(125, 20);
            this.name1.TabIndex = 1;
            // 
            // ddl1
            // 
            this.ddl1.DisplayMember = "(none)";
            this.ddl1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddl1.FormattingEnabled = true;
            this.ddl1.Items.AddRange(new object[] {
            "Human",
            "Computer - Easy",
            "Computer - Hard"});
            this.ddl1.Location = new System.Drawing.Point(6, 18);
            this.ddl1.Name = "ddl1";
            this.ddl1.Size = new System.Drawing.Size(177, 21);
            this.ddl1.TabIndex = 0;
            this.ddl1.SelectedIndexChanged += new System.EventHandler(this.ddl1_SelectedIndexChanged);
            // 
            // btnHighScores
            // 
            this.btnHighScores.Location = new System.Drawing.Point(22, 271);
            this.btnHighScores.Name = "btnHighScores";
            this.btnHighScores.Size = new System.Drawing.Size(144, 36);
            this.btnHighScores.TabIndex = 3;
            this.btnHighScores.Text = "High scores";
            this.btnHighScores.UseVisualStyleBackColor = true;
            this.btnHighScores.Click += new System.EventHandler(this.btnHighScores_Click);
            // 
            // btnLoadGame
            // 
            this.btnLoadGame.Location = new System.Drawing.Point(22, 218);
            this.btnLoadGame.Name = "btnLoadGame";
            this.btnLoadGame.Size = new System.Drawing.Size(144, 36);
            this.btnLoadGame.TabIndex = 2;
            this.btnLoadGame.Text = "Load game";
            this.btnLoadGame.UseVisualStyleBackColor = true;
            // 
            // btnNewGame
            // 
            this.btnNewGame.Location = new System.Drawing.Point(22, 166);
            this.btnNewGame.Name = "btnNewGame";
            this.btnNewGame.Size = new System.Drawing.Size(144, 36);
            this.btnNewGame.TabIndex = 1;
            this.btnNewGame.Text = "New game";
            this.btnNewGame.UseVisualStyleBackColor = true;
            this.btnNewGame.Click += new System.EventHandler(this.btnNewGame_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::VP_Reversi.Properties.Resources.hd1;
            this.pictureBox1.Location = new System.Drawing.Point(22, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(363, 121);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblVtor);
            this.panel2.Controls.Add(this.lblPrv);
            this.panel2.ForeColor = System.Drawing.Color.Black;
            this.panel2.Location = new System.Drawing.Point(12, 42);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(419, 331);
            this.panel2.TabIndex = 6;
            this.panel2.Visible = false;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            this.panel2.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel2_MouseClick);
            // 
            // lblVtor
            // 
            this.lblVtor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVtor.Location = new System.Drawing.Point(305, 119);
            this.lblVtor.Name = "lblVtor";
            this.lblVtor.Size = new System.Drawing.Size(111, 68);
            this.lblVtor.TabIndex = 1;
            this.lblVtor.Text = "label2";
            this.lblVtor.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblPrv
            // 
            this.lblPrv.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPrv.Location = new System.Drawing.Point(305, 40);
            this.lblPrv.Name = "lblPrv";
            this.lblPrv.Size = new System.Drawing.Size(111, 65);
            this.lblPrv.TabIndex = 0;
            this.lblPrv.Text = "label1";
            this.lblPrv.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // Form1
            // 
            this.AcceptButton = this.btnNewGame;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 391);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "Reversi";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton newToolStripButton;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripButton saveToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button color2;
        private System.Windows.Forms.TextBox name2;
        private System.Windows.Forms.ComboBox ddl2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button color1;
        private System.Windows.Forms.TextBox name1;
        private System.Windows.Forms.ComboBox ddl1;
        private System.Windows.Forms.Button btnHighScores;
        private System.Windows.Forms.Button btnLoadGame;
        private System.Windows.Forms.Button btnNewGame;
        private System.Windows.Forms.Panel panelColor2;
        private System.Windows.Forms.Panel panelColor1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Label lblVtor;
        private System.Windows.Forms.Label lblPrv;
    }
}

