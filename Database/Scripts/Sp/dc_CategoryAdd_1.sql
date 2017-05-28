USE [MarkdownNotes]

IF object_id( N'dbo.dc_CategoryAdd_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_CategoryAdd_1...'
 DROP PROCEDURE dbo.dc_CategoryAdd_1
END

PRINT 'Creating Procedure dbo.dc_CategoryAdd_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_CategoryAdd_1]	
	@Name nvarchar(Max),
	@UserName nvarchar(Max),
	@ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  DECLARE @OwnerID int

  SET @OwnerID = (SELECT UserID FROM dbo.Users where Name = @UserName);

  IF NOT EXISTS (SELECT 1  FROM dbo.Category ct 
					 JOIN dbo.Users us ON ct.OwnerID = us.UserID 
						 where us.Name = @UserName and ct.Name = @Name)
  BEGIN
	INSERT INTO [dbo].[Category] ([Name], [OwnerID]) VALUES  (@Name, @OwnerID)
  END
  ELSE
  BEGIN
	SET @ReturnValue = 'Category "' + @Name + '" exist in DB'
  END

  END

GO


