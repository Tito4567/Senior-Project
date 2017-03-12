CREATE TABLE [dbo].[Conditions] (
	  [ConditionId]	[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [Name]		[varchar](40)	NOT NULL
  CONSTRAINT [PK_Conditions] PRIMARY KEY CLUSTERED
(
	[ConditionId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

INSERT INTO Conditions
(
	[Name]
)
VALUES
(
	'Unkown'
)
GO

INSERT INTO Conditions
(
	[Name]
)
VALUES
(
	'New'
)
GO

INSERT INTO Conditions
(
	[Name]
)
VALUES
(
	'Used'
)
GO

INSERT INTO Conditions
(
	[Name]
)
VALUES
(
	'Damaged'
)
GO