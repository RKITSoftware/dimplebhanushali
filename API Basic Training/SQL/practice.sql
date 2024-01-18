use practice;

Drop table student;

CREATE TABLE STUDENT(
	id INT PRIMARY KEY auto_increment,
    name varchar(50),
    age numeric(2),
    course varchar(10)
);

insert into Student (name,age,course)
values ('Abhay',23,'MCA'),
		('Pooja',26,'BSC'),
		('Kruser',26,'BSC'),
		('Hinal',26,'BSC'),
		('Jalpa',23,'MCA');

-- Limit 
select * from student where age > 23 limit 5;

SELECT * FROM Student  
ORDER BY age  LIMIT 2,3;  

select * from student limit 7;

-- Data Sorting

CREATE TABLE CUSTOMERS (
   ID INT primary key auto_increment,
   NAME VARCHAR (20) NOT NULL,
   AGE INT NOT NULL,
   ADDRESS CHAR (25),
   SALARY DECIMAL (18, 2)       
);

INSERT INTO CUSTOMERS (NAME,AGE,ADDRESS,SALARY) VALUES 
('Ramesh', 32, 'Ahmedabad', 2000),
('Dimple', 23, 'Mumbai', 1500),
('Ankit', 26, 'London', 1500),
('Khilan', 25, 'Delhi', 1500),
('kaushik', 23, 'Kota', 2000),
('Chaitali', 25, 'Mumbai', 6500),
('Hardik', 27, 'Bhopal', 8500),
('Komal', 22, 'Hyderabad', 4500),
('Muffy', 24, 'Indore', 10000);

-- sorting
select * from customers order by salary;

select * from customers order by salary desc;

select * from customers where salary > 5000 order by salary desc;

-- Null Value & Keyword

update customers 
set salary = null where id between 5 and 10;

-- Null Value & Keyword

select * from customers where salary is null;
select * from customers where salary is not null;

-- Aggregate Functions

-- Count
select count(*) from customers;
select count(*) from customers where salary is null;

-- Sum
select sum(salary) from customers;
select * from customers;

-- Min
select min(salary) from customers;

-- max
select max(salary) from customers;

-- Avg
select avg(salary) from customers;

-- Sub QUeries
select * from customers
where salary >= (select salary from customers where name = 'Ankit');

select * from customers where name = 'Ankit';

-- Joins

CREATE TABLE ORDERS (
   OID INT primary key,
   DATE VARCHAR (20) NOT NULL,
   CUSTOMER_ID INT NOT NULL,
   AMOUNT DECIMAL (18, 2)
);

INSERT INTO ORDERS VALUES 
(102, '2009-10-08 00:00:00', 3, 3000.00),
(103, '2009-10-09 00:00:00', 3, 1500.00),
(104, '2009-10-15 00:00:00', 3, 2500.00),
(105, '2009-10-13 00:00:00', 3, 3500.00),
(106, '2009-10-10 00:00:00', 3, 7500.00),
(107, '2009-11-21 00:00:00', 2, 4560.00),
(108, '2009-11-25 00:00:00', 2, 1560.00),
(109, '2009-11-27 00:00:00', 2, 8560.00),
(110, '2008-05-23 00:00:00', 4, 2560.00);

-- Inner Join
select id, name, amount 
from customers c inner join orders o
on c.id = o.CUSTOMER_ID;

-- left Join
select id, name, amount 
from customers c left join orders o
on c.id = o.CUSTOMER_ID;

-- right Join
select id, name, amount 
from customers c right join orders o
on c.id = o.CUSTOMER_ID;

-- UNION
SELECT  ID, NAME, AMOUNT, DATE
   FROM CUSTOMERS
   LEFT JOIN ORDERS
   ON CUSTOMERS.ID = ORDERS.CUSTOMER_ID
UNION ALL
   SELECT  ID, NAME, AMOUNT, DATE
   FROM CUSTOMERS
   RIGHT JOIN ORDERS
   ON CUSTOMERS.ID = ORDERS.CUSTOMER_ID;
   
-- Select   
select 
	*
from
	CUSTOMERS;

-- Views
CREATE VIEW CUSTOMERS_VIEW AS
SELECT name, age,salary
FROM  CUSTOMERS;