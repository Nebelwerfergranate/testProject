-- DROP VIEW vw_ProductsOperationsDetail
CREATE VIEW vw_ProductsOperationsDetail
AS
SELECT 
	po.Id AS Id
	, po.OperatorName AS OperatorName
	, po.OperationTypeId AS OperationTypeId
	, po.ProductsCount AS ProductsCount
	, po.DateTimeCreated AS DateTimeCreated
	, po.ProductsCountDelta AS ProductsCountDelta
	, p.Id AS ProductId
	, p.Name AS ProductName
	, p.Price AS ProductPrice
	, p.IsDeleted AS ProductIsDeleted
FROM vw_ProductsOperations po	
JOIN Products p ON p.Id = po.ProductId		