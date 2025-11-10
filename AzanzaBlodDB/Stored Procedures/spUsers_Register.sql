CREATE PROCEDURE [dbo].[spUsers_Register]
    @username NVARCHAR(50),
    @password NVARCHAR(50),
    @firstName NVARCHAR(50),
    @lastName NVARCHAR(50)
AS
BEGIN
    INSERT INTO [dbo].[Users] ([Username], [Password], [FirstName], [LastName])
    VALUES (@username, @password, @firstName, @lastName)
END