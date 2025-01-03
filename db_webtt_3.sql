USE [Web_DonNghiPhep]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ApprovalHistories]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApprovalHistories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LeaveRequestId] [int] NOT NULL,
	[ApprovedById] [nvarchar](max) NOT NULL,
	[Action] [nvarchar](max) NOT NULL,
	[ProcessedAt] [datetime2](7) NOT NULL,
	[Employee_ID] [nvarchar](20) NULL,
 CONSTRAINT [PK_ApprovalHistories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Department_id] [nvarchar](450) NOT NULL,
	[DepartmentName] [nvarchar](100) NOT NULL,
	[ManagerId] [nvarchar](max) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
	[ParentId] [nvarchar](450) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Department_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DepartmentEmployee]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DepartmentEmployee](
	[DepartmentId] [nvarchar](450) NOT NULL,
	[EmployeeId] [nvarchar](20) NOT NULL,
	[EmployeeIsManager] [bit] NOT NULL,
 CONSTRAINT [PK_DepartmentEmployee] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC,
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Employee_ID] [nvarchar](20) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Dob] [datetime2](7) NOT NULL,
	[Email] [nvarchar](max) NOT NULL,
	[PhoneNumber] [nvarchar](max) NOT NULL,
	[Title_id] [nvarchar](450) NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Employee_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveBalance]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveBalance](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [nvarchar](20) NULL,
	[Year] [int] NOT NULL,
	[TotalDays] [int] NOT NULL,
	[UsedDays] [int] NOT NULL,
	[RemainingDays] [int] NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_LeaveBalance] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveRequest]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRequest](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Employee_id] [nvarchar](20) NOT NULL,
	[StartDate] [datetime2](7) NOT NULL,
	[EndDate] [datetime2](7) NOT NULL,
	[Reason] [nvarchar](500) NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[ApprovedById] [nvarchar](20) NULL,
	[NextApproverId] [nvarchar](20) NULL,
	[DepartmentId] [nvarchar](450) NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UpdatedAt] [datetime2](7) NULL,
 CONSTRAINT [PK_LeaveRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Log]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Log](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NULL,
	[Action] [nvarchar](255) NOT NULL,
	[ActionTime] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Role_ID] [int] IDENTITY(1,1) NOT NULL,
	[Role_Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[Role_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Title]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Title](
	[Title_id] [nvarchar](450) NOT NULL,
	[Title_name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Title] PRIMARY KEY CLUSTERED 
(
	[Title_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [uniqueidentifier] NOT NULL,
	[Employee_ID] [nvarchar](20) NOT NULL,
	[UserName] [nvarchar](20) NOT NULL,
	[Password] [nvarchar](20) NOT NULL,
	[Status] [bit] NOT NULL,
	[created_at] [datetime2](7) NOT NULL,
	[updated_at] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 1/2/2025 4:13:27 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserID] [uniqueidentifier] NOT NULL,
	[RoleID] [int] NOT NULL,
 CONSTRAINT [PK_UserRole] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241221174108_InitialDB', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241221191320_Updatedb_v1', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241222181957_Updatedb_v2', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241222182555_Updatedb_v3', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241224204358_UpdateDB_V4', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241225185534_UpdateDatabase_v5', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226174659_UpdateDB_v5', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241226182850_UpdateDB_v6', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227162932_UpdateDB_V7', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241227165719_UpdateDB_V8', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20241229184436_UpdateDatabase_v9', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101164918_UpdateDB_V9', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250101172927_UpdateDB_V10', N'9.0.0')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250102074748_UpdateDB_V11', N'9.0.0')
GO
SET IDENTITY_INSERT [dbo].[ApprovalHistories] ON 

INSERT [dbo].[ApprovalHistories] ([Id], [LeaveRequestId], [ApprovedById], [Action], [ProcessedAt], [Employee_ID]) VALUES (1, 14, N'EMP004', N'Approved and Forwarded', CAST(N'2025-01-02T14:50:12.2158236' AS DateTime2), NULL)
INSERT [dbo].[ApprovalHistories] ([Id], [LeaveRequestId], [ApprovedById], [Action], [ProcessedAt], [Employee_ID]) VALUES (2, 12, N'EMP004', N'Rejected', CAST(N'2025-01-02T14:50:50.5155264' AS DateTime2), NULL)
INSERT [dbo].[ApprovalHistories] ([Id], [LeaveRequestId], [ApprovedById], [Action], [ProcessedAt], [Employee_ID]) VALUES (3, 14, N'EMP002', N'Approved', CAST(N'2025-01-02T14:51:26.7958869' AS DateTime2), NULL)
SET IDENTITY_INSERT [dbo].[ApprovalHistories] OFF
GO
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP001', N'Phòng Nhân Sự', N'NVIT007', CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), NULL)
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP002', N'Phòng Kế Toán', N'EMP003', CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), CAST(N'2024-12-23T12:53:24.3211373' AS DateTime2), NULL)
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP003', N'Phòng Kỹ Thuật', NULL, CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), NULL)
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP004', N'Phòng Marketing', N'EMP004', CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), CAST(N'2024-12-25T00:50:05.8166652' AS DateTime2), N'DEP006')
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP005', N'Phòng Bán Hàng', NULL, CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), CAST(N'2024-12-22T00:52:32.6433333' AS DateTime2), NULL)
INSERT [dbo].[Department] ([Department_id], [DepartmentName], [ManagerId], [CreatedAt], [UpdatedAt], [ParentId]) VALUES (N'DEP006', N'Phòng Quản Lý', N'EMP002', CAST(N'2024-12-22T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-22T00:00:00.0000000' AS DateTime2), NULL)
GO
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP001', N'NVIT007', 1)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP001', N'NVIT008', 0)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP004', N'EMP003', 0)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP004', N'EMP004', 1)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP004', N'EMP005', 0)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP004', N'NVIT001', 0)
INSERT [dbo].[DepartmentEmployee] ([DepartmentId], [EmployeeId], [EmployeeIsManager]) VALUES (N'DEP006', N'EMP002', 1)
GO
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'EMP002', N'Trần Thị Bình', CAST(N'1992-02-02T00:00:00.0000000' AS DateTime2), N'tranthibinh@example.com', N'0987654322', N'tp')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'EMP003', N'Lê Văn Cường', CAST(N'1985-03-03T00:00:00.0000000' AS DateTime2), N'levancuong@example.com', N'0987654323', N'nv')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'EMP004', N'Phạm Thị Dung', CAST(N'1998-04-04T00:00:00.0000000' AS DateTime2), N'phamthidung@example.com', N'0987654324', N'tp')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'EMP005', N'Hoàng Văn Em', CAST(N'2000-05-05T00:00:00.0000000' AS DateTime2), N'hoangvanem@example.com', N'0987654325', N'nv')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'EMPADMIN', N'ADMIN_1', CAST(N'2005-12-22T00:00:00.0000000' AS DateTime2), N'admin@gmail.com', N'0123456789', N'gd')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'NVIT001', N'Nguyễn Hoàng Dương', CAST(N'2024-12-28T00:00:00.0000000' AS DateTime2), N'28rodina@rowdydow.com', N'0323565586', N'nv')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'NVIT005', N'Nguyễn Hoàng Lâm', CAST(N'2024-12-13T00:00:00.0000000' AS DateTime2), N'28rodina@rowdydow.com', N'0323565586', N'gd')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'NVIT007', N'Nguyễn Hoàng Đức', CAST(N'2025-01-11T00:00:00.0000000' AS DateTime2), N'28rodina@rowdydow.com', N'0323565586', N'tp')
INSERT [dbo].[Employee] ([Employee_ID], [FullName], [Dob], [Email], [PhoneNumber], [Title_id]) VALUES (N'NVIT008', N'Nguyễn Hoàng Phương', CAST(N'2025-01-23T00:00:00.0000000' AS DateTime2), N'28rodina@rowdydow.com', N'0323565586', N'nv')
GO
SET IDENTITY_INSERT [dbo].[LeaveBalance] ON 

