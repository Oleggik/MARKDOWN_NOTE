USE [MarkdownNotes]

IF object_id( N'dbo.dc_NoteAdd_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_NoteAdd_1...'
 DROP PROCEDURE dbo.dc_NoteAdd_1
END

PRINT 'Creating Procedure dbo.dc_NoteAdd_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_NoteAdd_1]
  @UserName nvarchar(Max),
  @NoteName nvarchar(Max),
  @NoteText nvarchar(Max),
  @IsHidden bit,
  @SharedToEveryone bit,
  @ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

  DECLARE @UserID int  
  DECLARE @NewNoteID int 

	set @UserID = (SELECT UserID FROM dbo.Users where Name =  @UserName)

  IF NOT EXISTS (SELECT 1 FROM dbo.NotesSharing ns INNER JOIN dbo.Notes nt ON ns.NoteID = nt.ID where ns.UserID = @UserID and nt.Name = @NoteName)
  BEGIN
	INSERT INTO [dbo].[Notes] ([Name] ,[Text], [IsHidden],[SharedToEveryone], [OwnerID], [CategoryID]) VALUES  (@NoteName,@NoteText, @IsHidden, @SharedToEveryone,@UserID, 2 )
	SELECT @NewNoteID = SCOPE_IDENTITY()

	INSERT INTO [dbo].[NotesSharing] ([NoteID] ,[UserID],[ReadOnly]) VALUES  (@NewNoteID,@UserID, 0)
  END
  ELSE
  BEGIN
	SET @ReturnValue = 'Note "' + @NoteName + '" exist for user ' +@UserName
  END

  END

GO


