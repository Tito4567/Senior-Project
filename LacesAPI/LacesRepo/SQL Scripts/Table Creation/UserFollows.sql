CREATE TABLE [dbo].[UserFollows] (
	  [UserFollowId]	[int]		IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [FollowingUserId]	[int]		NOT NULL
	, [FollowedUserId]	[int]		NOT NULL
	, [CreatedDate]		[datetime]	NOT NULL
  CONSTRAINT [PK_UserFollows] PRIMARY KEY CLUSTERED
(
	[UserFollowId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO