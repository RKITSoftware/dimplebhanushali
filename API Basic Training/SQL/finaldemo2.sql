create database hospitalmanagement;
use hospitalmanagement;

-- Create Ward Table
-- drop table wrd01;
CREATE TABLE wrd01 (
  d01f01 INT NOT NULL AUTO_INCREMENT COMMENT 'ward_id' ,
  d01f02 INT NOT NULL COMMENT 'ward_number',
  d01f03 VARCHAR(50) NOT NULL COMMENT 'ward_type',
  d01f04 INT NOT NULL COMMENT 'capacity',
  PRIMARY KEY (d01f01)
)
COMMENT = 'WardTable';

-- Create Patient Table
CREATE TABLE ptnt01 (
  t01f01 INT NOT NULL AUTO_INCREMENT COMMENT 'patient_id',
  t01f02 VARCHAR(50) NOT NULL COMMENT 'first_name',
  t01f03 VARCHAR(50) NOT NULL COMMENT 'last_name',
  t01f04 VARCHAR(100) COMMENT 'email',
  t01f05 VARCHAR(15) COMMENT 'phone_number',
  PRIMARY KEY (t01f01)
)
COMMENT = 'PatientTable';

-- Create Appointment Table
CREATE TABLE apmt01 (
  t01f01 INT NOT NULL AUTO_INCREMENT COMMENT 'appointment_id ',
  t01f02 INT NOT NULL COMMENT 'patient_id ',
  t01f03 INT NOT NULL COMMENT 'ward_id ',
  t01f04 DATE NOT NULL COMMENT 'appointment_date',
  t01f05 TIME NOT NULL COMMENT 'appointment_time',
  PRIMARY KEY (t01f01),
  FOREIGN KEY (t01f02) REFERENCES ptnt01(t01f01),
  FOREIGN KEY (t01f03) REFERENCES wrd01(d01f01)
)
COMMENT = 'AppointmentTable';

-- Insert Sample Data into Wards Table
INSERT INTO wrd01 (D01F02, d01f03, d01f04) VALUES
(101, 'General', 20),
(102, 'ICU', 10),
(201, 'Maternity', 15);

-- Insert Sample Data into Patient Table
INSERT INTO ptnt01 (t01f02, t01f03, t01f04, t01f05) VALUES
('Dimple', 'Mithiya', 'dimple@example.com', '123-456-7890'),
('Pankaj', 'Mithiya', 'Pankaj@example.com', '987-654-3210');

-- Insert Sample Data into Reservations Table
INSERT INTO apmt01 (t01f02, t01f03, t01f04, t01f05) VALUES
(1, 1, '2024-01-01', '09:00:00'),
(1, 1, '2024-01-15', '14:30:00'),
(2, 3, '2024-02-10', '10:00:00');

-- Retrieve all rooms
SELECT 
	d01f01,
    d01f02,
    d01f03,
    d01f04
FROM 
	wrd01;

-- Retrieve all guests
SELECT 
	t01f01,
    t01f02,
    t01f03,
    t01f04,
    t01f05
FROM ptnt01;


-- Update check-out date for reservation with ID 1
UPDATE apmt01
SET t01f05 = '10:30:00'
WHERE t01f01 = 1;

-- Delete or cancle reservation with ID 2

DELETE FROM apmt01
WHERE t01f01 = 2;


-- Retrieve all reservations with guest information
SELECT 
	apmt01.*, 
    ptnt01.t01f02 AS patient_first_name, 
    ptnt01.t01f03 AS patient_last_name
FROM 
	apmt01
JOIN 
	ptnt01 
ON 
	apmt01.t01f02 = ptnt01.t01f01
LIMIT 
	1,2;


-- Retrieve the count of reservations for each day in the first month with guest details
create view vws_reservationDetails2 AS
SELECT
  apmt01.t01f04 AS reservation_date,
  ptnt01.t01f02 AS patient_first_name,
  ptnt01.t01f03 AS patient_last_name,
  DATEDIFF(apmt01.t01f05, apmt01.t01f04) AS remaining_hours_in_day
FROM
  apmt01
JOIN
  ptnt01 ON apmt01.t01f02 = ptnt01.t01f01
WHERE
  MONTH(apmt01.t01f04) = 1 -- Assuming January is the first month
GROUP BY
  apmt01.t01f04, ptnt01.t01f01;

select * from vws_appointmentDetails;

drop view vws_appointmentDetails2;
