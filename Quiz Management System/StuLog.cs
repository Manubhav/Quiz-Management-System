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
        private static FirebaseClient firebaseClient;

        public StuLog()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
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

        private bool Authenticate()
        {
            if (string.IsNullOrWhiteSpace(EmailLog.Text) || string.IsNullOrWhiteSpace(PassLog.Text))
                return false;

            return true;
        }
    }
}
