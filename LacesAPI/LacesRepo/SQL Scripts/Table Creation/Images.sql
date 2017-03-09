CREATE TABLE [dbo].[Images] (
	  [ImageId]				[int]			IDENTITY(1,1)	NOT FOR REPLICATION	NOT NULL
	, [AssociatedEntityId]	[int]			NOT NULL
	, [ImageEntityTypeId]	[int]			NOT NULL
	, [FilePath]			[varchar](100)	NOT NULL
	, [FileName]			[varchar](20)	NOT NULL
	, [FileFormat]			[varchar](20)	NOT NULL
	, [CreatedDate]			[DateTime]		NOT NULL
	, [UpdatedDate]			[DateTime]		NOT NULL
  CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED
(
	[ImageId] ASC
) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO