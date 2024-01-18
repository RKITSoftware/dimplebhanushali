use employees;
-- subquery
 
	(SELECT 
		p01f01,
		p01f02 
	FROM 
		emp01 
	WHERE 
		p01f01 > 
			(SELECT 
				AVG(p01f01) 
			FROM 
				emp01)) 
	
