CREATE VIEW [dbo].[CategoryView] AS
SELECT 
c.CategoryID AS ID,
c.CategoryName AS Category,
pa.CategoryName AS ParentCategory,
COUNT(pr.ProductID) AS ProductCount
FROM ProductCategories AS c
left outer join ProductCategories AS pa ON  pa.CategoryID = c.ParentCategoryID
left outer join Products AS pr ON pr.CategoryID = c.CategoryID
where c.CategoryID is not null
GROUP BY c.CategoryID, c.CategoryName, pa.CategoryName