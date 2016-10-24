CREATE TABLE [dbo].[Addresses](
	[AddressID] [uniqueidentifier] NOT NULL,
	[AddressName] [nvarchar](100) NOT NULL,
	[CountryID] [uniqueidentifier] NOT NULL,
	[Zip] [nvarchar](10) NOT NULL,
	[City] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Addresses] ADD  CONSTRAINT [FK_Addresses_Countries] FOREIGN KEY([CountryID])
REFERENCES [dbo].[Countries] ([CountryID])
GO

ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Countries]