INSERT [dbo].[LeaveBalance] ([Id], [Employee_id], [Year], [TotalDays], [UsedDays], [RemainingDays], [UpdatedAt]) VALUES (1, N'EMP005', 2024, 15, 2, 13, CAST(N'2024-12-25T23:01:13.6634471' AS DateTime2))
INSERT [dbo].[LeaveBalance] ([Id], [Employee_id], [Year], [TotalDays], [UsedDays], [RemainingDays], [UpdatedAt]) VALUES (2, N'EMP003', 2024, 15, 3, 12, CAST(N'2024-12-25T22:46:54.1928827' AS DateTime2))
INSERT [dbo].[LeaveBalance] ([Id], [Employee_id], [Year], [TotalDays], [UsedDays], [RemainingDays], [UpdatedAt]) VALUES (3, N'NVIT001', 2023, 16, 0, 16, CAST(N'2024-12-25T22:46:35.5668264' AS DateTime2))
INSERT [dbo].[LeaveBalance] ([Id], [Employee_id], [Year], [TotalDays], [UsedDays], [RemainingDays], [UpdatedAt]) VALUES (4, N'EMP004', 2024, 16, 0, 16, CAST(N'2024-12-25T22:46:35.0000000' AS DateTime2))
SET IDENTITY_INSERT [dbo].[LeaveBalance] OFF
GO
SET IDENTITY_INSERT [dbo].[LeaveRequest] ON 

INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (1, N'EMP005', CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-20T00:00:00.0000000' AS DateTime2), N'nghỉ ốm', N'Approved', N'EMP002', N'EMP002', N'DEP004', CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-19T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (2, N'EMP005', CAST(N'2024-05-19T00:00:00.0000000' AS DateTime2), CAST(N'2024-05-20T00:00:00.0000000' AS DateTime2), N'đi ăn đám cưới bên cồn', N'Pending', NULL, N'EMP004', N'DEP004', CAST(N'2024-12-24T01:58:54.9848841' AS DateTime2), CAST(N'2024-12-24T01:58:54.9849258' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (5, N'EMP003', CAST(N'2024-10-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-10-10T00:00:00.0000000' AS DateTime2), N'đi ăn đám giỗ bên cồn', N'Pending', NULL, N'EMP004', N'DEP004', CAST(N'2024-10-09T00:00:00.0000000' AS DateTime2), CAST(N'2024-10-09T00:00:00.0000000' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (9, N'EMP004', CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), CAST(N'2024-12-30T00:00:00.0000000' AS DateTime2), N'xcvbnm,', N'Pending', NULL, N'EMP002', N'DEP004', CAST(N'2024-12-29T03:53:45.6773759' AS DateTime2), CAST(N'2024-12-29T03:53:45.6774509' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (10, N'EMP005', CAST(N'2024-01-01T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-02T00:00:00.0000000' AS DateTime2), N'ăn cỗ tết dương', N'Pending', NULL, N'EMP004', N'DEP004', CAST(N'2024-12-30T01:39:41.8257014' AS DateTime2), CAST(N'2024-12-30T01:39:41.8259265' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (11, N'EMP003', CAST(N'2024-01-10T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-11T00:00:00.0000000' AS DateTime2), N'họp cái bang', N'Pending', NULL, N'EMP004', N'DEP004', CAST(N'2024-01-02T00:33:32.0000000' AS DateTime2), CAST(N'2024-01-02T00:40:04.0000000' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (12, N'EMP003', CAST(N'2024-01-05T00:00:00.0000000' AS DateTime2), CAST(N'2024-01-06T00:00:00.0000000' AS DateTime2), N'ăn sinh nhật', N'Rejected', N'EMP004', NULL, N'DEP004', CAST(N'2024-01-02T01:04:08.0000000' AS DateTime2), CAST(N'2025-01-02T14:50:50.5154981' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (13, N'EMP003', CAST(N'2025-01-04T00:00:00.0000000' AS DateTime2), CAST(N'2025-01-04T00:00:00.0000000' AS DateTime2), N'sadsdsd', N'Pending', NULL, N'EMP004', N'DEP004', CAST(N'2025-01-02T14:24:00.7266703' AS DateTime2), CAST(N'2025-01-02T14:24:00.7267169' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (14, N'EMP003', CAST(N'2025-01-04T00:00:00.0000000' AS DateTime2), CAST(N'2025-01-04T00:00:00.0000000' AS DateTime2), N'ádtyu', N'Approved', N'EMP002', N'EMP002', N'DEP004', CAST(N'2025-01-02T14:25:48.0163184' AS DateTime2), CAST(N'2025-01-02T14:51:26.7958785' AS DateTime2))
INSERT [dbo].[LeaveRequest] ([Id], [Employee_id], [StartDate], [EndDate], [Reason], [Status], [ApprovedById], [NextApproverId], [DepartmentId], [CreatedAt], [UpdatedAt]) VALUES (15, N'EMP003', CAST(N'2025-01-03T00:00:00.0000000' AS DateTime2), CAST(N'2025-01-03T00:00:00.0000000' AS DateTime2), N'ádfghj', N'Rejected', N'EMP004', NULL, N'DEP004', CAST(N'2025-01-02T14:26:10.9564004' AS DateTime2), CAST(N'2025-01-02T14:26:10.9564006' AS DateTime2))
SET IDENTITY_INSERT [dbo].[LeaveRequest] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([Role_ID], [Role_Name]) VALUES (1, N'Admin')
INSERT [dbo].[Role] ([Role_ID], [Role_Name]) VALUES (2, N'Quản lý')
INSERT [dbo].[Role] ([Role_ID], [Role_Name]) VALUES (3, N'Nhân viên')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
INSERT [dbo].[Title] ([Title_id], [Title_name]) VALUES (N'gd', N'Giám đốc')
INSERT [dbo].[Title] ([Title_id], [Title_name]) VALUES (N'nv', N'Nhân viên')
INSERT [dbo].[Title] ([Title_id], [Title_name]) VALUES (N'tp', N'Trưởng phòng')
INSERT [dbo].[Title] ([Title_id], [Title_name]) VALUES (N'tts', N'Thực tập sinh')
GO
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'df5ce10f-ed09-45ec-8bfb-126381c6dda7', N'EMP002', N'quanly01', N'Password123', 1, CAST(N'2024-12-22T00:57:07.1500000' AS DateTime2), CAST(N'2024-12-29T02:38:49.6025444' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'8a222226-f55d-4d33-ab3e-36b20eb29783', N'NVIT007', N'NVIT007', N'Password123', 1, CAST(N'2025-01-02T15:17:14.0788207' AS DateTime2), CAST(N'2025-01-02T15:17:14.0788796' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'2dd97819-f8a1-4fec-bcb9-5e6bbd1a73fd', N'NVIT005', N'NVIT002', N'Admin123', 1, CAST(N'2024-12-28T01:57:03.0709531' AS DateTime2), CAST(N'2024-12-28T01:57:03.0710184' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'17853819-cf92-4540-a04f-6ff8f949ab93', N'EMP005', N'nhanvien02', N'password123', 1, CAST(N'2024-12-22T00:57:07.1500000' AS DateTime2), CAST(N'2024-12-22T01:31:54.7238285' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'1e975144-0ced-4a2c-8afa-80308d518212', N'EMPADMIN', N'admin', N'Admin123', 1, CAST(N'2024-12-22T01:00:03.6700000' AS DateTime2), CAST(N'2025-01-02T15:57:40.8395579' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'fb0c861f-4205-4838-83b6-997182d72042', N'EMP004', N'quanly02', N'Password123', 1, CAST(N'2024-12-22T00:57:07.1500000' AS DateTime2), CAST(N'2024-12-28T23:54:52.6643727' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'df7b8e0d-c7fd-4586-88b9-cbf73a10d955', N'NVIT008', N'NVIT008', N'Password123', 1, CAST(N'2025-01-02T15:17:47.1858881' AS DateTime2), CAST(N'2025-01-02T15:17:47.1858882' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'3c69883b-8c09-43d7-84c6-e26ac11a9979', N'EMP003', N'nhanvien01', N'Password123', 1, CAST(N'2024-12-22T00:57:07.1500000' AS DateTime2), CAST(N'2024-12-28T01:31:21.4150259' AS DateTime2))
INSERT [dbo].[User] ([UserID], [Employee_ID], [UserName], [Password], [Status], [created_at], [updated_at]) VALUES (N'194e9dcf-7b3b-47a2-8a3a-f4f8ac2fa32e', N'NVIT001', N'admin001', N'Admin123', 1, CAST(N'2024-12-22T22:52:41.7810971' AS DateTime2), CAST(N'2025-01-02T14:14:54.1746501' AS DateTime2))
GO
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'1e975144-0ced-4a2c-8afa-80308d518212', 1)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'df5ce10f-ed09-45ec-8bfb-126381c6dda7', 2)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'8a222226-f55d-4d33-ab3e-36b20eb29783', 2)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'fb0c861f-4205-4838-83b6-997182d72042', 2)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'8a222226-f55d-4d33-ab3e-36b20eb29783', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'2dd97819-f8a1-4fec-bcb9-5e6bbd1a73fd', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'17853819-cf92-4540-a04f-6ff8f949ab93', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'fb0c861f-4205-4838-83b6-997182d72042', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'df7b8e0d-c7fd-4586-88b9-cbf73a10d955', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'3c69883b-8c09-43d7-84c6-e26ac11a9979', 3)
INSERT [dbo].[UserRole] ([UserID], [RoleID]) VALUES (N'194e9dcf-7b3b-47a2-8a3a-f4f8ac2fa32e', 3)
GO
ALTER TABLE [dbo].[ApprovalHistories]  WITH CHECK ADD  CONSTRAINT [FK_ApprovalHistories_Employee_Employee_ID] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[Employee] ([Employee_ID])
GO
ALTER TABLE [dbo].[ApprovalHistories] CHECK CONSTRAINT [FK_ApprovalHistories_Employee_Employee_ID]
GO
ALTER TABLE [dbo].[ApprovalHistories]  WITH CHECK ADD  CONSTRAINT [FK_ApprovalHistories_LeaveRequest_LeaveRequestId] FOREIGN KEY([LeaveRequestId])
REFERENCES [dbo].[LeaveRequest] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApprovalHistories] CHECK CONSTRAINT [FK_ApprovalHistories_LeaveRequest_LeaveRequestId]
GO
ALTER TABLE [dbo].[Department]  WITH CHECK ADD  CONSTRAINT [FK_Department_Department_ParentId] FOREIGN KEY([ParentId])
REFERENCES [dbo].[Department] ([Department_id])
GO
ALTER TABLE [dbo].[Department] CHECK CONSTRAINT [FK_Department_Department_ParentId]
GO
ALTER TABLE [dbo].[DepartmentEmployee]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentEmployee_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Department_id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DepartmentEmployee] CHECK CONSTRAINT [FK_DepartmentEmployee_Department_DepartmentId]
GO
ALTER TABLE [dbo].[DepartmentEmployee]  WITH CHECK ADD  CONSTRAINT [FK_DepartmentEmployee_Employee_EmployeeId] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Employee_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[DepartmentEmployee] CHECK CONSTRAINT [FK_DepartmentEmployee_Employee_EmployeeId]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Title_Title_id] FOREIGN KEY([Title_id])
REFERENCES [dbo].[Title] ([Title_id])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Title_Title_id]
GO
ALTER TABLE [dbo].[LeaveBalance]  WITH CHECK ADD  CONSTRAINT [FK_LeaveBalance_Employee_Employee_id] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaveBalance] CHECK CONSTRAINT [FK_LeaveBalance_Employee_Employee_id]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Department_DepartmentId] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([Department_id])
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Department_DepartmentId]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Employee_ApprovedById] FOREIGN KEY([ApprovedById])
REFERENCES [dbo].[Employee] ([Employee_ID])
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Employee_ApprovedById]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Employee_Employee_id] FOREIGN KEY([Employee_id])
REFERENCES [dbo].[Employee] ([Employee_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Employee_Employee_id]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Employee_NextApproverId] FOREIGN KEY([NextApproverId])
REFERENCES [dbo].[Employee] ([Employee_ID])
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Employee_NextApproverId]
GO
ALTER TABLE [dbo].[Log]  WITH CHECK ADD  CONSTRAINT [FK_Log_User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserID])
ON DELETE SET NULL
GO
ALTER TABLE [dbo].[Log] CHECK CONSTRAINT [FK_Log_User_UserId]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Employee_Employee_ID] FOREIGN KEY([Employee_ID])
REFERENCES [dbo].[Employee] ([Employee_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Employee_Employee_ID]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_Role_RoleID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([Role_ID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_Role_RoleID]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_UserRole_User_UserID] FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_UserRole_User_UserID]
GO
