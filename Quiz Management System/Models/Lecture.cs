namespace Quiz_Management_System.Models
{
    public class Lecture
    {
        public string Id { get; set; } // Make sure to match the type with your Firebase key format
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Subject_id { get; set; } // Assuming this matches the field in your Firebase
        public byte[] Data { get; set; } // If you're storing file data in Firebase
    }
}
