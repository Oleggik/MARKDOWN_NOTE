USE [MarkdownNotes]

IF object_id( N'dbo.dc_UserGet_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_UserGet_1...'
 DROP PROCEDURE dbo.dc_UserGet_1
END

PRINT 'Creating Procedure dbo.dc_UserGet_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


CREATE PROCEDURE dbo.dc_UserGet_1
  @Name nvarchar(Max),
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;
  
  DECLARE @test varchar(50)


	set @ReturnValue = (SELECT [Password] FROM MarkdownNotes.dbo.Users Where Name =  @Name)
  

/*GRANT EXEC ON dbo.dc_UserGet_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Users Where UserName =  'User1'
*/
END
GO


   