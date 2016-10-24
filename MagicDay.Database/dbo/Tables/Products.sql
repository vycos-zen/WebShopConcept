CREATE TABLE [dbo].[Products](
	[ProductID] [uniqueidentifier] NOT NULL,
	[CategoryID] [uniqueidentifier] NOT NULL,
	[ProductCode] [nvarchar](20) NOT NULL,
	[ProductName] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](1000) NULL,
	[Price] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK__Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [FK_Products_Category] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[ProductCategories] ([CategoryID])
GO

ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Category]