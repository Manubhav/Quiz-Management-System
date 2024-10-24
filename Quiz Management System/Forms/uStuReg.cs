using Firebase.Database;
using System.Data;
using Quiz_Management_System.Models;
using Firebase.Database.Query;

namespace Quiz_Management_System
{
    public partial class uStuReg : UserControl
    {
        private static FirebaseClient firebaseClient; // Firebase client for database operations

        public uStuReg()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app"); // Set up Firebase client
        }

        // Event handler for group registration button click
        private async void button1_Click(object sender, EventArgs e)
        {
            // Check for empty textboxes
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Don't keep any textbox blank!"); // Show error if any textbox is empty
                return;
            }

            // Create a new group object
            var group = new Group
            {
                Group_no = textBox1.Text, // Group number from textbox
                Specialty = textBox2.Text // Specialty from textbox
            };

            // Save the group to Firebase
            await firebaseClient.Child("Groups").PostAsync(group);
            MessageBox.Show("Group registration successful!"); // Show success message
        }

        // Asynchronously loads data into a DataGridView from Firebase
        private async Task LoadData<T>(string childName, DataGridView dgv, Func<T, Task<string[]>> rowMapper) where T : class
        {
            var items = await firebaseClient.Child(childName).OnceAsync<T>(); // Fetch data from Firebase
            var dt = new DataTable(); // Create a new DataTable

            // Check if there are any items to load
            if (!items.Any())
            {
                dgv.DataSource = null; // Set DataSource to null if no items
                return;
            }

            // Use the first item to infer columns dynamically
            var firstItem = items.FirstOrDefault()?.Object; // Get the first item
            if (firstItem != null)
            {
                var sampleRow = await rowMapper(firstItem); // Map the first item to a row

                // Generate columns based on the first row of data
                foreach (var property in sampleRow)
                {
                    dt.Columns.Add(property); // Add columns for each property
                }
            }
            else
            {
                throw new InvalidOperationException("No data available to generate columns."); // Throw error if no data
            }

            // Add rows to the DataTable
            foreach (var item in items)
            {
                if (item.Object != null)
                {
                    dt.Rows.Add(await rowMapper(item.Object)); // Add mapped rows to DataTable
                }
            }

            dgv.DataSource = dt; // Set DataSource of DataGridView to the populated DataTable
        }

        // Event handler for loading groups into the DataGridView when button2 is clicked
        private async void button2_Click(object sender, EventArgs e)
        {
            await LoadData<Group>("Groups", groupsDGV, async g => new[] { g.Group_no, g.Specialty }); // Load group data
        }

        // Event handler for form load; populates DataGridViews with groups and students
        private async void uStuReg_Load(object sender, EventArgs e)
        {
            await LoadData<Group>("Groups", groupsDGV, async g => new[] { g.Group_no, g.Specialty }); // Load groups into DataGridView
            await LoadData<Student>("Students", newstuDGV, async s => new[] // Load students into DataGridView
            {
                await GetGroupNo(s.Group_id), // Fetch group number based on group ID
                s.Name, // Student name
                s.Surname, // Student surname
                s.Age.ToString(), // Student age
                s.Email, // Student email
                s.Password // Student password
            });
        }

        // Asynchronously gets the group number based on the group ID
        private async Task<string> GetGroupNo(string groupId)
        {
            var groups = await firebaseClient.Child("Groups").OnceAsync<Group>(); // Fetch groups from Firebase
            return groups.FirstOrDefault(g => g.Key == groupId)?.Object.Group_no ?? ""; // Return group number or empty string if not found
        }

        // Event handler for student registration button click
        private async void button3_Click(object sender, EventArgs e)
        {
            // Create a new student object
            var student = new Student
            {
                Group_id = textBox3.Text, // Group ID from textbox
                Name = textBox4.Text, // Name from textbox
                Surname = textBox5.Text, // Surname from textbox
                Age = int.Parse(textBox6.Text), // Age from textbox
                Email = textBox7.Text, // Email from textbox
                Password = textBox8.Text // Password from textbox
            };

            // Save the student to Firebase
            await firebaseClient.Child("Students").PostAsync(student);
            MessageBox.Show("Student registration successful!"); // Show success message
        }

        // Event handler for loading students into the DataGridView when button4 is clicked
        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadData<Student>("Students", newstuDGV, async s => new[] // Load student data
            {
                await GetGroupNo(s.Group_id), // Fetch group number
                s.Name, // Student name
                s.Surname, // Student surname
                s.Age.ToString(), // Student age
                s.Email, // Student email
                s.Password // Student password
            });
        }
    }
}
