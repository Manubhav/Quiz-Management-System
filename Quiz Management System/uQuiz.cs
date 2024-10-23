using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;
using System.Diagnostics;

namespace Quiz_Management_System
{
    public partial class uQuiz : UserControl
    {
        private readonly FirebaseClient firebaseClient;
        private int currentQuestionIndex = 0, score = 0;
        private List<Question> questions;

        public uQuiz()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void uQuiz_Load(object sender, EventArgs e)
        {
            lblEmail.Text = Properties.Settings.Default.studentEmail;
            await LoadSubjectsAsync();
        }

        private async Task LoadSubjectsAsync()
        {
            try
            {
                var subjects = await firebaseClient
                    .Child("Subjects")
                    .OnceAsync<dynamic>();

                comboBox1.DataSource = subjects
                    .Select(item => new
                    {
                        Name = item.Object.Name,
                        Id = item.Key
                    })
                    .ToList();
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task LoadQuestionsAsync()
        {
            questions = (await firebaseClient
                .Child("Questions")
                .OrderBy("Subject_name")
                .EqualTo(comboBox1.Text)
                .OnceAsync<Question>())
                .Select(item => item.Object)
                .ToList();

            if (questions.Count > 0)
            {
                DisplayQuestion();
            }
        }

        private void DisplayQuestion()
        {
            var question = questions[currentQuestionIndex];
            textBox2.Text = question.Content;
            radioButton1.Text = question.Answer1;
            radioButton2.Text = question.Answer2;
            radioButton3.Text = question.Answer3;
            radioButton4.Text = question.Answer4;

            // Set the button text when displaying the question
            button2.Text = (currentQuestionIndex == questions.Count - 1) ? "Finish" : "Next";
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await LoadQuestionsAsync();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count)
            {
                // Check if the selected answer is correct and update the score
                var selectedAnswer = GetSelectedAnswer();
                if (selectedAnswer == questions[currentQuestionIndex].CorrectAnswer)
                {
                    score++;
                }

                lblPoint.Text = $"Score: {score}"; // Use string interpolation for readability

                // Move to the next question
                currentQuestionIndex++;

                // Check if there are more questions
                if (currentQuestionIndex < questions.Count)
                {
                    DisplayQuestion();
                }
                else
                {
                    await FinishQuizAsync();
                }
            }
        }


        private string GetSelectedAnswer()
        {
            if (radioButton1.Checked) return radioButton1.Text;
            if (radioButton2.Checked) return radioButton2.Text;
            if (radioButton3.Checked) return radioButton3.Text;
            if (radioButton4.Checked) return radioButton4.Text;
            return null;
        }

        private async Task FinishQuizAsync()
        {
            MessageBox.Show($"Quiz finished. Your score: {score}");

            // Save the score in Firebase
            var studentId = (await firebaseClient
                .Child("Students")
                .OrderBy("Email")
                .EqualTo(lblEmail.Text)
                .OnceAsync<dynamic>())
                .FirstOrDefault()?.Key;

            await firebaseClient
                .Child("SubjectStudent")
                .PostAsync(new
                {
                    Student_id = studentId,
                    Subject_name = comboBox1.Text,
                    Score = score
                });

            NavigateBack();
        }

        private void NavigateBack()
        {
            this.Visible = false;
        }
    }
}
