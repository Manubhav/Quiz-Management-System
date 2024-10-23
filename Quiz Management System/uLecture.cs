using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;

namespace Quiz_Management_System
{
    public partial class uLecture : UserControl
    {
        private readonly FirebaseClient firebaseClient;
        private Dictionary<string, string> subjectDictionary;
        private DataTable lectureDataTable;

        public uLecture()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async Task InitializeDataAsync()
        {
            subjectDictionary = await LoadSubjectsAsync();
            lectureDataTable = CreateLecturesDataTable();
            await LoadLecturesAsync();
        }

        private async Task LoadLecturesAsync()
        {
            var lectures = await firebaseClient.Child("Lectures").OnceAsync<Lecture>();
            foreach (var lecture in lectures)
            {
                string subjectValue = subjectDictionary.TryGetValue(lecture.Object.Subject_name, out var name) ? name : "Unknown";
                lectureDataTable.Rows.Add(lecture.Key, lecture.Object.FileName, lecture.Object.Extension, subjectValue);
            }
            dataGridView1.DataSource = lectureDataTable;
        }

        private async Task<Dictionary<string, string>> LoadSubjectsAsync()
        {
            var subjects = await firebaseClient.Child("Subjects").OnceAsync<Subject>();
            return subjects.ToDictionary(subject => subject.Key, subject => subject.Object.Name);
        }

        private DataTable CreateLecturesDataTable()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("FileName");
            dataTable.Columns.Add("Extension");
            dataTable.Columns.Add("Subject");
            return dataTable;
        }

        private async void uLecture_Load(object sender, EventArgs e)
        {
            await InitializeDataAsync();
            LoadComboBoxData();
        }

        private void LoadComboBoxData()
        {
            comboBox1.Items.Clear();
            foreach (var subject in subjectDictionary.Values)
            {
                comboBox1.Items.Add(subject);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            var filteredData = FilterLectures(comboBox1.Text);
            dataGridView1.DataSource = filteredData;
        }

        private DataTable FilterLectures(string filter)
        {
            var filteredTable = CreateLecturesDataTable();
            foreach (DataRow row in lectureDataTable.Rows)
            {
                string subjectValue = row["Subject"].ToString();
                if (subjectValue.Contains(filter, StringComparison.OrdinalIgnoreCase))
                {
                    filteredTable.Rows.Add(row.ItemArray);
                }
            }
            return filteredTable;
        }

        private async Task OpenFileAsync(string id)
        {
            var lecture = await firebaseClient.Child("Lectures").Child(id).OnceSingleAsync<Lecture>();
            if (lecture != null)
            {
                string newFileName = Path.Combine(Path.GetTempPath(), lecture.FileName);
                try
                {
                    await File.WriteAllBytesAsync(newFileName, lecture.Data);
                    Process.Start(new ProcessStartInfo { FileName = newFileName, UseShellExecute = true });
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error opening file: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Lecture not found.");
            }
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                string id = row.Cells[0].Value.ToString();
                await OpenFileAsync(id);
            }
        }
    }
}
