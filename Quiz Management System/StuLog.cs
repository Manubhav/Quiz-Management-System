using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class StuLog : Form
    {
        private static readonly FirebaseClient firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");

        public StuLog()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.studentEmail = EmailLog.Text;
            Properties.Settings.Default.Save();

            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            var student = await GetStudentByEmail(EmailLog.Text);
            if (student == null)
            {
                ShowMessage("User does not exist!");
                return;
            }

            if (!IsPasswordValid(student.Password, PassLog.Text))
            {
                ShowMessage("Invalid password!");
                return;
            }

            // Proceed to next form
            Hide();
            using (var stuApp = new StuForm())
            {
                stuApp.ShowDialog();
            }
            Close();
        }

        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(EmailLog.Text) && !string.IsNullOrWhiteSpace(PassLog.Text);

        private async Task<Student> GetStudentByEmail(string email)
        {
            var students = await firebaseClient.Child("Students").OrderByKey().OnceAsync<Student>();
            return students.FirstOrDefault(s => s.Object.Email == email)?.Object;
        }

        private bool IsPasswordValid(string storedPassword, string enteredPassword) =>
            storedPassword == enteredPassword;

        private void ShowMessage(string message) =>
            MessageBox.Show(message);
    }
}
