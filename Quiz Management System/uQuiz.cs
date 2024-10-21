using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uQuiz : UserControl
    {
        private readonly FirebaseClient firebaseClient;
        int i = 0, point = 0, questionCount = 0;
        List<string> answers;
        List<string> variantA;
        List<string> variantB;
        List<string> variantC;
        List<string> variantD;
        List<string> content;

        public uQuiz()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void ComboLoad()
        {
            comboBox1.Items.Clear();

            var subjects = (await firebaseClient
                .Child("Subjects")
                .OnceAsync<dynamic>())
                .Select(item => new
                {
                    Name = item.Object.Name,
                    Id = item.Key
                })
                .ToList();

            comboBox1.DataSource = subjects;
            comboBox1.DisplayMember = "Name";
            comboBox1.ValueMember = "Id";
        }

        private void uQuiz_Load(object sender, EventArgs e)
        {
            lblEmail.Text = Properties.Settings.Default.studentEmail;
            loadSubjects();
        }

        private async void loadSubjects()
        {
            try
            {
                var subjects = (await firebaseClient
                    .Child("Subjects")
                    .OnceAsync<dynamic>())
                    .Select(item => new
                    {
                        Name = item.Object.Name,
                        Id = item.Key
                    })
                    .ToList();

                comboBox1.DataSource = subjects;
                comboBox1.DisplayMember = "Name";
                comboBox1.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task RandomData()
        {
            answers = new List<string>();
            variantA = new List<string>();
            variantB = new List<string>();
            variantC = new List<string>();
            variantD = new List<string>();
            content = new List<string>();

            var questions = (await firebaseClient
                .Child("Questions")
                .OrderBy("Subject_id")
                .EqualTo(comboBox1.SelectedValue.ToString())
                .OnceAsync<dynamic>())
                .Select(item => new
                {
                    Content = item.Object.Content,
                    Answer1 = item.Object.Answer1,
                    Answer2 = item.Object.Answer2,
                    Answer3 = item.Object.Answer3,
                    Answer4 = item.Object.Answer4,
                    CorrectAnswer = item.Object.CorrectAnswer
                })
                .ToList();

            foreach (var question in questions)
            {
                content.Add(question.Content);
                variantA.Add(question.Answer1);
                variantB.Add(question.Answer2);
                variantC.Add(question.Answer3);
                variantD.Add(question.Answer4);
                answers.Add(question.CorrectAnswer);
            }

            questionCount = questions.Count;

            if (questionCount > 0)
            {
                textBox2.Text = content[i];
                radioButton1.Text = variantA[i];
                radioButton2.Text = variantB[i];
                radioButton3.Text = variantC[i];
                radioButton4.Text = variantD[i];
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            await RandomData();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
        A:
            if (i < questionCount)
            {
                if (radioButton1.Checked && answers[i] == radioButton1.Text)
                {
                    point++;
                }
                else if (radioButton2.Checked && answers[i] == radioButton2.Text)
                {
                    point++;
                }
                else if (radioButton3.Checked && answers[i] == radioButton3.Text)
                {
                    point++;
                }
                else if (radioButton4.Checked && answers[i] == radioButton4.Text)
                {
                    point++;
                }

                lblPoint.Text = "Score : " + point;
                i++;

                if (i == questionCount)
                    goto A;

                textBox2.Text = content[i];
                radioButton1.Text = variantA[i];
                radioButton2.Text = variantB[i];
                radioButton3.Text = variantC[i];
                radioButton4.Text = variantD[i];
            }
            else
            {
                MessageBox.Show($"Quiz finished. Your score: {point}");

                // Save the score in Firebase
                await firebaseClient        
                    .Child("SubjectStudent")
                    .PostAsync(new
                    {
                        Student_id = (await firebaseClient
                            .Child("Students")
                            .OrderBy("Email")
                            .EqualTo(lblEmail.Text)
                            .OnceAsync<dynamic>())
                            .FirstOrDefault()?.Key,
                        Subject_id = comboBox1.SelectedValue.ToString(),
                        Score = point
                    });
            }
        }
    }
}
