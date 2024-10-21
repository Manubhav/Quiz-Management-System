using System.Data;
using System.Diagnostics;
using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uLecture : UserControl
    {
        private readonly FirebaseClient firebaseClient;

        public uLecture()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async Task LoadData()
        {
            // Load lecture data from Firebase
            var lectures = await firebaseClient
                .Child("Lectures")
                .OnceAsync<Lecture>();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");

            foreach (var lecture in lectures)
            {
                // Fetch the subject using the Subject_id stored in the lecture
                var subject = await firebaseClient
                    .Child("Subjects")
                    .OrderByKey()
                    .EqualTo(lecture.Object.Subject_id)
                    .OnceSingleAsync<Subject>();

                // Check if subject is found
                string subjectName = subject?.Name ?? "Unknown"; // Default to "Unknown" if subject is null

                dataTable.Rows.Add(lecture.Key, lecture.Object.FileName, lecture.Object.Extension, subjectName);
            }

            dataGridView1.DataSource = dataTable;
        }


        private async Task ComboLoad()
        {
            comboBox1.Items.Clear();
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<Subject>();

            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        private async Task FilterCombo()
        {
            var filteredLectures = await firebaseClient
                .Child("Lectures")
                .OnceAsync<Lecture>();

            var filteredData = new List<Lecture>();
            foreach (var lecture in filteredLectures)
            {
                // Get the subject based on Subject_id from Lecture
                var subject = await firebaseClient
                    .Child("Subjects")
                    .Child(lecture.Object.Subject_id)
                    .OnceSingleAsync<Subject>();

                // Check if subject is found
                if (subject != null && subject.Name.Contains(comboBox1.Text, StringComparison.OrdinalIgnoreCase))
                {
                    filteredData.Add(lecture.Object);
                }
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");

            foreach (var lecture in filteredData)
            {
                var subject = await firebaseClient
                    .Child("Subjects")
                    .Child(lecture.Subject_id) 
                    .OnceSingleAsync<Subject>();

                // Check if subject is found
                string subjectName = subject?.Name ?? "Unknown"; // Default to "Unknown" if subject is null

                dataTable.Rows.Add(lecture.Key, lecture.Object.FileName, lecture.Object.Extension, subjectName);
            }

            dataGridView1.DataSource = dataTable;
        }


        private async void uLecture_Load(object sender, EventArgs e)
        {
            await ComboLoad();
            await LoadData();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await FilterCombo();
        }

        private async Task OpenFile(string id)
        {
            // Fetch the lecture data from Firebase
            var lecture = await firebaseClient
                .Child("Lecture")
                .Child(id)
                .OnceSingleAsync<Lecture>();

            // Check if the lecture was found
            if (lecture != null)
            {
                // Construct the full path for the temporary file
                var newFileName = Path.Combine(Path.GetTempPath(), lecture.FileName);

                // Write the byte data to the file
                await File.WriteAllBytesAsync(newFileName, lecture.Data);

                // Start the process to open the file
                Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true });
            }
            else
            {
                MessageBox.Show("Lecture not found.");
            }
        }


        private async void button2_Click(object sender, EventArgs e)
        {
            var selectedRows = dataGridView1.SelectedRows;
            foreach (DataGridViewRow row in selectedRows)
            {
                string id = row.Cells[0].Value.ToString();
                await OpenFile(id);
            }
        }
    }
}
