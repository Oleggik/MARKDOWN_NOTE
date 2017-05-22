USE [MarkdownNotes]

IF object_id( N'dbo.dc_UserListGet_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_UserListGet_1...'
 DROP PROCEDURE dbo.dc_UserListGet_1
END

PRINT 'Creating Procedure dbo.dc_UserListGet_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


CREATE PROCEDURE dbo.dc_UserListGet_1
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;
  
  DECLARE @test varchar(50)


	SELECT UserID, Name FROM MarkdownNotes.dbo.Users
  

/*GRANT EXEC ON dbo.dc_UserListGet_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Users Where UserName =  'User1'
*/
END
GO


   