-- DROP PROCEDURE GetProductCountById
CREATE PROCEDURE GetProductCountById
(
@productId INT,
@count BIGINT OUTPUT
)
AS
BEGIN
	SELECT @count =
		SUM (po.ProductsCountDelta)  
	FROM vw_ProductsOperations po
	WHERE po.ProductId = @productId
	GROUP BY po.ProductId
END