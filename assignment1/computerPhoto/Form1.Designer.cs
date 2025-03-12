namespace computerPhoto
{
    partial class 计算器
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(计算器));
            运算 = new Button();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            SuspendLayout();
            // 
            // 运算
            // 
            运算.BackColor = SystemColors.ActiveCaption;
            运算.Cursor = Cursors.SizeAll;
            resources.ApplyResources(运算, "运算");
            运算.Name = "运算";
            运算.UseVisualStyleBackColor = false;
            运算.Click += 运算_Click;
            运算.MouseClick += 运算_MouseClick;
            // 
            // textBox1
            // 
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            // 
            // textBox2
            // 
            resources.ApplyResources(textBox2, "textBox2");
            textBox2.Name = "textBox2";
            // 
            // textBox3
            // 
            resources.ApplyResources(textBox3, "textBox3");
            textBox3.Name = "textBox3";
            // 
            // button1
            // 
            button1.BackColor = SystemColors.Highlight;
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.UseVisualStyleBackColor = false;
            button1.MouseClick += Button1_MouseClick;
            // 
            // button2
            // 
            button2.BackColor = SystemColors.MenuHighlight;
            resources.ApplyResources(button2, "button2");
            button2.Name = "button2";
            button2.UseVisualStyleBackColor = false;
            button2.MouseClick += Button2_MouseClick;
            // 
            // button3
            // 
            button3.BackColor = SystemColors.MenuHighlight;
            resources.ApplyResources(button3, "button3");
            button3.Name = "button3";
            button3.UseVisualStyleBackColor = false;
            button3.MouseClick += Button3_MouseClick;
            // 
            // button4
            // 
            button4.BackColor = SystemColors.MenuHighlight;
            resources.ApplyResources(button4, "button4");
            button4.Name = "button4";
            button4.UseVisualStyleBackColor = false;
            button4.MouseClick += Button4_MouseClick;
            // 
            // 计算器
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(运算);
            Name = "计算器";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button 运算;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private Button button1;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
