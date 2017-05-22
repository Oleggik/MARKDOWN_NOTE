USE [MarkdownNotes]

IF object_id( N'dbo.dc_UserAdd_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_UserAdd_1...'
 DROP PROCEDURE dbo.dc_UserAdd_1
END

PRINT 'Creating Procedure dbo.dc_UserAdd_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_UserAdd_1]
  @Name nvarchar(Max),
  @Password nvarchar(Max),
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;
 
  IF NOT EXISTS (SELECT 1 FROM dbo.Users WHERE Name = @Name)
  BEGIN
	INSERT INTO [dbo].[Users] ([Name] ,[Password]) VALUES  (@Name,@Password)
  END
  ELSE
  BEGIN
	SET @ReturnValue = 'User "' + @Name + '" exist in DB'
  END

  

/*GRANT EXEC ON dbo.dc_UserGet_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Users Where UserName =  'User1'
*/
END

GO


