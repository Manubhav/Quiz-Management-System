namespace Quiz_Management_System
{
    public partial class Form1 : Form
    {
        // Constructor to initialize the form and its components
        public Form1()
        {
            InitializeComponent();
        }

        // Event handler for the "Teacher Login" button click
        private void button1_Click(object sender, EventArgs e)
        {
            // Opens the TeacherLogin form
            OpenForm<TeacherLogin>();
        }

        // Event handler for the "Student Login" button click
        private void button2_Click(object sender, EventArgs e)
        {
            // Opens the Student Login form
            OpenForm<StuLog>();
        }

        // Event handler for the form load event
        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code can go here if needed (currently empty)
        }

        // Generic method to open a form of type T
        private void OpenForm<T>() where T : Form, new()
        {
            // Create a new instance of the specified form type T
            using (T form = new T())
            {
                // Show the form as a modal dialog
                form.ShowDialog();
            }
        }
    }
}
