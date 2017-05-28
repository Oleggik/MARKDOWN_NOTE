USE [MarkdownNotes]

IF object_id( N'dbo.dc_CategoryListGet_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_CategoryListGet_1...'
 DROP PROCEDURE dbo.dc_CategoryListGet_1
END

PRINT 'Creating Procedure dbo.dc_CategoryListGet_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


CREATE PROCEDURE dbo.dc_CategoryListGet_1
  @UserName nvarchar(Max),	
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  SELECT DISTINCT  ct.ID, ct.Name FROM dbo.Category ct 
  JOIN dbo.Users us ON ct.OwnerID = us.UserID 
  where us.Name = @UserName  
  
  --DECLARE @OwnerID int

  --SET @OwnerID = (SELECT UserID FROM dbo.Users where Name = @UserName);

  --SELECT DISTINCT Name FROM dbo.Category
  --WHERE OwnerID = @OwnerID

/*GRANT EXEC ON dbo.dc_CategoryListGet_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Users Where UserName =  'User1'
*/
END
GO


   