USE [MarkdownNotes]

IF object_id( N'dbo.dc_NoteUpdate_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NoteUpdate_1...'
 DROP PROCEDURE dbo.dc_NoteUpdate_1
END

PRINT 'Creating Procedure dbo.dc_NoteUpdate_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_NoteUpdate_1]
  @NoteID int,
  @NoteName nvarchar(Max) = NULL,
  @NoteText nvarchar(Max) = NULL,
  @IsHidden bit = NULL,
  @SharedToEveryone bit = NULL,
  @OwnerID int = NULL,
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  UPDATE [dbo].[Notes]
   SET [Name] = IsNull(@NoteName,Name)
      ,[Text] = IsNull(@NoteText, Text)
      ,[IsHidden] = IsNull(@IsHidden, IsHidden)
      ,[SharedToEveryone] = IsNull(@SharedToEveryone,SharedToEveryone)
      ,[OwnerID] = IsNull(@OwnerID, OwnerID)
 WHERE ID = @NoteID


END
GO

