USE [MarkdownNotes]

IF object_id( N'dbo.dc_CategoryRename_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_CategoryRename_1...'
 DROP PROCEDURE dbo.dc_CategoryRename_1
END

PRINT 'Creating Procedure dbo.dc_CategoryRename_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_CategoryRename_1]
  @ID int,
  @Name nvarchar(Max),
  @ReturnValue nvarchar(50) output
  
AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

	UPDATE [dbo].[Category] set [Name] = @Name
	WHERE ID = @ID

END
GO