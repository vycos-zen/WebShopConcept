CREATE TABLE [dbo].[ProductImages](
	[ProductImageID] [uniqueidentifier] NOT NULL,
	[ProductID] [uniqueidentifier] NOT NULL,
	[ImageNo] [tinyint] NOT NULL,
	[ImageDesciption] [nvarchar](100) NULL,
	[Image] [varbinary](max) NOT NULL,
	[ImageThumbnail] [varbinary](max) NOT NULL,
	[ImageMimeType] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ProductImages] PRIMARY KEY CLUSTERED 
(
	[ProductImageID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductImages] ADD  CONSTRAINT [FK_ProductImages_Product] FOREIGN KEY([ProductID])
REFERENCES [dbo].[Products] ([ProductID])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ProductImages] CHECK CONSTRAINT [FK_ProductImages_Product]