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
        // Initialize Firebase client
        private readonly FirebaseClient firebaseClient;

        public NewSubject()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Adding a new subject to Firebase
        private async void addSubBtn_Click(object sender, EventArgs e)
        {
            if (!Authenticate())
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            // Add new subject to Firebase
            await firebaseClient
                .Child("Subjects")
                .PostAsync(new { Name = addnewsubtxt.Text });
            MessageBox.Show("Subject added successfully");
        }

        // Validate if subject textbox is not empty
        bool Authenticate()
        {
            return !string.IsNullOrWhiteSpace(addnewsubtxt.Text);
        }

        // Open a file using OpenFileDialog
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.ShowDialog();
            filePath.Text = dlg.FileName;
        }

        // Save the file to Firebase
        private async void button2_Click(object sender, EventArgs e)
        {
            await SaveFile(filePath.Text);
            MessageBox.Show("File saved successfully");
        }

        // Method to save a file (as a byte array in Firebase)
        private async Task SaveFile(string filePath)
        {
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);

                var fi = new FileInfo(filePath);
                string extn = fi.Extension;
                string name = fi.Name;

                // Store file as Base64 string in Firebase
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
        }

        private string SafeSubjectName(string subjectName)
        {
            return subjectName.Replace(".", "dot").Replace("+", "plus");
        }

        // Load subjects and lectures from Firebase on form load
        private async void NewSubject_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Method to load data from Firebase (subjects and lectures)
        private async Task LoadData()
        {
            // Load subjects
            var subjects = (await firebaseClient
                .Child("Subjects")
                .OnceAsync<dynamic>())
                .Select(item => new
                {
                    Id = item.Key,
                    Name = item.Object.Name
                })
                .ToList();

            if (subjects.Any())
            {
                dgvSub.DataSource = subjects;
            }

            // Load lectures
            var lectures = (await firebaseClient
                .Child("Lectures")
                .OnceAsync<dynamic>())
                .Select(item => new
                {
                    Id = item.Key,
                    FileName = item.Object.FileName,
                    Extension = item.Object.Extension,
                    Subject_id = item.Object.Subject_id
                })
                .ToList();

            if (lectures.Any())
            {
                dgvDocuments.DataSource = lectures;
            }
        }

        // Open file for the selected row in dgvDocuments
        private async void button3_Click(object sender, EventArgs e)
        {
            var selectedRow = dgvDocuments.SelectedRows;
            foreach (var row in selectedRow)
            {
                string id = ((DataGridViewRow)row).Cells[0].Value.ToString();
                await OpenFile(id);
            }
        }

        // Method to retrieve and open a file from Firebase
        private async Task OpenFile(string id)
        {
            // Get file data from Firebase
            var lecture = await firebaseClient
                .Child("Lectures")
                .Child(id)
                .OnceSingleAsync<dynamic>();

            var name = lecture.FileName.ToString();
            var data = Convert.FromBase64String(lecture.Data.ToString());
            var extn = lecture.Extension.ToString();

            var newFileName = name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss")) + extn;
            File.WriteAllBytes(newFileName, data);

            // Open the saved file
            Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true });
        }

        // Reload subjects and lectures data
        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Reload data on button5 click
        private async void button5_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Optional event if specific action needed when cell clicked
        }
    }
}
