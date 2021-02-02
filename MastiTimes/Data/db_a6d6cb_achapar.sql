-- phpMyAdmin SQL Dump
-- version 5.0.2
-- https://www.phpmyadmin.net/
--
-- Host: mysql5044.site4now.net
-- Generation Time: Feb 01, 2021 at 11:14 PM
-- Server version: 8.0.21
-- PHP Version: 7.4.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_a6d6cb_achapar`
--

DELIMITER $$
--
-- Procedures
--
CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `GetMoviesTheater` ()  BEGIN
SELECT movie.title, theater.name, theater.city 
FROM movie
inner join movie_theater
ON movie.id = movie_theater.movie_id
join theater
ON movie_theater.theater_id = theater.id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie` (IN `movie_id` INT)  NO SQL
BEGIN
	SELECT * FROM movie
    WHERE movie.movie_id = movie_id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movies` ()  BEGIN
	 SELECT * FROM movie;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movies_by_theater` (IN `theater_id` INT)  NO SQL
BEGIN
	SELECT movie.movie_id, movie.movie_title, movie.poster_url, movie.actors, movie.rated, movie.rating, movie_theater.show_time
    FROM movie_theater
    Inner join movie ON movie_theater.movie_id = movie.movie_id
    Inner join theater ON movie_theater.theater_id = theater.theater_id
    WHERE movie_theater.theater_id = theater_id && movie_theater.now_playing = 1;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie_id` (IN `id` INT)  BEGIN
	 SELECT * FROM movie
	 WHERE movie.id = id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie_likes` (IN `movie_id` INT)  NO SQL
BEGIN
    SET @likes = 0;
    SELECT COUNT(movie_id) INTO @likes
    FROM `like`
    WHERE `like`.`movie_id` = `movie_id`;
    SELECT @likes;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie_showtimes` (IN `movie_id` INT)  NO SQL
BEGIN
	SELECT movie_theater.movie_theater_id, movie_theater.movie_id, movie_theater.theater_id, movie_theater.show_time, movie_theater.now_playing
    FROM movie_theater
    INNER JOIN theater ON theater.theater_id = movie_theater.theater_id
    where movie_theater.movie_id = movie_id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie_times` ()  BEGIN
SELECT theater.theater_name,  theater.city, theater.address, theater.phone_number, movie.movie_title, movie.poster_url, movie.rated, movie.rating, 
		movie.duration, movie.genre, movie_theater.show_time, movie_theater.now_playing 
FROM movie
inner join movie_theater
ON movie.movie_id = movie_theater.movie_id
join theater
ON movie_theater.theater_id = theater.theater_id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_movie_timess` ()  NO SQL
BEGIN
	SELECT * FROM movie_theater;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_now_showing_movies` ()  NO SQL
BEGIN
	SELECT * FROM movie_theater
    WHERE movie_theater.now_playing = 1;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_showtimes_movietheater` (IN `movie_id` INT, IN `theater_id` INT)  BEGIN
    SELECT movie.movie_title, movie_theater.show_time
    FROM movie_theater
    Inner join movie ON movie_theater.movie_id = movie.movie_id
    Inner join theater ON movie_theater.theater_id = theater.theater_id
    WHERE movie_theater.movie_id = movie_id && movie_theater.theater_id = theater_id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_theater` (IN `theater_id` INT)  BEGIN
	SELECT * FROM theater
    WHERE theater.theater_id = theater_id;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_theaters` ()  NO SQL
BEGIN
	SELECT * FROM theater;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_theaters_by_city` (IN `city` VARCHAR(128))  NO SQL
