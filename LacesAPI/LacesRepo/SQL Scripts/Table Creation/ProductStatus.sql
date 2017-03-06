CREATE TABLE [dbo].[ProductStatus] (
	  [ProductStatusId]	[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [Status]			[varchar](40)	NOT NULL
  CONSTRAINT [PK_ProductStatus] PRIMARY KEY CLUSTERED
(
	[ProductStatusId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO ProductStatus
(
	[Status]
)
VALUES
(
	'Unkown'
)
GO

INSERT INTO ProductStatus
(
	[Status]
)
VALUES
(
	'Available'
)
GO

INSERT INTO ProductStatus
(
	[Status]
)
VALUES
(
	'Sold out'
)
GO

INSERT INTO ProductStatus
(
	[Status]
)
VALUES
(
	'Removed'
)
GO