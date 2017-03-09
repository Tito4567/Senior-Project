CREATE TABLE [dbo].[ImageEntityTypes] (
	  [ImageEntityTypeId]	[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [Name]				[varchar](40)	NOT NULL
  CONSTRAINT [PK_ImageEntityTypes] PRIMARY KEY CLUSTERED
(
	[ImageEntityTypeId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO ImageEntityTypes
(
	[Name]
)
VALUES
(
	'Unkown'
)
GO

INSERT INTO ImageEntityTypes
(
	[Name]
)
VALUES
(
	'User'
)
GO

INSERT INTO ImageEntityTypes
(
	[Name]
)
VALUES
(
	'Product'
)
GO