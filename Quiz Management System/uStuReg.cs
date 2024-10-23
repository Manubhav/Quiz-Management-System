using Firebase.Database;
using System.Data;
using Quiz_Management_System.Models;
using Firebase.Database.Query;

namespace Quiz_Management_System
{
    public partial class uStuReg : UserControl
    {
        private static FirebaseClient firebaseClient;

        public uStuReg()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text) || string.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("Don't keep any textbox blank!");
                return;
            }

            var group = new Group
            {
                Group_no = textBox1.Text,
                Specialty = textBox2.Text
            };

            await firebaseClient.Child("Groups").PostAsync(group);
            MessageBox.Show("Group registration successful!");
        }

        private async Task LoadData<T>(string childName, DataGridView dgv, Func<T, Task<string[]>> rowMapper) where T : class
        {
            var items = await firebaseClient.Child(childName).OnceAsync<T>();
            var dt = new DataTable();

            // Check if there are any items to load
            if (!items.Any())
            {
                dgv.DataSource = null;
                return;
            }

            // Use the first item to infer columns dynamically
            var firstItem = items.FirstOrDefault()?.Object;
            if (firstItem != null)
            {
                var sampleRow = await rowMapper(firstItem);

                // Generate columns based on the first row of data
                foreach (var property in sampleRow)
                {
                    dt.Columns.Add(property);
                }
            }
            else
            {
                throw new InvalidOperationException("No data available to generate columns.");
            }

            // Add rows to the DataTable
            foreach (var item in items)
            {
                if (item.Object != null)
                {
                    dt.Rows.Add(await rowMapper(item.Object));
                }
            }

            dgv.DataSource = dt;
        }



        private async void button2_Click(object sender, EventArgs e)
        {
            await LoadData<Group>("Groups", groupsDGV, async g => new[] { g.Group_no, g.Specialty });
        }

        private async void uStuReg_Load(object sender, EventArgs e)
        {
            await LoadData<Group>("Groups", groupsDGV, async g => new[] { g.Group_no, g.Specialty });
            await LoadData<Student>("Students", newstuDGV, async s => new[]
            {
                await GetGroupNo(s.Group_id),
                s.Name,
                s.Surname,
                s.Age.ToString(),
                s.Email,
                s.Password
            });
        }

        private async Task<string> GetGroupNo(string groupId)
        {
            var groups = await firebaseClient.Child("Groups").OnceAsync<Group>();
            return groups.FirstOrDefault(g => g.Key == groupId)?.Object.Group_no ?? "";
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            var student = new Student
            {
                Group_id = textBox3.Text,
                Name = textBox4.Text,
                Surname = textBox5.Text,
                Age = int.Parse(textBox6.Text),
                Email = textBox7.Text,
                Password = textBox8.Text
            };

            await firebaseClient.Child("Students").PostAsync(student);
            MessageBox.Show("Student registration successful!");
        }

        private async void button4_Click(object sender, EventArgs e)
        {
            await LoadData<Student>("Students", newstuDGV, async s => new[]
            {
                await GetGroupNo(s.Group_id),
                s.Name,
                s.Surname,
                s.Age.ToString(),
                s.Email,
                s.Password
            });
        }
    }
}
