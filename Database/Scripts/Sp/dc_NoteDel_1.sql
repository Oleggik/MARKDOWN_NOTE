USE [MarkdownNotes]

IF object_id( N'dbo.dc_NoteDel_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NoteDel_1...'
 DROP PROCEDURE dbo.dc_NoteDel_1
END

PRINT 'Creating Procedure dbo.dc_NoteDel_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO


CREATE PROCEDURE dbo.dc_NoteDel_1
  @NoteID int,
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;
  
  DELETE FROM dbo.Notes where ID = @NoteID
  DELETE FROM dbo.NotesSharing where ID = @NoteID

/*GRANT EXEC ON dbo.dc_NoteDel_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Notes Where NoteName =  'User1'
*/
END
GO


   