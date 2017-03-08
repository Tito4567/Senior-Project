CREATE TABLE [dbo].[Users] (
	  [UserId]			[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [UserName]		[varchar](15)	UNIQUE			NOT NULL
	, [Password]		[varchar](300)	NOT NULL
	, [DisplayName]		[varchar](40)	NOT NULL
	, [Description]		[varchar](max)
	, [Email]			[varchar](40)	UNIQUE			NOT NULL
	, [UsersFollowed]	[int]			NOT NULL
	, [UsersFollowing]	[int]			NOT NULL
	, [CreatedDate]		[datetime]		NOT NULL
	, [UpdatedDate]		[datetime]		NOT NULL
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
	, @userId		INT				OUTPUT
)
AS
BEGIN
	INSERT INTO Users
	(
		[UserName]	
		, [Password]
		, [DisplayName]
		, [Description]
		, [Email]
		, [UsersFollowed]
		, [UsersFollowing]
		, [CreatedDate]
		, [UpdatedDate]
	)
	VALUES
	(
		@userName
		, HASHBYTES('SHA2_256',@password)
		, @displayName
		, @description
		, @email
		, 0
		, 0
		, GETDATE()
		, GETDATE()
	)
	
	SET @userId = (SELECT SCOPE_IDENTITY())
END
GO

CREATE PROCEDURE [dbo].[pr_validateLogin]
(
	@userName	VARCHAR(15)
	, @password	VARCHAR(15)
	, @userId	INT			OUTPUT
)
AS
BEGIN
	SELECT @userId=UserId FROM Users
	WHERE
		UserName=@userName
		AND [Password]=HASHBYTES('SHA2_256',@password)
END
GO

CREATE PROCEDURE [dbo].[pr_changePassword]
(
	@userId			INT
	, @oldPassword	VARCHAR(15)
	, @newPassword	VARCHAR(15)
	, @result		BIT			OUTPUT
)
AS
BEGIN
	SELECT @userId=UserId FROM Users
	WHERE
		UserId=@userId
		AND [Password]=HASHBYTES('SHA2_256',@oldPassword)

	IF (@@ROWCOUNT > 0)
	BEGIN
		UPDATE Users
		SET [Password] = HASHBYTES('SHA2_256',@newPassword)
		WHERE UserId=@userId

		SET @result = 1
	END
	ELSE
		SET @result = 0
END
GO

GRANT EXECUTE ON [dbo].[pr_addUser] TO LACES_USER
GRANT EXECUTE ON [dbo].[pr_validateLogin] TO LACES_USER
GRANT EXECUTE ON [dbo].[pr_changePassword] TO LACES_USER
GO