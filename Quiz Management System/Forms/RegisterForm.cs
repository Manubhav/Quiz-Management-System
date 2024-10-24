using Firebase.Database;
using System.Text.RegularExpressions;
using Quiz_Management_System.Models;
using Firebase.Database.Query;

namespace Quiz_Management_System
{
    public partial class RegisterForm : Form
    {
        private static readonly FirebaseClient firebaseClient;

        // Static constructor to initialize Firebase client
        static RegisterForm()
        {
            FirebaseInitializer.InitializeFirebase(); // Initialize Firebase
            firebaseClient = new FirebaseClient("https://smart-learning-system-a2c86-default-rtdb.asia-southeast1.firebasedatabase.app");
        }

        // Constructor for RegisterForm
        public RegisterForm()
        {
            InitializeComponent();
        }

        // Event handler for the registration button click
        private async void RegBtn_Click(object sender, EventArgs e)
        {
            // Check for empty fields before proceeding
            if (!Authenticate())
            {
                ShowMessage("Don't keep any textbox blank!");
                return;
            }

            // Validate email format
            if (!ValidateEmail(EmailTxt.Text))
            {
                ShowMessage("Invalid email format!");
                return;
            }

            // Validate password strength
            if (!ValidatePassword(PassTxt.Text))
            {
                ShowMessage("Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one digit!");
                return;
            }

            // Validate contact number format
            if (!ValidateContactNumber(NumTxt.Text))
            {
                ShowMessage("Contact number must be exactly 10 digits!");
                return;
            }

            // Create a Teacher object from input fields
            var teacher = CreateTeacherFromInput();
            await SaveTeacherToFirebase(teacher); // Save the teacher data to Firebase

            ShowMessage("Registration successful!"); // Show success message
            Close(); // Optionally close the registration form
        }

        // Method to check if all required fields are filled
        private bool Authenticate() =>
            !string.IsNullOrWhiteSpace(NameTxt.Text) &&
            !string.IsNullOrWhiteSpace(SurnameTxt.Text) &&
            !string.IsNullOrWhiteSpace(NumTxt.Text) &&
            !string.IsNullOrWhiteSpace(EmailTxt.Text) &&
            !string.IsNullOrWhiteSpace(PassTxt.Text);

        // Method to create a Teacher object from user input
        private Teacher CreateTeacherFromInput() => new Teacher
        {
            Name = NameTxt.Text,
            Surname = SurnameTxt.Text,
            Number = NumTxt.Text,
            Email = EmailTxt.Text,
            Password = PassTxt.Text
        };

        // Method to save the Teacher object to Firebase
        private async Task SaveTeacherToFirebase(Teacher teacher)
        {
            await firebaseClient
                .Child("Teachers")
                .PostAsync(teacher); // Post teacher data to Firebase under "Teachers" node
        }

        // Method to show messages in a message box
        private void ShowMessage(string message) =>
            MessageBox.Show(message);

        // Regex validation for email
        private bool ValidateEmail(string email)
        {
            var emailPattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$"; // Basic email validation pattern
            return Regex.IsMatch(email, emailPattern); // Check if email matches the pattern
        }

        // Regex validation for password strength
        private bool ValidatePassword(string password)
        {
            var passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,}$"; // Minimum 8 characters, at least one uppercase, one lowercase, and one digit
            return Regex.IsMatch(password, passwordPattern); // Check if password matches the pattern
        }

        // Regex validation for contact number
        private bool ValidateContactNumber(string contactNumber)
        {
            var contactNumberPattern = @"^\d{10}$"; // Exactly 10 digits
            return Regex.IsMatch(contactNumber, contactNumberPattern); // Check if contact number matches the pattern
        }
    }
}
