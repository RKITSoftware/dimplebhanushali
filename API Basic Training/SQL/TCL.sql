use DDL;
-- Start a transaction
START TRANSACTION;

-- Update an employee's salary
UPDATE 
	emp01 
SET 
	emp01f05 = 60000  -- salary
WHERE 
	emp01f01 = 1; -- id

-- Savepoint to mark a specific point within the transaction
SAVEPOINT before_update;

-- Attempt to update another employee's salary
INSERT INTO 
	emp01 
VALUES
	(4, 'Kamal', 'Chavda', 'IT', 75000.00);

-- If there's an issue, rollback to the savepoint
ROLLBACK to before_update;
-- Alternatively, you can use ROLLBACK; to undo the entire transaction
select * from emp01;
-- End the transaction
COMMIT;
