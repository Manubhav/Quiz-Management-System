using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models; // Ensure you're referencing the correct namespace

namespace Quiz_Management_System
{
    public partial class uStuReg : UserControl
    {
        private static FirebaseClient firebaseClient;

        public uStuReg()
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

            var group = new Group
            {
                Group_no = textBox1.Text,
                Specialty = textBox2.Text
            };

            // Save to Firebase
            await firebaseClient
                .Child("Groups")
                .PostAsync(group);

            MessageBox.Show("Group registration successful!");
        }

        bool Authenticate()
        {
            return !(string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text));
        }

        private async Task LoadData()
        {
            var groups = await firebaseClient
                .Child("Groups")
                .OnceAsync<Group>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Group_no");
            dt.Columns.Add("Specialty");

            foreach (var group in groups)
            {
                dt.Rows.Add(group.Object.Group_no, group.Object.Specialty);
            }

            groupsDGV.DataSource = dt;
        }

        private async Task LoadDataStu()
        {
            var students = await firebaseClient
                .Child("Students")
                .OnceAsync<Student>();

            var groups = await firebaseClient
                .Child("Groups")
                .OnceAsync<Group>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Group_no");
            dt.Columns.Add("Name");
            dt.Columns.Add("Surname");
            dt.Columns.Add("Age");
            dt.Columns.Add("Email");
            dt.Columns.Add("Password");
            dt.Columns.Add("Specialty");

            foreach (var student in students)
            {
                var group = groups.FirstOrDefault(g => g.Key == student.Object.Group_id);
                dt.Rows.Add(
                    group != null ? group.Object.Group_no : "",
                    student.Object.Name,
                    student.Object.Surname,
                    student.Object.Age,
                    student.Object.Email,
                    student.Object.Password,
                    group != null ? group.Object.Specialty : ""
                );
            }

            newstuDGV.DataSource = dt;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void uStuReg_Load(object sender, EventArgs e)
        {
            await LoadData();
            await LoadDataStu();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                Group_id = textBox3.Text,
                Name = textBox4.Text,
                Surname = textBox5.Text,
                Age = int.Parse(textBox6.Text), // Ensure proper type conversion
                Email = textBox7.Text,
                Password = textBox8.Text
            };

            // Save to Firebase
            await firebaseClient
                .Child("Students")
                .PostAsync(student);

            MessageBox.Show("Student registration successful!");
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadDataStu();
        }
    }

    public class Student
    {
        public string Group_id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
