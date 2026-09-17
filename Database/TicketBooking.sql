USE [master]
GO

IF DB_ID('TicketBooking') IS NULL
BEGIN
    CREATE DATABASE [TicketBooking]
END
GO

USE [TicketBooking]
GO

-- =============================================
-- Table: tbl_Movie_Category
-- =============================================
IF OBJECT_ID('tbl_Movie_Category', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_Movie_Category
    (
        Category_id INT NOT NULL,
        Type NCHAR(50) NULL,
        CONSTRAINT PK_tbl_Movie_Category PRIMARY KEY (Category_id)
    )
END
GO

-- =============================================
-- Table: tbl_User
-- =============================================
IF OBJECT_ID('tbl_User', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_User
    (
        User_id INT NOT NULL,
        User_name NCHAR(50) NULL,
        Email_id NCHAR(50) NULL,
        User_password NCHAR(50) NULL,
        City NCHAR(50) NULL,
        Phone_no BIGINT NULL,
        CONSTRAINT PK_tbl_User PRIMARY KEY (User_id)
    )
END
GO

-- =============================================
-- Table: tbl_Movie
-- =============================================
IF OBJECT_ID('tbl_Movie', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_Movie
    (
        Movie_id INT NOT NULL,
        Movie_name NCHAR(50) NULL,
        Release_date DATE NULL,
        Category_id INT NULL,
        Rate FLOAT NULL,
        CONSTRAINT PK_tbl_Movie PRIMARY KEY (Movie_id)
    )
END
GO

-- =============================================
-- Table: Table
-- =============================================
IF OBJECT_ID('Table', 'U') IS NULL
BEGIN
    CREATE TABLE [Table]
    (
        Movie_id INT NOT NULL,
        Movie_name NCHAR(50) NULL,
        Release_date DATE NULL,
        Category_id INT NULL,
        Rate FLOAT NULL,
        CONSTRAINT PK_Table PRIMARY KEY (Movie_id)
    )
END
GO

-- =============================================
-- Table: tbl_Booking
-- =============================================
IF OBJECT_ID('tbl_Booking', 'U') IS NULL
BEGIN
    CREATE TABLE tbl_Booking
    (
        Booking_id INT NOT NULL,
        User_id INT NULL,
        Category_id INT NULL,
        Movie_id INT NULL,
        No_Of_Tickets INT NULL,
        Amount INT NULL,
        CONSTRAINT PK_tbl_Booking PRIMARY KEY (Booking_id)
    )
END
GO

-- =============================================
-- Category Data
-- =============================================
INSERT INTO tbl_Movie_Category (Category_id, Type)
VALUES
(1, N'Thiller'),
(2, N'Horror'),
(3, N'Romantic'),
(4, N'Action');
GO

-- =============================================
-- Movie Data
-- =============================================
INSERT INTO tbl_Movie
(Movie_id, Movie_name, Release_date, Category_id, Rate)
VALUES
(1, N'Mirzapur', '2026-09-01', 2, 250.0),
(4, N'KGF', '2015-12-30', 1, 540.0),
(5, N'Toxic', '2026-09-05', 4, 650.0);
GO

-- =============================================
-- Sample User Data
-- Real passwords/contact details intentionally
-- excluded from this GitHub version.
-- =============================================
INSERT INTO tbl_User
(User_id, User_name, Email_id, User_password, City, Phone_no)
VALUES
(1, N'Demo User 1', N'demo1@example.com', N'Demo@123', N'Surat', 9000000001),
(2, N'Demo User 2', N'demo2@example.com', N'Demo@123', N'Surat', 9000000002),
(3, N'Demo User 3', N'demo3@example.com', N'Demo@123', N'Surat', 9000000003),
(4, N'Demo User 4', N'demo4@example.com', N'Demo@123', N'Surat', 9000000004),
(5, N'Demo User 5', N'demo5@example.com', N'Demo@123', N'Surat', 9000000005),
(6, N'Demo User 6', N'demo6@example.com', N'Demo@123', N'Surat', 9000000006),
(7, N'Demo User 7', N'demo7@example.com', N'Demo@123', N'Surat', 9000000007),
(8, N'Demo User 8', N'demo8@example.com', N'Demo@123', N'Surat', 9000000008),
(9, N'Demo User 9', N'demo9@example.com', N'Demo@123', N'Surat', 9000000009),
(10, N'Demo User 10', N'demo10@example.com', N'Demo@123', N'Navsari', 9000000010),
(11, N'Demo User 11', N'demo11@example.com', N'Demo@123', N'Bardoli', 9000000011),
(12, N'Demo User 12', N'demo12@example.com', N'Demo@123', N'Surat', 9000000012);
GO

-- =============================================
-- Booking Data
-- =============================================
INSERT INTO tbl_Booking
(Booking_id, User_id, Category_id, Movie_id, No_Of_Tickets, Amount)
VALUES
(1, 1, 1, 4, 4, 2160);
GO

-- =============================================
-- Foreign Keys
-- =============================================
ALTER TABLE [Table]
ADD CONSTRAINT FK_Movie_Category_1
FOREIGN KEY (Category_id)
REFERENCES tbl_Movie_Category(Category_id);
GO

ALTER TABLE tbl_Booking
ADD CONSTRAINT FK_Movie_Category_3
FOREIGN KEY (User_id)
REFERENCES tbl_User(User_id);
GO

ALTER TABLE tbl_Booking
ADD CONSTRAINT FK_tbl_Booking_ToTable
FOREIGN KEY (Category_id)
REFERENCES tbl_Movie_Category(Category_id);
GO

ALTER TABLE tbl_Booking
ADD CONSTRAINT FK_tbl_Booking_ToTable_4
FOREIGN KEY (Movie_id)
REFERENCES tbl_Movie(Movie_id);
GO

PRINT 'TicketBooking database script completed successfully.';
GO

-- =============================================
-- Stored Procedures
-- =============================================

USE [TicketBooking]
GO

-- =============================================
-- Procedure: add_user
-- =============================================
CREATE PROCEDURE dbo.add_user
    @User_name VARCHAR(100),
    @Email_id VARCHAR(100),
    @User_password VARCHAR(100),
    @City VARCHAR(50),
    @Phone_no BIGINT
AS
BEGIN
    SET NOCOUNT ON;

    DECLARE @NewUserId INT;

    SELECT @NewUserId = ISNULL(MAX(User_id), 0) + 1
    FROM dbo.tbl_User;

    INSERT INTO dbo.tbl_User
    (
        User_id,
        User_name,
        Email_id,
        User_password,
        City,
        Phone_no
    )
    VALUES
    (
        @NewUserId,
        @User_name,
        @Email_id,
        @User_password,
        @City,
        @Phone_no
    );
END
GO

-- =============================================
-- Procedure: delete_movie
-- =============================================
CREATE PROCEDURE dbo.delete_movie
    @Movie_id INT
AS
BEGIN
    DELETE FROM tbl_Movie
    WHERE Movie_id = @Movie_id;
END
GO

-- =============================================
-- Procedure: get_user
-- =============================================
CREATE PROCEDURE dbo.get_user
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        User_id AS UserId,
        User_name AS UserName,
        Email_id AS Email,
        User_password AS Password,
        City,
        Phone_no AS PhoneNumber
    FROM dbo.tbl_User;
END
GO

-- =============================================
-- Procedure: get_user_by_id
-- =============================================
CREATE PROCEDURE dbo.get_user_by_id
    @User_id INT
AS
BEGIN
    SELECT
        User_id,
        User_name,
        Email_id,
        User_password,
        City,
        Phone_no
    FROM tbl_User
    WHERE User_id = @User_id;
END
GO

-- =============================================
-- Procedure: login_user
-- =============================================
CREATE PROCEDURE dbo.login_user
    @Email_id VARCHAR(100),
    @User_password VARCHAR(100)
AS
BEGIN
    SELECT
        User_id,
        User_name,
        Email_id,
        User_password,
        City,
        Phone_no
    FROM tbl_User
    WHERE Email_id = @Email_ID
      AND User_password = @User_password;
END
GO

-- =============================================
-- Procedure: SearchMovieByCategory
-- =============================================
CREATE PROCEDURE dbo.SearchMovieByCategory
    @Category_id INT
AS
BEGIN
    SELECT
        Movie_id,
        Movie_name,
        Release_date,
        Category_id,
        Rate
    FROM tbl_Movie
    WHERE Category_id = @Category_id;
END
GO

-- =============================================
-- Procedure: update_user
-- =============================================
CREATE PROCEDURE dbo.update_user
    @User_id INT,
    @User_name VARCHAR(100),
    @Email_id VARCHAR(100),
    @User_password VARCHAR(100),
    @City VARCHAR(50),
    @Phone_no BIGINT
AS
BEGIN
    UPDATE tbl_User
    SET
        User_name = @User_name,
        Email_id = @Email_id,
        User_password = @User_password,
        City = @City,
        Phone_no = @Phone_no
    WHERE User_id = @User_id;
END
GO

