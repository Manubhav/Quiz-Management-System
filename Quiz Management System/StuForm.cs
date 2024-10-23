namespace Quiz_Management_System
{
    public partial class StuForm : Form
    {
        public StuForm()
        {
            InitializeComponent();
        }

        private void LectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(uLecture1);
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(uQuiz1);
        }

        private void ShowControl(Control controlToShow)
        {
            // Hide both controls first
            uLecture1.Visible = false;
            uQuiz1.Visible = false;

            // Show the specified control
            controlToShow.Visible = true;
        }

        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Close the current form or navigate back to the login screen
                this.Close(); // This closes the current form.
            }
        }
    }
}
