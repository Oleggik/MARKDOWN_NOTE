USE [MarkdownNotes]

IF object_id( N'dbo.dc_NoteSharingAdd_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NoteSharingAdd_1...'
 DROP PROCEDURE dbo.dc_NoteSharingAdd_1
END

PRINT 'Creating Procedure dbo.dc_NoteSharingAdd_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_NoteSharingAdd_1]
  @NoteId int,
  @UserId int,
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  INSERT INTO [dbo].[NotesSharing] ([NoteID] ,[UserID],[ReadOnly] ) VALUES  (@NoteId,@UserId, 0 )
 
  

/*GRANT EXEC ON dbo.dc_NoteGet_1 TO sa
SELECT [Password] FROM MarkdownNotes.dbo.Notes Where NoteName =  'Note1'
*/
END

GO


