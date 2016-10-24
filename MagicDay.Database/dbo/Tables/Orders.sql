CREATE TABLE [dbo].[Orders](
	[OrderID] [uniqueidentifier] NOT NULL,
	[UserID] [uniqueidentifier] NOT NULL,
	[OrderNo] [int] NULL,
	[OrderDate] [datetime] NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[BillingAddressID] [uniqueidentifier] NULL,
	[ShippingAddressID] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[OrderID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [FK_Orders_Billing] FOREIGN KEY([BillingAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Billing]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [FK_Orders_Shipping] FOREIGN KEY([ShippingAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Shipping]
GO
ALTER TABLE [dbo].[Orders] ADD  CONSTRAINT [FK_Orders_User] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([UserID])
GO

ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_User]