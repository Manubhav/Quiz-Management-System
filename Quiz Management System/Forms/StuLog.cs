using Firebase.Database;
using Firebase.Database.Query;
using Quiz_Management_System.Models;
using System.Text.RegularExpressions;

namespace Quiz_Management_System
{
    public partial class StuLog : Form
    {
        // Firebase client to connect to the Firebase Realtime Database
        private static readonly FirebaseClient firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");

        public StuLog()
        {
            InitializeComponent();
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase connection
        }

        // Event handler for the login button click
        private async void button1_Click(object sender, EventArgs e)
        {
            // Save the entered email to application settings
            Properties.Settings.Default.studentEmail = EmailLog.Text;
            Properties.Settings.Default.Save();

            // Check for empty input fields
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!"); // Display error message
                return;
            }

            // Validate the email format
            if (!ValidateEmail(EmailLog.Text))
            {
                ShowMessage("Invalid email format!"); // Display error message
                return;
            }

            // Validate the password length
            if (!ValidatePassword(PassLog.Text))
            {
                ShowMessage("Password must be at least 8 characters long!"); // Display error message
                return;
            }

            // Retrieve the student object by email
            var student = await GetStudentByEmail(EmailLog.Text);
            if (student == null)
            {
                ShowMessage("User does not exist!"); // Display error message
                return;
            }

            // Validate the entered password against the stored password
            if (!IsPasswordValid(student.Password, PassLog.Text))
            {
                ShowMessage("Invalid password!"); // Display error message
                return;
            }

            // If authentication is successful, proceed to the student application form
            Hide(); // Hide the current login form
            using (var stuApp = new StuForm())
            {
                stuApp.ShowDialog(); // Show the student application form
            }
            Close(); // Close the login form
        }

        // Check if all required input fields are filled
        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(EmailLog.Text) && !string.IsNullOrWhiteSpace(PassLog.Text);

        // Asynchronously retrieve a student object by email
        private async Task<Student> GetStudentByEmail(string email)
        {
            var students = await firebaseClient.Child("Students").OrderByKey().OnceAsync<Student>();
            // Return the student object if found, otherwise return null
            return students.FirstOrDefault(s => s.Object.Email == email)?.Object;
        }

        // Compare stored password with the entered password
        private bool IsPasswordValid(string storedPassword, string enteredPassword) =>
            storedPassword == enteredPassword;

        // Show a message box with the provided message
        private void ShowMessage(string message) =>
            MessageBox.Show(message);

        // Validate the format of the entered email using regex
        private bool ValidateEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basic email validation pattern
            return Regex.IsMatch(email, emailPattern); // Return true if email matches the pattern
        }

        // Validate that the password length is at least 8 characters
        private bool ValidatePassword(string password) =>
            password.Length >= 8; // Return true if password meets length requirement
    }
}
