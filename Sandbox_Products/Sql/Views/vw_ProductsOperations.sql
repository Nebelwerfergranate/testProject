-- DROP VIEW vw_ProductsOperations
CREATE VIEW vw_ProductsOperations
AS
SELECT 
	po.Id AS Id
	, po.ProductId AS ProductId
	, po.OperatorName AS OperatorName
	, po.OperationTypeId AS OperationTypeId
	, po.ProductsCount AS ProductsCount
	, po.DateTimeCreated AS DateTimeCreated
	, CASE
		WHEN po.OperationTypeId = 'receipt' THEN po.ProductsCount
		WHEN po.OperationTypeId = 'expense' THEN (po.ProductsCount * -1)
	END	AS ProductsCountDelta
FROM ProductsOperations po			