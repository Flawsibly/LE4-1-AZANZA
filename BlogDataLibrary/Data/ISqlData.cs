using BlogDataLibrary.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogDataLibrary.Data
{
    public interface ISqlData
    {
        Task<UserModel> Authenticate(string username, string password);
        Task Register(string username, string password, string firstName, string lastName);
        Task AddPost(int userId, string title, string body);
        List<ListPostModel> ListPosts();

        // ADD THIS:
        PostModel ShowPostDetails(int id);
    }
}
