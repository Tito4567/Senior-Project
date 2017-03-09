CREATE TABLE [dbo].[Comments] (
	[CommentId]		[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [UserId]		[int]			NOT NULL
	, [ProductId]	[int]			NOT NULL
	, [Text]		[varchar](max)	NOT NULL
	, [CreatedDate]	[datetime]		NOT NULL
	, [UpdatedDate]	[datetime]		NOT NULL
  CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED
(
	[CommentId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO