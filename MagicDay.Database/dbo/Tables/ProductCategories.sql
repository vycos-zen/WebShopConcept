CREATE TABLE [dbo].[ProductCategories](
	[CategoryID] [uniqueidentifier] NOT NULL,
	[ParentCategoryID] [uniqueidentifier] NULL,
	[CategoryName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_ProductCategories] PRIMARY KEY CLUSTERED 
(
	[CategoryID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductCategories] ADD  CONSTRAINT [FK_ProductCategories_ParentCategory] FOREIGN KEY([ParentCategoryID])
REFERENCES [dbo].[ProductCategories] ([CategoryID])
GO

ALTER TABLE [dbo].[ProductCategories] CHECK CONSTRAINT [FK_ProductCategories_ParentCategory]