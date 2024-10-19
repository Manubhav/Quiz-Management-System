namespace Quiz_Management_System
{
    partial class AppForm
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
            label1 = new Label();
            panel1 = new Panel();
            button7 = new Button();
            button6 = new Button();
            button5 = new Button();
            button4 = new Button();
            button3 = new Button();
            button2 = new Button();
            button1 = new Button();
            pictureBox1 = new PictureBox();
            contentPanel = new Panel();
            ustuResults1 = new UstuResults();
            uStuReg1 = new uStuReg();
            uUpdate1 = new uUpdate();
            udelete1 = new Udelete();
            newSubject1 = new NewSubject();
            uC_AddNewQuestion1 = new Teacher_UC.UC_AddNewQuestion();
            pictureBox2 = new PictureBox();
            label2 = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            contentPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(426, 161);
            label1.Name = "label1";
            label1.Size = new Size(0, 20);
            label1.TabIndex = 0;
            // 
            // panel1
            // 
            panel1.BackColor = Color.DodgerBlue;
            panel1.Controls.Add(button7);
            panel1.Controls.Add(button6);
            panel1.Controls.Add(button5);
            panel1.Controls.Add(button4);
            panel1.Controls.Add(button3);
            panel1.Controls.Add(button2);
            panel1.Controls.Add(button1);
            panel1.Controls.Add(pictureBox1);
            panel1.Dock = DockStyle.Left;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(3, 4, 3, 4);
            panel1.Name = "panel1";
            panel1.Size = new Size(223, 847);
            panel1.TabIndex = 1;
            // 
            // button7
            // 
            button7.BackColor = Color.Transparent;
            button7.FlatAppearance.BorderSize = 0;
            button7.FlatStyle = FlatStyle.Flat;
            button7.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button7.Location = new Point(14, 161);
            button7.Margin = new Padding(3, 4, 3, 4);
            button7.Name = "button7";
            button7.Size = new Size(177, 43);
            button7.TabIndex = 6;
            button7.Text = "Add Subject";
            button7.UseVisualStyleBackColor = false;
            button7.Click += button7_Click;
            // 
            // button6
            // 
            button6.FlatAppearance.BorderColor = Color.Red;
            button6.FlatAppearance.MouseDownBackColor = Color.Red;
            button6.FlatAppearance.MouseOverBackColor = Color.Red;
            button6.FlatStyle = FlatStyle.Flat;
            button6.Font = new Font("Segoe UI Semibold", 9.75F, FontStyle.Bold, GraphicsUnit.Point);
            button6.Location = new Point(14, 759);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new Size(186, 45);
            button6.TabIndex = 5;
            button6.Text = "Exit";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.FlatAppearance.BorderSize = 0;
            button5.FlatStyle = FlatStyle.Flat;
            button5.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button5.Location = new Point(33, 517);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new Size(167, 79);
            button5.TabIndex = 3;
            button5.Text = "Results";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button4
            // 
            button4.FlatAppearance.BorderSize = 0;
            button4.FlatStyle = FlatStyle.Flat;
            button4.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button4.Location = new Point(14, 448);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new Size(186, 48);
            button4.TabIndex = 3;
            button4.Text = "Registration";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button3
            // 
            button3.FlatAppearance.BorderSize = 0;
            button3.FlatStyle = FlatStyle.Flat;
            button3.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button3.Location = new Point(7, 371);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new Size(193, 53);
            button3.TabIndex = 4;
            button3.Text = "Delete Question";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.FlatAppearance.BorderSize = 0;
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.Location = new Point(39, 303);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new Size(142, 43);
            button2.TabIndex = 3;
            button2.Text = "Edit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button1
            // 
            button1.BackColor = Color.Transparent;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.Location = new Point(39, 232);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(142, 43);
            button1.TabIndex = 2;
            button1.Text = "Add Question";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources._11640168385jtmh7kpmvna5ddyynoxsjy5leb1nmpvqooaavkrjmt9zs7vtvuqi4lcwofkzsaejalxn7ggpim4hkg0wbwtzsrp1ldijzbdbsj5z;
            pictureBox1.Location = new Point(61, 16);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(96, 101);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 2;
            pictureBox1.TabStop = false;
            // 
            // contentPanel
            // 
            contentPanel.Controls.Add(ustuResults1);
            contentPanel.Controls.Add(uStuReg1);
            contentPanel.Controls.Add(uUpdate1);
            contentPanel.Controls.Add(udelete1);
            contentPanel.Controls.Add(newSubject1);
            contentPanel.Controls.Add(uC_AddNewQuestion1);
            contentPanel.Controls.Add(pictureBox2);
            contentPanel.Controls.Add(label2);
            contentPanel.Dock = DockStyle.Fill;
            contentPanel.Location = new Point(223, 0);
            contentPanel.Margin = new Padding(3, 4, 3, 4);
            contentPanel.Name = "contentPanel";
            contentPanel.Size = new Size(747, 847);
            contentPanel.TabIndex = 2;
            // 
            // ustuResults1
            // 
            ustuResults1.BackColor = Color.SkyBlue;
            ustuResults1.Location = new Point(0, 0);
            ustuResults1.Margin = new Padding(3, 5, 3, 5);
            ustuResults1.Name = "ustuResults1";
            ustuResults1.Size = new Size(747, 847);
            ustuResults1.TabIndex = 7;
            // 
            // uStuReg1
            // 
            uStuReg1.BackColor = Color.SkyBlue;
            uStuReg1.Location = new Point(0, 0);
            uStuReg1.Margin = new Padding(3, 5, 3, 5);
            uStuReg1.Name = "uStuReg1";
            uStuReg1.Size = new Size(747, 847);
            uStuReg1.TabIndex = 10;
            // 
            // uUpdate1
            // 
            uUpdate1.BackColor = Color.SkyBlue;
            uUpdate1.Location = new Point(0, 0);
            uUpdate1.Margin = new Padding(3, 5, 3, 5);
            uUpdate1.Name = "uUpdate1";
            uUpdate1.Size = new Size(747, 847);
            uUpdate1.TabIndex = 9;
            // 
            // udelete1
            // 
            udelete1.BackColor = Color.SkyBlue;
            udelete1.Location = new Point(0, 0);
            udelete1.Margin = new Padding(3, 5, 3, 5);
            udelete1.Name = "udelete1";
            udelete1.Size = new Size(747, 847);
            udelete1.TabIndex = 8;
            // 
            // newSubject1
            // 
            newSubject1.BackColor = Color.SkyBlue;
            newSubject1.Location = new Point(0, 0);
            newSubject1.Margin = new Padding(3, 5, 3, 5);
            newSubject1.Name = "newSubject1";
            newSubject1.Size = new Size(747, 847);
            newSubject1.TabIndex = 7;
            // 
            // uC_AddNewQuestion1
            // 
            uC_AddNewQuestion1.BackColor = Color.SkyBlue;
            uC_AddNewQuestion1.Location = new Point(0, 0);
            uC_AddNewQuestion1.Margin = new Padding(3, 5, 3, 5);
            uC_AddNewQuestion1.Name = "uC_AddNewQuestion1";
            uC_AddNewQuestion1.Size = new Size(747, 847);
            uC_AddNewQuestion1.TabIndex = 2;
            // 
            // pictureBox2
            // 
            pictureBox2.Image = Properties.Resources._1;
            pictureBox2.Location = new Point(189, 84);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(286, 221);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Arial Rounded MT Bold", 15.75F, FontStyle.Italic, GraphicsUnit.Point);
            label2.Location = new Point(240, 419);
            label2.Name = "label2";
            label2.Size = new Size(214, 32);
            label2.TabIndex = 0;
            label2.Text = "Hello, Teacher!";
            // 
            // AppForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(970, 847);
            Controls.Add(contentPanel);
            Controls.Add(panel1);
            Controls.Add(label1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "AppForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "-";
            Load += AppForm_Load;
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            contentPanel.ResumeLayout(false);
            contentPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Panel panel1;
        private Button button6;
        private Button button5;
        private Button button4;
        private Button button3;
        private Button button2;
        private Button button1;
        private PictureBox pictureBox1;
        private Panel contentPanel;
        private PictureBox pictureBox2;
        private Label label2;
        private Button button7;
        private Teacher_UC.UC_AddNewQuestion uC_AddNewQuestion1;
        private NewSubject newSubject1;
        private Udelete udelete1;
        private uUpdate uUpdate1;
        private uStuReg uStuReg1;
        private UstuResults ustuResults1;
    }
}