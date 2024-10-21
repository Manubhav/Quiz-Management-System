using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz_Management_System.Models
{
    public class Question
    {
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string Answer1 { get; set; }
        public string Answer2 { get; set; }
        public string Answer3 { get; set; }
        public string Answer4 { get; set; }
        public string CorrectAnswer { get; set; }
        public int Subject_id { get; set; }
    }
}
