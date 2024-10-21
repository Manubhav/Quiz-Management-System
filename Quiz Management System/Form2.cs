using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz_Management_System
{
    public partial class Form2 : Form
    {
        private static FirebaseClient firebaseClient;

        public Form2()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            // Check if the user exists in the Teachers collection
            var teachers = await firebaseClient
                .Child("Teachers")
                .OrderByKey()
                .OnceAsync<Teacher>();

            var teacher = teachers.FirstOrDefault(t => t.Object.Email == EmailLog.Text);

            if (teacher == null)
            {
                MessageBox.Show("User does not exist!");
                return;
            }

            if (teacher.Object.Password != PassLog.Text)
            {
                MessageBox.Show("Invalid password!");
                return;
            }

            // Proceed to next form
            Hide();
            AppForm app = new AppForm();
            app.ShowDialog();
            Close();
        }

        private bool Authenticate()
        {
            if (string.IsNullOrWhiteSpace(EmailLog.Text) || string.IsNullOrWhiteSpace(PassLog.Text))
                return false;

            return true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisterForm register = new();
            register.ShowDialog();
        }
    }
}
