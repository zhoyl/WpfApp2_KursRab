USE [master]
GO
/****** Object:  Database [alekseeva_KP]    Script Date: 22.05.2023 20:12:57 ******/
CREATE DATABASE [alekseeva_KP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'alekseeva_KP', FILENAME = N'D:\sql\MSSQL16.MSSQLSERVER\MSSQL\DATA\alekseeva_KP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'alekseeva_KP_log', FILENAME = N'D:\sql\MSSQL16.MSSQLSERVER\MSSQL\DATA\alekseeva_KP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [alekseeva_KP] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [alekseeva_KP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [alekseeva_KP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [alekseeva_KP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [alekseeva_KP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [alekseeva_KP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [alekseeva_KP] SET ARITHABORT OFF 
GO
ALTER DATABASE [alekseeva_KP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [alekseeva_KP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [alekseeva_KP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [alekseeva_KP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [alekseeva_KP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [alekseeva_KP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [alekseeva_KP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [alekseeva_KP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [alekseeva_KP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [alekseeva_KP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [alekseeva_KP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [alekseeva_KP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [alekseeva_KP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [alekseeva_KP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [alekseeva_KP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [alekseeva_KP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [alekseeva_KP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [alekseeva_KP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [alekseeva_KP] SET  MULTI_USER 
GO
ALTER DATABASE [alekseeva_KP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [alekseeva_KP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [alekseeva_KP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [alekseeva_KP] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [alekseeva_KP] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [alekseeva_KP] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'alekseeva_KP', N'ON'
GO
ALTER DATABASE [alekseeva_KP] SET QUERY_STORE = OFF
GO
USE [alekseeva_KP]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[id_Class] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Hours_Per_Week] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[id_Class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[id_Client] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Passport_data] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[id_Client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contracts](
	[id_Contract] [int] IDENTITY(1,1) NOT NULL,
	[id_Client] [int] NOT NULL,
	[id_Season_ticket] [int] NOT NULL,
	[Date_of_conclusion] [date] NOT NULL,
	[id_Worker] [int] NOT NULL,
	[id_Trainer] [int] NOT NULL,
	[Cost] [int] NOT NULL,
	[id_Class] [int] NOT NULL,
 CONSTRAINT [PK_Contracts] PRIMARY KEY CLUSTERED 
(
	[id_Contract] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[id_Role] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[id_Role] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SeasonTicket]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SeasonTicket](
	[id_Season_ticket] [int] IDENTITY(1,1) NOT NULL,
	[Days] [int] NOT NULL,
 CONSTRAINT [PK_SeasonTicket] PRIMARY KEY CLUSTERED 
(
	[id_Season_ticket] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Trainers]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Trainers](
	[id_Trainer] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Experience] [int] NOT NULL,
	[Category] [int] NOT NULL,
	[Passport_data] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
 CONSTRAINT [PK_Trainers] PRIMARY KEY CLUSTERED 
(
	[id_Trainer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 22.05.2023 20:12:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Workers](
	[id_Worker] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Surname] [nvarchar](50) NOT NULL,
	[Patronymic] [nvarchar](50) NULL,
	[Passport_data] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
	[Login] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[id_Role] [int] NOT NULL,
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[id_Worker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type]) VALUES (1, N'Спортивный бассейн', 7, N'Индивидуальный')
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type]) VALUES (2, N'Зал', 7, N'Индивидуальный')
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type]) VALUES (3, N'Спортивный бассейн', 3, N'Групповой')
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type]) VALUES (4, N'Зал', 3, N'Групповой')
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type]) VALUES (5, N'Йога', 2, N'Групповой')
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone]) VALUES (3, N'Константин', N'Сурганов', N'Игоревич', N'5454525252', N'88005553535')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Contracts] ON 

INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (1, 3, 2, CAST(N'2010-01-01' AS Date), 1, 2, 550, 1)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (2, 3, 3, CAST(N'0001-01-01' AS Date), 1, 1, 777, 2)
SET IDENTITY_INSERT [dbo].[Contracts] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (2, N'Старший администратор')
INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (3, N'Директор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[SeasonTicket] ON 

INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (1, 30)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (2, 90)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (3, 120)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (4, 360)
SET IDENTITY_INSERT [dbo].[SeasonTicket] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainers] ON 

INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Category], [Passport_data], [Telephone]) VALUES (1, N'нет', N'нет', N'нет', 0, 0, N'0', N'0')
INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Category], [Passport_data], [Telephone]) VALUES (2, N'Александр', N'Петров', N'Иванович', 2, 1, N'5252323232', N'88005632525')
SET IDENTITY_INSERT [dbo].[Trainers] OFF
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role]) VALUES (1, N'Александр', N'Петренко', N'Юрьевич', N'5454525252', N'88005362563', N'1', N'1', 2)
SET IDENTITY_INSERT [dbo].[Workers] OFF
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Class] FOREIGN KEY([id_Class])
REFERENCES [dbo].[Class] ([id_Class])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Class]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Clients] FOREIGN KEY([id_Client])
REFERENCES [dbo].[Clients] ([id_Client])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Clients]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_SeasonTicket] FOREIGN KEY([id_Season_ticket])
REFERENCES [dbo].[SeasonTicket] ([id_Season_ticket])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_SeasonTicket]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Trainers] FOREIGN KEY([id_Trainer])
REFERENCES [dbo].[Trainers] ([id_Trainer])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Trainers]
GO
ALTER TABLE [dbo].[Contracts]  WITH CHECK ADD  CONSTRAINT [FK_Contracts_Workers] FOREIGN KEY([id_Worker])
REFERENCES [dbo].[Workers] ([id_Worker])
GO
ALTER TABLE [dbo].[Contracts] CHECK CONSTRAINT [FK_Contracts_Workers]
GO
ALTER TABLE [dbo].[Workers]  WITH CHECK ADD  CONSTRAINT [FK_Workers_Role] FOREIGN KEY([id_Role])
REFERENCES [dbo].[Role] ([id_Role])
GO
ALTER TABLE [dbo].[Workers] CHECK CONSTRAINT [FK_Workers_Role]
GO
USE [master]
GO
ALTER DATABASE [alekseeva_KP] SET  READ_WRITE 
GO
