using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogDataLibrary.Data
{
    public class SqlData : ISqlData, ISqlData1
    {
        private ISqlDataAccess _db;

        public SqlData(ISqlDataAccess db)
        {
            _db = db;
        }

        // Authenticate user
        public async Task<UserModel> Authenticate(string username, string password)
        {
            var results = await _db.LoadData<UserModel, dynamic>(
                "spUsers_Authenticate",
                new { username, password });

            return results.FirstOrDefault();
        }

        // Register new user
        public async Task Register(string username, string password, string firstName, string lastName)
        {
            await _db.SaveData(
                "spUsers_Register",
                new { username, password, firstName, lastName });
        }

        // Add a new post
        public async Task AddPost(int userId, string title, string body)
        {
            await _db.SaveData(
                "spPosts_Insert",
                new { userId, title, body });
        }

        // List all posts
        public List<ListPostModel> ListPosts()
        {
            return _db.LoadData<ListPostModel, dynamic>(
                "spPosts_List",
                new { }) // no parameters
                .Result;
        }

        // Show details of a single post
        public PostModel ShowPostDetails(int id)
        {
            return _db.LoadData<PostModel, dynamic>(
                "spPosts_Detail",
                new { id })   // only two parameters, match LoadData signature
                .Result.FirstOrDefault();  // take the first result
        }
    }
}
