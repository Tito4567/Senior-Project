CREATE TABLE [dbo].[Transactions] (
	  [TransactionId]	[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [SellerId]		[int]			NOT NULL
	, [BuyerId]			[int]			NOT NULL
	, [ProductId]		[int]			NOT NULL
	, [Amount]			[decimal](9,2)
	, [ReferenceNumber]	[varchar](50)
	, [CreatedDate]		[datetime]		NOT NULL
  CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED
(
	[TransactionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO