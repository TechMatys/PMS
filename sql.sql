USE [MailCampaign]
GO
/****** Object:  Table [dbo].[EmailTemplateAttachments]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailTemplateAttachments](
	[EmailTemplateAttachmentId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[Path] [varchar](50) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_EmailTemplateAttachments] PRIMARY KEY CLUSTERED 
(
	[EmailTemplateAttachmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MailSettings]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MailSettings](
	[MailSettingId] [int] IDENTITY(1,1) NOT NULL,
	[Provider] [varchar](50) NULL,
	[SecretKey] [varchar](50) NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_MailSettings] PRIMARY KEY CLUSTERED 
(
	[MailSettingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipientGroupMapping]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipientGroupMapping](
	[RecipientGroupMappingId] [int] IDENTITY(1,1) NOT NULL,
	[RecipientId] [int] NULL,
	[RecipientGroupId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_RecipientGroupMapping] PRIMARY KEY CLUSTERED 
(
	[RecipientGroupMappingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RecipientGroups]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RecipientGroups](
	[RecipientGroupId] [int] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](20) NULL,
	[Description] [varchar](100) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_RecipientGroups] PRIMARY KEY CLUSTERED 
(
	[RecipientGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Recipients]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Recipients](
	[RecipientId] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [varchar](20) NOT NULL,
	[AliasName] [varchar](20) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Recipients] PRIMARY KEY CLUSTERED 
(
	[RecipientId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Templates]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Templates](
	[TemplateId] [int] IDENTITY(1,1) NOT NULL,
	[Title] [varchar](50) NULL,
	[Description] [varchar](100) NULL,
	[HtmlContent] [varchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_Templates] PRIMARY KEY CLUSTERED 
(
	[TemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserEmailTemplates]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserEmailTemplates](
	[UserEmailTemplateId] [int] IDENTITY(1,1) NOT NULL,
	[RecipientGroupId] [int] NULL,
	[TemplateId] [int] NULL,
	[UserId] [int] NULL,
	[RecipientId] [int] NULL,
	[Subject] [varchar](100) NULL,
	[HtmlContent] [varchar](4000) NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [int] NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDeleted] [bit] NOT NULL,
	[DeletedBy] [int] NULL,
	[DeletedDate] [datetime] NULL,
 CONSTRAINT [PK_UserEmailTemplates] PRIMARY KEY CLUSTERED 
(
	[UserEmailTemplateId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/11/2022 2:12:37 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[EmailAddress] [varchar](20) NOT NULL,
	[UserName] [varchar](20) NOT NULL,
	[Password] [varchar](20) NOT NULL,
	[RoleId] [int] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[RecipientGroups] ON 

INSERT [dbo].[RecipientGroups] ([RecipientGroupId], [GroupName], [Description], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Sales', N'Sales description', 1, -1, CAST(N'2022-02-11T00:00:00.000' AS DateTime), -1, CAST(N'2022-02-11T07:31:36.007' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[RecipientGroups] ([RecipientGroupId], [GroupName], [Description], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'Admins', N'test', 1, -1, CAST(N'2022-02-05T00:00:00.000' AS DateTime), -1, CAST(N'2022-02-11T07:31:55.950' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[RecipientGroups] ([RecipientGroupId], [GroupName], [Description], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (3, N'Patient', NULL, 1, -1, CAST(N'2022-01-05T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[RecipientGroups] OFF
GO
SET IDENTITY_INSERT [dbo].[Recipients] ON 

INSERT [dbo].[Recipients] ([RecipientId], [EmailAddress], [AliasName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'test@gmail.com', N'test', 1, 1, CAST(N'2022-02-02T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
INSERT [dbo].[Recipients] ([RecipientId], [EmailAddress], [AliasName], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'test1@gmail.com', N'test 2', 1, 1, CAST(N'2022-02-02T00:00:00.000' AS DateTime), NULL, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[Recipients] OFF
GO
SET IDENTITY_INSERT [dbo].[Templates] ON 

INSERT [dbo].[Templates] ([TemplateId], [Title], [Description], [HtmlContent], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (1, N'Sales', N'dsadsdsad', N'<p><strong>This is test co</strong></p>', 1, 1, CAST(N'2022-02-10T00:00:00.000' AS DateTime), -1, CAST(N'2022-02-10T14:58:52.190' AS DateTime), 0, NULL, NULL)
INSERT [dbo].[Templates] ([TemplateId], [Title], [Description], [HtmlContent], [IsActive], [CreatedBy], [CreatedDate], [ModifiedBy], [ModifiedDate], [IsDeleted], [DeletedBy], [DeletedDate]) VALUES (2, N'Admin', NULL, NULL, 1, 1, CAST(N'2022-02-01T00:00:00.000' AS DateTime), NULL, NULL, 1, -1, CAST(N'2022-02-10T11:40:57.933' AS DateTime))
SET IDENTITY_INSERT [dbo].[Templates] OFF
GO
ALTER TABLE [dbo].[EmailTemplateAttachments] ADD  CONSTRAINT [DF_EmailTemplateAttachments_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[EmailTemplateAttachments] ADD  CONSTRAINT [DF_EmailTemplateAttachments_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[MailSettings] ADD  CONSTRAINT [DF_MailSettings_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[RecipientGroupMapping] ADD  CONSTRAINT [DF_RecipientGroupMapping_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RecipientGroupMapping] ADD  CONSTRAINT [DF_RecipientGroupMapping_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[RecipientGroups] ADD  CONSTRAINT [DF_RecipientGroups_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[RecipientGroups] ADD  CONSTRAINT [DF_RecipientGroups_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Recipients] ADD  CONSTRAINT [DF_Recipients_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Recipients] ADD  CONSTRAINT [DF_Recipients_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Templates] ADD  CONSTRAINT [DF_Templates_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Templates] ADD  CONSTRAINT [DF_Templates_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[UserEmailTemplates] ADD  CONSTRAINT [DF_UserEmailTemplates_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[UserEmailTemplates] ADD  CONSTRAINT [DF_UserEmailTemplates_IsDeleted]  DEFAULT ((0)) FOR [IsDeleted]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [DF_Users_IsActive]  DEFAULT ((1)) FOR [IsActive]
GO
