-- TABLE
IF OBJECT_ID('dbo.Posts', 'U') IS NULL
BEGIN
    CREATE TABLE dbo.Posts
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        UserId INT NOT NULL,
        Title NVARCHAR(200) NOT NULL,
        Body NVARCHAR(MAX) NOT NULL,
        CreatedDate DATETIME NOT NULL DEFAULT(GETDATE())
    );
END
GO

-- ADD
IF OBJECT_ID('dbo.spPosts_Add', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spPosts_Add;
GO
CREATE PROCEDURE dbo.spPosts_Add
    @UserId INT,
    @Title NVARCHAR(200),
    @Body NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Posts (UserId, Title, Body) VALUES (@UserId, @Title, @Body);
END
GO

-- LIST
IF OBJECT_ID('dbo.spPosts_List', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spPosts_List;
GO
CREATE PROCEDURE dbo.spPosts_List
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, CreatedDate, UserId
    FROM dbo.Posts
    ORDER BY Id DESC;
END
GO

-- SHOW
IF OBJECT_ID('dbo.spPosts_Show', 'P') IS NOT NULL
    DROP PROCEDURE dbo.spPosts_Show;
GO
CREATE PROCEDURE dbo.spPosts_Show
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT Id, Title, Body, CreatedDate, UserId
    FROM dbo.Posts
    WHERE Id = @Id;
END
GO
