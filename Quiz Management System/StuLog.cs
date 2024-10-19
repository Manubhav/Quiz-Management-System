using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Firebase.Database;
using Firebase.Database.Query;
using Google.Apis.Auth.OAuth2;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz_Management_System
{
    public partial class StuLog : Form
    {
        private static FirebaseClient firebaseClient;

        public StuLog()
        {
            InitializeComponent();

            // Initialize Firebase
            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.FromFile("F:\\UTS\\Sem-IV\\.NET App Dev\\Quiz_Management_System-master\\Quiz Management System\\smart-learning-system-a2c86-firebase-adminsdk-265q7-82bc2c0986.json"),
            });

            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.studentEmail = EmailLog.Text;
            Properties.Settings.Default.Save();

            if (!Authenticate())
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            // Check if the user exists
            var students = await firebaseClient
                .Child("Students")
                .OrderByKey()
                .OnceAsync<Student>();

            var student = students.FirstOrDefault(s => s.Object.Email == EmailLog.Text);

            if (student == null)
            {
                MessageBox.Show("User does not exist!");
                return;
            }

            if (student.Object.Password != PassLog.Text)
            {
                MessageBox.Show("Invalid password!");
                return;
            }

            // Proceed to next form
            Hide();
            StuForm StuApp = new StuForm();
            StuApp.ShowDialog();
            Close();
        }

        bool Authenticate()
        {
            if (string.IsNullOrWhiteSpace(EmailLog.Text) || string.IsNullOrWhiteSpace(PassLog.Text))
                return false;

            return true;
        }
    }

    public class Student
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
