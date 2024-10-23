using System;
using System.Data;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uUpdate : UserControl
    {
        private readonly FirebaseClient firebaseClient;

        public uUpdate()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private void LoadData()
        {
            // Load data from Firebase
            var questions = firebaseClient
                .Child("Questions")
                .OnceAsync<Question>()
                .Result;

            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question_id");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Answer1");
            dataTable.Columns.Add("Answer2");
            dataTable.Columns.Add("Answer3");
            dataTable.Columns.Add("Answer4");
            dataTable.Columns.Add("CorrectAnswer");

            foreach (var question in questions)
            {
                // Assuming the Question class has properties matching your structure
                var q = question.Object;
                dataTable.Rows.Add(q.Id, q.Subject, q.Content, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer);
            }

            updateDGV.DataSource = dataTable;
        }

        private void FilterCombo()
        {
            var selectedSubject = comboBox1.Text;

            // Filter logic to be implemented based on Firebase queries
            var filteredQuestions = firebaseClient
                .Child("Questions")
                .OnceAsync<Question>()
                .Result
                .Where(q => q.Object.Subject_name == selectedSubject);

            // Create a DataTable to bind to the DataGridView
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Question_id");
            dataTable.Columns.Add("Subject");
            dataTable.Columns.Add("Content");
            dataTable.Columns.Add("Answer1");
            dataTable.Columns.Add("Answer2");
            dataTable.Columns.Add("Answer3");
            dataTable.Columns.Add("Answer4");
            dataTable.Columns.Add("CorrectAnswer");

            foreach (var question in filteredQuestions)
            {
                var q = question.Object;
                dataTable.Rows.Add(q.Id, q.Subject, q.Content, q.Answer1, q.Answer2, q.Answer3, q.Answer4, q.CorrectAnswer);
            }

            updateDGV.DataSource = dataTable;
        }

        private void ComboLoad()
        {
            // Load subjects from Firebase
            comboBox1.Items.Clear();
            var subjects = firebaseClient.Child("Subjects").OnceAsync<Subject>().Result;

            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        private void uUpdate_Load(object sender, EventArgs e)
        {
            LoadData();
            ComboLoad();
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            FilterCombo();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            // Get the question ID from the label
            var questionId = label10.Text;

            // Create the updated question object
            var updatedQuestion = new Question
            {
                Id = Convert.ToInt32(questionId),
                Content = richTextBox1.Text,
                Answer1 = textBox2.Text,
                Answer2 = textBox3.Text,
                Answer3 = textBox4.Text,
                Answer4 = textBox5.Text,
                CorrectAnswer = textBox6.Text,
                Subject = comboBox1.Text 
            };

            // Update the question in Firebase
            await firebaseClient
                .Child("Questions")
                .Child(questionId)
                .PutAsync(updatedQuestion);

            MessageBox.Show("Updated");
            FilterCombo();
        }

        private void updateDGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = updateDGV.Rows[e.RowIndex];
                label10.Text = selectedRow.Cells[0].Value.ToString();
                richTextBox1.Text = selectedRow.Cells[2].Value.ToString();
                textBox2.Text = selectedRow.Cells[3].Value.ToString();
                textBox3.Text = selectedRow.Cells[4].Value.ToString();
                textBox4.Text = selectedRow.Cells[5].Value.ToString();
                textBox5.Text = selectedRow.Cells[6].Value.ToString();
                textBox6.Text = selectedRow.Cells[7].Value.ToString();
            }
        }
    }
}
