using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class RegisterForm : Form
    {
        private static FirebaseClient firebaseClient;

        public RegisterForm()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void RegBtn_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            // Create a new teacher object
            var teacher = new Teacher
            {
                Name = NameTxt.Text,
                Surname = SurnameTxt.Text,
                Number = NumTxt.Text,
                Email = EmailTxt.Text,
                Password = PassTxt.Text
            };

            // Save to Firebase
            await firebaseClient
                .Child("Teachers")
                .PostAsync(teacher);

            MessageBox.Show("Registration successful!");
            Close(); // Optionally close the registration form
        }

        private bool Authenticate()
        {
            if (string.IsNullOrWhiteSpace(NameTxt.Text) ||
                string.IsNullOrWhiteSpace(SurnameTxt.Text) ||
                string.IsNullOrWhiteSpace(NumTxt.Text) ||
                string.IsNullOrWhiteSpace(EmailTxt.Text) ||
                string.IsNullOrWhiteSpace(PassTxt.Text))
            {
                return false;
            }
            return true;
        }
    }
}
