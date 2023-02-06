CREATE DATABASE [Test_Invoice]
GO

USE [Test_Invoice]
GO

CREATE TABLE [CustomerTypes](
	[ID] INT IDENTITY PRIMARY KEY,
	[Description] VARCHAR(70) NOT NULL
)

GO
CREATE TABLE [Customers](
	[ID] INT IDENTITY PRIMARY KEY,
	[CustName] VARCHAR(70) NOT NULL,
	[Adress] VARCHAR(150) NOT NULL,
	[Status] BIT NOT NULL,
	[CustomerTypeID] INT NOT NULL,
	CONSTRAINT FK_Customers_CustomerTypeID FOREIGN KEY ([CustomerTypeID]) REFERENCES [CustomerTypes]([ID])
)
GO

CREATE TABLE [Invoice](
	[ID] INT IDENTITY PRIMARY KEY,
	[CustomerID] INT NOT NULL,
	[TotalItbis] MONEY NOT NULL,
	[SubTotal] MONEY NOT NULL,
	[Total] MONEY NOT NULL,
	CONSTRAINT FK_Invoice_CustomerID FOREIGN KEY ([CustomerID]) REFERENCES [CustomerS]([ID])
)
GO

CREATE TABLE [InvoiceDetail](
	[ID] INT IDENTITY PRIMARY KEY,
	[InvoiceID] INT NOT NULL,
	[TotalItbis] MONEY NOT NULL,
	[SubTotal] MONEY NOT NULL,
	[Total] MONEY NOT NULL,
	CONSTRAINT FK_InvoiceDetail_InvoiceID FOREIGN KEY ([InvoiceID]) REFERENCES [Invoice]([ID])
)
GO

