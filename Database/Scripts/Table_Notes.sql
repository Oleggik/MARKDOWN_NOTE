USE [MarkdownNotes]
GO

/****** Object:  Table [dbo].[Notes]    Script Date: 3/15/2017 9:41:16 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Notes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Text] [nvarchar](max) NOT NULL,
	[IsHidden] [bit] NULL,
	[SharedToEveryone] [bit] NULL,
	OwnerID int NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


