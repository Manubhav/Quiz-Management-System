namespace Quiz_Management_System.Models
{
    public class Lecture
    {
        public string Id { get; set; } 
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Subject_name { get; set; } 
        public byte[] Data { get; set; } 
    }
}
