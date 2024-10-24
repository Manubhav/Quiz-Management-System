using Firebase.Database;
using System.Data;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class UstuResults : UserControl, ISubjectManagement
    {
        private static readonly FirebaseClient firebaseClient; // Firebase client for database operations

        static UstuResults()
        {
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app"); // Set up Firebase client
        }

        public UstuResults()
        {
            InitializeComponent(); // Initialize form components
        }

        // Implementation of LoadSubjectsAsync from ISubjectManagement
        public async Task<List<string>> LoadSubjectsAsync()
        {
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>(); // Fetch subjects from Firebase
            return subjects.Select(s => s.Object.Name).ToList(); // Return list of subject names
        }

        // Event handler for form load; populates the subject and group comboboxes and loads data
        private async void UstuResults_Load(object sender, EventArgs e)
        {
            var subjectList = await LoadSubjectsAsync(); // Load subjects
            comboBox1.Items.AddRange(subjectList.ToArray()); // Populate subject combobox
            await ComboLoad(comboBox2, "Groups"); // Load groups into combobox
            await LoadDataAsync(); // Load initial data into DataGridView
        }

        // Asynchronously fetches results based on optional subject and group filters
        public async Task<DataTable> GetResultsAsync(string subject = null, string group = null)
        {
            var scores = await firebaseClient.Child("SubjectStudent").OnceAsync<SubjectStudent>(); // Fetch scores
            var students = await firebaseClient.Child("Students").OnceAsync<Student>(); // Fetch students
            var groups = await firebaseClient.Child("Groups").OnceAsync<Group>(); // Fetch groups
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>(); // Fetch subjects

            var dt = new DataTable(); // Create a new DataTable for results
            dt.Columns.AddRange(new[] // Define columns for the DataTable
            {
                new DataColumn("Name"),
                new DataColumn("Surname"),
                new DataColumn("Group_no"),
                new DataColumn("Subject"),
                new DataColumn("Score")
            });

            // Loop through each score and gather relevant student data
            foreach (var score in scores)
            {
                var student = students.FirstOrDefault(s => s.Key == score.Object.Student_id); // Find corresponding student
                if (student == null) continue; // Skip if student not found

                var groupObj = groups.FirstOrDefault(g => g.Key == student.Object.Group_id); // Find corresponding group
                var subjectObj = subjects.FirstOrDefault(sub => sub.Key == score.Object.Subject_id); // Find corresponding subject

                if (groupObj == null || subjectObj == null) continue; // Skip if group or subject not found

                // Check if subject and group filters are satisfied
                if ((string.IsNullOrWhiteSpace(subject) || subjectObj.Object.Name.Contains(subject)) &&
                    (string.IsNullOrWhiteSpace(group) || groupObj.Object.Group_no.Contains(group)))
                {
                    // Add the student's data to the DataTable
                    dt.Rows.Add(student.Object.Name, student.Object.Surname, groupObj.Object.Group_no, subjectObj.Object.Name, score.Object.Score);
                }
            }

            return dt; // Return the populated DataTable
        }

        // Asynchronously loads data into the DataGridView
        private async Task LoadDataAsync()
        {
            resultDGV.DataSource = await GetResultsAsync(); // Set DataSource of DataGridView to the results
        }

        // Filters the DataGridView based on the selected subject
        private async void FilterCombo()
        {
            resultDGV.DataSource = await GetResultsAsync(subject: comboBox1.Text); // Update DataGridView with filtered results
        }

        // Filters the DataGridView based on the selected subject and group
        private async void FilterComboGroup()
        {
            resultDGV.DataSource = await GetResultsAsync(subject: comboBox1.Text, group: comboBox2.Text); // Update DataGridView with filtered results
        }

        // Loads items into a ComboBox from Firebase based on the specified path
        private async Task ComboLoad(ComboBox comboBox, string path)
        {
            comboBox.Items.Clear(); // Clear existing items
            var items = await firebaseClient.Child(path).OnceAsync<dynamic>(); // Fetch items from Firebase
            foreach (var item in items)
            {
                comboBox.Items.Add(item.Object.Name ?? item.Object.Group_no); // Add items to the ComboBox
            }
        }

        // Event handler for the first filter button click
        private void button1_Click(object sender, EventArgs e) => FilterCombo();

        // Event handler for the second filter button click
        private void button2_Click(object sender, EventArgs e) => FilterComboGroup();

        // Event handler for the third button click to reload data
        private async void button3_Click(object sender, EventArgs e) => await LoadDataAsync();
    }
}
