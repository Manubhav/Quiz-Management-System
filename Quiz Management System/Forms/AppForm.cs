namespace Quiz_Management_System
{
    public partial class AppForm : Form
    {
        // Constructor to initialize the form and its components
        public AppForm()
        {
            InitializeComponent();
        }

        // Event handler for the Close button click
        private void button6_Click(object sender, EventArgs e)
        {
            // Closes the application form
            Close();
        }

        // Event handler for the form load event
        private void AppForm_Load(object sender, EventArgs e)
        {
            // Hides all controls when the form loads
            HideAllControls();
        }

        // Event handler for the "Add New Question" button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Shows the control for adding a new question
            ShowControl(uC_AddNewQuestion1);
        }

        // Event handler for the "New Subject" button click
        private void button7_Click(object sender, EventArgs e)
        {
            // Shows the control for creating a new subject
            ShowControl(newSubject1);
        }

        // Event handler for the "Update" button click
        private void button2_Click(object sender, EventArgs e)
        {
            // Shows the control for updating an existing question
            ShowControl(uUpdate1);
        }

        // Event handler for the "Delete" button click
        private void button3_Click(object sender, EventArgs e)
        {
            // Shows the control for deleting a question
            ShowControl(udelete1);
        }

        // Event handler for the "Student Registration" button click
        private void button4_Click(object sender, EventArgs e)
        {
            // Shows the control for student registration
            ShowControl(uStuReg1);
        }

        // Event handler for the "Student Results" button click
        private void button5_Click(object sender, EventArgs e)
        {
            // Shows the control for viewing student results
            ShowControl(ustuResults1);
        }

        // Method to hide all controls in the form
        private void HideAllControls()
        {
            // Set visibility of all user controls to false
            uC_AddNewQuestion1.Visible = false;
            newSubject1.Visible = false;
            udelete1.Visible = false;
            uUpdate1.Visible = false;
            uStuReg1.Visible = false;
            ustuResults1.Visible = false;
        }

        // Method to show a specific control and hide others
        private void ShowControl(Control controlToShow)
        {
            // Hide all controls before showing the selected one
            HideAllControls();
            // Set the selected control's visibility to true
            controlToShow.Visible = true;
        }
    }
}
