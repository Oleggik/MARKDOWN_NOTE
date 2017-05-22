USE [MarkdownNotes]
GO

/****** Object:  Table [dbo].[NotesSharing]    Script Date: 3/15/2017 9:41:36 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[NotesSharing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[NoteID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[ReadOnly] [bit] NOT NULL
) ON [PRIMARY]

GO


