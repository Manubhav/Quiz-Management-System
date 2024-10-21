using Firebase.Database;
using Firebase.Database.Query;
using System.Data;

namespace Quiz_Management_System.Teacher_UC
{
    public partial class UC_AddNewQuestion : UserControl
    {
        // Initialize Firebase client
        private readonly FirebaseClient firebaseClient;

        public UC_AddNewQuestion()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Adding a new question to Firebase
        private async void addQuesBtn_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            // Add new question to Firebase
            await firebaseClient
                .Child("Questions")
                .PostAsync(new
                {
                    Subject_id = comboBox1.SelectedIndex + 1,
                    Content = questionContent.Text,
                    Answer1 = answerA.Text,
                    Answer2 = answerB.Text,
                    Answer3 = answerC.Text,
                    Answer4 = answerD.Text,
                    CorrectAnswer = CorrectAnswer.Text
                });

            MessageBox.Show("Question added successfully");
        }

        // Authenticate input fields
        bool Authenticate()
        {
            return !(string.IsNullOrWhiteSpace(comboBox1.Text) ||
                     string.IsNullOrWhiteSpace(questionContent.Text) ||
                     string.IsNullOrWhiteSpace(answerA.Text) ||
                     string.IsNullOrWhiteSpace(answerB.Text) ||
                     string.IsNullOrWhiteSpace(answerC.Text) ||
                     string.IsNullOrWhiteSpace(answerD.Text) ||
                     string.IsNullOrWhiteSpace(CorrectAnswer.Text));
        }

        // Load subjects into comboBox from Firebase
        private async void ComboLoad()
        {
            comboBox1.Items.Clear();

            // Get subjects from Firebase
            var subjects = (await firebaseClient
                .Child("Subjects")
                .OnceAsync<dynamic>())
                .Select(item => new
                {
                    Name = item.Object.Name
                })
                .ToList();

            foreach (var subject in subjects)
            {
                comboBox1.Items.Add(subject.Name);
            }
            MessageBox.Show($"Subjects fetched: {subjects.Count}");

        }

        // Load subjects when the form loads
        private void UC_AddNewQuestion_Load(object sender, EventArgs e)
        {
            ComboLoad();
        }
    }
}
