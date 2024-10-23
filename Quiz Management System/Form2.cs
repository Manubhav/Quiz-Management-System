using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class Form2 : Form
    {
        private static readonly FirebaseClient firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");

        public Form2()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            var teacher = await GetTeacherByEmail(EmailLog.Text);
            if (teacher == null)
            {
                ShowMessage("User does not exist!");
                return;
            }

            if (!IsPasswordValid(teacher.Password, PassLog.Text))
            {
                ShowMessage("Invalid password!");
                return;
            }

            // Proceed to next form
            Hide();
            using (var app = new AppForm())
            {
                app.ShowDialog();
            }
            Close();
        }

        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(EmailLog.Text) && !string.IsNullOrWhiteSpace(PassLog.Text);

        private async Task<Teacher> GetTeacherByEmail(string email)
        {
            var teachers = await firebaseClient.Child("Teachers").OrderByKey().OnceAsync<Teacher>();
            return teachers.FirstOrDefault(t => t.Object.Email == email)?.Object;
        }

        private bool IsPasswordValid(string storedPassword, string enteredPassword) =>
            storedPassword == enteredPassword;

        private void ShowMessage(string message) =>
            MessageBox.Show(message);

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (var register = new RegisterForm())
            {
                register.ShowDialog();
            }
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
