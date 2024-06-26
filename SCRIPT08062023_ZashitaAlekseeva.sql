USE [master]
GO
/****** Object:  Database [alekseeva_KP]    Script Date: 08.06.2023 1:17:35 ******/
CREATE DATABASE [alekseeva_KP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'alekseeva_KP', FILENAME = N'F:\Visual\MSSQL16.MSSQLSERVER\MSSQL\DATA\alekseeva_KP.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'alekseeva_KP_log', FILENAME = N'F:\Visual\MSSQL16.MSSQLSERVER\MSSQL\DATA\alekseeva_KP_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
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
/****** Object:  Table [dbo].[Categories]    Script Date: 08.06.2023 1:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[id_Category] [int] IDENTITY(1,1) NOT NULL,
	[Num_Category] [int] NOT NULL,
	[Cost_Category] [int] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[id_Category] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 08.06.2023 1:17:35 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[id_Class] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Hours_Per_Week] [int] NOT NULL,
	[Type] [nvarchar](50) NOT NULL,
	[Cost_One] [int] NOT NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[id_Class] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clients]    Script Date: 08.06.2023 1:17:35 ******/
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
	[Status] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Clients] PRIMARY KEY CLUSTERED 
(
	[id_Client] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contracts]    Script Date: 08.06.2023 1:17:35 ******/
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
/****** Object:  Table [dbo].[Role]    Script Date: 08.06.2023 1:17:35 ******/
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
/****** Object:  Table [dbo].[SeasonTicket]    Script Date: 08.06.2023 1:17:35 ******/
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
/****** Object:  Table [dbo].[Trainers]    Script Date: 08.06.2023 1:17:35 ******/
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
	[Passport_data] [nvarchar](10) NOT NULL,
	[Telephone] [nvarchar](11) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[id_Category] [int] NOT NULL,
 CONSTRAINT [PK_Trainers] PRIMARY KEY CLUSTERED 
(
	[id_Trainer] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Workers]    Script Date: 08.06.2023 1:17:35 ******/
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
	[Login] [nvarchar](50) NULL,
	[Password] [nvarchar](50) NULL,
	[id_Role] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Workers] PRIMARY KEY CLUSTERED 
