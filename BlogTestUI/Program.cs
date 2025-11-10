using BlogDataLibrary.Data;
using BlogDataLibrary.Database;
using BlogDataLibrary.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace BlogTestUI
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            ISqlData db = GetConnection();

            // 1) LOGIN
            UserModel currentUser = null;
            while (currentUser == null)
            {
                currentUser = await Authenticate(db);
                if (currentUser == null)
                {
                    Console.WriteLine("\nInvalid credentials. Try again.\n");
                }
            }

            // 2) REGISTER NEW USER (optional, can skip)
            await Register(db);

            // 3) ADD POST
            await AddPost(db, currentUser);

            // 4) LIST POSTS
            ListPosts(db);

            // 5) SHOW POST DETAILS
            await ShowPostDetails(db);

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }

        private static ISqlData GetConnection()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");

            IConfiguration config = builder.Build();

            ISqlDataAccess dataAccess = new SqlDataAccess(config);
            ISqlData data = new SqlData(dataAccess);

            return data;
        }

        private static async Task<UserModel> GetCurrentUser(ISqlData db)
        {
            Console.Write("Username: ");
            string username = Console.ReadLine();

            Console.Write("Password: ");
            string password = Console.ReadLine();

            return await db.Authenticate(username, password);
        }

        private static async Task<UserModel> Authenticate(ISqlData db)
        {
            Console.WriteLine("\n=== LOGIN ===");
            UserModel user = await GetCurrentUser(db);

            if (user != null)
            {
                Console.WriteLine($"\nWelcome, {user.FirstName} {user.LastName}!");
            }

            return user;
        }

        private static async Task Register(ISqlData db)
        {
            Console.WriteLine("\n=== REGISTER ===");

            Console.Write("New Username: ");
            string username = Console.ReadLine();

            Console.Write("New Password: ");
            string password = Console.ReadLine();

            Console.Write("First Name: ");
            string firstName = Console.ReadLine();

            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();

            await db.Register(username, password, firstName, lastName);

            Console.WriteLine("\nUser has been registered successfully!");
        }

        private static async Task AddPost(ISqlData db, UserModel user)
        {
            if (user == null)
            {
                Console.WriteLine("\nYou must be logged in to add a post.");
                return;
            }

            Console.WriteLine("\n=== ADD POST ===");

            Console.Write("Title: ");
            string title = Console.ReadLine();

            Console.Write("Body: ");
            string body = Console.ReadLine();

            await db.AddPost(user.Id, title, body);

            Console.WriteLine("\nPost added successfully!");
        }

        private static void ListPosts(ISqlData db)
        {
            Console.WriteLine("\n=== LIST OF POSTS ===\n");

            List<ListPostModel> posts = db.ListPosts();

            foreach (var post in posts)
            {
                Console.WriteLine($"ID: {post.Id}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"Author: {post.Username}");
                Console.WriteLine($"Date: {post.DateCreated}");
                Console.WriteLine($"Preview: {post.BodyPreview}...");
                Console.WriteLine("---");
            }
        }

        private static async Task ShowPostDetails(ISqlData db)
        {
            Console.WriteLine("\n=== POST DETAILS ===");
            Console.Write("Enter Post ID: ");
            int postId = int.Parse(Console.ReadLine());

            PostModel post = db.ShowPostDetails(postId);

            if (post != null)
            {
                Console.WriteLine($"\nID: {post.Id}");
                Console.WriteLine($"Title: {post.Title}");
                Console.WriteLine($"User ID: {post.UserId}");
                Console.WriteLine($"Date Created: {post.DateCreated}");
                Console.WriteLine($"Body:\n{post.Body}");
                Console.WriteLine($"Author: {post.FirstName} {post.LastName} ({post.Username})");
            }
            else
            {
                Console.WriteLine("\nPost not found.");
            }
        }
    }
}
