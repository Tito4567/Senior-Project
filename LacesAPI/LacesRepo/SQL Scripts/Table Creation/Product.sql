CREATE TABLE [dbo].[Products] (
	  [ProductId]		[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [Name]			[varchar](40)	NOT NULL
	, [Description]		[varchar](max)	NOT NULL
	, [SellerId]		[int]			NOT NULL
	, [AskingPrice]		[decimal](9,2)
	, [ProductStatusId]	[int]			NOT NULL
	, [Brand]			[varchar](30)
	, [Size]			[varchar](10)
	, [ProductTypeId]	[int]			NOT NULL
	, [ConditionId]		[int]
	, [CreatedDate]		[datetime]		NOT NULL
	, [UpdatedDate]		[datetime]		NOT NULL
  CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED
(
	[ProductId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO