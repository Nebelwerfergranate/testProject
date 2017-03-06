--DROP TABLE Logs
CREATE TABLE Logs (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Text NVARCHAR(MAX),
	DateTimeCreated DATETIME NOT NULL,
	Handled BIT NOT NULL 
);

--DROP TABLE Products
CREATE TABLE Products (
	Id INT IDENTITY(1,1) PRIMARY KEY,
	Name NVARCHAR(50) UNIQUE NOT NULL,
	Price DECIMAL(7,2) NOT NULL,
	IsDeleted BIT NOT NULL DEFAULT(0)
);

--DROP TABLE ProductsOperations
CREATE TABLE ProductsOperations(
	Id INT IDENTITY(1,1) PRIMARY KEY,
	ProductId INT NOT NULL,
	OperatorName NVARCHAR(255) NOT NULL,
	OperationTypeId NVARCHAR(10) NOT NULL,
	ProductsCount SMALLINT NOT NULL,
	DateTimeCreated DATETIME NOT NULL
);

--DROP TABLE ProductOperationTypes
CREATE TABLE ProductsOperationsTypes(
	Id NVARCHAR(10) PRIMARY KEY,
	DisplayName NVARCHAR(50),
	DisplayOrder INT,
	DisplayColor VARCHAR(20)
);

ALTER TABLE ProductsOperations
ADD CONSTRAINT fk_ProductOperations_Products
FOREIGN KEY (ProductId)
REFERENCES Products(Id);

ALTER TABLE ProductsOperations
ADD CONSTRAINT fk_ProductsOperations_ProductsOperationsTypes
FOREIGN KEY (OperationTypeId)
REFERENCES ProductsOperationsTypes(Id);

INSERT INTO ProductsOperationsTypes VALUES('receipt', 'Receipt');
INSERT INTO ProductsOperationsTypes VALUES('expense', 'Expense');