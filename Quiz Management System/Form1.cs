namespace Quiz_Management_System
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenForm<Form2>();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenForm<StuLog>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Initialization code can go here if needed.
        }

        private void OpenForm<T>() where T : Form, new()
        {
            using (T form = new T())
            {
                form.ShowDialog();
            }
        }
    }
}
