use ddl;
-- explain 
explain select * from emp01;

-- in tree format shows us the query plan and cost estimates
explain format=TREE
(SELECT 
		emp01f02,
		emp01f05 
	FROM 
		emp01 
	WHERE 
		emp01f05 > 
			(SELECT 
				AVG(emp01f05) 
			FROM 
			emp01	emp01)) ;
-- EXPLAIN ANALYZE will do if those estimates are correct, or on which operations in the query plan the time is actually spent
explain analyze
(SELECT 
		emp01f02,
		emp01f05 
	FROM 
		emp01 
	WHERE 
		emp01f05 > 
			(SELECT 
				AVG(emp01f05) 
			FROM 
				emp01)) 
