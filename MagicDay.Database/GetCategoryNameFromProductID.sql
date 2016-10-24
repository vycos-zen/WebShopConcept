CREATE PROCEDURE [dbo].[GetCategoryNameFromProductID]
	@ProductID uniqueidentifier
AS
	SET NOCOUNT ON;
	select ProductCategories.CategoryName
from ProductCategories
join Products on Products.CategoryID = ProductCategories.CategoryID
where ProductID = @ProductID

