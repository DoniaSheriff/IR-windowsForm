USE [College]
GO

/****** Object:  Table [dbo].[SpellcheckModule]    Script Date: 5/3/2019 1:51:11 AM ******/
DROP TABLE [dbo].[SpellcheckModule]
GO

/****** Object:  Table [dbo].[SpellcheckModule]    Script Date: 5/3/2019 1:51:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING OFF
GO

CREATE TABLE [dbo].[SpellcheckModule](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Term] [varchar](450) NOT NULL,
	[DocID] [varchar](max) NOT NULL,
 CONSTRAINT [PK_SpellcheckModule] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [pinky] UNIQUE NONCLUSTERED 
(
	[Term] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