(
	[id_Worker] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([id_Category], [Num_Category], [Cost_Category]) VALUES (1, 1, 500)
INSERT [dbo].[Categories] ([id_Category], [Num_Category], [Cost_Category]) VALUES (2, 2, 700)
INSERT [dbo].[Categories] ([id_Category], [Num_Category], [Cost_Category]) VALUES (3, 3, 1000)
INSERT [dbo].[Categories] ([id_Category], [Num_Category], [Cost_Category]) VALUES (4, 0, 300)
INSERT [dbo].[Categories] ([id_Category], [Num_Category], [Cost_Category]) VALUES (5, -1, 0)
SET IDENTITY_INSERT [dbo].[Categories] OFF
GO
SET IDENTITY_INSERT [dbo].[Class] ON 

INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (1, N'Спортивный бассейн', 7, N'Индивидуальный', 400)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (2, N'Зал', 7, N'Индивидуальный', 450)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (3, N'Спортивный бассейн', 3, N'Групповой', 450)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (4, N'Зал', 3, N'Групповой', 500)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (5, N'Йога', 2, N'Групповой', 500)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (6, N'Тайский бокс', 2, N'Групповой', 300)
INSERT [dbo].[Class] ([id_Class], [Name], [Hours_Per_Week], [Type], [Cost_One]) VALUES (8, N'Степ-аэробика', 3, N'Групповой', 350)
SET IDENTITY_INSERT [dbo].[Class] OFF
GO
SET IDENTITY_INSERT [dbo].[Clients] ON 

INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (3, N'Константин', N'Сурганов', N'Игоревич', N'5454525252', N'88005553535', N'Неактивный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (5, N'Емильян', N'Васильев', N'Станиславович', N'5454252525', N'88005236565', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (6, N'Зиновий', N'Марков ', N'Лукьевич', N'5656787878', N'8800553535', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (7, N'Тарас', N'Веселов', N'Семенович', N'6767989898', N'88005553535', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (8, N'Ираклий ', N'Кабанов ', N'Витальевич', N'6767989894', N'88967986565', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (22, N'Емельян', N'Сулейманов', N'Андреевич', N'7896545456', N'88796543656', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (23, N'Кристина', N'Рабой', N'Николаевна', N'7867545454', N'88906758783', N'Неактивный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (24, N'Данил', N'Петренко', N'Иванович', N'4566789898', N'88976756545', N'Неактивный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (25, N'Семён', N'Тихонов', N'Тахирович', N'8585849923', N'89524367789', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (26, N'Емельян', N'Андреев', N'Захарович', N'7829398345', N'88976367873', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (27, N'Петр', N'Николаев', N'Алексеевич', N'7878978978', N'88988838990', N'Активный')
INSERT [dbo].[Clients] ([id_Client], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Status]) VALUES (28, N'Кристиан', N'Петров', N'Иванович', N'8658658798', N'88005554678', N'Активный')
SET IDENTITY_INSERT [dbo].[Clients] OFF
GO
SET IDENTITY_INSERT [dbo].[Contracts] ON 

INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (13, 8, 2, CAST(N'2023-05-30' AS Date), 3, 12, 4500, 4)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (17, 3, 1, CAST(N'2002-05-30' AS Date), 2, 12, 900, 1)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (18, 5, 2, CAST(N'2023-05-30' AS Date), 1, 12, 4275, 3)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (19, 6, 2, CAST(N'2023-06-01' AS Date), 3, 12, 4275, 2)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (20, 6, 2, CAST(N'2023-06-03' AS Date), 3, 12, 4050, 1)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (21, 6, 1, CAST(N'2023-06-04' AS Date), 1, 12, 900, 1)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (22, 7, 2, CAST(N'2023-06-04' AS Date), 1, 12, 3600, 6)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (23, 23, 2, CAST(N'2023-04-29' AS Date), 1, 9, 2025, 2)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (24, 24, 1, CAST(N'2023-05-16' AS Date), 3, 12, 800, 6)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (25, 25, 3, CAST(N'2023-06-06' AS Date), 2, 12, 12240, 8)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (26, 8, 3, CAST(N'2023-06-06' AS Date), 3, 12, 14400, 5)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (27, 26, 4, CAST(N'2023-06-06' AS Date), 13, 12, 18360, 8)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (28, 5, 4, CAST(N'2023-06-06' AS Date), 13, 12, 17280, 6)
INSERT [dbo].[Contracts] ([id_Contract], [id_Client], [id_Season_ticket], [Date_of_conclusion], [id_Worker], [id_Trainer], [Cost], [id_Class]) VALUES (29, 27, 5, CAST(N'2023-06-06' AS Date), 1, 9, 24300, 2)
SET IDENTITY_INSERT [dbo].[Contracts] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (1, N'Администратор')
INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (2, N'Старший администратор')
INSERT [dbo].[Role] ([id_Role], [Role_Name]) VALUES (3, N'Директор')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[SeasonTicket] ON 

INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (1, 1)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (2, 30)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (3, 90)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (4, 120)
INSERT [dbo].[SeasonTicket] ([id_Season_ticket], [Days]) VALUES (5, 360)
SET IDENTITY_INSERT [dbo].[SeasonTicket] OFF
GO
SET IDENTITY_INSERT [dbo].[Trainers] ON 

INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Passport_data], [Telephone], [Status], [id_Category]) VALUES (9, N'нет', N'нет', N'нет', 0, N'0', N'0', N'нет', 5)
INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Passport_data], [Telephone], [Status], [id_Category]) VALUES (12, N'Пётр', N'Сергеев', N'Александрович', 4, N'4568953236', N'88005623535', N'Работает', 1)
INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Passport_data], [Telephone], [Status], [id_Category]) VALUES (21, N'Алексей', N'Вавилов', N'Евгеньевич', 5, N'8458458458', N'89657878388', N'Работает', 2)
INSERT [dbo].[Trainers] ([id_Trainer], [Name], [Surname], [Patronymic], [Experience], [Passport_data], [Telephone], [Status], [id_Category]) VALUES (22, N'Николай', N'Науменко', N'Демидович', 12, N'8987676554', N'88976887764', N'Работает', 3)
SET IDENTITY_INSERT [dbo].[Trainers] OFF
GO
SET IDENTITY_INSERT [dbo].[Workers] ON 

INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (1, N'Александр', N'Петренко', N'Юрьевич', N'5454525252', N'88005362563', N'1', N'1', 2, N'Работает', N'myfitnessapp.app@gmail.com')
INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (2, N'Елизавета', N'Гражданова', N'Емельяновна', N'5453626568', N'88566235454', N'2', N'2', 1, N'Работает', N'myfitnessapp.app@gmail.com')
INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (3, N'Афанасий', N'Егоров', N'Юрьевич', N'5656767676', N'88009876554', NULL, NULL, 3, N'Не работает', N'myfitnessapp.app@gmail.com')
INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (8, N'Данил', N'Деректоров', N'Деректрович', N'4567878787', N'88009087867', N'3', N'3', 3, N'Работает', N'myfitnessapp.app@gmail.com')
INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (13, N'Алевтинеа', N'Егорова', N'Игоревна', N'7348743784', N'88007457548', N'alevtiNa228', N'AA5C19C8E29A3D3939AA736AB5222AE1', 1, N'Работает', N'alevtina@mail.ru')
INSERT [dbo].[Workers] ([id_Worker], [Name], [Surname], [Patronymic], [Passport_data], [Telephone], [Login], [Password], [id_Role], [Status], [Email]) VALUES (14, N'Виталий', N'Самойлов', N'Антонович', N'8878979879', N'78989898989', N'123', N'202CB962AC59075B964B07152D234B70', 2, N'Работает', N'email@email.ru')
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
ALTER TABLE [dbo].[Trainers]  WITH CHECK ADD  CONSTRAINT [FK_Trainers_Categories] FOREIGN KEY([id_Category])
REFERENCES [dbo].[Categories] ([id_Category])
GO
ALTER TABLE [dbo].[Trainers] CHECK CONSTRAINT [FK_Trainers_Categories]
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
