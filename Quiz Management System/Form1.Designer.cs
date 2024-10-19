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
            pictureBox2 = new PictureBox();
            button1 = new Button();
            label1 = new Label();
            button2 = new Button();
            pictureBoxLogo = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).BeginInit();
            SuspendLayout();
            // 
            // pictureBox2
            // 
            pictureBox2.Dock = DockStyle.Fill;
            pictureBox2.Image = Properties.Resources.UTS_bg;
            pictureBox2.Location = new Point(0, 0);
            pictureBox2.Margin = new Padding(3, 4, 3, 4);
            pictureBox2.Name = "pictureBox2";
            pictureBox2.Size = new Size(941, 593);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox2.TabIndex = 1;
            pictureBox2.TabStop = false;
            // 
            // button1
            // 
            button1.FlatStyle = FlatStyle.Flat;
            button1.Font = new Font("Corbel", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button1.ForeColor = Color.Navy;
            button1.Size = new Size(205, 53);
            button1.TabIndex = 2;
            button1.Text = "Teacher";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            button1.Paint += Button_Paint;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Corbel", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            label1.ForeColor = Color.Navy;
            label1.TextAlign = ContentAlignment.MiddleCenter;
            label1.Size = new Size(290, 33);
            label1.TabIndex = 1;
            label1.Text = "Smart Learning System";
            // 
            // button2
            // 
            button2.FlatStyle = FlatStyle.Flat;
            button2.Font = new Font("Corbel", 12F, FontStyle.Bold, GraphicsUnit.Point);
            button2.ForeColor = Color.Navy;
            button2.Size = new Size(205, 53);
            button2.TabIndex = 0;
            button2.Text = "Student";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            button2.Paint += Button_Paint;
            // 
            // pictureBoxLogo
            // 
            pictureBoxLogo.BackColor = Color.Black;
            pictureBoxLogo.Image = Properties.Resources.UTS_Logo;
            pictureBoxLogo.Size = new Size(171, 133);
            pictureBoxLogo.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxLogo.TabIndex = 3;
            pictureBoxLogo.TabStop = false;
            pictureBoxLogo.Paint += PictureBoxLogo_Paint;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(941, 593);
            Controls.Add(button2);
            Controls.Add(label1);
            Controls.Add(button1);
            Controls.Add(pictureBoxLogo);
            Controls.Add(pictureBox2);
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Form1";
            Resize += Form1_Resize; // Add the Resize event handler
            ((System.ComponentModel.ISupportInitialize)pictureBox2).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxLogo).EndInit();
            ResumeLayout(false);
            PerformLayout();

            CenterControls();
        }

        #endregion

        private PictureBox pictureBox2;
        private PictureBox pictureBoxLogo;
        private Button button1;
        private Label label1;
        private Button button2;

        // Handle form resize event
        private void Form1_Resize(object sender, EventArgs e)
        {
            CenterControls();
        }

        // Center the controls on the form
        private void CenterControls()
        {
            // Center logo
            pictureBoxLogo.Location = new Point((ClientSize.Width - pictureBoxLogo.Width) / 2, 67);

            // Center label
            label1.Location = new Point((ClientSize.Width - label1.Width) / 2, 240);

            // Center buttons
            button1.Location = new Point((ClientSize.Width - button1.Width) / 2, 333);
            button2.Location = new Point((ClientSize.Width - button2.Width) / 2, 400);
        }

        // Rounded corners for buttons
        private void Button_Paint(object sender, PaintEventArgs e)
        {
            Button button = sender as Button;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90); // Top-left corner
            path.AddArc(button.Width - 20, 0, 20, 20, 270, 90); // Top-right corner
            path.AddArc(button.Width - 20, button.Height - 20, 20, 20, 0, 90); // Bottom-right corner
            path.AddArc(0, button.Height - 20, 20, 20, 90, 90); // Bottom-left corner
            path.CloseFigure();
            button.Region = new System.Drawing.Region(path);
        }

        // Rounded corners for logo
        private void PictureBoxLogo_Paint(object sender, PaintEventArgs e)
        {
            PictureBox logo = sender as PictureBox;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddArc(0, 0, 20, 20, 180, 90); // Top-left corner
            path.AddArc(logo.Width - 20, 0, 20, 20, 270, 90); // Top-right corner
            path.AddArc(logo.Width - 20, logo.Height - 20, 20, 20, 0, 90); // Bottom-right corner
            path.AddArc(0, logo.Height - 20, 20, 20, 90, 90); // Bottom-left corner
            path.CloseFigure();
            logo.Region = new System.Drawing.Region(path);
        }
    }
}
