CREATE PROCEDURE [dbo].[spPosts_List]
AS
BEGIN
    SELECT 
        p.[Id],
        p.[Title],
        u.[Username],
        p.[DateCreated],
        LEFT(p.[Content], 20) AS [BodyPreview]
    FROM [dbo].[Posts] p
    INNER JOIN [dbo].[Users] u ON p.[UserId] = u.[Id]
    ORDER BY p.[DateCreated] DESC
END
