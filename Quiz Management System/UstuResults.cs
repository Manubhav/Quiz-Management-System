using Firebase.Database;
using Firebase.Database.Query;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using System;
using System.Data;
using Quiz_Management_System.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz_Management_System
{
    public partial class UstuResults : UserControl
    {
        private static FirebaseClient firebaseClient;

        public UstuResults()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async Task<DataTable> GetResultsAsync(string subject = null, string group = null)
        {
            // Fetch results from Firebase
            var scores = await firebaseClient
                .Child("SubjectStudent") // Replace with your actual Firebase path
                .OnceAsync<SubjectStudent>();

            var students = await firebaseClient
                .Child("Students") // Replace with your actual Firebase path
                .OnceAsync<Student>();

            var groups = await firebaseClient
                .Child("Groups") // Replace with your actual Firebase path
                .OnceAsync<Group>();

            var subjects = await firebaseClient
                .Child("Subjects") // Replace with your actual Firebase path
                .OnceAsync<Subject>();

            DataTable dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Surname");
            dt.Columns.Add("Group_no");
            dt.Columns.Add("Subject");
            dt.Columns.Add("Score");

            // Loop through the data and filter if necessary
            foreach (var score in scores)
            {
                var student = students.FirstOrDefault(s => s.Key == score.Object.Student_id);
                var groupObj = groups.FirstOrDefault(g => g.Key == student.Object.Group_id);
                var subjectObj = subjects.FirstOrDefault(sub => sub.Key == score.Object.Subject_id);

                if (student != null && groupObj != null && subjectObj != null)
                {
                    if ((subject == null || subjectObj.Object.Name.Contains(subject)) &&
                        (group == null || groupObj.Object.Group_no.Contains(group)))
                    {
                        dt.Rows.Add(student.Object.Name, student.Object.Surname, groupObj.Object.Group_no, subjectObj.Object.Name, score.Object.Score);
                    }
                }
            }

            return dt;
        }

        private async Task LoadData()
        {
            DataTable dt = await GetResultsAsync();
            resultDGV.DataSource = dt;
        }

        private async void FilterCombo()
        {
            string selectedSubject = comboBox1.Text;
            DataTable dt = await GetResultsAsync(subject: selectedSubject);
            resultDGV.DataSource = dt;
        }

        private async void FilterComboGroup()
        {
            string selectedGroup = comboBox2.Text;
            string selectedSubject = comboBox1.Text;
            DataTable dt = await GetResultsAsync(subject: selectedSubject, group: selectedGroup);
            resultDGV.DataSource = dt;
        }

        private async Task ComboLoad()
        {
            comboBox1.Items.Clear();
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>();
            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        private async Task ComboLoadGroup()
        {
            comboBox2.Items.Clear();
            var groups = await firebaseClient.Child("Groups").OnceAsync<Group>();
            foreach (var group in groups)
            {
                comboBox2.Items.Add(group.Object.Group_no);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FilterCombo();
        }

        private async void UstuResults_Load(object sender, EventArgs e)
        {
            await ComboLoad();
            await LoadData();
            await ComboLoadGroup();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FilterComboGroup();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }
    }

    // Model classes for Firebase Data (ensure these match your Firebase structure)
    public class SubjectStudent
    {
        public string Student_id { get; set; }
        public string Subject_id { get; set; }
        public int Score { get; set; }
    }

    
}
