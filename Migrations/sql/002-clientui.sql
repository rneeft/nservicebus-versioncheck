CREATE TABLE [ClientUIEndpoint](
	[Id] [uniqueidentifier] NOT NULL,
	[CorrelationId] [varchar](255) NULL,
	[ReplyToAddress] [varchar](255) NULL,
	[Recoverable] [bit] NOT NULL,
	[Expires] [datetime] NULL,
	[Headers] [nvarchar](max) NOT NULL,
	[Body] [varbinary](max) NULL,
	[RowVersion] [bigint] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE TABLE [ClientUIEndpoint.Delayed](
	[Headers] [nvarchar](max) NOT NULL,
	[Body] [varbinary](max) NULL,
	[Due] [datetime] NOT NULL,
	[RowVersion] [bigint] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_ClientUIEndpoint_RowVersion
ON ClientUIEndpoint (RowVersion);
GO

CREATE NONCLUSTERED INDEX IX_ClientUIEndpoint_Delayed_RowVersion
ON [ClientUIEndpoint.Delayed] (RowVersion);
GO