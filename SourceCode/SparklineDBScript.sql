USE [SkarplineChatDB]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/29/2015 6:07:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [uniqueidentifier] NOT NULL,
	[UserName] [varchar](50) NOT NULL,
	[LoginDateTime] [datetime] NULL,
	[LogoutDateTime] [datetime] NULL,
	[IsLogout] [bit] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[UserChat]    Script Date: 1/29/2015 6:07:01 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserChat](
	[ChatId] [uniqueidentifier] NOT NULL,
	[MessageFrom] [uniqueidentifier] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[MessageDateTime] [datetime] NOT NULL,
 CONSTRAINT [PK_UserChat] PRIMARY KEY CLUSTERED 
(
	[ChatId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[UserChat]  WITH CHECK ADD  CONSTRAINT [FK_UserChat_User] FOREIGN KEY([MessageFrom])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[UserChat] CHECK CONSTRAINT [FK_UserChat_User]
GO
