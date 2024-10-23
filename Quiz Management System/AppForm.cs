namespace Quiz_Management_System
{
    public partial class AppForm : Form
    {
        public AppForm()
        {
            InitializeComponent();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AppForm_Load(object sender, EventArgs e)
        {
            HideAllControls();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowControl(uC_AddNewQuestion1);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ShowControl(newSubject1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowControl(uUpdate1);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowControl(udelete1);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ShowControl(uStuReg1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ShowControl(ustuResults1);
        }

        private void HideAllControls()
        {
            uC_AddNewQuestion1.Visible = false;
            newSubject1.Visible = false;
            udelete1.Visible = false;
            uUpdate1.Visible = false;
            uStuReg1.Visible = false;
            ustuResults1.Visible = false;
        }

        private void ShowControl(Control controlToShow)
        {
            HideAllControls();
            controlToShow.Visible = true;
        }
    }
}
