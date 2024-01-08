-- Create a sample database
CREATE DATABASE IF NOT EXISTS final_demo;
USE final_demo;

-- Create an employee table with primary key and auto-increment
CREATE TABLE Employee (
  'EmployeeID' INT NOT NULL AUTO_INCREMENT COMMENT 'Employee ID',
  'EmployeeName' VARCHAR(50) NULL COMMENT 'Employee Name',
  'EmployeeRole' VARCHAR(50) NULL COMMENT 'Employee Role',
  PRIMARY KEY ('EmployeeID')
);

-- Create a task assignment table with a foreign key referencing the employee table
CREATE TABLE TaskAssignment (
  'TaskID' INT NOT NULL AUTO_INCREMENT COMMENT 'Task ID',
  'TaskDescription' VARCHAR(100) NULL COMMENT 'Task Description',
  'TaskDeadline' DATE NULL COMMENT 'Task Deadline',
  'EmployeeID' INT NULL COMMENT 'Employee ID',
  PRIMARY KEY ('TaskID'),
  FOREIGN KEY ('EmployeeID') REFERENCES 'Employee' ('EmployeeID')
);

-- Insert data into the employee table
INSERT INTO 'Employee' ('EmployeeName', 'EmployeeRole') VALUES ('John Doe', 'Manager');
INSERT INTO 'Employee' ('EmployeeName', 'EmployeeRole') VALUES ('Jane Smith', 'Developer');
INSERT INTO 'Employee' ('EmployeeName', 'EmployeeRole') VALUES ('Bob Johnson', 'Designer');
INSERT INTO 'Employee' ('EmployeeName', 'EmployeeRole') VALUES ('Alice Brown', 'Tester');
INSERT INTO 'Employee' ('EmployeeName', 'EmployeeRole') VALUES ('Charlie Green', 'Analyst');

-- Insert data into the task assignment table
INSERT INTO TaskAssignment ('TaskDescription', 'TaskDeadline', 'EmployeeID') VALUES ('Project A Design', '2024-01-15', 2);
INSERT INTO TaskAssignment ('TaskDescription', 'TaskDeadline', 'EmployeeID') VALUES ('Code Refactoring', '2024-01-20', 3);
INSERT INTO TaskAssignment ('TaskDescription', 'TaskDeadline', 'EmployeeID') VALUES ('UI/UX Enhancement', '2024-02-01', 4);
INSERT INTO TaskAssignment ('TaskDescription', 'TaskDeadline', 'EmployeeID') VALUES ('Testing Phase 1', '2024-01-02', 2);

-- Read data from the employee table
SELECT 'EmployeeID', 'EmployeeName', 'EmployeeRole' FROM 'Employee';

-- Read data from the task assignment table
SELECT 'TaskID', 'TaskDescription', 'TaskDeadline', 'EmployeeID' FROM TaskAssignment;

-- Select data from task assignment table with employee information
SELECT
    t.'TaskID',
    t.'TaskDescription',
    t.'TaskDeadline',
    e.'EmployeeName',
    e.'EmployeeRole'
FROM TaskAssignment t
INNER JOIN 'Employee' e ON t.'EmployeeID' = e.'EmployeeID'
LIMIT 2, 1;

-- Update data in employee table
UPDATE 'Employee' SET 'EmployeeRole' = 'Supervisor' WHERE 'EmployeeID' = 1;

-- Update data in task assignment table
UPDATE TaskAssignment SET 'TaskDeadline' = '2024-01-25' WHERE 'TaskID' = 1;

-- Delete data from employee table
DELETE FROM 'Employee' WHERE 'EmployeeID' = 1;

-- Delete data from task assignment table
DELETE FROM TaskAssignment WHERE 'TaskID' = 1;

-- Subquery using Aggregate Function
CREATE VIEW 'vws_Employee' AS
SELECT AVG('avgTasks')
FROM (
    SELECT e.EmployeeID AS 'employee_id', COUNT(t.TaskID) AS 'avgTasks'
    FROM Employee e
    JOIN TaskAssignment t ON e.EmployeeID = t.EmployeeID
    GROUP BY e.EmployeeID
    HAVING COUNT(t.TaskID) > 2
) AS 'subquery';

SELECT * FROM 'vws_Employee';

-- Create an index on EmployeeName column
CREATE INDEX 'idx_e_employee_name' ON 'Employee' ('EmployeeName');

-- Drop the index on EmployeeName column
DROP INDEX 'idx_e_employee_name' ON 'Employee';

-- Update data in employee table
UPDATE 'Employee' SET 'EmployeeRole' = 'Lead Developer' WHERE 'EmployeeName' = 'Jane Smith';

-- Savepoint after Employee Update
SAVEPOINT after_employee_update;

-- Update data in task assignment table
UPDATE 'Employee' SET 'EmployeeRole' = 'Developer' WHERE 'EmployeeName' = 'Jane Smith';

-- Commit changes so far
COMMIT;

-- Delete data from employee table
DELETE FROM 'Employee' WHERE 'EmployeeName' = 'Charlie Green';

-- Rollback to the savepoint after Employee Update
ROLLBACK TO SAVEPOINT after_employee_update;

-- Final Transaction End: Rollback or Commit
COMMIT;

-- Read data from task assignment table
SELECT * FROM TaskAssignment;

-- Select data from task assignment table with employee information
SELECT
    e.'EmployeeName' AS 'employee_name',
    e.'EmployeeRole' AS 'employee_role',
    t.'TaskDescription' AS 'task_description',
    t.'TaskDeadline' AS 'task_deadline'
FROM TaskAssignment t
JOIN 'Employee' e ON t.'EmployeeID' = e.'EmployeeID';
