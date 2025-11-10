using BlogDataLibrary.Models;

namespace BlogDataLibrary.Data
{
    public interface ISqlData1
    {
        Task AddPost(int userId, string title, string body);
        Task<UserModel> Authenticate(string username, string password);
        List<ListPostModel> ListPosts();
        Task Register(string username, string password, string firstName, string lastName);
        PostModel ShowPostDetails(int id);
    }
}