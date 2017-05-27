USE [MarkdownNotes]

IF object_id( N'dbo.dc_CategoryAdd_1') IS NOT NULL
BEGIN 
 PRINT 'Dropping Procedure dbo.dc_CategoryAdd_1...'
 DROP PROCEDURE dbo.dc_CategoryAdd_1
END

PRINT 'Creating Procedure dbo.dc_CategoryAdd_1...'
SET QUOTED_IDENTIFIER ON
SET ANSI_NULLS ON
GO



CREATE PROCEDURE [dbo].[dc_CategoryAdd_1]	
	@Name nvarchar(Max),
	@ReturnValue nvarchar(50) output

AS
BEGIN
  SET NOCOUNT ON;
  SET XACT_ABORT ON;

   IF NOT EXISTS (SELECT 1 FROM dbo.Category WHERE Name = @Name)
  BEGIN
	INSERT INTO [dbo].[Category] ([Name]) VALUES  (@Name)
  END
  ELSE
  BEGIN
	SET @ReturnValue = 'Ctaegory "' + @Name + '" exist in DB'
  END

  END

GO


