USE [MarkdownNotes]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Category](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[OwnerID] [int] NOT NULL,
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


GO