BEGIN
	SELECT * FROM theater
    WHERE theater.city = city;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `get_user` (IN `UserName` VARCHAR(128))  BEGIN
	 SELECT * FROM User
	 WHERE User.UserName = UserName;
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `insert_movie` (OUT `movie_id` INT, IN `movie_title` VARCHAR(256), IN `movie_released` DATETIME, IN `poster_url` VARCHAR(256), IN `actors` VARCHAR(256), IN `likes` INT, IN `rated` VARCHAR(32), IN `imdb_votes` INT(32), IN `rating` VARCHAR(32), IN `country` VARCHAR(64), IN `language` VARCHAR(64), IN `trailer` VARCHAR(256), IN `duration` VARCHAR(64), IN `genre` VARCHAR(64))  BEGIN
insert into movie (movie_title, movie_released, poster_url, actors, likes, rated, imdb_votes, rating, country, language, trailer, duration, genre) values (movie_title, movie_released, poster_url, actors, likes, rated, imdb_votes, rating, country, language, trailer, duration, genre);
SET movie_id = LAST_INSERT_ID();	
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `insert_movie_theater` (OUT `movie_theater_id` INT, IN `movie_id` INT, IN `theater_id` INT, IN `show_time` DATETIME)  BEGIN
insert into movie_theater (movie_id, theater_id, show_time) values (movie_id ,theater_id, show_time);
SET movie_theater_id = LAST_INSERT_ID();	

END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `insert_user` (OUT `UserID` INT, IN `EmailAddress` VARCHAR(256), IN `UserName` VARCHAR(256), IN `Salt` VARCHAR(256), IN `Password` VARCHAR(256))  NO SQL
BEGIN
insert into user (EmailAddress, UserName, Salt, Password) values (EmailAddress, UserName, Salt, Password);
SET UserID = LAST_INSERT_ID();	
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `like_movie` (OUT `like_id` INT, IN `movie_id` INT, IN `UserID` INT)  BEGIN
insert into `like`(movie_id, UserID) values (movie_id ,UserID);
SET like_id = LAST_INSERT_ID();	
END$$

CREATE DEFINER=`a6d6cb_achapar`@`%` PROCEDURE `sproc_CheckUserName` (IN `UserName` VARCHAR(128))  BEGIN
    SET @User_exists = 0;
    SELECT 1 INTO @User_exists
    FROM `User`
    WHERE User.`UserName` = `UserName`;
    SELECT @User_exists;
END$$

DELIMITER ;

-- --------------------------------------------------------

--
-- Table structure for table `like`
--

