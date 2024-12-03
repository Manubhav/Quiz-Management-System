using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uQuiz : UserControl
    {
        private readonly FirebaseClient firebaseClient; // Firebase client for database operations
        private int currentQuestionIndex = 0; // Tracks the index of the current question
        private int score = 0; // Tracks the user's score
        private List<Question> questions; // List to store questions fetched from Firebase

        public uQuiz()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app"); // Set up Firebase client
        }

        // Event handler for form load; sets the email label and loads subjects
        private async void uQuiz_Load(object sender, EventArgs e)
        {
            lblEmail.Text = Properties.Settings.Default.studentEmail; // Display the logged-in student's email
            await LoadSubjectsAsync(); // Load subjects from Firebase
        }

        // Asynchronously loads subjects into the ComboBox from Firebase
        private async Task LoadSubjectsAsync()
        {
            try
            {
                var subjects = await firebaseClient
                    .Child("Subjects")
                    .OnceAsync<dynamic>(); // Fetch subjects from Firebase

                // Populate ComboBox with subjects
                comboBox1.DataSource = subjects
                    .Select(item => new
                    {
                        Name = item.Object.Name, // Subject name
                        Id = item.Key // Subject ID
                    })
                    .ToList();
                comboBox1.DisplayMember = "Name"; // Display name in ComboBox
                comboBox1.ValueMember = "Id"; // Set ID as value in ComboBox
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading subjects: {ex.Message}"); // Show error message if loading fails
            }
        }

        // Asynchronously loads questions based on the selected subject
        private async Task LoadQuestionsAsync()
        {
            questions = (await firebaseClient
                .Child("Questions")
                .OrderBy("Subject_name")
                .EqualTo(comboBox1.Text) // Filter questions by the selected subject
                .OnceAsync<Question>())
                .Select(item => item.Object) // Select the question objects
                .ToList();

            // Display the first question if any questions are loaded
            if (questions.Count > 0)
            {
                DisplayQuestion(); // Show the first question
            }
        }

        // Displays the current question and its possible answers
        private void DisplayQuestion()
        {
            var question = questions[currentQuestionIndex]; // Get the current question
            textBox2.Text = question.Content; // Set the question content
            radioButton1.Text = question.Answer1; // Set answer options
            radioButton2.Text = question.Answer2;
            radioButton3.Text = question.Answer3;
            radioButton4.Text = question.Answer4;

            // Set the button text based on the current question index
            button2.Text = (currentQuestionIndex == questions.Count - 1) ? "Finish" : "Next";
        }

        // Event handler for loading questions when button1 is clicked
        private async void button1_Click(object sender, EventArgs e)
        {
            await LoadQuestionsAsync(); // Load questions based on selected subject
        }

        // Event handler for submitting the answer when button2 is clicked
        private async void button2_Click(object sender, EventArgs e)
        {
            if (currentQuestionIndex < questions.Count)
            {
                // Check if the selected answer is correct and update the score
                var selectedAnswer = GetSelectedAnswer(); // Get the selected answer
                if (selectedAnswer == questions[currentQuestionIndex].CorrectAnswer) // Compare with correct answer
                {
                    score++; // Increment score if correct
                }

                lblPoint.Text = $"Score: {score}"; // Display the current score

                // Move to the next question
                currentQuestionIndex++; // Increment question index

                // Check if there are more questions to display
                if (currentQuestionIndex < questions.Count)
                {
                    DisplayQuestion(); // Display the next question
                }
                else
                {
                    await FinishQuizAsync(); // Finish the quiz if no more questions
                }
            }
        }

        // Returns the selected answer from the radio buttons
        private string GetSelectedAnswer()
        {
            if (radioButton1.Checked) return radioButton1.Text; // Check which answer is selected
            if (radioButton2.Checked) return radioButton2.Text;
            if (radioButton3.Checked) return radioButton3.Text;
            if (radioButton4.Checked) return radioButton4.Text;
            return null; // Return null if no answer is selected
        }

        // Asynchronously handles the completion of the quiz
        private async Task FinishQuizAsync()
        {
            MessageBox.Show($"Quiz finished. Your score: {score}"); // Show final score

            // Save the score in Firebase
            var studentId = (await firebaseClient
                .Child("Students")
                .OrderBy("Email")
                .EqualTo(lblEmail.Text) // Find the student by email
                .OnceAsync<dynamic>())
                .FirstOrDefault()?.Key; // Get the student's ID

            // Save the score for the subject
            await firebaseClient
                .Child("SubjectStudent")
                .PostAsync(new
                {
                    Student_id = studentId, // Save student ID
                    Subject_name = comboBox1.Text, // Save subject name
                    Score = score // Save the score
                });

            NavigateBack(); // Navigate back to the previous screen
        }

        // Hides the current user control to navigate back
        private void NavigateBack()
        {
            this.Visible = false; // Set visibility to false to hide the control
        }
    }
}
