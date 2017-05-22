USE [MarkdownNotes]

IF object_id( N'dbo.dc_NoteRename_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NoteRename_1...'
 DROP PROCEDURE dbo.dc_NoteRename_1
END

PRINT 'Creating Procedure dbo.dc_NoteRename_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_NoteRename_1]
  @NoteID int,
  @NoteName nvarchar(Max)
  
AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

	UPDATE [dbo].[Notes] set [Name] = IsNull(@NoteName,Name)
	WHERE ID = @NoteID

END
GO