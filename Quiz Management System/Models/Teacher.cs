namespace Quiz_Management_System.Models
{
    public class Teacher
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Number { get; set; }
        public string Email { get; set; }
        public string Password { get; set; } // You might want to consider encrypting the password
    }

}
