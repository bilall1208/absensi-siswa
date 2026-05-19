-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: May 19, 2026 at 02:05 AM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `school_attendance`
--

-- --------------------------------------------------------

--
-- Table structure for table `tabsensi`
--

CREATE TABLE `tabsensi` (
  `IDAbsen` int(11) NOT NULL,
  `Tanggal` date NOT NULL,
  `Idp` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tabsensi`
--

INSERT INTO `tabsensi` (`IDAbsen`, `Tanggal`, `Idp`) VALUES
(1, '2026-04-06', 1),
(2, '2026-04-07', 2),
(3, '2026-05-02', 2),
(4, '2026-05-02', 2),
(5, '2026-05-07', 2),
(6, '2026-05-07', 2),
(7, '2026-05-01', 1),
(8, '2026-05-01', 1),
(9, '2026-05-07', 2),
(10, '2026-05-07', 2),
(11, '2026-05-07', 2),
(12, '2026-05-07', 2),
(13, '2026-05-07', 4),
(14, '2026-05-06', 4),
(15, '2026-05-05', 4),
(16, '2026-05-08', 2),
(17, '2026-05-12', 4),
(18, '2026-05-12', 9),
(19, '2026-05-13', 8),
(20, '2026-05-07', 3),
(21, '2026-05-13', 3);

-- --------------------------------------------------------

--
-- Table structure for table `tdetailabsensi`
--

CREATE TABLE `tdetailabsensi` (
  `ID` int(11) NOT NULL,
  `IDAbsen` int(11) NOT NULL,
  `NISN` varchar(20) NOT NULL,
  `Hadir` int(11) NOT NULL,
  `Sakit` int(11) NOT NULL,
  `Izin` int(11) NOT NULL,
  `Alpa` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tdetailabsensi`
--

INSERT INTO `tdetailabsensi` (`ID`, `IDAbsen`, `NISN`, `Hadir`, `Sakit`, `Izin`, `Alpa`) VALUES
(25, 13, '897653712', 1, 0, 0, 0),
(26, 13, '896543786', 0, 0, 0, 1),
(27, 14, '897653712', 1, 0, 0, 0),
(28, 14, '896543786', 0, 1, 0, 0),
(29, 15, '897653712', 1, 0, 0, 0),
(30, 15, '896543786', 1, 0, 0, 0),
(31, 16, '897653712', 1, 0, 0, 0),
(32, 16, '896543786', 0, 1, 0, 0),
(33, 17, '895555555', 1, 0, 0, 0),
(34, 17, '891018091', 0, 1, 0, 0),
(35, 18, '898976789', 0, 1, 0, 0),
(36, 19, '897653712', 0, 1, 0, 0),
(37, 19, '896543786', 0, 1, 0, 0),
(38, 20, '895555555', 1, 0, 0, 0),
(39, 20, '891018091', 1, 0, 0, 0),
(40, 21, '895555555', 1, 0, 0, 0),
(41, 21, '891018091', 1, 0, 0, 0),
(42, 19, '8976534256', 1, 0, 0, 0),
(43, 19, '890653087', 1, 0, 0, 0),
(44, 19, '891624536', 0, 0, 1, 0),
(45, 19, '897653764', 1, 0, 0, 0),
(46, 19, '896753462', 1, 0, 0, 0),
(47, 19, '896342571', 1, 0, 0, 0);

-- --------------------------------------------------------

--
-- Table structure for table `tkelas`
--

CREATE TABLE `tkelas` (
  `IDK` int(11) NOT NULL,
  `Kelas` varchar(20) NOT NULL,
  `Wali_Kelas` varchar(50) NOT NULL,
  `IDP` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tkelas`
--

INSERT INTO `tkelas` (`IDK`, `Kelas`, `Wali_Kelas`, `IDP`) VALUES
(0, 'I', '3 - Pak Taryat', 3),
(1, 'II', '4 - Pak Budi', 4),
(2, 'III', '5 - Bu Siti', 5),
(3, 'IV', '6 - Bu Eka', 6),
(4, 'V', '7 - Pak Eko', 7),
(5, 'VI', '8 - Pak Asep', 8);

-- --------------------------------------------------------

--
-- Table structure for table `tpetugas`
--

CREATE TABLE `tpetugas` (
  `IDP` int(11) NOT NULL,
  `Nama` varchar(20) NOT NULL,
  `username` varchar(50) NOT NULL,
  `password` varchar(50) NOT NULL,
  `role` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tpetugas`
--

INSERT INTO `tpetugas` (`IDP`, `Nama`, `username`, `password`, `role`) VALUES
(2, 'Bilal', 'admin1', '1', 'Admin'),
(3, 'Pak Taryat', 'guru1', '1', 'Guru'),
(4, 'Pak Budi', 'guru2', '2', 'Guru'),
(5, 'Bu Siti', 'guru3', '3', 'Guru'),
(6, 'Bu Eka', 'guru4', '4', 'Guru'),
(7, 'Pak Eko', 'guru5', '5', 'Guru'),
(8, 'Pak Asep', 'guru6', '6', 'Guru');

-- --------------------------------------------------------

--
-- Table structure for table `tsiswa`
--

CREATE TABLE `tsiswa` (
  `NISN` varchar(20) NOT NULL,
  `Nama` varchar(20) NOT NULL,
  `Alamat` text NOT NULL,
  `IDK` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `tsiswa`
--

INSERT INTO `tsiswa` (`NISN`, `Nama`, `Alamat`, `IDK`) VALUES
('890653087', 'Gaza', 'Melong', 5),
('891018091', 'Keisha', 'Bandung', 0),
('891624536', 'Gina', 'Haji Gofur', 5),
('892222222', 'Bilal', 'Cikuya', 3),
('892923741', 'Adam', 'Cimindi', 1),
('893333333', 'Citra', 'Cibeber', 4),
('895555555', 'Dicky', 'Utama', 0),
('896342571', 'Sapta', 'Batujajar', 5),
('896542873', 'Widodo', 'Solo', 4),
('896543786', 'Sintya', 'Padasuka', 5),
('896666666', 'Rini', 'Cibogo', 1),
('896753462', 'Mufti', 'Cibabat', 5),
('897653421', 'Gibran', 'Solo', 3),
('8976534256', 'Ajmi', 'Cimareme', 5),
('897653712', 'Roni', 'Leuwigajah', 5),
('897653764', 'Kendra', 'Cibeber', 5),
('897654362', 'Yudi', 'Cimahi', 2),
('897777777', 'Nina', 'Cibodas', 2);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `tabsensi`
--
ALTER TABLE `tabsensi`
  ADD PRIMARY KEY (`IDAbsen`);

--
-- Indexes for table `tdetailabsensi`
--
ALTER TABLE `tdetailabsensi`
  ADD PRIMARY KEY (`ID`);

--
-- Indexes for table `tkelas`
--
ALTER TABLE `tkelas`
  ADD PRIMARY KEY (`IDK`);

--
-- Indexes for table `tpetugas`
--
ALTER TABLE `tpetugas`
  ADD PRIMARY KEY (`IDP`);

--
-- Indexes for table `tsiswa`
--
ALTER TABLE `tsiswa`
  ADD PRIMARY KEY (`NISN`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `tabsensi`
--
ALTER TABLE `tabsensi`
  MODIFY `IDAbsen` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `tdetailabsensi`
--
ALTER TABLE `tdetailabsensi`
  MODIFY `ID` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- AUTO_INCREMENT for table `tpetugas`
--
ALTER TABLE `tpetugas`
  MODIFY `IDP` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
