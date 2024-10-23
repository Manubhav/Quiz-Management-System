using Firebase.Database;
using System.Data;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class UstuResults : UserControl
    {
        private static readonly FirebaseClient firebaseClient;

        static UstuResults()
        {
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        public UstuResults()
        {
            InitializeComponent();
        }

        public async Task<DataTable> GetResultsAsync(string subject = null, string group = null)
        {
            var scores = await firebaseClient.Child("SubjectStudent").OnceAsync<SubjectStudent>();
            var students = await firebaseClient.Child("Students").OnceAsync<Student>();
            var groups = await firebaseClient.Child("Groups").OnceAsync<Group>();
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>();

            var dt = new DataTable();
            dt.Columns.AddRange(new[]
            {
                new DataColumn("Name"),
                new DataColumn("Surname"),
                new DataColumn("Group_no"),
                new DataColumn("Subject"),
                new DataColumn("Score")
            });

            foreach (var score in scores)
            {
                var student = students.FirstOrDefault(s => s.Key == score.Object.Student_id);
                if (student == null) continue;

                var groupObj = groups.FirstOrDefault(g => g.Key == student.Object.Group_id);
                var subjectObj = subjects.FirstOrDefault(sub => sub.Key == score.Object.Subject_id);

                if (groupObj == null || subjectObj == null) continue;

                if ((string.IsNullOrWhiteSpace(subject) || subjectObj.Object.Name.Contains(subject)) &&
                    (string.IsNullOrWhiteSpace(group) || groupObj.Object.Group_no.Contains(group)))
                {
                    dt.Rows.Add(student.Object.Name, student.Object.Surname, groupObj.Object.Group_no, subjectObj.Object.Name, score.Object.Score);
                }
            }

            return dt;
        }

        private async Task LoadDataAsync()
        {
            resultDGV.DataSource = await GetResultsAsync();
        }

        private async void FilterCombo()
        {
            resultDGV.DataSource = await GetResultsAsync(subject: comboBox1.Text);
        }

        private async void FilterComboGroup()
        {
            resultDGV.DataSource = await GetResultsAsync(subject: comboBox1.Text, group: comboBox2.Text);
        }

        private async Task ComboLoad(ComboBox comboBox, string path)
        {
            comboBox.Items.Clear();
            var items = await firebaseClient.Child(path).OnceAsync<dynamic>();
            foreach (var item in items)
            {
                comboBox.Items.Add(item.Object.Name ?? item.Object.Group_no);
            }
        }

        private async void UstuResults_Load(object sender, EventArgs e)
        {
            await ComboLoad(comboBox1, "Subjects");
            await ComboLoad(comboBox2, "Groups");
            await LoadDataAsync();
        }

        private void button1_Click(object sender, EventArgs e) => FilterCombo();

        private void button2_Click(object sender, EventArgs e) => FilterComboGroup();

        private async void button3_Click(object sender, EventArgs e) => await LoadDataAsync();
    }
}
