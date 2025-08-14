-- Step 1: Create Employees table
-- Creates a new table named Employees with columns ID, Name, and Salary
CREATE TABLE Employees
(
ID INT PRIMARY KEY,
Name VARCHAR(100),
Salary DECIMAL(10, 2)
);

-- Step 2: Add Department column to Employees
-- Adds a column named Department to the Employees table
ALTER TABLE Employees
ADD Department VARCHAR(50);

-- Step 3: Remove Salary column from Employees
-- Removes the Salary column from the Employees table
ALTER TABLE Employees
DROP COLUMN Salary;

-- Step 4: Rename Department column to DeptName
-- Renames the Department column in the Employees table to DeptName
EXEC sp_rename
'Employees.Department',
'DeptName',
'COLUMN';

-- Step 5: Create Projects table
-- Creates a new table named Projects with columns ProjectID and ProjectName
CREATE TABLE Projects
(
ProjectID INT PRIMARY KEY,
ProjectName VARCHAR(100)
);

-- Step 6: Add Primary Key to Employees (only if not already defined)
-- Adds a primary key constraint on the ID column in the Employees table
ALTER TABLE Employees
ADD CONSTRAINT PK_Employees_ID PRIMARY KEY (ID);

-- Step 7: Add Unique constraint on Name in Employees
-- Adds a unique constraint on the Name column in the Employees table
ALTER TABLE Employees
ADD CONSTRAINT UQ_Employees_Name UNIQUE (Name);

-- Step 8: Create Customers table
-- Creates a new table named Customers with various customer details
CREATE TABLE Customers
(
CustomerID INT PRIMARY KEY,
FirstName VARCHAR(50),
LastName VARCHAR(50),
Email VARCHAR(100),
Status VARCHAR(20)
);

-- Step 9: Add Unique constraint on FirstName and LastName in Customers
-- Adds a unique constraint on the combination of FirstName and LastName in the Customers table
ALTER TABLE Customers
ADD CONSTRAINT UQ_Customers_FirstLastName UNIQUE (FirstName, LastName);

-- Step 10: Create Orders table
-- Creates a new table named Orders with foreign key relation to Customers
CREATE TABLE Orders
(
OrderID INT PRIMARY KEY,
CustomerID INT,
OrderDate DATETIME,
TotalAmount DECIMAL(10, 2),
FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

-- Step 11: Add Check constraint on TotalAmount in Orders
-- Ensures TotalAmount is always greater than zero in the Orders table
ALTER TABLE Orders
ADD CONSTRAINT CHK_Orders_TotalAmount_Positive
CHECK (TotalAmount > 0);

-- Step 12: Create Sales schema and move Orders table into it
-- Creates the Sales schema and moves the Orders table into it
CREATE SCHEMA Sales;
ALTER SCHEMA Sales
TRANSFER dbo.Orders;

-- Step 13: Rename Orders table to SalesOrders
-- Renames the Orders table to SalesOrders within the Sales schema
EXEC sp_rename
'Sales.Orders',
'SalesOrders';