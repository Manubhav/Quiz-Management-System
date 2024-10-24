using System.Data;
using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uUpdate : UserControl, IQuestionManagement
    {
        // Firebase client for interacting with the Firebase database
        private readonly FirebaseClient firebaseClient;

        public uUpdate()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Method to add a new question to Firebase
        public async Task AddQuestionAsync(Question question)
        {
            await firebaseClient
                .Child("Questions")
                .PostAsync(question); // Post the question object to Firebase
        }

        // Method to retrieve questions from Firebase based on the subject
        public async Task<DataTable> GetQuestionsAsync(string subject)
        {
            var questions = await firebaseClient
                .Child("Questions")
                .OnceAsync<Question>(); // Get all questions from Firebase

            // Filter questions by the provided subject
            // example of Anonymous method with LINQ using Lambda expression
            var filteredQuestions = questions
                .Where(q => q.Object.Subject_name == subject)
                .ToList();

            // Create a DataTable to hold the filtered questions
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question_id");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Answer1");
            dataTable.Columns.Add("Answer2");
            dataTable.Columns.Add("Answer3");
            dataTable.Columns.Add("Answer4");
            dataTable.Columns.Add("CorrectAnswer");

            // Populate the DataTable with the filtered questions
            foreach (var question in filteredQuestions)
            {
                var q = question.Object;
                dataTable.Rows.Add(q.Id, q.Subject, q.Content, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer);
            }

            return dataTable; // Return the populated DataTable
        }

        // Event handler for button click to update an existing question
        private async void button1_Click(object sender, EventArgs e)
        {
            // Create the updated question object based on user input
            var updatedQuestion = new Question
            {
                Id = Convert.ToInt32(label10.Text),
                Content = richTextBox1.Text,
                Answer1 = textBox2.Text,
                Answer2 = textBox3.Text,
                Answer3 = textBox4.Text,
                Answer4 = textBox5.Text,
                CorrectAnswer = textBox6.Text,
                Subject = comboBox1.Text
            };

            // Update the question in Firebase using the question ID
            await firebaseClient
                .Child("Questions")
                .Child(label10.Text)
                .PutAsync(updatedQuestion); // Send the updated question to Firebase

            MessageBox.Show("Updated"); // Notify the user of success
            await FilterCombo(); // Refresh the displayed questions
        }

        // Asynchronous method to load all questions into the DataGridView
        private async Task LoadDataAsync()
        {
            var questions = await firebaseClient
                .Child("Questions")
                .OnceAsync<Question>(); // Get all questions from Firebase

            // Create a DataTable to hold all questions
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question_id");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Answer1");
            dataTable.Columns.Add("Answer2");
            dataTable.Columns.Add("Answer3");
            dataTable.Columns.Add("Answer4");
            dataTable.Columns.Add("CorrectAnswer");

            // Populate the DataTable with all questions
            foreach (var question in questions)
            {
                var q = question.Object;
                dataTable.Rows.Add(q.Id, q.Subject, q.Content, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer);
            }

            updateDGV.DataSource = dataTable; // Bind the DataTable to the DataGridView
        }

        // Asynchronous method to filter questions based on the selected subject
        private async Task FilterCombo()
        {
            var selectedSubject = comboBox1.Text; // Get the selected subject from the dropdown
            var questions = await firebaseClient.Child("Questions").OnceAsync<Question>(); // Get all questions

            // Filter questions that match the selected subject
            var filteredQuestions = questions
                .Where(q => q.Object.Subject_name == selectedSubject);

            // Create a DataTable to hold the filtered questions
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question_id");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Answer1");
            dataTable.Columns.Add("Answer2");
            dataTable.Columns.Add("Answer3");
            dataTable.Columns.Add("Answer4");
            dataTable.Columns.Add("CorrectAnswer");

            // Populate the DataTable with filtered questions
            foreach (var question in filteredQuestions)
            {
                var q = question.Object;
                dataTable.Rows.Add(q.Id, q.Subject, q.Content, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer);
            }

            updateDGV.DataSource = dataTable; // Bind the DataTable to the DataGridView
        }

        // Asynchronous method to load subjects from Firebase into the comboBox
        private async Task ComboLoadAsync()
        {
            comboBox1.Items.Clear(); // Clear existing items in the dropdown
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>(); // Get all subjects

            // Add each subject to the dropdown
            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        // Event handler for form load
        private async void uUpdate_Load(object sender, EventArgs e)
        {
            await LoadDataAsync(); // Load questions into the DataGridView
            await ComboLoadAsync(); // Load subjects into the comboBox
        }

        // Event handler for filtering questions based on the selected subject
        private async void button4_Click(object sender, EventArgs e)
        {
            await FilterCombo(); // Apply filter to the displayed questions
        }

        // Event handler for selecting a question in the DataGridView
        private void updateDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Ensure a valid row is selected
            {
                DataGridViewRow selectedRow = updateDGV.Rows[e.RowIndex];
                // Populate fields with the selected question's data
                label10.Text = selectedRow.Cells[0].Value.ToString(); // Question ID
                richTextBox1.Text = selectedRow.Cells[2].Value.ToString(); // Content
                textBox2.Text = selectedRow.Cells[3].Value.ToString(); // Answer1
                textBox3.Text = selectedRow.Cells[4].Value.ToString(); // Answer2
                textBox4.Text = selectedRow.Cells[5].Value.ToString(); // Answer3
                textBox5.Text = selectedRow.Cells[6].Value.ToString(); // Answer4
                textBox6.Text = selectedRow.Cells[7].Value.ToString(); // Correct Answer
            }
        }
    }
}
