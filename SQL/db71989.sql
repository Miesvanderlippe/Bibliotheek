-- phpMyAdmin SQL Dump
-- version 4.0.4.2
-- http://www.phpmyadmin.net
--
-- Host: localhost
-- Generation Time: Oct 28, 2014 at 10:20 PM
-- Server version: 5.6.13
-- PHP Version: 5.4.17

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;

--
-- Database: `db71989`
--
CREATE DATABASE IF NOT EXISTS `db71989` DEFAULT CHARACTER SET latin1 COLLATE latin1_swedish_ci;
USE `db71989`;

-- --------------------------------------------------------

--
-- Table structure for table `auteurs`
--

CREATE TABLE IF NOT EXISTS `auteurs` (
  `ID` int(16) NOT NULL,
  `Voornaam` varchar(255) NOT NULL,
  `Achternaam` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `auteurs`
--

INSERT INTO `auteurs` (`ID`, `Voornaam`, `Achternaam`) VALUES
(0, 'Mies', 'van der Lippe'),
(0, 'Mark', 'Kathmann'),
(1, 'Mies', 'van der Lippe'),
(2, 'Mark', 'Kathmann');

-- --------------------------------------------------------

--
-- Table structure for table `boeken`
--

CREATE TABLE IF NOT EXISTS `boeken` (
  `ID` int(16) NOT NULL AUTO_INCREMENT,
  `ISBN` int(16) NOT NULL,
  `DateAdded` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `IssuedAt` datetime NOT NULL,
  `IssuedTo` int(16) DEFAULT '0',
  `Location` varchar(255) NOT NULL DEFAULT 'Achter de balie',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=11 ;

--
-- Dumping data for table `boeken`
--

INSERT INTO `boeken` (`ID`, `ISBN`, `DateAdded`, `IssuedAt`, `IssuedTo`, `Location`) VALUES
(1, 324324324, '2013-10-27 20:21:05', '2014-10-22 21:18:29', 2, 'Achter de balie'),
(2, 324324324, '2014-10-27 20:21:05', '2014-10-28 22:00:02', 2, 'Achter de balie'),
(3, 324324324, '2014-10-27 20:21:05', '2014-10-28 22:00:04', 2, 'Achter de balie'),
(4, 324324324, '2014-10-27 20:21:05', '2014-10-28 22:00:05', 2, 'Achter de balie'),
(5, 34083084, '2014-10-27 20:21:05', '2014-08-11 00:00:00', 2, 'Achter de balie'),
(6, 34083084, '2014-10-27 20:21:05', '2014-10-28 22:07:55', 0, 'Achter de balie'),
(7, 24937234, '2014-10-27 20:21:05', '0000-00-00 00:00:00', 0, 'Achter de balie'),
(8, 1321324324, '2014-10-27 20:21:05', '2014-10-28 22:08:18', 0, 'Achter de balie'),
(9, 1321324324, '2014-10-27 20:21:05', '0000-00-00 00:00:00', 0, 'Achter de balie'),
(10, 1321324324, '2014-10-27 20:28:05', '0000-00-00 00:00:00', 0, 'Achter de balie');

-- --------------------------------------------------------

--
-- Table structure for table `gebruikers`
--

CREATE TABLE IF NOT EXISTS `gebruikers` (
  `ID` int(16) NOT NULL AUTO_INCREMENT,
  `Email` varchar(255) NOT NULL,
  `Password` varchar(64) NOT NULL,
  `Salt` varchar(64) NOT NULL,
  `Admin` tinyint(1) NOT NULL DEFAULT '0',
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB  DEFAULT CHARSET=latin1 AUTO_INCREMENT=3 ;

--
-- Dumping data for table `gebruikers`
--

INSERT INTO `gebruikers` (`ID`, `Email`, `Password`, `Salt`, `Admin`) VALUES
(2, 'test@gmail.com', 'YKbn+mF2+fFEdbAQyKZZrIFlfMlNBNyaQDnGepQlnOQ=', 'bwg7SKW0dMgjDktA', 1);

-- --------------------------------------------------------

--
-- Table structure for table `isbn`
--

CREATE TABLE IF NOT EXISTS `isbn` (
  `ISBN` int(16) NOT NULL,
  `Naam` varchar(255) NOT NULL,
  `Auteur` int(16) NOT NULL,
  `Uitgever` int(16) NOT NULL,
  PRIMARY KEY (`ISBN`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `isbn`
--

INSERT INTO `isbn` (`ISBN`, `Naam`, `Auteur`, `Uitgever`) VALUES
(423234, 'Welkom bij de kookclub', 2, 2),
(24937234, 'Roodkapje', 1, 2),
(34083084, 'Leren lezen', 2, 2),
(35493549, 'PHP MVC leren', 2, 1),
(324324324, 'SQL enzo', 2, 1),
(1321324324, 'Dit is een test', 1, 1);

-- --------------------------------------------------------

--
-- Table structure for table `uitgevers`
--

CREATE TABLE IF NOT EXISTS `uitgevers` (
  `ID` int(16) NOT NULL,
  `Naam` varchar(255) NOT NULL,
  `Land` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `uitgevers`
--

INSERT INTO `uitgevers` (`ID`, `Naam`, `Land`) VALUES
(1, 'De vliegende valbij', 'Nederland'),
(2, 'Le Ordinateur', 'Frankrijk');

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