CREATE TABLE `like` (
  `like_id` int NOT NULL,
  `movie_id` int DEFAULT NULL,
  `UserID` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `like`
--

INSERT INTO `like` (`like_id`, `movie_id`, `UserID`) VALUES
(1, 101, 2),
(2, 101, 1),
(3, 101, 2),
(4, 101, 2),
(5, 101, 2),
(6, 101, 2),
(7, 100, 2),
(8, 100, 2),
(9, 100, 2),
(10, 100, 2),
(11, 100, 2),
(12, 100, 2),
(13, 100, 2),
(14, 100, 2),
(15, 100, 2),
(16, 100, 2),
(17, 100, 2),
(19, 101, 2),
(20, 101, 2),
(21, 100, 2),
(22, 100, 2),
(23, 100, 2),
(24, 100, 2),
(25, 100, 2),
(26, 100, 2),
(27, 100, 2),
(28, 30, 2);

-- --------------------------------------------------------

--
-- Table structure for table `movie`
--

CREATE TABLE `movie` (
  `movie_id` int NOT NULL,
  `movie_title` varchar(256) DEFAULT NULL,
  `poster_url` varchar(256) DEFAULT NULL,
  `rated` varchar(32) DEFAULT NULL,
  `rating` varchar(32) DEFAULT NULL,
  `duration` varchar(32) DEFAULT NULL,
  `genre` varchar(32) DEFAULT NULL,
  `country` varchar(32) DEFAULT NULL,
  `language` varchar(32) DEFAULT NULL,
  `trailer` varchar(256) DEFAULT NULL,
  `actors` varchar(256) DEFAULT NULL,
  `movie_released` date DEFAULT NULL,
  `likes` int DEFAULT NULL,
  `imdb_votes` int DEFAULT NULL,
  `plot` varchar(1024) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `movie`
--

INSERT INTO `movie` (`movie_id`, `movie_title`, `poster_url`, `rated`, `rating`, `duration`, `genre`, `country`, `language`, `trailer`, `actors`, `movie_released`, `likes`, `imdb_votes`, `plot`) VALUES
(1, 'Test test', 'test', 'test', 'test', 'test', 'test', 'test', 'test', 'test', 'test', '2020-04-20', 11, 7, NULL),
(2, 'Wonder Woman 1984', 'https://image.tmdb.org/t/p/w500/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', 'R', 'test', 'test', 'teest', 'test', 'test', 'test', 'test', '2021-01-02', 10, 11, NULL),
(3, 'Skylines', 'https://image.tmdb.org/t/p/w500/2W4ZvACURDyhiNnSIaFPHfNbny3.jpg', 'R', NULL, 'test', NULL, NULL, NULL, NULL, NULL, '0001-01-01', 0, NULL, NULL),
(5, 'Breach', 'https://image.tmdb.org/t/p/w500/13B6onhL6FzSN2KaNeQeMML05pS.jpg', 'PG-13', NULL, 'test', NULL, NULL, NULL, NULL, NULL, '0001-01-01', 0, NULL, NULL),
(22, 'Wonder Woman 1984', 'https://image.tmdb.org/t/p/w500/8UlWHLMpgZm9bx6QYh0NFoq67TZ.jpg', 'PG-13', '5.5', 'test', 'Action, Adventure, Fantasy', 'USA, UK, Spain', 'English', 'test', 'Gal Gadot, Chris Pine, Kristen Wiig, Pedro Pascal', '2020-12-25', 11, 7, NULL),
(23, 'Breach', 'https://image.tmdb.org/t/p/w500/13B6onhL6FzSN2KaNeQeMML05pS.jpg', 'R', '3.6', 'test', 'Action, Sci-Fi', 'Canada', 'English', 'test', 'Cody Kearsley, Bruce Willis, Rachel Nichols, Kassandra Clementi', '2020-12-18', 11, 5, NULL),
(25, 'Vanguard', 'https://image.tmdb.org/t/p/w500/vYvppZMvXYheYTWVd8Rnn9nsmNp.jpg', 'PG-13', '4.6', 'test', 'Action', 'China', 'Mandarin, English', 'test', 'Jackie Chan, Yang Yang, Lun Ai, Miya Muqi', '2020-11-20', 11, 7, NULL),
(26, 'Honest Thief', 'https://image.tmdb.org/t/p/w500/zeD4PabP6099gpE0STWJrJrCBCs.jpg', 'PG-13', '6.0', 'test', 'Action, Crime, Drama, Thriller', 'USA', 'English, Spanish', 'test', 'Liam Neeson, Kate Walsh, Jai Courtney, Jeffrey Donovan', '2020-10-16', 11, 7, NULL),
(27, 'New Order', 'https://image.tmdb.org/t/p/w500/v6NodCMzqilx0Xw541P65WFnDfE.jpg', 'N/A', '6.2', 'test', 'Drama', 'Mexico, France', 'Spanish', 'test', 'Samantha Yazareth Anaya, Dario Yazbek Bernal, Patricia Bernal, Diego Boneta', '2020-10-22', 11, 7, NULL),
(28, 'The White Tiger', 'https://image.tmdb.org/t/p/w500/7K4mdWaLGF2F4ASb2L12tlya9c9.jpg', 'R', '6.6', 'test', 'Crime, Drama', 'India, USA', 'N/A', 'test', 'Priyanka Chopra, Rajkummar Rao, Adarsh Gourav, Mahesh Manjrekar', '2021-01-22', 11, 7, NULL),
(29, 'Lassie Come Home', 'https://image.tmdb.org/t/p/w500/82yxvnYtgeRzsq5f9USlrFJI05s.jpg', 'N/A', '5.7', 'test', 'Adventure, Drama, Family', 'Germany', 'German', 'test', 'Sebastian Bezzel, Anna Maria Mühe, Nico Marischka, Bella Bading', '2020-02-20', 11, 7, NULL),
(30, 'Monster Hunter', 'https://image.tmdb.org/t/p/w500/1UCOF11QCw8kcqvce8LKOO6pimh.jpg', 'PG-13', '5.3', 'test', 'Action, Adventure, Fantasy', 'China, Germany, Japan, USA', 'English', 'test', 'Milla Jovovich, Tony Jaa, T.I., Meagan Good', '2020-12-18', 11, 5, NULL),
(31, 'The Little Things', 'https://image.tmdb.org/t/p/w500/1ihJ0yr7v6YqP6zvPVpPKUrOuQ3.jpg', 'R', 'N/A', 'test', 'Crime, Drama, Thriller', 'USA', 'English', 'test', 'Denzel Washington, Rami Malek, Jared Leto, Chris Bauer', '2021-01-29', 11, 6, NULL),
(32, 'The New Mutants', 'https://image.tmdb.org/t/p/w500/xrI4EnZWftpo1B7tTvlMUXVOikd.jpg', 'PG-13', '5.3', 'test', 'Action, Horror, Sci-Fi', 'USA', 'English, Portuguese, Latin', 'test', 'Maisie Williams, Anya Taylor-Joy, Charlie Heaton, Alice Braga', '2020-08-28', 11, 6, NULL),
(33, 'Jumanji: Level One', 'https://image.tmdb.org/t/p/w500/mI7sIBqIsCsTjLvuiVVTfvW3FLU.jpg', 'N/A', '6.1', 'test', 'Action, Adventure, Family', 'USA', 'English', 'test', 'Aqeel Ash-Shakoor, Aaron Matthew Atkisson, Roe Dayzon, Adam DeFilippi', '2021-01-20', 11, 5, NULL),
(34, 'Ghosts of War', 'https://image.tmdb.org/t/p/w500/jBeL6pPUPo0wnyTmiuxPegcibPf.jpg', 'R', '5.5', 'test', 'Horror, Thriller, War', 'UK', 'English', 'test', 'Brenton Thwaites, Kyle Gallner, Alan Ritchson, Theo Rossi', '2020-07-17', 11, 6, NULL),
(35, 'The First King', 'https://image.tmdb.org/t/p/w500/yO3749nMhVrPW2uzKZkkaKNfXvD.jpg', 'TV-MA', '6.5', 'test', 'Drama, History', 'Italy, Belgium', 'Latin', 'test', 'Alessandro Borghi, Alessio Lapice, Fabrizio Rongione, Massimiliano Rossi', '2019-01-31', 11, 7, NULL),
(36, 'The Nightingale', 'https://image.tmdb.org/t/p/w500/hWA8QwSM1kJYMoTANEPoqrqBapg.jpg', 'R', '7.3', 'test', 'Adventure, Drama, Thriller', 'Australia, USA', 'English, Irish, Aboriginal', 'test', 'Aisling Franciosi, Baykali Ganambarr, Damon Herriman, Charlie Jampijinpa Brown', '2019-08-29', 11, 7, NULL),
(100, 'The Invisible Man', 'https://m.media-amazon.com/images/M/MV5BZjFhM2I4ZDYtZWMwNC00NTYzLWE3MDgtNjgxYmM3ZWMxYmVmXkEyXkFqcGdeQXVyMTkxNjUyNQ@@._V1_SX300.jpg', 'R', '7.1', 'test', 'Drama, Horror, Mystery', 'Canada, Australia, USA', 'English', 'test', 'Elisabeth Moss, Oliver Jackson-Cohen, Harriet Dyer, Aldis Hodge', '2020-02-28', 11, 7, NULL),
(101, 'Master', 'https://m.media-amazon.com/images/M/MV5BNmU1OTYzYzAtMDcyOS00MDI0LTg2ZmQtYTEyMDdmMmQ0MjY5XkEyXkFqcGdeQXVyOTk3NTc2MzE@._V1_SX300.jpg', 'Not Rated', '7.7', 'test', 'Crime, Thriller', 'India', 'Tamil, Hindi', 'test', 'Malavika Mohanan, Joseph Vijay, Vijay Sethupathi, Andrea Jeremiah', '2021-01-14', 101, 8, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `movie_theater`
--

CREATE TABLE `movie_theater` (
  `movie_theater_id` int NOT NULL,
  `movie_id` int DEFAULT NULL,
  `theater_id` int DEFAULT NULL,
  `show_time` datetime NOT NULL,
  `now_playing` tinyint(1) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `movie_theater`
--

INSERT INTO `movie_theater` (`movie_theater_id`, `movie_id`, `theater_id`, `show_time`, `now_playing`) VALUES
(1, 2, 2, '2021-01-26 16:29:00', 0),
(2, 31, 3, '2021-01-11 15:20:00', 0),
(3, 1, 3, '2021-01-11 16:20:00', 0),
(7, 28, 1, '2021-01-30 04:20:00', 0),
(8, 28, 1, '2021-01-29 22:35:00', 0),
(9, 30, 1, '2021-01-30 12:15:00', 1),
(10, 101, 1, '2021-01-30 12:00:00', 1),
(11, 100, 1, '2021-01-30 06:00:00', 1),
(12, 101, 1, '2021-01-30 14:30:00', 1),
(13, 101, 1, '2021-01-30 17:30:00', 1),
(14, 100, 4, '2021-01-30 12:45:00', 1),
(15, 30, 4, '2021-01-30 15:45:00', 1),
(16, 101, 4, '2021-01-30 15:00:00', 1),
(17, 101, 4, '2021-01-30 17:00:00', 1),
(18, 101, 6, '2021-01-31 12:30:00', NULL),
(19, 101, 6, '2021-01-31 14:30:00', NULL),
(20, 101, 6, '2021-01-31 17:30:00', NULL),
(21, 101, 7, '2021-01-31 12:00:00', NULL),
(22, 101, 7, '2021-01-31 17:45:00', NULL),
(23, 100, 7, '2021-01-31 15:30:00', NULL),
(24, 30, 7, '2021-01-31 12:45:00', NULL),
(25, 30, 7, '2021-01-31 18:15:00', NULL),
(26, 30, 8, '2021-01-31 17:45:00', NULL),
(27, 101, 8, '2021-01-31 14:30:00', NULL),
(28, 101, 8, '2021-01-31 18:00:00', NULL),
(29, 101, 9, '2021-01-31 17:45:00', NULL),
(30, 101, 9, '2021-01-31 17:45:00', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `theater`
--

CREATE TABLE `theater` (
  `theater_id` int NOT NULL,
  `theater_name` varchar(256) DEFAULT NULL,
  `address` varchar(256) DEFAULT NULL,
  `city` varchar(128) DEFAULT NULL,
  `phone_number` varchar(128) DEFAULT NULL,
  `likes` int DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `theater`
--

INSERT INTO `theater` (`theater_id`, `theater_name`, `address`, `city`, `phone_number`, `likes`) VALUES
(1, 'QFX Civil Mall', ' Kathmandu 44600', 'Kathmandu', '+977 1-4212095', NULL),
(2, 'QFX Kumari Hall', 'धोबीधारा मार्ग, Kathmandu 44600', 'Kathmandu', '+977 1-4442220', NULL),
(3, 'QFX Jai Nepal', 'Tukucha River', 'Kathmandu', '+977 1-4442220', NULL),
(4, 'QFX Labim Mall', 'Labim mall', 'Kathmandu', '+977 1-4442220', NULL),
(5, 'QFX Chaya Center', 'Chaya Center', 'Kathmandu', '+977 1-4442220', NULL),
(6, 'Big Movies', 'City Center', 'Kathmandu', '4011643', 11),
(7, 'FCube Cinemas', 'KL Tower\r\nChuchepati, Chabahil', 'Kathmandu', '4468700', NULL),
(8, 'INI Cinemas -Karmacharya Complex', 'Karmacharya Complex, Gongabu', 'Kathmandu', '01-4364540', NULL),
(9, 'INI Cinemas -Lotse Mall', 'Lotse Mall, Gongabu Buspark', 'Kathmandu', '01-4362683', NULL);

-- --------------------------------------------------------

--
-- Table structure for table `user`
--

CREATE TABLE `user` (
  `UserID` int NOT NULL,
  `FirstName` varchar(256) DEFAULT NULL,
  `LastName` varchar(256) DEFAULT NULL,
  `EmailAddress` varchar(256) DEFAULT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `Salt` varchar(256) DEFAULT NULL,
  `Password` varchar(256) DEFAULT NULL,
  `DateCreated` date DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- Dumping data for table `user`
--

INSERT INTO `user` (`UserID`, `FirstName`, `LastName`, `EmailAddress`, `UserName`, `Salt`, `Password`, `DateCreated`) VALUES
(1, 'test', 'test', 'test', 'test', 'test', 'test', NULL),
(2, NULL, NULL, 't', 't', 'nuf6peqxc8aEHznX7tUMJCaeVQxRj7AXISzi77YaQSxnVwvcdo', '5zPu1D2GDWbHxa4+3wAF3v5gzFIxktY69Kc52pmU6B2il6gPu4FtyVa2LoZx0Unq', NULL);

--
-- Indexes for dumped tables
--

--
-- Indexes for table `like`
--
ALTER TABLE `like`
  ADD PRIMARY KEY (`like_id`),
  ADD KEY `movie_id` (`movie_id`),
  ADD KEY `UserID` (`UserID`);

--
-- Indexes for table `movie`
--
ALTER TABLE `movie`
  ADD PRIMARY KEY (`movie_id`);

--
-- Indexes for table `movie_theater`
--
ALTER TABLE `movie_theater`
  ADD PRIMARY KEY (`movie_theater_id`),
  ADD KEY `movie_id` (`movie_id`),
  ADD KEY `theater_id` (`theater_id`);

--
-- Indexes for table `theater`
--
ALTER TABLE `theater`
  ADD PRIMARY KEY (`theater_id`);

--
-- Indexes for table `user`
--
ALTER TABLE `user`
  ADD PRIMARY KEY (`UserID`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `like`
--
ALTER TABLE `like`
  MODIFY `like_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=29;

--
-- AUTO_INCREMENT for table `movie`
--
ALTER TABLE `movie`
  MODIFY `movie_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=102;

--
-- AUTO_INCREMENT for table `movie_theater`
--
ALTER TABLE `movie_theater`
  MODIFY `movie_theater_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=31;

--
-- AUTO_INCREMENT for table `theater`
--
ALTER TABLE `theater`
  MODIFY `theater_id` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT for table `user`
--
ALTER TABLE `user`
  MODIFY `UserID` int NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `like`
--
ALTER TABLE `like`
  ADD CONSTRAINT `like_ibfk_1` FOREIGN KEY (`movie_id`) REFERENCES `movie` (`movie_id`),
  ADD CONSTRAINT `like_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `user` (`UserID`);

--
-- Constraints for table `movie_theater`
--
ALTER TABLE `movie_theater`
  ADD CONSTRAINT `movie_theater_ibfk_1` FOREIGN KEY (`movie_id`) REFERENCES `movie` (`movie_id`),
  ADD CONSTRAINT `movie_theater_ibfk_2` FOREIGN KEY (`theater_id`) REFERENCES `theater` (`theater_id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
