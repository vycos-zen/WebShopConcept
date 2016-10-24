CREATE VIEW [dbo].[ProductView] AS
SELECT
p.ProductID AS ID, 
 p.ProductCode AS Code, 
 p.ProductName AS Name, 
 p.Description AS Description,
 p.Price,
 c.CategoryName AS Category,
 COUNT(i.ProductImageID) AS ImageCount 
FROM Products AS p
join ProductCategories AS c on p.CategoryID = c.CategoryID
left outer join ProductImages AS i on p.ProductID = i.ProductID
where p.ProductID is not null
GROUP BY p.ProductID, p.ProductCode, p.ProductName, p.Description, p.Price, c.CategoryName
