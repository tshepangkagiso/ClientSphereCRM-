using CRM_API.Models;

namespace CRM_API.Data.Services.Interfaces
{
    public interface ICommentDbServices
    {
        Task CreateComment(string Message, Guid SentByID, Guid SentToID);
        Task<List<Comment>> GetAllComments(Guid SentByID, Guid SentToID);
    }
}