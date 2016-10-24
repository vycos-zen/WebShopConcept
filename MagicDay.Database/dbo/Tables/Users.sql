CREATE TABLE [dbo].[Users](
	[UserID] [uniqueidentifier] NOT NULL,
	[EmailAddress] [nvarchar](100) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[BillingAddressID] [uniqueidentifier] NULL,
	[ShippingAddressID] [uniqueidentifier] NULL,
	[PasswordHash] [varchar](88) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [FK_Users_Billing] FOREIGN KEY([BillingAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Billing]
GO
ALTER TABLE [dbo].[Users] ADD  CONSTRAINT [FK_Users_Shipping] FOREIGN KEY([ShippingAddressID])
REFERENCES [dbo].[Addresses] ([AddressID])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Shipping]