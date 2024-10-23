using Firebase.Database;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models;
using Firebase.Database.Query;

namespace Quiz_Management_System
{
    public partial class RegisterForm : Form
    {
        private static readonly FirebaseClient firebaseClient;

        static RegisterForm()
        {
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private async void RegBtn_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            var teacher = CreateTeacherFromInput();
            await SaveTeacherToFirebase(teacher);

            ShowMessage("Registration successful!");
            Close(); // Optionally close the registration form
        }

        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(NameTxt.Text) &&
            !string.IsNullOrWhiteSpace(SurnameTxt.Text) &&
            !string.IsNullOrWhiteSpace(NumTxt.Text) &&
            !string.IsNullOrWhiteSpace(EmailTxt.Text) &&
            !string.IsNullOrWhiteSpace(PassTxt.Text);

        private Teacher CreateTeacherFromInput() => new Teacher
        {
            Name = NameTxt.Text,
            Surname = SurnameTxt.Text,
            Number = NumTxt.Text,
            Email = EmailTxt.Text,
            Password = PassTxt.Text
        };

        private async Task SaveTeacherToFirebase(Teacher teacher)
        {
            await firebaseClient
                .Child("Teachers")
                .PostAsync(teacher);
        }

        private void ShowMessage(string message) =>
            MessageBox.Show(message);
    }
}
