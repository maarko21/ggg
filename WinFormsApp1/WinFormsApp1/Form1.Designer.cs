namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button1 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            pictureBox1 = new PictureBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            loadToolStripMenuItem1 = new ToolStripMenuItem();
            undoToolStripMenuItem = new ToolStripMenuItem();
            redoToolStripMenuItem = new ToolStripMenuItem();
            saveToolStripMenuItem = new ToolStripMenuItem();
            restoreToolStripMenuItem = new ToolStripMenuItem();
            loadmarkoToolStripMenuItem = new ToolStripMenuItem();
            savemarkoToolStripMenuItem = new ToolStripMenuItem();
            textBox3 = new TextBox();
            button2 = new Button();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            panel1 = new Panel();
            buttonACT = new Button();
            textBoxACT = new TextBox();
            panel2 = new Panel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            menuStrip1.SuspendLayout();
            panel1.SuspendLayout();
            panel2.SuspendLayout();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(57, 258);
            button1.Name = "button1";
            button1.Size = new Size(112, 34);
            button1.TabIndex = 0;
            button1.Text = "gamma";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(36, 47);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(150, 31);
            textBox1.TabIndex = 2;
            // 
            // textBox2
            // 
            textBox2.Location = new Point(60, 102);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(150, 31);
            textBox2.TabIndex = 3;
            // 
            // pictureBox1
            // 
            pictureBox1.Location = new Point(298, 88);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(490, 350);
            pictureBox1.TabIndex = 4;
            pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new Size(24, 24);
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 33);
            menuStrip1.TabIndex = 5;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { loadToolStripMenuItem1, undoToolStripMenuItem, redoToolStripMenuItem, saveToolStripMenuItem, restoreToolStripMenuItem, loadmarkoToolStripMenuItem, savemarkoToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(54, 29);
            fileToolStripMenuItem.Text = "File";
            // 
            // loadToolStripMenuItem1
            // 
            loadToolStripMenuItem1.Name = "loadToolStripMenuItem1";
            loadToolStripMenuItem1.Size = new Size(270, 34);
            loadToolStripMenuItem1.Text = "Load file";
            loadToolStripMenuItem1.Click += loadToolStripMenuItem1_Click;
            // 
            // undoToolStripMenuItem
            // 
            undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            undoToolStripMenuItem.Size = new Size(270, 34);
            undoToolStripMenuItem.Text = "Undo";
            undoToolStripMenuItem.Click += undoToolStripMenuItem_Click;
            // 
            // redoToolStripMenuItem
            // 
            redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            redoToolStripMenuItem.Size = new Size(270, 34);
            redoToolStripMenuItem.Text = "Redo";
            redoToolStripMenuItem.Click += redoToolStripMenuItem_Click;
            // 
            // saveToolStripMenuItem
            // 
            saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            saveToolStripMenuItem.Size = new Size(270, 34);
            saveToolStripMenuItem.Text = "Save";
            saveToolStripMenuItem.Click += saveToolStripMenuItem_Click;
            // 
            // restoreToolStripMenuItem
            // 
            restoreToolStripMenuItem.Name = "restoreToolStripMenuItem";
            restoreToolStripMenuItem.Size = new Size(270, 34);
            restoreToolStripMenuItem.Text = "Restore";
            restoreToolStripMenuItem.Click += restoreToolStripMenuItem_Click;
            // 
            // loadmarkoToolStripMenuItem
            // 
            loadmarkoToolStripMenuItem.Name = "loadmarkoToolStripMenuItem";
            loadmarkoToolStripMenuItem.Size = new Size(270, 34);
            loadmarkoToolStripMenuItem.Text = "load .marko";
            loadmarkoToolStripMenuItem.Click += loadmarkoToolStripMenuItem_Click;
            // 
            // savemarkoToolStripMenuItem
            // 
            savemarkoToolStripMenuItem.Name = "savemarkoToolStripMenuItem";
            savemarkoToolStripMenuItem.Size = new Size(270, 34);
            savemarkoToolStripMenuItem.Text = "save .marko";
            savemarkoToolStripMenuItem.Click += savemarkoToolStripMenuItem_Click;
            // 
            // textBox3
            // 
            textBox3.Location = new Point(39, 162);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(150, 31);
            textBox3.TabIndex = 6;
            // 
            // button2
            // 
            button2.Location = new Point(597, 48);
            button2.Name = "button2";
            button2.Size = new Size(175, 34);
            button2.TabIndex = 7;
            button2.Text = "sharpen";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(0, 50);
            label1.Name = "label1";
            label1.Size = new Size(45, 25);
            label1.TabIndex = 8;
            label1.Text = "RED";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(-21, 105);
            label2.Name = "label2";
            label2.Size = new Size(66, 25);
            label2.TabIndex = 9;
            label2.Text = "GREEN";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(-6, 162);
            label3.Name = "label3";
            label3.Size = new Size(51, 25);
            label3.TabIndex = 10;
            label3.Text = "BLUE";
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(button1);
            panel1.Controls.Add(label3);
            panel1.Controls.Add(textBox3);
            panel1.Controls.Add(textBox2);
            panel1.Controls.Add(textBox1);
            panel1.Controls.Add(label2);
            panel1.Controls.Add(label1);
            panel1.Location = new Point(79, 36);
            panel1.Name = "panel1";
            panel1.Size = new Size(213, 295);
            panel1.TabIndex = 11;
            // 
            // buttonACT
            // 
            buttonACT.Location = new Point(31, 49);
            buttonACT.Name = "buttonACT";
            buttonACT.Size = new Size(155, 34);
            buttonACT.TabIndex = 12;
            buttonACT.Text = "AdjustColor Temperature";
            buttonACT.UseVisualStyleBackColor = true;
            buttonACT.Click += buttonACT_Click;
            // 
            // textBoxACT
            // 
            textBoxACT.Location = new Point(62, 26);
            textBoxACT.Name = "textBoxACT";
            textBoxACT.Size = new Size(150, 31);
            textBoxACT.TabIndex = 13;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.Controls.Add(textBoxACT);
            panel2.Controls.Add(buttonACT);
            panel2.Location = new Point(76, 337);
            panel2.Name = "panel2";
            panel2.Size = new Size(216, 114);
            panel2.TabIndex = 14;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(panel2);
            Controls.Add(panel1);
            Controls.Add(button2);
            Controls.Add(pictureBox1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private TextBox textBox1;
        private TextBox textBox2;
        private PictureBox pictureBox1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem loadToolStripMenuItem1;
        private TextBox textBox3;
        private Button button2;
        private ToolStripMenuItem undoToolStripMenuItem;
        private ToolStripMenuItem redoToolStripMenuItem;
        private ToolStripMenuItem saveToolStripMenuItem;
        private ToolStripMenuItem restoreToolStripMenuItem;
        private Label label1;
        private Label label2;
        private Label label3;
        private ToolStripMenuItem loadmarkoToolStripMenuItem;
        private ToolStripMenuItem savemarkoToolStripMenuItem;
        private Panel panel1;
        private Button buttonACT;
        private TextBox textBoxACT;
        private Panel panel2;
    }
}
