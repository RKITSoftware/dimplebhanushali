-- MySQL dump 10.13  Distrib 8.0.34, for Win64 (x86_64)
--
-- Host: 127.0.0.2    Database: ddl
-- ------------------------------------------------------
-- Server version	8.0.31

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `emp01`
--

DROP TABLE IF EXISTS `emp01`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `emp01` (
  `p01f01` int NOT NULL AUTO_INCREMENT COMMENT 'Employee_id',
  `p01f02` varchar(15) DEFAULT NULL COMMENT 'Employee_First_Name',
  `p01f03` varchar(15) DEFAULT NULL COMMENT 'Employee_Last_Name',
  `p01f04` varchar(50) DEFAULT NULL COMMENT 'Employee_Department',
  `p01f05` decimal(10,2) DEFAULT NULL COMMENT 'Employee_Salary',
  PRIMARY KEY (`p01f01`),
  KEY `idx_firstname` (`p01f02`),
  KEY `idx_p01_f02_f03` (`p01f02`,`p01f03`)
) ENGINE=MyISAM AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='Employee';
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `emp01`
--

LOCK TABLES `emp01` WRITE;
/*!40000 ALTER TABLE `emp01` DISABLE KEYS */;
INSERT INTO `emp01` VALUES (1,'Dimple','Mithiya','IT',60000.00),(2,'Pankaj','Mithiya','Marketing',60000.00),(3,'Ankit','Bhanushali','Sales',55000.00),(4,'Kamal','Chavda','IT',75000.00),(8,'abc','xyz','IT',30000.00),(9,'abc','xyz','IT',30000.00);
/*!40000 ALTER TABLE `emp01` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-01-03 11:39:02
