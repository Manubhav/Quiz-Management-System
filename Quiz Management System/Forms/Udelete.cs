using Firebase.Database;
using Firebase.Database.Query;
using System.Data;
using Quiz_Management_System.Models;
using System.Diagnostics;

namespace Quiz_Management_System
{
    public partial class Udelete : UserControl
    {
        private static FirebaseClient firebaseClient;

        // Constructor for the Udelete user control
        public Udelete()
        {
            InitializeComponent(); // Initialize the user control components
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Fetch data based on Subject ID entered in textBox1
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Query Firebase for questions matching the Subject ID
                var questions = await firebaseClient
                    .Child("Questions")
                    .OrderBy("Subject_id")
                    .EqualTo(int.Parse(textBox1.Text)) // Convert text to int for comparison
                    .OnceAsync<Question>();

                // Create a DataTable to hold the results
                DataTable dtb = new DataTable();
                dtb.Columns.Add("Question_id");
                dtb.Columns.Add("Subject");
                dtb.Columns.Add("Content");
                dtb.Columns.Add("Answer1");
                dtb.Columns.Add("Answer2");
                dtb.Columns.Add("Answer3");
                dtb.Columns.Add("Answer4");
                dtb.Columns.Add("CorrectAnswer");

                // Populate the DataTable with question data
                foreach (var question in questions)
                {
                    DataRow row = dtb.NewRow();
                    row["Question_id"] = question.Object.Id;
                    row["Subject"] = question.Object.Subject;
                    row["Content"] = question.Object.Content;
                    row["Answer1"] = question.Object.Answer1;
                    row["Answer2"] = question.Object.Answer2;
                    row["Answer3"] = question.Object.Answer3;
                    row["Answer4"] = question.Object.Answer4;
                    row["CorrectAnswer"] = question.Object.CorrectAnswer;
                    dtb.Rows.Add(row);
                }

                // Bind the DataTable to the DataGridView
                deleteDGV.DataSource = dtb;
            }
            catch (Exception ex)
            {
                // Handle exceptions by showing an error message
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Load all questions from Firebase
        private async void LoadData()
        {
            // Query Firebase to get all questions
            var questions = await firebaseClient
                .Child("Questions")
                .OnceAsync<Question>();

            // Create a DataTable to hold the results
            DataTable dtb = new DataTable();
            dtb.Columns.Add("Question_id");
            dtb.Columns.Add("Subject");
            dtb.Columns.Add("Content");
            dtb.Columns.Add("Answer1");
            dtb.Columns.Add("Answer2");
            dtb.Columns.Add("Answer3");
            dtb.Columns.Add("Answer4");
            dtb.Columns.Add("CorrectAnswer");

            // Populate the DataTable with question data
            foreach (var question in questions)
            {
                DataRow row = dtb.NewRow();
                row["Question_id"] = question.Object.Id;
                row["Subject"] = question.Object.Subject;
                row["Content"] = question.Object.Content;
                row["Answer1"] = question.Object.Answer1;
                row["Answer2"] = question.Object.Answer2;
                row["Answer3"] = question.Object.Answer3;
                row["Answer4"] = question.Object.Answer4;
                row["CorrectAnswer"] = question.Object.CorrectAnswer;
                dtb.Rows.Add(row);
            }

            // Bind the DataTable to the DataGridView
            deleteDGV.DataSource = dtb;
        }

        // Load subjects into the ComboBox for filtering
        private async void ComboLoad()
        {
            // Query Firebase to get all subjects
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<Subject>();

            // Clear existing items in the ComboBox
            comboBox1.Items.Clear();
            // Populate the ComboBox with subject names
            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        // Filter questions by subject selected in the ComboBox
        private async void FilterCombo()
        {
            // Query Firebase for questions matching the selected subject
            var questions = await firebaseClient
                .Child("Questions")
                .OrderBy("Subject")
                .StartAt(comboBox1.Text) // Start filtering from the selected subject
                .OnceAsync<Question>();

            // Create a DataTable to hold the results
            DataTable dtb = new DataTable();
            dtb.Columns.Add("Question_id");
            dtb.Columns.Add("Subject");
            dtb.Columns.Add("Content");
            dtb.Columns.Add("Answer1");
            dtb.Columns.Add("Answer2");
            dtb.Columns.Add("Answer3");
            dtb.Columns.Add("Answer4");
            dtb.Columns.Add("CorrectAnswer");

            // Populate the DataTable with question data
            foreach (var question in questions)
            {
                Debug.WriteLine(question);
                DataRow row = dtb.NewRow();
                row["Question_id"] = question.Object.Id;
                row["Subject"] = question.Object.Subject_name;
                row["Content"] = question.Object.Content;
                row["Answer1"] = question.Object.Answer1;
                row["Answer2"] = question.Object.Answer2;
                row["Answer3"] = question.Object.Answer3;
                row["Answer4"] = question.Object.Answer4;
                row["CorrectAnswer"] = question.Object.CorrectAnswer;
                dtb.Rows.Add(row);
            }

            // Bind the DataTable to the DataGridView
            deleteDGV.DataSource = dtb;
        }

        // Handle cell click in DataGridView
        private void deleteDGV_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Event handler for DataGridView cell clicks (if needed)
        }

        // On load, fetch data and load ComboBox with subjects
        private void Udelete_Load(object sender, EventArgs e)
        {
            LoadData(); // Load all questions when the control is loaded
            ComboLoad(); // Load subjects into the ComboBox
        }

        // Refresh data when button3 is clicked
        private void button3_Click(object sender, EventArgs e)
        {
            LoadData(); // Reload all questions
        }

        // Filter data when button4 is clicked
        private void button4_Click(object sender, EventArgs e)
        {
            FilterCombo(); // Filter questions based on the selected subject
        }

        // Delete question by ID entered in textBox2
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox2.Text != null)
                {
                    Debug.WriteLine("\n"+textBox2.Text+"\n");
                    // Delete the question with the specified ID from Firebase
                    await firebaseClient
                        .Child("Questions")
                        .Child(textBox2.Text) // textBox2 contains the Question ID
                        .DeleteAsync();

                    MessageBox.Show("Deleted"); // Show confirmation message
                    FilterCombo(); // Refresh the questions list after deletion
                } 
                else
                {
                    MessageBox.Show("No Question Selected");
                }
                
            }
            catch (Exception ex)
            {
                // Handle exceptions by showing an error message
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Handle ComboBox selection change
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Optional event handler for ComboBox changes (if needed)
        }

        // Handle textBox1 text change event
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // Optional event handler for text changes in textBox1 (if needed)
        }
    }
}
