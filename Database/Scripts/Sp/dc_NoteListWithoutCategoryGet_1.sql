USE [MarkdownNotes]

IF object_id( N'dbo.dc_NotelistGet_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NotelistGet_1...'
 DROP PROCEDURE dbo.dc_NotelistGet_1
END

PRINT 'Creating Procedure dbo.dc_NotelistGet_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


CREATE PROCEDURE dbo.dc_NotelistGet_1
  @UserName nvarchar(Max),
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  SELECT DISTINCT  nt.ID, nt.Name FROM dbo.Notes nt 
  JOIN dbo.NotesSharing ns ON ns.NoteID = nt.ID
  JOIN dbo.Users us ON ns.UserID = us.UserID 
  where us.Name = @UserName and nt.CategoryID is null
  

/*GRANT EXEC ON dbo.dc_NotelistGet_1 TO sa 
SELECT [Password] FROM MarkdownNotes.dbo.Notes Where NoteName =  'User1'
*/
END

GO


   