USE [MarkdownNotes]

IF object_id( N'dbo.dc_CategorySet_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_CategorySet_1...'
 DROP PROCEDURE dbo.dc_CategorySet_1
END

PRINT 'Creating Procedure dbo.dc_CategorySet_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_CategorySet_1]	
	@NoteID int,
	@ID nvarchar(Max),
	@ReturnValue nvarchar(50) output

  AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;
  
  if @ID = 2
  UPDATE [dbo].[Notes] SET CategoryID = null where ID = @NoteID;
  else
  UPDATE [dbo].[Notes] SET CategoryID = @ID where ID = @NoteID;

END
GO