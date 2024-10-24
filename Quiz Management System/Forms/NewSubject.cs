using Firebase.Database;
using Firebase.Database.Query;
using System.Diagnostics;

namespace Quiz_Management_System
{
    public partial class NewSubject : UserControl
    {
        private readonly FirebaseClient firebaseClient;

        // Constructor initializes Firebase client
        public NewSubject()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Event handler for adding a new subject
        private async void addSubBtn_Click(object sender, EventArgs e)
        {
            // Check for empty fields before adding
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            // Add the subject to Firebase and show a success message
            await AddSubjectToFirebase(addnewsubtxt.Text);
            ShowMessage("Subject added successfully");
        }

        // Check if input fields are not empty
        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(addnewsubtxt.Text);

        // Method to add a new subject to Firebase
        private async Task AddSubjectToFirebase(string subjectName)
        {
            await firebaseClient
                .Child("Subjects")
                .PostAsync(new { Name = subjectName });
        }

        // Event handler for selecting a file
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                // Show dialog and get file path if selected
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    filePath.Text = dlg.FileName;
                }
            }
        }

        // Event handler for saving the selected file
        private async void button2_Click(object sender, EventArgs e)
        {
            await SaveFile(filePath.Text);
            ShowMessage("File saved successfully");
        }

        // Method to save the selected file to Firebase
        private async Task SaveFile(string filePath)
        {
            // Check if file path is not empty
            if (string.IsNullOrWhiteSpace(filePath))
            {
                ShowMessage("File path cannot be empty!");
                return;
            }

            // Read the file and save its contents to Firebase
            using (Stream stream = File.OpenRead(filePath))
            {
                byte[] buffer = new byte[stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);

                var fi = new FileInfo(filePath);
                await SaveFileToFirebase(buffer, fi.Extension, fi.Name);
            }
        }

        // Method to save file data to Firebase
        private async Task SaveFileToFirebase(byte[] buffer, string extn, string name)
        {
            await firebaseClient
                .Child("Lectures")
                .PostAsync(new
                {
                    Data = Convert.ToBase64String(buffer), // Convert file to Base64
                    Extension = extn,
                    FileName = name,
                    Subject_name = SafeSubjectName(subLec.Text) // Safe subject name
                });
        }

        // Method to replace special characters in subject name
        private string SafeSubjectName(string subjectName) =>
            subjectName.Replace(".", "dot").Replace("+", "plus");

        // Event handler for loading data when the control loads
        private async void NewSubject_Load(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Load subjects and lectures into DataGridViews
        private async Task LoadData()
        {
            dgvSub.DataSource = await LoadSubjects();
            dgvDocuments.DataSource = await LoadLectures();
        }

        // Method to load subjects from Firebase
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

        // Method to load lectures from Firebase
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

        // Event handler for opening a selected lecture file
        private async void button3_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow selectedRow in dgvDocuments.SelectedRows)
            {
                string id = selectedRow.Cells[0].Value.ToString();
                await OpenFile(id);
            }
        }

        // Method to open a lecture file based on its ID
        private async Task OpenFile(string id)
        {
            var lecture = await firebaseClient
                .Child("Lectures")
                .Child(id)
                .OnceSingleAsync<dynamic>();

            var data = Convert.FromBase64String(lecture.Data.ToString());
            string newFileName = GenerateFileName(lecture.FileName, lecture.Extension);

            // Save the file locally and open it
            File.WriteAllBytes(newFileName, data);
            Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true });
        }

        // Generate a unique file name for the saved file
        private string GenerateFileName(string name, string extn) =>
            $"{name.Replace(extn, DateTime.Now.ToString("ddMMyyyyhhmmss"))}{extn}";

        // Event handler for refreshing data
        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Event handler for refreshing data
        private async void button5_Click(object sender, EventArgs e)
        {
            await LoadData();
        }

        // Method to show messages in a message box
        private void ShowMessage(string message) =>
            MessageBox.Show(message);

        // Optional event if specific action needed when cell clicked
        private void dgvDocuments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Additional functionality can be added here if needed
        }
    }
}
