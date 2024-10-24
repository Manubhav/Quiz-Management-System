using Quiz_Management_System.Models;
using System.Data;

public interface IQuestionManagement
{
    Task AddQuestionAsync(Question question);
    Task<DataTable> GetQuestionsAsync(string subject);
}
