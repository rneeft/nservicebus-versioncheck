INSERT INTO [dbo].[Subscription]
           ([QueueAddress]
           ,[Endpoint]
           ,[Topic])
     VALUES
           ('PlaceOrderEndpoint'
           ,'PlaceOrderEndpoint'
           ,'Messages.PlaceOrderV2')
GO