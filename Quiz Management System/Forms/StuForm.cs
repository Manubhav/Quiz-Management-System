namespace Quiz_Management_System
{
    public partial class StuForm : Form
    {
        // Constructor for the Student Form
        public StuForm()
        {
            InitializeComponent(); // Initialize the components in the form
        }

        // Event handler for the Lecture menu item click
        private void LectureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(uLecture1); // Show the lecture control when the menu item is clicked
        }

        // Event handler for the Test menu item click
        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowControl(uQuiz1); // Show the quiz control when the menu item is clicked
        }

        // Method to show a specified control and hide others
        private void ShowControl(Control controlToShow)
        {
            // Hide both controls first to avoid displaying multiple controls at once
            uLecture1.Visible = false;
            uQuiz1.Visible = false;

            // Show the specified control
            controlToShow.Visible = true;
        }

        // Event handler for the Logout menu item click
        private void logoutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Show a confirmation dialog before logging out
            DialogResult result = MessageBox.Show("Are you sure you want to logout?", "Logout", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                // Close the current form or navigate back to the login screen
                this.Close(); // This closes the current form, effectively logging out the user
            }
        }
    }
}
