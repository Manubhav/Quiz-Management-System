using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.IO;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class NewSubject : UserControl
    {
        private readonly FirebaseClient firebaseClient;

        public NewSubject()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void addSubBtn_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            await AddSubjectToFirebase(addnewsubtxt.Text);
            ShowMessage("Subject added successfully");
        }

        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(addnewsubtxt.Text);

        private async Task AddSubjectToFirebase(string subjectName)
        {
            await firebaseClient
                .Child("Subjects")
                .PostAsync(new { Name = subjectName });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath.Text = dlg.FileName;
                }
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            await SaveFile(filePath.Text);
            ShowMessage("File saved successfully");
        }

        private async Task SaveFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                ShowMessage("File path cannot be empty!");
                return;
            }

            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                var fi = new FileInfo(filePath);
                await SaveFileToFirebase(buffer, fi.Extension, fi.Name);
            }
        }

        private async Task SaveFileToFirebase(byte[] buffer, string extn, string name)
        {
            await firebaseClient
                .Child("Lectures")
                .PostAsync(new
                {
                    Data = Convert.ToBase64String(buffer),
                    Extension = extn,
                    FileName = name,
                    Subject_name = SafeSubjectName(subLec.Text)
                });
        }

        private string SafeSubjectName(string subjectName) =>
            subjectName.Replace(".", "dot").Replace("+", "plus");

        private async void NewSubject_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async Task LoadData()
        {
            dgvSub.DataSource = await LoadSubjects();
            dgvDocuments.DataSource = await LoadLectures();
        }

        private async Task<object> LoadSubjects()
        {
            var subjects = await firebaseClient
                .Child("Subjects")
                .OnceAsync<dynamic>();

            return subjects.Select(item => new
            {
                Id = item.Key,
                Name = item.Object.Name
            }).ToList();
        }

        private async Task<object> LoadLectures()
        {
            var lectures = await firebaseClient
                .Child("Lectures")
                .OnceAsync<dynamic>();

            return lectures.Select(item => new
            {
                Id = item.Key,
                FileName = item.Object.FileName,
                Extension = item.Object.Extension,
                Subject_id = item.Object.Subject_id
            }).ToList();
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in dgvDocuments.SelectedRows)
            {
                string id = selectedRow.Cells[0].Value.ToString();
                await OpenFile(id);
            }
        }

        private async Task OpenFile(string id)
        {
            var lecture = await firebaseClient
                .Child("Lectures")
                .Child(id)
                .OnceSingleAsync<dynamic>();

            var data = Convert.FromBase64String(lecture.Data.ToString());
            string newFileName = GenerateFileName(lecture.FileName, lecture.Extension);

            File.WriteAllBytes(newFileName, data);
            Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true });
        }

        private string GenerateFileName(string name, string extn) =>
            $"{name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss"))}{extn}";

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private async void button5_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void ShowMessage(string message) =>
            MessageBox.Show(message);

        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional event if specific action needed when cell clicked
        }
    }
}
