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
            // Load all subjects from Firebase and cache them in a dictionary
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<Subject>();

            // Create a dictionary to store subject data for quick lookup
            var subjectDictionary = subjects.ToDictionary(
                subject => subject.Key,
                subject => subject.Object.Name);

            // Load lecture data from Firebase
            var lectures = await firebaseClient
                .Child("Lectures")
                .OnceAsync<Lecture>();

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");

            // Process lectures and match subjects using the dictionary
            foreach (var lecture in lectures)
            {
                // Try to find the subject in the dictionary using the Subject_name
                string subjectName = lecture.Object.Subject_name;
                string subjectValue = "Unknown"; // Default if the subject is not found

                // Manually search for the value in the dictionary
                foreach (var kvp in subjectDictionary)
                {
                    if (kvp.Value.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                    {
                        subjectValue = kvp.Value; // Found a match
                        break;
                    }
                }

                // Add the lecture data to the data table
                dataTable.Rows.Add(lecture.Key, lecture.Object.FileName, lecture.Object.Extension, subjectName);
            }

            // Bind the data to the DataGridView
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
            // Load all subjects from Firebase and cache them in a dictionary
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<Subject>();

            // Create a dictionary to store subject data for quick lookup
            var subjectDictionary = subjects.ToDictionary(
                subject => subject.Key,
                subject => subject.Object.Name);

            // Load all lectures from Firebase
            var filteredLectures = await firebaseClient
                .Child("Lectures")
                .OnceAsync<Lecture>();

            var filteredData = new List<(FirebaseObject<Lecture> Lecture, string SubjectName)>();

            // Process each lecture
            foreach (var lecture in filteredLectures)
            {
                // Try to find the subject in the dictionary using the Subject_name
                string subjectName = lecture.Object.Subject_name;
                string subjectValue = "Unknown"; // Default if the subject is not found

                // Manually search for the value in the dictionary
                foreach (var kvp in subjectDictionary)
                {
                    if (kvp.Value.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                    {
                        subjectValue = kvp.Value; // Found a match
                        break;
                    }
                }
                // Check if the subject matches the filter criteria
                if (subjectValue.Contains(comboBox1.Text, StringComparison.OrdinalIgnoreCase))
                {
                    // Store both the lecture and the subject name to avoid fetching the subject again
                    filteredData.Add((lecture, subjectValue));
                }
            }

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");

            // Populate the data table with filtered data
            foreach (var item in filteredData)
            {
                dataTable.Rows.Add(item.Lecture.Key, item.Lecture.Object.FileName, item.Lecture.Object.Extension, item.SubjectName);
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
                .Child("Lectures")
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
