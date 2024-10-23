using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class Udelete : UserControl
    {
        private static FirebaseClient firebaseClient;

        public Udelete()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Fetch data based on Subject ID entered in textBox1
        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var questions = await firebaseClient
                    .Child("Questions")
                    .OrderBy("Subject_id")
                    .EqualTo(int.Parse(textBox1.Text))
                    .OnceAsync<Question>();

                DataTable dtb = new DataTable();
                dtb.Columns.Add("Question_id");
                dtb.Columns.Add("Subject");
                dtb.Columns.Add("Content");
                dtb.Columns.Add("Answer1");
                dtb.Columns.Add("Answer2");
                dtb.Columns.Add("Answer3");
                dtb.Columns.Add("Answer4");
                dtb.Columns.Add("CorrectAnswer");

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

                deleteDGV.DataSource = dtb;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Load all questions from Firebase
        private async void LoadData()
        {
            var questions = await firebaseClient
                .Child("Questions")
                .OnceAsync<Question>();

            DataTable dtb = new DataTable();
            dtb.Columns.Add("Question_id");
            dtb.Columns.Add("Subject");
            dtb.Columns.Add("Content");
            dtb.Columns.Add("Answer1");
            dtb.Columns.Add("Answer2");
            dtb.Columns.Add("Answer3");
            dtb.Columns.Add("Answer4");
            dtb.Columns.Add("CorrectAnswer");

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

            deleteDGV.DataSource = dtb;
        }

        // Load subjects into the ComboBox
        private async void ComboLoad()
        {
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<Subject>();

            comboBox1.Items.Clear();
            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Object.Name);
            }
        }

        // Filter questions by subject selected in the ComboBox
        private async void FilterCombo()
        {
            var questions = await firebaseClient
                .Child("Questions")
                .OrderBy("Subject")
                .StartAt(comboBox1.Text)
                .OnceAsync<Question>();

            DataTable dtb = new DataTable();
            dtb.Columns.Add("Question_id");
            dtb.Columns.Add("Subject");
            dtb.Columns.Add("Content");
            dtb.Columns.Add("Answer1");
            dtb.Columns.Add("Answer2");
            dtb.Columns.Add("Answer3");
            dtb.Columns.Add("Answer4");
            dtb.Columns.Add("CorrectAnswer");

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
            LoadData();
            ComboLoad();
        }

        // Refresh data when button3 is clicked
        private void button3_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        // Filter data when button4 is clicked
        private void button4_Click(object sender, EventArgs e)
        {
            FilterCombo();
        }

        // Delete question by ID entered in textBox2
        private async void button2_Click(object sender, EventArgs e)
        {
            try
            {
                await firebaseClient
                    .Child("Questions")
                    .Child(textBox2.Text) // Assuming textBox2 contains the Question ID
                    .DeleteAsync();

                MessageBox.Show("Deleted");
                FilterCombo();
            }
            catch (Exception ex)
            {
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
