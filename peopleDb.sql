USE [master]
GO
/****** Object:  Database [PeopleDB]    Script Date: 12/22/2021 11:18:56 AM ******/
CREATE DATABASE [PeopleDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PeopleDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PeopleDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PeopleDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\PeopleDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PeopleDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PeopleDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PeopleDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PeopleDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PeopleDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PeopleDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PeopleDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PeopleDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PeopleDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PeopleDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PeopleDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PeopleDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PeopleDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PeopleDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PeopleDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PeopleDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [PeopleDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PeopleDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PeopleDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PeopleDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PeopleDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PeopleDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PeopleDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PeopleDB] SET RECOVERY FULL 
GO
ALTER DATABASE [PeopleDB] SET  MULTI_USER 
GO
ALTER DATABASE [PeopleDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PeopleDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PeopleDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PeopleDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
EXEC sys.sp_db_vardecimal_storage_format N'PeopleDB', N'ON'
GO
USE [PeopleDB]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/22/2021 11:18:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](60) NULL,
	[DateOfBirth] [date] NULL,
	[PhoneNumber] [varchar](12) NULL,
	[Email] [varchar](150) NOT NULL,
	[BankAccountNumber] [varchar](20) NULL,
	[CreatedOn] [smalldatetime] NOT NULL,
 CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [Email], [BankAccountNumber], [CreatedOn]) VALUES (2, N'dda', N'dsadasd', CAST(N'2021-12-06' AS Date), N'9123511145', N'asd@asda.xom', N'1243131231', CAST(N'2021-12-22T10:53:00' AS SmallDateTime))
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [Email], [BankAccountNumber], [CreatedOn]) VALUES (3, N'a', N'q', CAST(N'2021-12-06' AS Date), N'9123511145', N'asdd@asda.xom', N'1243131231', CAST(N'2021-12-22T10:53:00' AS SmallDateTime))
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [DateOfBirth], [PhoneNumber], [Email], [BankAccountNumber], [CreatedOn]) VALUES (4, N'vv', N'2', CAST(N'2021-12-06' AS Date), N'9123511145', N'a3d@asda.xom', N'1243131231', CAST(N'2021-12-22T10:53:00' AS SmallDateTime))
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
/****** Object:  StoredProcedure [dbo].[GenerateClassFromTable]    Script Date: 12/22/2021 11:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- EXEC dbo.GenerateClassFromTable @TableName = 'Table' -- sysname

CREATE PROC [dbo].[GenerateClassFromTable]
(@TableName sysname)
AS
--DECLARE @TableName sysname = 'Contract';
DECLARE @Result VARCHAR(MAX) = 'public class ' + @TableName + '
{';
SELECT @Result = @Result + '
    public '     + (CASE
                        WHEN ColumnName = 'RowVersion' THEN
                            'byte[]'
                        ELSE
                            ColumnType
                    END
                   ) + NullableSign + ' ' + ColumnName + ' { get; set; }
'
FROM
(
    SELECT REPLACE(col.name, ' ', '_') ColumnName,
           column_id ColumnId,
           CASE typ.name
               WHEN 'bigint' THEN
                   'long'
               WHEN 'binary' THEN
                   'byte[]'
               WHEN 'bit' THEN
                   'bool'
               WHEN 'char' THEN
                   'string'
               WHEN 'date' THEN
                   'DateTime'
               WHEN 'datetime' THEN
                   'DateTime'
               WHEN 'datetime2' THEN
                   'DateTime'
               WHEN 'datetimeoffset' THEN
                   'DateTimeOffset'
               WHEN 'decimal' THEN
                   'decimal'
               WHEN 'float' THEN
                   'float'
               WHEN 'image' THEN
                   'byte[]'
               WHEN 'int' THEN
                   'int'
               WHEN 'money' THEN
                   'decimal'
               WHEN 'nchar' THEN
                   'string'
               WHEN 'ntext' THEN
                   'string'
               WHEN 'numeric' THEN
                   'decimal'
               WHEN 'nvarchar' THEN
                   'string'
               WHEN 'real' THEN
                   'double'
               WHEN 'smalldatetime' THEN
                   'DateTime'
               WHEN 'smallint' THEN
                   'short'
               WHEN 'smallmoney' THEN
                   'decimal'
               WHEN 'text' THEN
                   'string'
               WHEN 'time' THEN
                   'TimeSpan'
               WHEN 'timestamp' THEN
                   'timestamp'
               WHEN 'rowversion' THEN
                   'byte[]'
               WHEN 'tinyint' THEN
                   'byte'
               WHEN 'uniqueidentifier' THEN
                   'Guid'
               WHEN 'varbinary' THEN
                   'byte[]'
               WHEN 'varchar' THEN
                   'string'
               ELSE
                   'UNKNOWN_' + typ.name
           END ColumnType,
           CASE
               WHEN col.is_nullable = 1
                    AND typ.name IN ( 'bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal',
                                      'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint',
                                      'smallmoney', 'time', 'tinyint', 'uniqueidentifier'
                                    ) THEN
                   '?'
               ELSE
                   ''
           END NullableSign
    FROM sys.columns col
        JOIN sys.types typ
            ON col.system_type_id = typ.system_type_id
               AND col.user_type_id = typ.user_type_id
    WHERE object_id = OBJECT_ID(@TableName)
) t
ORDER BY ColumnId;
SET @Result = @Result + '
}';
PRINT @Result;
GO
/****** Object:  StoredProcedure [dbo].[usp_Customer_Delete]    Script Date: 12/22/2021 11:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Customer_Delete]
(@Id AS INT)
AS
DELETE FROM [dbo].[Customer]
WHERE [Id] = @Id;

SELECT CONVERT(BIT, 1) AS Result;
GO
/****** Object:  StoredProcedure [dbo].[usp_Customer_Insert]    Script Date: 12/22/2021 11:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Customer_Insert]
(
    @Id AS INT = NULL,
    @FirstName AS NVARCHAR(50) = NULL,
    @LastName AS NVARCHAR(60) = NULL,
    @DateOfBirth AS DATE = NULL,
    @PhoneNumber AS VARCHAR(12) = NULL,
    @Email AS VARCHAR(150),
    @BankAccountNumber AS VARCHAR(20) = NULL
)
AS
INSERT INTO [dbo].[Customer]
(
    [FirstName],
    [LastName],
    [DateOfBirth],
    [PhoneNumber],
    [Email],
    [BankAccountNumber],
    [CreatedOn]
)
VALUES
(@FirstName, @LastName, @DateOfBirth, @PhoneNumber, @Email, @BankAccountNumber, GETDATE());

SET @Id = @@IDENTITY;

SELECT @Id AS Result;
GO
/****** Object:  StoredProcedure [dbo].[usp_Customer_List]    Script Date: 12/22/2021 11:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Customer_List]
(
    @Id AS INT = NULL,
    @FirstName AS NVARCHAR(50) = NULL,
    @LastName AS NVARCHAR(60) = NULL,
    @DateOfBirth AS DATE = NULL,
    @PhoneNumber AS VARCHAR(12) = NULL,
    @Email AS VARCHAR(150),
    @BankAccountNumber AS VARCHAR(20) = NULL,
    @CreatedOn AS DATETIME = NULL,
    @PageSize AS INT = 50,
    @PageIndex AS INT = 0
)
AS
SELECT *
FROM [dbo].[Customer]
WHERE (
          @FirstName IS NULL
          OR [FirstName] = @FirstName
      )
      AND
      (
          @LastName IS NULL
          OR [LastName] = @LastName
      )
      AND
      (
          @DateOfBirth IS NULL
          OR DateOfBirth = @DateOfBirth
      )
      AND
      (
          @PhoneNumber IS NULL
          OR PhoneNumber = @PhoneNumber
      )
      AND
      (
          @Email IS NULL
          OR Email = @Email
      )
      AND
      (
          @BankAccountNumber IS NULL
          OR BankAccountNumber = @BankAccountNumber
      )
      AND
      (
          @CreatedOn IS NULL
          OR CreatedOn = @CreatedOn
      )
      AND
      (
          @Id IS NULL
          OR [Id] = @Id
      )
ORDER BY CreatedOn DESC OFFSET (@PageIndex) ROWS FETCH NEXT @PageSize ROWS ONLY;
GO
/****** Object:  StoredProcedure [dbo].[usp_Customer_Update]    Script Date: 12/22/2021 11:18:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[usp_Customer_Update]
(
    @Id AS INT = NULL,
    @FirstName AS NVARCHAR(50) = NULL,
    @LastName AS NVARCHAR(60) = NULL,
    @DateOfBirth AS DATE = NULL,
    @PhoneNumber AS VARCHAR(12) = NULL,
    @Email AS VARCHAR(150),
    @BankAccountNumber AS VARCHAR(20) = NULL
)
AS
UPDATE [dbo].[Customer]
SET FirstName = ISNULL(@FirstName, [FirstName]),
    LastName = ISNULL(@LastName, LastName),
    DateOfBirth = ISNULL(@DateOfBirth, DateOfBirth),
    PhoneNumber = ISNULL(@PhoneNumber, PhoneNumber),
    Email = ISNULL(@Email, Email),
    BankAccountNumber = ISNULL(@BankAccountNumber, [BankAccountNumber])
WHERE [Id] = @Id;

SELECT @Id AS Result;
GO
USE [master]
GO
ALTER DATABASE [PeopleDB] SET  READ_WRITE 
GO
