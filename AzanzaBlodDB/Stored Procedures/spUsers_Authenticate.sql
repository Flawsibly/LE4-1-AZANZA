CREATE PROCEDURE [dbo].[spUsers_Authenticate]
    @username NVARCHAR(50),
    @password NVARCHAR(50)
AS
BEGIN
    SELECT [Id], [Username], [Password], [FirstName], [LastName]
    FROM [dbo].[Users]
    WHERE [Username] = @username AND [Password] = @password
END