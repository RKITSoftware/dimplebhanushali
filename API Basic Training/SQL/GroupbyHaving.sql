use ddl;
SELECT
    dep01.p01f01 AS departmentId, 
    dep01.p01f02 AS departmentName,
     AVG(emp01.p01f05) AS avgSalary
FROM
    dep01
JOIN
    emp01 ON dep01.p01f02 = emp01.p01f04 
GROUP BY
     dep01.p01f02
HAVING
    AVG(emp01.p01f05) > 50000;
    
select * from emp01;
select * from dep01