namespace Quiz_Management_System
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.pictureBoxLogo = new System.Windows.Forms.PictureBox();

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).BeginInit();
            this.SuspendLayout();

            // 
            // pictureBox2 (Background Image)
            // 
            this.pictureBox2.Image = global::Quiz_Management_System.Properties.Resources.UTS_bg;
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Fill;  // Full form background
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;

            // 
            // pictureBoxLogo (Logo)
            // 
            this.pictureBoxLogo.Image = global::Quiz_Management_System.Properties.Resources.UTS_Logo;
            this.pictureBoxLogo.Size = new System.Drawing.Size(150, 100);
            this.pictureBoxLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBoxLogo.BackColor = Color.Transparent;
            this.pictureBoxLogo.TabStop = false;
            this.pictureBoxLogo.Location = new System.Drawing.Point((this.ClientSize.Width - this.pictureBoxLogo.Width) / 2, 50); // Center logo horizontally

            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button1.ForeColor = System.Drawing.Color.Navy;
            this.button1.Size = new System.Drawing.Size(179, 40);
            this.button1.Text = "I am a teacher";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Location = new System.Drawing.Point((this.ClientSize.Width - 179) / 2, 250); // Center button horizontally
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.Paint += new System.Windows.Forms.PaintEventHandler(this.Button_Paint); // Rounded corners

            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Corbel", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.label1.ForeColor = System.Drawing.Color.Navy;
            this.label1.Text = "Smart Learning System";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label1.Location = new System.Drawing.Point((this.ClientSize.Width - this.label1.Width) / 2, 180); // Center label horizontally

            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Corbel", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.button2.ForeColor = System.Drawing.Color.Navy;
            this.button2.Size = new System.Drawing.Size(179, 40);
            this.button2.Text = "I am a student";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Location = new System.Drawing.Point((this.ClientSize.Width - 179) / 2, 300); // Center button horizontally
            this.button2.Click += new System.EventHandler(this.button2_Click);
            this.button2.Paint += new System.Windows.Forms.PaintEventHandler(this.Button_Paint); // Rounded corners

            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 445);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBoxLogo);
            this.Controls.Add(this.pictureBox2); // Ensure the background is added last
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";

            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLogo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBoxLogo;
        private Button button1;
        private Label label1;
        private Button button2;

        // Rounded corners for buttons
        private void Button_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddEllipse(0, 0, button.Width, button.Height);
            button.Region = new System.Drawing.Region(path);
        }
    }
}
