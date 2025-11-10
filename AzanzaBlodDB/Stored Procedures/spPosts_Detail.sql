CREATE PROCEDURE [dbo].[spPosts_Detail]
    @id INT
AS
BEGIN
    SELECT 
        p.[Id],
        p.[UserId],
        p.[Title],
        p.[Content] AS Body,       
        p.[DateCreated],
        u.[Username],
        u.[FirstName],
        u.[LastName]
    FROM [dbo].[Posts] p
    INNER JOIN [dbo].[Users] u ON p.[UserId] = u.[Id]
    WHERE p.[Id] = @id
END
