CREATE TABLE [dbo].[Users] (
	  [UserId]			[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [UserName]		[varchar](15)	UNIQUE			NOT NULL
	, [Password]		[varchar](300)	NOT NULL
	, [DisplayName]	[varchar](40)		NOT NULL
	, Description		[varchar](max)
	, Email				[varchar](40)	UNIQUE			NOT NULL
	, CreatedDate		[datetime]		NOT NULL
  CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED
(
	[UserId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE PROCEDURE [dbo].[pr_addUser]
(
	@userName		VARCHAR(15)
	, @email		VARCHAR(40)
	, @password		VARCHAR(15)
	, @displayName	VARCHAR(40)
	, @description	VARCHAR(MAX)
	, @createdDate	DATETIME
	, @userId		INT				OUTPUT
)
AS
BEGIN
	INSERT INTO Users
	(
		[UserName]	
		, [Password]
		, [DisplayName]
		, Description
		, Email
		, CreatedDate
	)
	VALUES
	(
		@userName
		, HASHBYTES('SHA2_256',@password)
		, @displayName
		, @description
		, @email
		, @createdDate
	)
	
	SET @userId = (SELECT SCOPE_IDENTITY())
END
GO

GRANT EXECUTE ON [dbo].[pr_addUser] TO LACES_USER
GO