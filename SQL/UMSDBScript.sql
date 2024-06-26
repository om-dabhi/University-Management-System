USE [master]
GO
/****** Object:  Database [UMSDB]    Script Date: 11-06-2024 20:34:37 ******/
CREATE DATABASE [UMSDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'UMSDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\UMSDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'UMSDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\UMSDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [UMSDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [UMSDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [UMSDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [UMSDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [UMSDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [UMSDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [UMSDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [UMSDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [UMSDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [UMSDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [UMSDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [UMSDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [UMSDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [UMSDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [UMSDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [UMSDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [UMSDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [UMSDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [UMSDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [UMSDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [UMSDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [UMSDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [UMSDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [UMSDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [UMSDB] SET RECOVERY FULL 
GO
ALTER DATABASE [UMSDB] SET  MULTI_USER 
GO
ALTER DATABASE [UMSDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [UMSDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [UMSDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [UMSDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [UMSDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [UMSDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'UMSDB', N'ON'
GO
ALTER DATABASE [UMSDB] SET QUERY_STORE = OFF
GO
USE [UMSDB]
GO
/****** Object:  Table [dbo].[AttendenceTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttendenceTBL](
	[AttendenceID] [int] NOT NULL,
	[StudentID] [int] NOT NULL,
	[CourseID] [int] NOT NULL,
	[Status] [bit] NULL,
	[Date] [datetime] NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_AttendenceTBL] PRIMARY KEY CLUSTERED 
(
	[AttendenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BranchTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BranchTBL](
	[BranchID] [int] IDENTITY(1,1) NOT NULL,
	[CourseID] [int] NOT NULL,
	[BranchName] [varchar](100) NULL,
	[DeanName] [varchar](100) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_BranchTBL] PRIMARY KEY CLUSTERED 
(
	[BranchID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CourseTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CourseTBL](
	[CourseID] [int] IDENTITY(1,1) NOT NULL,
	[CourseName] [varchar](50) NULL,
	[Sem] [int] NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_CourseTBL] PRIMARY KEY CLUSTERED 
(
	[CourseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ExperienceTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ExperienceTBL](
	[ExperienceID] [int] IDENTITY(1,1) NOT NULL,
	[Experience] [nvarchar](50) NULL,
 CONSTRAINT [PK_ExperienceTBL] PRIMARY KEY CLUSTERED 
(
	[ExperienceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[FacultyTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FacultyTBL](
	[FacultyID] [int] IDENTITY(1,1) NOT NULL,
	[FacultyName] [varchar](50) NULL,
	[CourseID] [int] NULL,
	[ExperienceID] [int] NULL,
	[Experience] [varchar](20) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [varchar](50) NULL,
	[Location] [varchar](50) NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
 CONSTRAINT [PK_Faculty] PRIMARY KEY CLUSTERED 
(
	[FacultyID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[StudentTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StudentTBL](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[BranchID] [int] NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Gender] [varchar](20) NULL,
	[DOB] [datetime] NULL,
	[Address] [varchar](250) NULL,
	[PhoneNumber] [varchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[EnrollmentNo] [varchar](20) NULL,
	[CourseID] [int] NULL,
	[Created] [datetime] NULL,
	[Modified] [datetime] NULL,
	[IsOnRoll] [bit] NULL,
 CONSTRAINT [PK__StudentT__32C52A79A2E5BCBA] PRIMARY KEY CLUSTERED 
(
	[StudentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ__StudentT__7F686F61FA2E8CF6] UNIQUE NONCLUSTERED 
(
	[EnrollmentNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserTBL]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserTBL](
	[UserID] [int] NOT NULL,
	[UserName] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
 CONSTRAINT [PK_UserTBL] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AttendenceTBL]  WITH CHECK ADD  CONSTRAINT [FK_AttendenceTBL_AttendenceTBL] FOREIGN KEY([StudentID])
REFERENCES [dbo].[StudentTBL] ([StudentID])
GO
ALTER TABLE [dbo].[AttendenceTBL] CHECK CONSTRAINT [FK_AttendenceTBL_AttendenceTBL]
GO
ALTER TABLE [dbo].[BranchTBL]  WITH CHECK ADD  CONSTRAINT [FK__BranchTBL__Cours__46E78A0C] FOREIGN KEY([CourseID])
REFERENCES [dbo].[CourseTBL] ([CourseID])
GO
ALTER TABLE [dbo].[BranchTBL] CHECK CONSTRAINT [FK__BranchTBL__Cours__46E78A0C]
GO
ALTER TABLE [dbo].[FacultyTBL]  WITH CHECK ADD  CONSTRAINT [FK_Faculty_CourseTBL] FOREIGN KEY([ExperienceID])
REFERENCES [dbo].[ExperienceTBL] ([ExperienceID])
GO
ALTER TABLE [dbo].[FacultyTBL] CHECK CONSTRAINT [FK_Faculty_CourseTBL]
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_Delete]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_Delete]

		@BranchID		int
AS
BEGIN
		DELETE
			FROM
				BranchTBL
		WHERE
				BranchTBL.BranchID=@BranchID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_Dropdown]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[PR_Branch_Dropdown]
as
begin 
	Select BranchID,BranchName from BranchTBL order by BranchName
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_Insert]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_Insert]
    @BranchName varchar(50),
    @DeanName varchar(50),
	@CourseID int
AS
BEGIN
    INSERT INTO BranchTBL
    (
        BranchName,
        DeanName,
		CourseID,
        Created,
        Modified
    )
    VALUES
    (
        @BranchName,
        @DeanName,
		@CourseID,
        GETDATE(),
        GETDATE()
    )
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_SelectAll]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_SelectAll]

AS
BEGIN
	SELECT 
			BranchID,
			BranchName,
			CourseName,
			DeanName
	FROM
			BranchTBL
			INNER JOIN CourseTBL
			ON CourseTBL.CourseID=BranchTBL.CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_SelectByPK]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_SelectByPK]

	@BranchID int
AS
BEGIN
		SELECT 
			BranchID,
			BranchName,
			CourseName,
			DeanName,
			BranchTBL.Created,
			BranchTBL.Modified

	FROM
			BranchTBL
			INNER JOIN CourseTBL
			ON CourseTBL.CourseID=BranchTBL.CourseID
	WHERE 
			BranchTBL.BranchID=@BranchID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Branch_Update]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Branch_Update]
		
			@BranchID			int,
			@BranchName			varchar(50),
			@DeanName			varchar(50),
			@CourseID			int

AS
BEGIN 
		UPDATE 
				BranchTBL
		SET
				BranchName=@BranchName	,
				DeanName=@DeanName,
				CourseID=@CourseID,
				Modified=GETDATE()
		WHERE
				BranchTBL.BranchID=@BranchID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_Delete]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Course_Delete]

		@CourseID		int
AS
BEGIN
		DELETE
			FROM
				CourseTBL
		WHERE
				CourseTBL.CourseID=@CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_Dropdown]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[PR_Course_Dropdown]
as
begin 
	Select CourseID,CourseName from CourseTBL order by CourseName
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_Insert]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Course_Insert]

		@CourseName			varchar(50)
AS
BEGIN

	INSERT INTO CourseTBL
	(
		CourseName,
		Created,
		Modified
	)
	VALUES
	(
		@CourseName,
		getdate(),		
		getdate()
	)
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_SelectAll]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[PR_Course_SelectAll]

AS
BEGIN
	SELECT 
			CourseID,
			CourseName

	FROM
			CourseTBL
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_SelectByPK]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Course_SelectByPK]

	@CourseID int
AS
BEGIN
		SELECT 
			CourseID,
			CourseName

	FROM
			CourseTBL
	WHERE 
			CourseTBL.CourseID=@CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Course_Update]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Course_Update]
		@CourseID			int,
		@CourseName			varchar(50)

AS
BEGIN 
		UPDATE 
				CourseTBL
		SET
				CourseName=@CourseName,
				Modified=GETDATE()
		WHERE
				CourseTBL.CourseID=@CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Dash_Count]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Dash_Count]
AS
BEGIN
    DECLARE @Temp TABLE
    (
        ColName varchar(50),
        CountValue INT,
		Icon varchar(50),
		Area varchar(50),
		Controller varchar(50),
		Action varchar(50)
    );

    INSERT INTO @Temp (ColName, CountValue, Icon, Area, Controller, Action)
    SELECT 'Student', COUNT(StudentID) AS StudentCount, 'bi bi-person', 'Student', 'Student', 'StudentList' FROM [dbo].[StudentTBL];

    INSERT INTO @Temp (ColName, CountValue, Icon, Area, Controller, Action)
    SELECT 'Course', COUNT(CourseID) AS CourseCount, 'bi bi-card-checklist', 'Course', 'Course', 'CourseList' FROM [dbo].[CourseTBL];

	INSERT INTO @Temp (ColName, CountValue, Icon, Area, Controller, Action)
    SELECT 'Branch', COUNT(BranchID) AS BranchCount, 'bi bi-journal-text', 'Branch', 'Branch', 'BranchList' FROM [dbo].[BranchTBL];

	INSERT INTO @Temp (ColName, CountValue, Icon, Area, Controller, Action)
    SELECT 'Faculty', COUNT(FacultyID) AS BranchCount, 'bi bi-bar-chart', 'Faculty', 'Faculty', 'FacultyList' FROM[dbo].[FacultyTBL];

    SELECT * FROM @Temp;
END;
GO
/****** Object:  StoredProcedure [dbo].[PR_Experience_Dropdown]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[PR_Experience_Dropdown]
as
begin 
	Select ExperienceID,Experience from ExperienceTBL order by ExperienceID
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_Delete]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Faculty_Delete]
    @FacultyID INT
AS
BEGIN
    DELETE FROM FacultyTBL
    WHERE FacultyID = @FacultyID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_Dropdown]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[PR_Faculty_Dropdown]
as
begin 
	Select FacultyID,FacultyName from FacultyTBL order by FacultyName
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_Insert]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Faculty_Insert]
    @FacultyName VARCHAR(100),
    @PhoneNumber VARCHAR(20),
    @Location VARCHAR(100),
    @Email VARCHAR(100),
    @CourseID INT,
	@ExperienceID INT
AS
BEGIN
    INSERT INTO FacultyTBL 
	(
		FacultyName,  
		PhoneNumber, 
		Location, 
		Email, 
		CourseID,
		ExperienceID,
		Created,
		Modified
	)
    VALUES 
	(
		@FacultyName,  
		@PhoneNumber, 
		@Location, 
		@Email, 
		@CourseID,
		@ExperienceID,
		getdate(),		
		getdate()
	)
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_SelectAll]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Faculty_SelectAll]
AS
BEGIN
Select
	FacultyID,
	FacultyName,
	Experience,
	PhoneNumber,
	Email,
	Location,
    CourseTBL.CourseName,
	FacultyTBL.Created,
	FacultyTBL.Modified
	from
	FacultyTBL
	INNER JOIN CourseTBL
			ON CourseTBL.CourseID=FacultyTBL.CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_SelectByPK]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Faculty_SelectByPK]
    @FacultyID INT
AS
BEGIN
    SELECT 
        FacultyID,
        FacultyName,
        Experience,
        PhoneNumber,
        Location,
        Email,
		Created,
		Modified,
        CourseID
    FROM
        FacultyTBL
    WHERE 
        FacultyID = @FacultyID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Faculty_Update]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Faculty_Update]
    @FacultyID INT,
    @FacultyName VARCHAR(100),
    @Experience INT,
    @PhoneNumber VARCHAR(20),
    @Location VARCHAR(100),
    @Email VARCHAR(100),
    @CourseID INT
AS
BEGIN
    UPDATE FacultyTBL
    SET FacultyName = @FacultyName,
        Experience = @Experience,
        PhoneNumber = @PhoneNumber,
        Location = @Location,
        Email = @Email,
        CourseID = @CourseID,
		[Modified]=GETDATE()
    WHERE 
		FacultyID = @FacultyID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Login]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Login]
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
   
    SELECT *
    FROM UserTBL
    WHERE Username = @Username
      AND Password = @Password;

 
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Sem]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE procedure [dbo].[PR_Sem](@CourseID int)
as
begin
select Sem from  CourseTBL where CourseID = @CourseID
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_Attendance_Insert]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[PR_Student_Attendance_Insert]
(
	@AttendenceID int,
	@StudentID int,
	@CourseID int,
	@Status bit,
	@Date datetime
)
AS
begin
INSERT INTO dbo.AttendenceTBL
  (
	   AttendenceID,
	   StudentID,
	   CourseID,
	   Status,
	   Date,
	   Created,
	   Modified
  )
  VALUES
  (
	  @AttendenceID,
	  @StudentID,
	  @CourseID,
	  @Status,
	  CONVERT(date,@date),
	  getdate(),		
	  getdate()
	)
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_Attendance_SelectAll]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[PR_Student_Attendance_SelectAll]
(
@CourseID int
)
AS
begin
select 
		StudentID,
		FirstName,
		CourseTBL.CourseName,
		CourseTBL.CourseID,
		StudentTBL.EnrollmentNo 
from StudentTBL

inner join CourseTBL on CourseTBL.CourseID=StudentTBL.CourseID
where CourseTBL.CourseID=@CourseID
end
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_Delete]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create PROCEDURE [dbo].[PR_Student_Delete]

		@StudentID		int
AS
BEGIN
		DELETE
			FROM
				StudentTBL
		WHERE
				StudentTBL.StudentID=@StudentId
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_Insert]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Student_Insert]

		@FirstName			varchar(50),
		@LastName			varchar(50),
		@Gender				varchar(20),
		@DOB				datetime,
		@Address			varchar(250),
		@PhoneNumber		varchar(20),
		@Email				varchar(20),
		@EnrollmentNo		varchar(20),
		@CourseID			int,
		@BranchID			int,
		@IsOnRoll			bit
AS
BEGIN

	INSERT INTO StudentTBL
	(
		[FirstName],
		[LastName],
		[Gender],
		[DOB],
		[Address],
		[PhoneNumber],
		[Email],
		[EnrollmentNo],
		[CourseID],
		[BranchID],
		[Created],
		[Modified],
		[IsOnRoll]

	)
	VALUES
	(
		@FirstName,		
		@LastName,	
		@Gender,	
		@DOB,			
		@Address,		
		@PhoneNumber,	
		@Email,			
		@EnrollmentNo,	
		@CourseID,
		@BranchID,
		getdate(),		
		getdate(),
		@IsOnRoll
	)
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_SelectAll]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Student_SelectAll]

AS
BEGIN
	SELECT 
			StudentID,
			FirstName,
			LastName,
			Gender,
			DOB,
			Address,
			PhoneNumber,
			Email,
			EnrollmentNo,
			BranchTBL.BranchName,
			CourseTBL.CourseName,
			StudentTBL.Created,
			StudentTBL.Modified,
			IsOnRoll

	FROM
			StudentTBL
			INNER JOIN BranchTBL
			ON BranchTBL.BranchID=StudentTBL.BranchID

			INNER JOIN CourseTBL
			ON CourseTBL.CourseID=StudentTBL.CourseID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_SelectByPK]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Student_SelectByPK]

	@StudentID int
AS
BEGIN
		SELECT 
			StudentID,
			BranchTBL.BranchName,
			CourseTBL.CourseName,
			FirstName,
			LastName,
			Gender,
			DOB,
			Address,
			PhoneNumber,
			Email,
			EnrollmentNo,
			StudentTBL.BranchID,
			StudentTBL.CourseID,
			StudentTBL.Created,
			StudentTBL.Modified,
			IsOnRoll

	FROM
			StudentTBL
		INNER JOIN BranchTBL
		ON BranchTBL.BranchID=StudentTBL.BranchID

		INNER JOIN CourseTBL
		ON CourseTBL.CourseID=StudentTBL.CourseID

	WHERE 
			StudentTBL.StudentID=@StudentID
END
GO
/****** Object:  StoredProcedure [dbo].[PR_Student_Update]    Script Date: 11-06-2024 20:34:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[PR_Student_Update]
		
		@StudentID			int,
		@BranchID			int,
		@CourseID			int,
		@FirstName			varchar(50),
		@LastName			varchar(50),
		@Gender				varchar(20),
		@DOB				datetime,
		@Address			varchar(250),
		@PhoneNumber		varchar(20),
		@Email				nvarchar(50),
		@EnrollmentNo		varchar(20),
		@IsOnRoll			bit

AS
BEGIN 
		UPDATE 
				StudentTBL
		SET
				BranchID=@BranchID		,
				CourseID=@CourseID		,
				FirstName=@FirstName	,
				LastName=@LastName		,
				Gender=@Gender			,
				DOB=@DOB			,
				Address=@Address	,	
				PhoneNumber=@PhoneNumber,	
				Email=@Email			,
				EnrollmentNo=@EnrollmentNo,	
				IsOnRoll=@IsOnRoll		,
				[Modified]=GETDATE()
		WHERE
				StudentTBL.StudentID=@StudentID
END
GO
USE [master]
GO
ALTER DATABASE [UMSDB] SET  READ_WRITE 
GO
