CREATE TABLE [dbo].[ProductTypes] (
	  [ProductTypeId]	[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [Name]			[varchar](40)	NOT NULL
  CONSTRAINT [PK_ProductTypes] PRIMARY KEY CLUSTERED
(
	[ProductTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO ProductTypes
(
	[Name]
)
VALUES
(
	'Unkown'
)
GO

INSERT INTO ProductTypes
(
	[Name]
)
VALUES
(
	'Hat'
)
GO

INSERT INTO ProductTypes
(
	[Name]
)
VALUES
(
	'Shoes'
)
GO