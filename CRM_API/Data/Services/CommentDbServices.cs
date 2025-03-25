using CRM_API.Data.Services.Interfaces;
using CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace CRM_API.Data.Services
{
    public class CommentDbServices : ICommentDbServices
    {
        private readonly ApplicationDbContext dbContext;

        public CommentDbServices(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        // Query to get all related comments from the stored procedure
        public async Task<List<Comment>> GetAllComments(Guid SentByID, Guid SentToID)
        {
            var Comments = await this.dbContext.Comments
                .FromSqlRaw("EXEC spGetRelatedComments @CommentSentBy = {0},@CommentSentTo ={1}", SentByID, SentToID)
                .ToListAsync();
            return Comments;
        }

        // Query to create comment from the stored procedure
        public async Task CreateComment(string Message, Guid SentByID, Guid SentToID)
        {
            await this.dbContext.Database.ExecuteSqlRawAsync("EXEC spCreateComment @CommentMessage={0}, @CommentSentBy={1},@CommentSentTo={2}", Message, SentByID, SentToID);
        }
    }
}
