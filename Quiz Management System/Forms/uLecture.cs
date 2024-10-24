using System.Data;
using System.Diagnostics;
using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uLecture : UserControl
    {
        private readonly FirebaseClient firebaseClient; // Firebase client for database operations
        private Dictionary<string, string> subjectDictionary; // Dictionary to hold subject IDs and names
        private DataTable lectureDataTable; // DataTable to store lecture data for display

        // Constructor for the uLecture user control
        public uLecture()
        {
            InitializeComponent(); // Initialize the user control components
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Asynchronously initialize data by loading subjects and lectures
        private async Task InitializeDataAsync()
        {
            subjectDictionary = await LoadSubjectsAsync(); // Load subjects into the dictionary
            lectureDataTable = CreateLecturesDataTable(); // Create an empty DataTable for lectures
            await LoadLecturesAsync(); // Load lectures from Firebase
        }

        // Asynchronously load lectures from Firebase into the DataTable
        private async Task LoadLecturesAsync()
        {
            var lectures = await firebaseClient.Child("Lectures").OnceAsync<Lecture>(); // Fetch lectures from Firebase
            foreach (var lecture in lectures)
            {
                // Get subject name from the dictionary or default to "Unknown"
                string subjectValue = subjectDictionary.TryGetValue(lecture.Object.Subject_name, out var name) ? name : "Unknown";
                // Add lecture data to the DataTable
                lectureDataTable.Rows.Add(lecture.Key, lecture.Object.FileName, lecture.Object.Extension, subjectValue);
            }
            dataGridView1.DataSource = lectureDataTable; // Bind the DataTable to the DataGridView
        }

        // Asynchronously load subjects from Firebase and return a dictionary
        private async Task<Dictionary<string, string>> LoadSubjectsAsync()
        {
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>(); // Fetch subjects from Firebase
            // Convert subjects into a dictionary of ID and Name
            return subjects.ToDictionary(subject => subject.Key, subject => subject.Object.Name);
        }

        // Create and return a new DataTable structure for lectures
        private DataTable CreateLecturesDataTable()
        {
            DataTable dataTable = new DataTable(); // Create a new DataTable instance
            // Define columns for the DataTable
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");
            return dataTable;
        }

        // Event handler for user control load event
        private async void uLecture_Load(object sender, EventArgs e)
        {
            await InitializeDataAsync(); // Initialize data when the control loads
            LoadComboBoxData(); // Load subjects into the ComboBox for filtering
        }

        // Load subjects into the ComboBox for filtering lectures
        private void LoadComboBoxData()
        {
            comboBox1.Items.Clear(); // Clear existing items in the ComboBox
            // Add each subject name from the dictionary to the ComboBox
            foreach (var subject in subjectDictionary.Values)
            {
                comboBox1.Items.Add(subject);
            }
        }

        // Event handler for filtering lectures based on the selected subject
        private async void button1_Click(object sender, EventArgs e)
        {
            var filteredData = FilterLectures(comboBox1.Text); // Filter lectures based on the selected subject
            dataGridView1.DataSource = filteredData; // Bind the filtered data to the DataGridView
        }

        // Filter the lectures in the DataTable based on the subject filter
        private DataTable FilterLectures(string filter)
        {
            var filteredTable = CreateLecturesDataTable(); // Create a new DataTable for filtered results
            // Iterate through each row in the original DataTable
            foreach (DataRow row in lectureDataTable.Rows)
            {
                string subjectValue = row["Subject"].ToString(); // Get the subject value from the row
                // Check if the subject matches the filter (case-insensitive)
                if (subjectValue.Contains(filter, StringComparison.OrdinalIgnoreCase))
                {
                    filteredTable.Rows.Add(row.ItemArray); // Add the matching row to the filtered DataTable
                }
            }
            return filteredTable; // Return the filtered DataTable
        }

        // Asynchronously open the selected lecture file
        private async Task OpenFileAsync(string id)
        {
            // Fetch the specific lecture by ID from Firebase
            var lecture = await firebaseClient.Child("Lectures").Child(id).OnceSingleAsync<Lecture>();
            if (lecture != null)
            {
                string newFileName = Path.Combine(Path.GetTempPath(), lecture.FileName); // Create a temporary file path
                try
                {
                    // Write the lecture data to a temporary file
                    await File.WriteAllBytesAsync(newFileName, lecture.Data);
                    Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true }); // Open the file
                }
                catch (Exception ex)
                {
                    // Handle any exceptions and show an error message
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Lecture not found."); // Show message if lecture is not found
            }
        }

        // Event handler for opening the selected lecture file
        private async void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows) // Iterate through selected rows in the DataGridView
            {
                string id = row.Cells[0].Value.ToString(); // Get the lecture ID from the selected row
                await OpenFileAsync(id); // Open the lecture file asynchronously
            }
        }
    }
}
