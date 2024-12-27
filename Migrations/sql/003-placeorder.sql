CREATE TABLE [PlaceOrderEndpoint](
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


CREATE TABLE [PlaceOrderEndpoint.Delayed](
	[Headers] [nvarchar](max) NOT NULL,
	[Body] [varbinary](max) NULL,
	[Due] [datetime] NOT NULL,
	[RowVersion] [bigint] IDENTITY(1,1) NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

CREATE NONCLUSTERED INDEX IX_PlaceOrderEndpoint_RowVersion
ON PlaceOrderEndpoint (RowVersion);
GO

CREATE NONCLUSTERED INDEX IX_PlaceOrderEndpoint_Delayed_RowVersion
ON [PlaceOrderEndpoint.Delayed] (RowVersion);
GO

INSERT INTO [dbo].[Subscription]
           ([QueueAddress]
           ,[Endpoint]
           ,[Topic])
     VALUES
           ('PlaceOrderEndpoint'
           ,'PlaceOrderEndpoint'
           ,'Messages.PlaceOrder')
GO