-- Create a new database (if not exists)
CREATE DATABASE DDL;

-- Use the created database
USE DDl;

-- Create an "employee" table
CREATE TABLE emp01  (
    emp01f01 INT PRIMARY KEY , -- employee_id
    emp01f02 VARCHAR(50), -- first_name
    emp01f03 VARCHAR(50),  -- last_name
    emp01f04 VARCHAR(100), -- department
    emp01f05 DECIMAL(10, 2) -- salary
);

-- Insert data into the "employee" table
INSERT INTO 
	emp01 
VALUES
	(1, 'Dimple', 'Mithiya', 'IT', 75000.00),
	(2, 'Pankaj', 'Mithiya', 'Marketing', 60000.00),
	(3, 'Ankit', 'Bhanushali','Sales', 55000.00);

-- View the data in the "employee" table
SELECT 
	emp01f01,
	emp01f02,
	emp01f03,
	emp01f04,
	emp01f05
FROM 
	emp01;
