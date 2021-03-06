/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2016 (13.0.4001)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [CMS_DB]
GO
/****** Object:  UserDefinedFunction [dbo].[QuestionExist]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<QUESTION EXIST>
-- =============================================
CREATE FUNCTION [dbo].[QuestionExist]
(
	@ColumnName VARCHAR(10),
	@Search VARCHAR(MAX)
)
RETURNS INT
AS
BEGIN
	-- Declare the return variable here
	DECLARE @RESPONSE INT = 0;

	SELECT @RESPONSE = COUNT(CQUES.Id) 
	FROM CMSQuestion CQUES 
	WHERE CASE @ColumnName
	WHEN 'Question' THEN CQUES.Question
	WHEN 'QueryString' THEN CQUES.QueryString END = @Search

	RETURN @RESPONSE;
END
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSAuthor]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSAuthor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[Image] [image] NULL,
	[DOB] [datetime] NULL,
	[Experience] [int] NULL,
	[city] [nvarchar](100) NULL,
	[UserId] [nvarchar](450) NULL,
	[AuthorName] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.CMSAuthor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSEmailSubscribe]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSEmailSubscribe](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[SubscribedOn] [datetime] NULL,
 CONSTRAINT [PK_dbo.CMSEmailSubscribe] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSNotFoundQuestions]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSNotFoundQuestions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_dbo.CMSNotFoundQuestions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSQuestion]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSQuestion](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Question] [nvarchar](max) NOT NULL,
	[Answer] [nvarchar](max) NOT NULL,
	[TechnologyId] [int] NOT NULL,
	[PublishedDate] [datetime] NOT NULL,
	[Count] [int] NULL,
	[QueryString] [nvarchar](max) NULL,
	[UploadedBy] [nvarchar](450) NULL,
 CONSTRAINT [PK_dbo.CMSQuestion] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSRoles]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSRoles](
	[Id] [nvarchar](450) NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
 CONSTRAINT [PK_dbo.CMSRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSTechnology]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSTechnology](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TechnologyName] [nvarchar](100) NOT NULL,
	[QueryString] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.CMSTechnology] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSUserRoles]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSUserRoles](
	[RoleId] [nvarchar](450) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_dbo.CMSUserRoles] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CMSUsers]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CMSUsers](
	[Id] [nvarchar](450) NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[PasswordSet] [bit] NOT NULL,
	[Block] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.CMSUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CMSAuthor]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CMSAuthor_dbo.CMSUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[CMSUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CMSAuthor] CHECK CONSTRAINT [FK_dbo.CMSAuthor_dbo.CMSUsers_UserId]
GO
ALTER TABLE [dbo].[CMSQuestion]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CMSQuestion_dbo.CMSTechnology_TechnologyId] FOREIGN KEY([TechnologyId])
REFERENCES [dbo].[CMSTechnology] ([Id])
GO
ALTER TABLE [dbo].[CMSQuestion] CHECK CONSTRAINT [FK_dbo.CMSQuestion_dbo.CMSTechnology_TechnologyId]
GO
ALTER TABLE [dbo].[CMSQuestion]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CMSQuestion_dbo.CMSUsers_UploadedBy] FOREIGN KEY([UploadedBy])
REFERENCES [dbo].[CMSUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CMSQuestion] CHECK CONSTRAINT [FK_dbo.CMSQuestion_dbo.CMSUsers_UploadedBy]
GO
ALTER TABLE [dbo].[CMSUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CMSUserRoles_dbo.CMSRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[CMSRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CMSUserRoles] CHECK CONSTRAINT [FK_dbo.CMSUserRoles_dbo.CMSRoles_RoleId]
GO
ALTER TABLE [dbo].[CMSUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.CMSUserRoles_dbo.CMSUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[CMSUsers] ([Id])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CMSUserRoles] CHECK CONSTRAINT [FK_dbo.CMSUserRoles_dbo.CMSUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[AddOrUpdateQuestion]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<ADD OR UPDATE QUESTION>
-- =============================================
CREATE PROCEDURE [dbo].[AddOrUpdateQuestion]
	@Id INT,
	@Answer NVARCHAR(MAX),
	@Question NVARCHAR(MAX),
	@QueryString NVARCHAR(MAX),
	@TechnologyId INT,
	@UploadedBy NVARCHAR(450)
AS
	BEGIN
	DECLARE @RESPONSE INT = 1;
	IF ((@Answer IS NULL OR @Answer = '') OR (@Question IS NULL OR @Question = '') OR (@QueryString IS NULL OR @QueryString = '')
		OR (@TechnologyId IS NULL OR @TechnologyId = 0) OR (@UploadedBy IS NULL OR @UploadedBy = ''))
		BEGIN
			SET @RESPONSE = 0;
		END
	ELSE IF ((dbo.QuestionExist('Question', @Question)) > 0 OR (dbo.QuestionExist('QueryString', @QueryString) > 0))
		BEGIN
			SET @RESPONSE = 2;
		END
	ELSE IF(@Id = 0 OR @Id IS NULL)
		BEGIN
			INSERT INTO CMSQuestion (Answer, Question, QueryString, TechnologyId, Uploadedby, PublishedDate) 
			VALUES (@Answer, @Question, @QueryString, @TechnologyId, @UploadedBy, GETDATE());
		END
	ELSE
		BEGIN
			UPDATE CMSQuestion SET Answer = @Answer, Question = @Question, QueryString = @QueryString, TechnologyId = @TechnologyId, Uploadedby = @UploadedBy, PublishedDate = GETDATE()
			WHERE Id = @Id;
		END
	END
	SELECT @RESPONSE;
GO
/****** Object:  StoredProcedure [dbo].[AddSubscription]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<ADD SUBSCRIPTION>
-- =============================================
CREATE PROCEDURE [dbo].[AddSubscription]
	@Email VARCHAR(100)
AS
BEGIN
	INSERT INTO CMSEmailSubscribe (Email, SubscribedOn)VALUES (@Email, GETDATE());
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteAuthorByID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<DELETE AUTHOR BY ID>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteAuthorByID]
	@Id INT
AS
BEGIN
	DELETE CMSAuthor WHERE Id = @Id 
END
GO
/****** Object:  StoredProcedure [dbo].[DeleteQuestionByID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<DELETE QUESTION BY ID>
-- =============================================
CREATE PROCEDURE [dbo].[DeleteQuestionByID]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DELETE CMSQuestion
	WHERE Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[EmailExist]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<EMAIL EXIST>
-- =============================================
CREATE PROCEDURE [dbo].[EmailExist]
	@Email VARCHAR(100)
AS
BEGIN
	DECLARE @RESPONSE INT = 0;
	SELECT @RESPONSE = COUNT(CES.Id) 
	FROM CMSEmailSubscribe CES
	WHERE CES.Email = @Email 
	
	SELECT @RESPONSE; 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllAuthors]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<GET ALL AUTHORS>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllAuthors]
AS
BEGIN
	SELECT CAUTH.Id, CAUTH.AuthorName, CUSER.Email AS AuthorEmail, CUSER.UserName AS AuthorUserName,  CAUTH.Description, CAUTH.Image AS Imgbytes, 
	CAUTH.Experience, CAUTH.city, CAUTH.DOB
	FROM CMSAuthor CAUTH 
	JOIN CMSUsers CUSER
	ON CAUTH.UserId = CUSER.Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetAllTechnologies]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET ALL TECHNOLOGIES>
-- =============================================
CREATE PROCEDURE [dbo].[GetAllTechnologies]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CTECH.Id AS Id, CTECH.QueryString AS TechnologyQueryString, CTECH.TechnologyName AS TechnologyName
	FROM CMSTechnology CTECH 
END
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorByID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<GET AUTHORS BY ID>
-- =============================================
CREATE PROCEDURE [dbo].[GetAuthorByID]
	@Id INT
AS
BEGIN
	IF(@Id IS NULL)
		BEGIN
			SELECT TOP(1) CAUTH.Id, CAUTH.AuthorName, CUSER.Email AS AuthorEmail, CUSER.UserName AS AuthorUserName,  CAUTH.Description, CAUTH.Image AS Imgbytes, 
			CAUTH.Experience, CAUTH.city, CAUTH.DOB
			FROM CMSAuthor CAUTH 
			JOIN CMSUsers CUSER
			ON CAUTH.UserId = CUSER.Id
			WHERE CAUTH.Id = (SELECT TOP(1) CQUES.UploadedBy FROM CMSQuestion CQUES GROUP BY CQUES.UploadedBy ORDER BY COUNT(CQUES.Id) DESC)
		END
	ELSE 
		BEGIN
			SELECT Top(1) CAUTH.Id, CAUTH.AuthorName, CUSER.Email AS AuthorEmail, CUSER.UserName AS AuthorUserName,  CAUTH.Description, CAUTH.Image AS Imgbytes, 
			CAUTH.Experience, CAUTH.city, CAUTH.DOB
			FROM CMSAuthor CAUTH 
			JOIN CMSUsers CUSER
			ON CAUTH.UserId = CUSER.Id
			WHERE CAUTH.Id  = @Id 
		END
	
END
GO
/****** Object:  StoredProcedure [dbo].[GetAuthorByUserID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<GET AUTHORS BY USER ID>
-- =============================================
CREATE PROCEDURE [dbo].[GetAuthorByUserID]
	@UserID VARCHAR(450)
AS
BEGIN
	SELECT CAUTH.Id, CAUTH.AuthorName, CUSER.Email AS AuthorEmail, CUSER.UserName AS AuthorUserName,  CAUTH.Description, CAUTH.Image AS Imgbytes, 
	CAUTH.Experience, CAUTH.city, CAUTH.DOB
	FROM CMSAuthor CAUTH 
	JOIN CMSUsers CUSER
	ON CAUTH.UserId = CUSER.Id
	WHERE CAUTH.UserId = @UserID 
END
		

GO
/****** Object:  StoredProcedure [dbo].[GetLatestQuestions]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET POPULAR QUESTIONS>
-- =============================================
CREATE PROCEDURE [dbo].[GetLatestQuestions]
	@Query VARCHAR(MAX)
AS
BEGIN
	SELECT TOP(5) CQUES.Question AS Text, CQUES.QueryString AS Value
	FROM CMSQuestion CQUES
	WHERE DATEDIFF(dd, GETDATE(), CQUES.PublishedDate) <= 30 AND CQUES.QueryString != @Query
END
GO
/****** Object:  StoredProcedure [dbo].[GetPasswordHash]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET HASH PASSWORD>
-- =============================================
CREATE PROCEDURE [dbo].[GetPasswordHash]
	-- Add the parameters for the stored procedure here
	@Id VARCHAR(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CUSER.PasswordHash
	FROM CMSUsers CUSER 
	WHERE CUSER.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetPopularQuestions]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET POPULAR QUESTIONS>
-- =============================================
CREATE PROCEDURE [dbo].[GetPopularQuestions]
	@Query VARCHAR(MAX)
AS
BEGIN
	SELECT TOP(5) CQUES.Question AS Text, CQUES.QueryString AS Value
	FROM CMSQuestion CQUES
	WHERE CQUES.QueryString != @Query
	ORDER BY CQUES.Count DESC
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionByID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET QUESTION BY ID>
-- =============================================
CREATE PROCEDURE [dbo].[GetQuestionByID]
	@Id INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CQUES.Id AS Id, CQUES.Question, CQUES.Answer, CQUES.TechnologyId, CQUES.PublishedBy, CQUES.UploadedBy, CQUES.PublishedDate,
	CQUES.QueryString AS TechnologyQueryString
	FROM CMSQuestion CQUES
	WHERE CQUES.Id = @Id
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionByQS]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET QUESTION BY QUERY STRING>
-- =============================================
CREATE PROCEDURE [dbo].[GetQuestionByQS]
	@QueryString VARCHAR(MAX)
AS
BEGIN
	SELECT CQUES.Id AS Id, CQUES.Question, CQUES.Answer, CQUES.TechnologyId, CQUES.PublishedBy, CQUES.UploadedBy, CQUES.PublishedDate,
	CQUES.QueryString AS TechnologyQueryString
	FROM CMSQuestion CQUES
	WHERE CQUES.QueryString = @QueryString
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionsByTechnology]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET QUESTIONS BY TECHNOLOGY>
-- =============================================
CREATE PROCEDURE [dbo].[GetQuestionsByTechnology]
	@TechnologyQueryString VARCHAR(MAX)
AS
BEGIN
	SELECT CQUES.Id AS Id, CQUES.Question, CQUES.Answer, CQUES.TechnologyId, CQUES.PublishedBy, CQUES.UploadedBy, CQUES.PublishedDate,
	CQUES.QueryString AS TechnologyQueryString
	FROM CMSQuestion CQUES
	JOIN CMSTechnology CTECH
	ON CQUES.TechnologyId = CTECH.Id
	WHERE CTECH.QueryString = @TechnologyQueryString
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionsByUser]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET QUESTION BY USER>
-- =============================================
CREATE PROCEDURE [dbo].[GetQuestionsByUser]
	@LoginUser VARCHAR(450)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CQUES.Id, CQUES.Question, CQUES.Answer, CQUES.TechnologyId, CQUES.UploadedBy, CQUES.PublishedDate,
	CQUES.QueryString, CTECH.TechnologyName
	FROM CMSQuestion CQUES
	JOIN CMSTechnology CTECH
	ON CQUES.TechnologyId = CTECH.Id
	WHERE CQUES.UploadedBy = @LoginUser
END
GO
/****** Object:  StoredProcedure [dbo].[GetQuestionsOfTechnologies]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET QUESTIONS OF TECHNOLOGIES>
-- =============================================
CREATE PROCEDURE [dbo].[GetQuestionsOfTechnologies]
AS
BEGIN
	SELECT  CTECH.TechnologyName, CQUES.Question, CQUES.QueryString, CQUES.PublishedDate, CQUES.Answer
	FROM CMSTechnology CTECH
	RIGHT JOIN CMSQuestion CQUES
	ON CTECH.Id = CQUES.TechnologyId
END
GO
/****** Object:  StoredProcedure [dbo].[GetUser]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Gurwinder Singh>
-- Create date: <2-28-2018>
-- Description:	<GET USER>
-- =============================================
CREATE PROCEDURE [dbo].[GetUser]
	-- Add the parameters for the stored procedure here
	@ColumnName VARCHAR(10),
	@Search VARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT CUSER.Id, CUSER.UserName, CUSER.PasswordHash, CUSER. SecurityStamp, CUSER.Email, CUSER.PasswordSet, CUSER.EmailConfirmed, CUSER.PhoneNumber,
	CUSER.PhoneNumberConfirmed, CUSER.LockoutEnabled, CUSER.AccessFailedCount, CUSER.Block
	FROM CMSUsers CUSER 
	WHERE CASE @ColumnName 
	WHEN 'Email' THEN CUSER.Email
	WHEN 'Id' THEN CUSER.Id
	WHEN 'UserName' THEN CUSER.UserName END = @Search
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateAuthorByID]    Script Date: 3/1/2018 1:06:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<GURWINDER SINGH>
-- Create date: <2-28-2018>
-- Description:	<UPDATE AUTHOR BY ID>
-- =============================================
CREATE PROCEDURE [dbo].[UpdateAuthorByID]
	@Id INT,
	@AuthorName VARCHAR(100) NULL,
	@City VARCHAR(100) NULL,
	@Description VARCHAR(MAX) NULL,
	@DOB DATETIME NULL,
	@Experience INT NULL,
	@Image VARBINARY(MAX) NULL
AS
BEGIN
	DECLARE @RESPONSE INT = 1;
	IF (@Id IS NULL OR @Id = 0)
		BEGIN 
			SET @RESPONSE = 0;
		END
	ELSE
		BEGIN
			UPDATE CMSAuthor SET AuthorName = @AuthorName, city = @City, Description = @Description, DOB = @DOB, Experience = @Experience, Image = @Image 
			WHERE Id = @Id;
		END
	SELECT @RESPONSE;
END
GO
