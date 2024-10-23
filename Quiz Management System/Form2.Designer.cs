namespace Quiz_Management_System
{
    partial class Form2
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
            backButton = new Button();
            pictureBox1 = new PictureBox();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            EmailLog = new TextBox();
            PassLog = new TextBox();
            button1 = new Button();
            label5 = new Label();
            linkLabel1 = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // backButton
            // 
            backButton.BackColor = Color.MediumSeaGreen;
            backButton.FlatAppearance.BorderColor = Color.MediumSeaGreen;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatAppearance.MouseDownBackColor = Color.LightSalmon;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new Font("Arial Rounded MT Bold", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(631, 450);
            backButton.Margin = new Padding(3, 4, 3, 4);
            backButton.Name = "backButton";
            backButton.Size = new Size(286, 44);
            backButton.TabIndex = 10;
            backButton.Text = "Back";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += backButton_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = Properties.Resources.ezgif_com_webp_to_jpg;
            pictureBox1.Location = new Point(-1, 0);
            pictureBox1.Margin = new Padding(3, 4, 3, 4);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(537, 659);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial Rounded MT Bold", 20.25F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(561, 28);
            label1.Name = "label1";
            label1.Size = new Size(180, 39);
            label1.TabIndex = 1;
            label1.Text = "Welcome!";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.BackColor = Color.Transparent;
            label2.Font = new Font("Arial Rounded MT Bold", 18F, FontStyle.Regular, GraphicsUnit.Point);
            label2.ForeColor = Color.MediumSeaGreen;
            label2.Location = new Point(759, 125);
            label2.Name = "label2";
            label2.Size = new Size(96, 34);
            label2.TabIndex = 2;
            label2.Text = "Login";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(597, 213);
            label3.Name = "label3";
            label3.Size = new Size(60, 25);
            label3.TabIndex = 3;
            label3.Text = "Email";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(597, 296);
            label4.Name = "label4";
            label4.Size = new Size(98, 25);
            label4.TabIndex = 4;
            label4.Text = "Password";
            // 
            // EmailLog
            // 
            EmailLog.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            EmailLog.Location = new Point(743, 212);
            EmailLog.Margin = new Padding(3, 4, 3, 4);
            EmailLog.Name = "EmailLog";
            EmailLog.Size = new Size(193, 30);
            EmailLog.TabIndex = 5;
            // 
            // PassLog
            // 
            PassLog.Font = new Font("Tahoma", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            PassLog.Location = new Point(743, 296);
            PassLog.Margin = new Padding(3, 4, 3, 4);
            PassLog.Name = "PassLog";
            PassLog.PasswordChar = '•';
            PassLog.Size = new Size(193, 30);
            PassLog.TabIndex = 6;
            // 
            // button1
            // 
            button1.BackColor = Color.MediumSeaGreen;
            button1.FlatAppearance.BorderColor = Color.MediumSeaGreen;
            button1.FlatAppearance.BorderSize = 0;
            button1.FlatAppearance.MouseDownBackColor = Color.LightSalmon;
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Arial Rounded MT Bold", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            button1.ForeColor = Color.White;
            button1.Location = new Point(631, 376);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new Size(286, 44);
            button1.TabIndex = 7;
            button1.Text = "Login";
            button1.UseVisualStyleBackColor = false;
            button1.Click += button1_Click;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Microsoft Sans Serif", 9.75F, FontStyle.Underline, GraphicsUnit.Point);
            label5.Location = new Point(648, 498);
            label5.Name = "label5";
            label5.Size = new Size(185, 20);
            label5.TabIndex = 8;
            label5.Text = "Don’t have an account?";
            // 
            // linkLabel1
            // 
            linkLabel1.AutoSize = true;
            linkLabel1.Font = new Font("Microsoft Sans Serif", 9F, FontStyle.Regular, GraphicsUnit.Point);
            linkLabel1.Location = new Point(839, 498);
            linkLabel1.Name = "linkLabel1";
            linkLabel1.Size = new Size(63, 18);
            linkLabel1.TabIndex = 9;
            linkLabel1.TabStop = true;
            linkLabel1.Text = "Register";
            linkLabel1.LinkClicked += linkLabel1_LinkClicked;
            // 
            // Form2
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1061, 659);
            Controls.Add(linkLabel1);
            Controls.Add(label5);
            Controls.Add(button1);
            Controls.Add(PassLog);
            Controls.Add(EmailLog);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(pictureBox1);
            Controls.Add(backButton);
            FormBorderStyle = FormBorderStyle.None;
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form2";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form2";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pictureBox1;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox EmailLog;
        private TextBox PassLog;
        private Button button1;
        private Label label5;
        private LinkLabel linkLabel1;
        private Button backButton;
    }
}
