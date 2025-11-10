CREATE PROCEDURE [dbo].[spPosts_Insert]
    @userId INT,
    @title NVARCHAR(100),
    @body NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO [dbo].[Posts] ([UserId], [Title], [Content])
    VALUES (@userId, @title, @body)
END
