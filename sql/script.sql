USE [master]
GO
/****** Object:  Database [NerdStoreEnterprise]    Script Date: 25/06/2023 20:05:20 ******/
CREATE DATABASE [NerdStoreEnterprise]
GO

USE [NerdStoreEnterprise]
GO

/****** Object:  Sequence [dbo].[OrderCodeSequence]    Script Date: 25/06/2023 20:05:20 ******/
CREATE SEQUENCE [dbo].[OrderCodeSequence] 
 AS [int]
 START WITH 1000
 INCREMENT BY 1
 MINVALUE -2147483648
 MAXVALUE 2147483647
 CACHE 
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 25/06/2023 20:05:20 ******/
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
/****** Object:  Table [dbo].[Addresses]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Addresses](
	[Id] [uniqueidentifier] NOT NULL,
	[StreetAddress] [varchar](200) NOT NULL,
	[BuildingNumber] [varchar](50) NOT NULL,
	[SecondaryAddress] [varchar](250) NOT NULL,
	[Neighborhood] [varchar](100) NOT NULL,
	[ZipCode] [varchar](20) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](50) NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Addresses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartItems]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartItems](
	[Id] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [varchar](100) NULL,
	[Quantity] [int] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Image] [varchar](100) NULL,
	[ShoppingCartId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_CartItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customers]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customers](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](200) NOT NULL,
	[Email] [varchar](254) NOT NULL,
	[Cpf] [varchar](11) NOT NULL,
	[Excluded] [bit] NOT NULL,
 CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerShoppingCarts]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerShoppingCarts](
	[Id] [uniqueidentifier] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[Total] [decimal](18, 2) NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[HasVoucher] [bit] NOT NULL,
	[VoucherCode] [varchar](50) NULL,
	[VoucherDiscount] [decimal](18, 2) NULL,
	[Voucher_DiscountType] [int] NOT NULL,
	[VoucherPercentage] [decimal](18, 2) NULL,
 CONSTRAINT [PK_CustomerShoppingCarts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderItems]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderItems](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[ProductId] [uniqueidentifier] NOT NULL,
	[ProductName] [varchar](100) NOT NULL,
	[Quantity] [int] NOT NULL,
	[ProductImage] [varchar](100) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_OrderItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [int] NOT NULL,
	[CustomerId] [uniqueidentifier] NOT NULL,
	[VoucherId] [uniqueidentifier] NULL,
	[HasVoucher] [bit] NOT NULL,
	[Discount] [decimal](18, 2) NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[DateAdded] [datetime2](7) NOT NULL,
	[OrderStatus] [int] NOT NULL,
	[StreetAddress] [varchar](100) NOT NULL,
	[BuildingNumber] [varchar](100) NOT NULL,
	[SecondaryAddress] [varchar](100) NOT NULL,
	[Neighborhood] [varchar](100) NOT NULL,
	[ZipCode] [varchar](100) NOT NULL,
	[City] [varchar](100) NOT NULL,
	[State] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Payments]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Payments](
	[Id] [uniqueidentifier] NOT NULL,
	[OrderId] [uniqueidentifier] NOT NULL,
	[PaymentType] [int] NOT NULL,
	[Amount] [decimal](18, 2) NOT NULL,
 CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produtos]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produtos](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [varchar](250) NOT NULL,
	[Description] [varchar](500) NOT NULL,
	[Active] [bit] NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[RegistrationDate] [datetime2](7) NOT NULL,
	[Image] [varchar](100) NOT NULL,
	[StockQuantity] [int] NOT NULL,
 CONSTRAINT [PK_Produtos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [uniqueidentifier] NOT NULL,
	[Token] [uniqueidentifier] NOT NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](max) NULL,
 CONSTRAINT [PK_RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SecurityKeys]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SecurityKeys](
	[Id] [uniqueidentifier] NOT NULL,
	[KeyId] [nvarchar](max) NULL,
	[Type] [nvarchar](max) NULL,
	[Parameters] [nvarchar](max) NULL,
	[IsRevoked] [bit] NOT NULL,
	[RevokedReason] [nvarchar](max) NULL,
	[CreationDate] [datetime2](7) NOT NULL,
	[ExpiredAt] [datetime2](7) NULL,
 CONSTRAINT [PK_SecurityKeys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transactions]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transactions](
	[Id] [uniqueidentifier] NOT NULL,
	[AuthorizationCode] [varchar](100) NULL,
	[CreditCardCompany] [varchar](100) NULL,
	[TransactionDate] [datetime2](7) NULL,
	[Amount] [decimal](18, 2) NOT NULL,
	[TransactionCost] [decimal](18, 2) NOT NULL,
	[TransactionStatus] [int] NOT NULL,
	[TID] [varchar](100) NULL,
	[NSU] [varchar](100) NULL,
	[PaymentId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Transactions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Vouchers]    Script Date: 25/06/2023 20:05:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vouchers](
	[Id] [uniqueidentifier] NOT NULL,
	[Code] [varchar](100) NOT NULL,
	[Percentage] [decimal](18, 2) NULL,
	[Discount] [decimal](18, 2) NULL,
	[Quantity] [int] NOT NULL,
	[DiscountType] [int] NOT NULL,
	[CreatedAt] [datetime2](7) NOT NULL,
	[UsedAt] [datetime2](7) NULL,
	[ExpirationDate] [datetime2](7) NOT NULL,
	[Active] [bit] NOT NULL,
	[Used] [bit] NOT NULL,
 CONSTRAINT [PK_Vouchers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221219190234_initial', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230105182951_Initial', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230120134950_Clientes', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230130214948_carrinho', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230201204518_carrinho-corrigido', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230226120356_Voucher', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230325124822_Voucher', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230325125406_Voucher-correcao', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230416011809_Order', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230422170747_Delete-Cascade', N'6.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230429184234_correcao-address', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230430033642_correcao-address-2', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230513192830_payment', N'7.0.3')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230618183132_SecurityKeysStore', N'6.0.12')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20230620224048_RefreshTokens', N'6.0.12')
GO
INSERT [dbo].[Addresses] ([Id], [StreetAddress], [BuildingNumber], [SecondaryAddress], [Neighborhood], [ZipCode], [City], [State], [CustomerId]) VALUES (N'79fc96ec-42f8-478f-9e68-5249f0cb87bd', N'Rua José de Pieri', N'71', N'71', N'Tanque Caio', N'09435410', N'Ribeirão Pires', N'SP', N'05c7441e-f0e4-4573-bacd-af71a49c8f67')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 

INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'05c7441e-f0e4-4573-bacd-af71a49c8f67', N'Catalogo', N'Read')
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'1772e17d-8857-4f63-b164-39c60bd69b83', N'Catalogo', N'Read')
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'05c7441e-f0e4-4573-bacd-af71a49c8f67', N'thiago.pereira.370@hotmail.com', N'THIAGO.PEREIRA.370@HOTMAIL.COM', N'thiago.pereira.370@hotmail.com', N'THIAGO.PEREIRA.370@HOTMAIL.COM', 1, N'AQAAAAEAACcQAAAAEMG5TYnRazqwMxVPVKp3ylw3FIP4UUPNKGamr/2StAGAg7HSdW/inG3QfdKj6H6WRw==', N'FKD3XQFPO6OZPGRVPGG4ESFCFA6CV3YG', N'2a936a5f-3024-47d7-9e01-d36cc829382a', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'1772e17d-8857-4f63-b164-39c60bd69b83', N'riquelme122@gmail.com', N'RIQUELME122@GMAIL.COM', N'riquelme122@gmail.com', N'RIQUELME122@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOv6nj5vRD9TDkhLHmX1pYYWKXRkZjwIpA0Y7uaahhbG6rNwFjiGmeGZevvLbp4g/g==', N'7RLWSK63T3YNHCRIZGBLU54MX4PZ6ELK', N'3cf0a847-27ae-459e-a1d0-43df5033048b', NULL, 0, 0, NULL, 1, 0)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'd8ee534d-a4d5-4bb8-80a9-2d88637b67ff', N'testeBus370@gmail.com', N'TESTEBUS370@GMAIL.COM', N'testeBus370@gmail.com', N'TESTEBUS370@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEOlrQYGoA/5+s/HX2sdlavLChzPevrCFsgA4zKxQH3MBTkmJHXUDY1uOMgJBlzYwBA==', N'32OY3VRCISXOL6LBQY3BRX7FJC4XXEWA', N'6bc01415-5dd2-4f06-8fc8-3a3e5ac350cf', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[CartItems] ([Id], [ProductId], [Name], [Quantity], [Price], [Image], [ShoppingCartId]) VALUES (N'2a313503-849d-4f6c-a1f0-1e3cec0228fa', N'6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb', N'Camiseta Code Life Cinza', 1, CAST(80.00 AS Decimal(18, 2)), N'camiseta3.jpg', N'f73f5119-e818-4b13-8384-b06dee218ebd')
GO
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Cpf], [Excluded]) VALUES (N'd8ee534d-a4d5-4bb8-80a9-2d88637b67ff', N'Teste Bus', N'testeBus370@gmail.com', N'45138409000', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Cpf], [Excluded]) VALUES (N'1772e17d-8857-4f63-b164-39c60bd69b83', N'Riquelme Santos', N'riquelme122@gmail.com', N'51999996801', 0)
INSERT [dbo].[Customers] ([Id], [Name], [Email], [Cpf], [Excluded]) VALUES (N'05c7441e-f0e4-4573-bacd-af71a49c8f67', N'Thiago Pereira dos Santos', N'thiago.pereira.370@hotmail.com', N'56301764048', 0)
GO
INSERT [dbo].[CustomerShoppingCarts] ([Id], [CustomerId], [Total], [Discount], [HasVoucher], [VoucherCode], [VoucherDiscount], [Voucher_DiscountType], [VoucherPercentage]) VALUES (N'f73f5119-e818-4b13-8384-b06dee218ebd', N'05c7441e-f0e4-4573-bacd-af71a49c8f67', CAST(80.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 0, NULL, NULL, 0, NULL)
GO
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [ProductImage], [Price]) VALUES (N'25b9ecab-380f-4409-b169-8c18610c792a', N'6de667f4-59c2-4f47-9903-dc8f3c2f0f27', N'7d67df76-2d4e-4a47-a19c-08eb80a9060b', N'Camiseta Code Life Preta', 1, N'camiseta2.jpg', CAST(90.00 AS Decimal(18, 2)))
INSERT [dbo].[OrderItems] ([Id], [OrderId], [ProductId], [ProductName], [Quantity], [ProductImage], [Price]) VALUES (N'665ccda9-ec33-4697-9e36-c735d8971a1f', N'4f810927-70a4-472b-8fca-ffa62a0afdaf', N'fc184e11-014c-4978-aa10-9eb5e1af369b', N'Camiseta Software Developer', 1, N'camiseta1.jpg', CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasVoucher], [Discount], [Amount], [DateAdded], [OrderStatus], [StreetAddress], [BuildingNumber], [SecondaryAddress], [Neighborhood], [ZipCode], [City], [State]) VALUES (N'6de667f4-59c2-4f47-9903-dc8f3c2f0f27', 1030, N'05c7441e-f0e4-4573-bacd-af71a49c8f67', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(90.00 AS Decimal(18, 2)), CAST(N'2023-06-13T23:31:37.3583453' AS DateTime2), 5, N'Rua José de Pieri', N'71', N'71', N'Tanque Caio', N'09435410', N'Ribeirão Pires', N'SP')
INSERT [dbo].[Orders] ([Id], [Code], [CustomerId], [VoucherId], [HasVoucher], [Discount], [Amount], [DateAdded], [OrderStatus], [StreetAddress], [BuildingNumber], [SecondaryAddress], [Neighborhood], [ZipCode], [City], [State]) VALUES (N'4f810927-70a4-472b-8fca-ffa62a0afdaf', 1031, N'05c7441e-f0e4-4573-bacd-af71a49c8f67', NULL, 0, CAST(0.00 AS Decimal(18, 2)), CAST(100.00 AS Decimal(18, 2)), CAST(N'2023-06-13T23:43:32.4963737' AS DateTime2), 2, N'Rua José de Pieri', N'71', N'71', N'Tanque Caio', N'09435410', N'Ribeirão Pires', N'SP')
GO
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Amount]) VALUES (N'5467a595-bc86-43e8-a6cb-07e0878df3da', N'6de667f4-59c2-4f47-9903-dc8f3c2f0f27', 1, CAST(90.00 AS Decimal(18, 2)))
INSERT [dbo].[Payments] ([Id], [OrderId], [PaymentType], [Amount]) VALUES (N'23e7f4fe-d517-4509-ad37-3b8d5b8f1cfe', N'4f810927-70a4-472b-8fca-ffa62a0afdaf', 1, CAST(100.00 AS Decimal(18, 2)))
GO
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'7d67df76-2d4e-4a47-a19c-08eb80a9060b', N'Camiseta Code Life Preta', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(90.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta2.jpg', 0)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'78162be3-61c4-4959-89d7-5ebfb476427e', N'Caneca No Coffee No Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(50.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca4.jpg', 100)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'6ecaaa6b-ad9f-422c-b3bb-6013ec27b4bb', N'Camiseta Debugar Preta', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(48.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta4.jpg', 120)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'6ecaaa6b-ad9f-422c-b3bb-6013ec27c4bb', N'Camiseta Code Life Cinza', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(80.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta3.jpg', 7)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'52dd696b-0882-4a73-9525-6af55dd416a4', N'Caneca Star Bugs Coffee', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca1.jpg', 0)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'191ddd3e-acd4-4c3b-ae74-8e473993c5da', N'Caneca Programmer Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(15.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca2.jpg', 1)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'fc184e11-014c-4978-aa10-9eb5e1af369b', N'Camiseta Software Developer', N'Camiseta 100% algodão, resistente a lavagens e altas temperaturas.', 1, CAST(100.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'camiseta1.jpg', 0)
INSERT [dbo].[Produtos] ([Id], [Name], [Description], [Active], [Price], [RegistrationDate], [Image], [StockQuantity]) VALUES (N'20e08cd4-2402-4e76-a3c9-a026185b193d', N'Caneca Turn Coffee in Code', N'Caneca de porcelana com impressão térmica de alta resistência.', 1, CAST(20.00 AS Decimal(18, 2)), CAST(N'2019-07-19T00:00:00.0000000' AS DateTime2), N'caneca3.jpg', 0)
GO
INSERT [dbo].[RefreshTokens] ([Id], [Token], [ExpirationDate], [UserName]) VALUES (N'34e02860-94d9-44fd-868f-54679f3c75fe', N'bb22dd49-44af-40f1-810a-a959eed71517', CAST(N'2023-06-25T06:23:16.2006018' AS DateTime2), N'thiago.pereira.370@hotmail.com')
GO
INSERT [dbo].[SecurityKeys] ([Id], [KeyId], [Type], [Parameters], [IsRevoked], [RevokedReason], [CreationDate], [ExpiredAt]) VALUES (N'8d7bc4e1-280b-4942-9217-064b027e842b', N'z5byUQgJpOkvubGCxezG0Q', N'RSA', N'{"AdditionalData":{},"Alg":null,"Crv":null,"D":"hU_Gp1umB3J1zYtoED9GguMF_F_YW4Ev84SesiZe6nxgM7_vxh0JJxh0rdxol1CB7QjeBVLQ4jUGPuAYl7OfAchGX1lwZ7t_5E6ad-9yvgPC4bDBs9O3lXvfl8XvWG_ZBnDhqrrvNbQAL-6raq5DRHz93kQzRCnqnDtWl2rFXR_HhYcocw363BcLFhU5-CXA7QT1cPEG_BGHRLcILICgd79nbBMlKS4I545KYlUvpxTXOURlBm_aQIw_QqiYGVHC8Vvx-a1-ZOXioLU7zIQypjQ1305vcPrAy8YRu9SIwhejvJZJu-3sdC1ICc_yoypXvYRcztcTikd1VcJbMi1y4TJ8wHANULIzMsTsfFZplGTKnC-7YiNUzVMMx8l1d1Dron3d7eGa8UW9oW2Or4ell0h6CRTSbv76DOgMHuFLpZ5ZsP8b4pQ_7G1XXA1TaLNN1MRhTwQ6vl8C4sZabcIuAJ5u-TfkIMZDUp6A4GKoz3Op5pln8Tom6xrvIUscFsu1","DP":"bOUZo1X7Q-a-f32HLvT_QlFYA2FGhrW_FRcKzCR7QxLM2ahcj_vOkezlEVsKTJzTum5qkmIXBnl_fZC2_BER3xoIrWqd8EKd_ztG1VmKR0VZrCU7ixZ5OKvpLntGqMVDNV1oqxCM2sRdK2zYpgSE19jWtxzxWOovbusw4P2pRHf240nD9ZCJI9D3KKD9FXBpSxfYw6zelSqE9ijpxxT2Ap7fGwsZuP1GkqfglYhpHM46Avemv1bVeaIOxHEnHUHP","DQ":"785LPgxbxHeMZiMexVmGo6E60RZ-FIQMOi_sFJqyZiiznNp7tN5e12FsB5ZhA6hyzIMqalH29k5xcu0f5Zi3bZeTViyEpb_dOO8efd9xp9fQXFNf7bLSa-HKBLlPFrGWPV_mpBJql1TIxUoWc1DYfqvyBdBEvMuHiAeihbwdtZ5GXCSsNSXoukvYJH5gJqnP45TjJSFnbqY6jL3GTCuH54wA_C4AqNf9iDHWSeWhO9EFnUPiiIQXtwTGAvMKgjPF","E":"AQAB","K":null,"KeyId":"z5byUQgJpOkvubGCxezG0Q","KeyOps":[],"Kid":"z5byUQgJpOkvubGCxezG0Q","Kty":"RSA","N":"6eyoMqCv7dpu-Hm3IwOQBB-CGMGm2VT7eWLYysGq8cXFV5Bvyx43Jnvo7kMCqhbcUgYJ-yqn0CuYoxC6RSk4NS8mzKdFFBbV-aSDup7b758rR-tZzT89gvXzwX_jQXkowCbgzbbg1oh7PT-ttAZtz3y-RPRS8GomhaFjrE4W0vDni8wj9X0ONZmF5-OKnArWTA3cy7nIzaGqwxMRW9GB3s-OIiNc3uUuCYyg6j-bhK5xc9rkm_RMMdyB3fz2PuvWUAvALrPyCpUO9p6gRREtzrIKWp0yv86ikPlUgOd-3lqyCJTXJ-xBLdZH3gwZu0IyFrFp3XRJwB31YdFE63AwB4u0gAXa34_DAZ9BUmFtxxBGvjhLTiaJ4lomZPiU1e2lqTXsIGEaF39PbnW5A2GEN-KrUHu8n6JzS5zVaJRU0lXVUFT5ttttqDX4j1rtRaYVB7DWfol9nus0W4xXutP6piTLBj3ugrReHLGKws73J16MwgZ_biZAGC0dqW2MnDnx","Oth":null,"P":"9ccWH8DKytisgKXycdsUPr4MQtbV7-ddhbNrmKGCkymxGQpO6MWvT34Axy395g38V-94Kjfs9jEo2v_oahQizvQxZuOmWHfQ-OoA-a6WL0UaGcl1mpy9VQajLR4DFgyJYbeHrcWeFGloVulefgfxOh-RRDD81Wv6DMWE-noXRroJZEGfR2PndVrudfI-dW3d6WMn9dKdou1EM0rXYhIVR2lKhhSnlOzxrD-uuc3kxKtF_F1czPnlv8KSP_P4eXZH","Q":"86ddCiKJT24jufy7tqgOyN8z-kIeORSuRiNxbKekHbsv1L9ln9GYRs5AeWzaQ56U-rKMF1KU1WtrohtVOPCYz4Uw5T3GVVk9at38CPoyFZSijK_7DRWZXCJYoHuGvj0Pj9EGR1UssD8_kNHLG8V3Y71LQ8H4AMNZWnaNZHZpuMA0TazEKiRUnfqo3mjYHfLtwbUsf8CMRqL3blwS7mjHR_yrEypmFzJXtnZYfSVkJ_vARN-vRljCva6gW2pIvxIH","QI":"e5yEAHA7Ebnweov3ExuqzOXTzPQ8dMLcSYnIXOUTgJFUoxiELYBuLVKJwN3yfm270_97lmB_CA9XnkPRQo2AIIZUDZ1gxih0gmjVa_-qqXJOqAZ782OeifxFr1LbgonP2JcNMYxilQii1M_nJWM55fzyjC0L-jNDFNaP2lk6nj5gRSF33zWyl7Iv6XKlzr95i0m1dWlMOWxOXWdXpd5GHWlL7ONByBG4P8i-LiAc_HrQJkOWqfS7BKmjz4dHHvOB","Use":null,"X":null,"X5c":[],"X5t":null,"X5tS256":null,"X5u":null,"Y":null,"KeySize":3072,"HasPrivateKey":true,"CryptoProviderFactory":{"CryptoProviderCache":{},"CustomCryptoProvider":null,"CacheSignatureProviders":true,"SignatureProviderObjectPoolCacheSize":32}}', 0, NULL, CAST(N'2023-06-18T19:14:52.6195484' AS DateTime2), NULL)
GO
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'879e413b-9654-4e76-bb38-0eb02ed8d6f3', N'OQ1NNCQA1S', N'MasterCard', CAST(N'2023-06-13T23:31:58.5431491' AS DateTime2), CAST(90.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'MIXFE1FG1I', N'PHB41UCHKJ', N'5467a595-bc86-43e8-a6cb-07e0878df3da')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'0f035fb7-c813-4eb9-9429-2c4f8dff06d8', N'D6QXFGEQ54', N'MasterCard', CAST(N'2023-06-13T23:43:32.4314317' AS DateTime2), CAST(100.00 AS Decimal(18, 2)), CAST(3.00 AS Decimal(18, 2)), 1, N'48CU5RXDWH', N'341TJ20OWB', N'23e7f4fe-d517-4509-ad37-3b8d5b8f1cfe')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'623fe61c-4dff-441e-8264-4cc3ab30df25', N'YGOM8WTZCK', N'MasterCard', CAST(N'2023-06-13T23:50:36.3287907' AS DateTime2), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'48CU5RXDWH', N'341TJ20OWB', N'23e7f4fe-d517-4509-ad37-3b8d5b8f1cfe')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'ea119b9b-ebbc-41ff-8784-764c6ee83cb4', N'DJL4VWYA59', N'MasterCard', CAST(N'2023-06-13T23:31:36.0192502' AS DateTime2), CAST(90.00 AS Decimal(18, 2)), CAST(2.70 AS Decimal(18, 2)), 1, N'MIXFE1FG1I', N'PHB41UCHKJ', N'5467a595-bc86-43e8-a6cb-07e0878df3da')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'f4c5c2e0-d94a-412e-8928-81d3c0180954', N'N2UGOOMHK1', N'MasterCard', CAST(N'2023-06-13T23:49:14.5581557' AS DateTime2), CAST(90.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'MIXFE1FG1I', N'PHB41UCHKJ', N'5467a595-bc86-43e8-a6cb-07e0878df3da')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'78257eab-20b2-4e47-80ab-b0fcfa3c99c7', N'HDG165PF3T', N'MasterCard', CAST(N'2023-06-13T23:31:45.6487296' AS DateTime2), CAST(90.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'MIXFE1FG1I', N'PHB41UCHKJ', N'5467a595-bc86-43e8-a6cb-07e0878df3da')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'1c37877d-90c9-487c-bb54-de61c0f9979d', N'JRY2N8O4N0', N'MasterCard', CAST(N'2023-06-13T23:49:44.5771267' AS DateTime2), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'48CU5RXDWH', N'341TJ20OWB', N'23e7f4fe-d517-4509-ad37-3b8d5b8f1cfe')
INSERT [dbo].[Transactions] ([Id], [AuthorizationCode], [CreditCardCompany], [TransactionDate], [Amount], [TransactionCost], [TransactionStatus], [TID], [NSU], [PaymentId]) VALUES (N'307b0ac2-2031-4d67-8831-e88aea83e974', N'PH0G8NRNT2', N'MasterCard', CAST(N'2023-06-13T23:43:34.5601590' AS DateTime2), CAST(100.00 AS Decimal(18, 2)), CAST(0.00 AS Decimal(18, 2)), 2, N'48CU5RXDWH', N'341TJ20OWB', N'23e7f4fe-d517-4509-ad37-3b8d5b8f1cfe')
GO
INSERT [dbo].[Vouchers] ([Id], [Code], [Percentage], [Discount], [Quantity], [DiscountType], [CreatedAt], [UsedAt], [ExpirationDate], [Active], [Used]) VALUES (N'2b653827-d64f-449f-9f30-777bed1d9631', N'150-0FF-GERAL', NULL, CAST(150.00 AS Decimal(18, 2)), 49, 1, CAST(N'2023-03-03T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-03T00:00:00.0000000' AS DateTime2), 1, 0)
INSERT [dbo].[Vouchers] ([Id], [Code], [Percentage], [Discount], [Quantity], [DiscountType], [CreatedAt], [UsedAt], [ExpirationDate], [Active], [Used]) VALUES (N'a1fea06b-a0bf-4d57-b6c9-cb50dcfef26d', N'50-0FF-GERAL', CAST(50.00 AS Decimal(18, 2)), NULL, 0, 0, CAST(N'2023-03-03T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-03T00:00:00.0000000' AS DateTime2), 1, 1)
INSERT [dbo].[Vouchers] ([Id], [Code], [Percentage], [Discount], [Quantity], [DiscountType], [CreatedAt], [UsedAt], [ExpirationDate], [Active], [Used]) VALUES (N'54c063b2-67c7-44fe-8fb9-e2d201bd5e4f', N'10-0FF-GERAL', CAST(10.00 AS Decimal(18, 2)), NULL, 50, 0, CAST(N'2023-03-03T00:00:00.0000000' AS DateTime2), NULL, CAST(N'2024-03-03T00:00:00.0000000' AS DateTime2), 1, 0)
GO
/****** Object:  Index [IX_Addresses_CustomerId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [IX_Addresses_CustomerId] ON [dbo].[Addresses]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetRoleClaims_RoleId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetRoleClaims_RoleId] ON [dbo].[AspNetRoleClaims]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [RoleNameIndex]    Script Date: 25/06/2023 20:05:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] ON [dbo].[AspNetRoles]
(
	[NormalizedName] ASC
)
WHERE ([NormalizedName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserClaims_UserId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserClaims_UserId] ON [dbo].[AspNetUserClaims]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserLogins_UserId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserLogins_UserId] ON [dbo].[AspNetUserLogins]
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_AspNetUserRoles_RoleId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_AspNetUserRoles_RoleId] ON [dbo].[AspNetUserRoles]
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [EmailIndex]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [EmailIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedEmail] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UserNameIndex]    Script Date: 25/06/2023 20:05:21 ******/
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex] ON [dbo].[AspNetUsers]
(
	[NormalizedUserName] ASC
)
WHERE ([NormalizedUserName] IS NOT NULL)
WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_CartItems_ShoppingCartId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_CartItems_ShoppingCartId] ON [dbo].[CartItems]
(
	[ShoppingCartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [idx_cliente]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [idx_cliente] ON [dbo].[CustomerShoppingCarts]
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_OrderItems_OrderId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_OrderItems_OrderId] ON [dbo].[OrderItems]
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Orders_VoucherId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_Orders_VoucherId] ON [dbo].[Orders]
(
	[VoucherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Transactions_PaymentId]    Script Date: 25/06/2023 20:05:21 ******/
CREATE NONCLUSTERED INDEX [IX_Transactions_PaymentId] ON [dbo].[Transactions]
(
	[PaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerShoppingCarts] ADD  DEFAULT ((0.0)) FOR [Discount]
GO
ALTER TABLE [dbo].[CustomerShoppingCarts] ADD  DEFAULT (CONVERT([bit],(0))) FOR [HasVoucher]
GO
ALTER TABLE [dbo].[CustomerShoppingCarts] ADD  DEFAULT ((0)) FOR [Voucher_DiscountType]
GO
ALTER TABLE [dbo].[Orders] ADD  DEFAULT (NEXT VALUE FOR [OrderCodeSequence]) FOR [Code]
GO
ALTER TABLE [dbo].[Addresses]  WITH CHECK ADD  CONSTRAINT [FK_Addresses_Customers_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customers] ([Id])
GO
ALTER TABLE [dbo].[Addresses] CHECK CONSTRAINT [FK_Addresses_Customers_CustomerId]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[CartItems]  WITH CHECK ADD  CONSTRAINT [FK_CartItems_CustomerShoppingCarts_ShoppingCartId] FOREIGN KEY([ShoppingCartId])
REFERENCES [dbo].[CustomerShoppingCarts] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[CartItems] CHECK CONSTRAINT [FK_CartItems_CustomerShoppingCarts_ShoppingCartId]
GO
ALTER TABLE [dbo].[OrderItems]  WITH CHECK ADD  CONSTRAINT [FK_OrderItems_Orders_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Orders] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrderItems] CHECK CONSTRAINT [FK_OrderItems_Orders_OrderId]
GO
ALTER TABLE [dbo].[Orders]  WITH CHECK ADD  CONSTRAINT [FK_Orders_Vouchers_VoucherId] FOREIGN KEY([VoucherId])
REFERENCES [dbo].[Vouchers] ([Id])
GO
ALTER TABLE [dbo].[Orders] CHECK CONSTRAINT [FK_Orders_Vouchers_VoucherId]
GO
ALTER TABLE [dbo].[Transactions]  WITH CHECK ADD  CONSTRAINT [FK_Transactions_Payments_PaymentId] FOREIGN KEY([PaymentId])
REFERENCES [dbo].[Payments] ([Id])
GO
ALTER TABLE [dbo].[Transactions] CHECK CONSTRAINT [FK_Transactions_Payments_PaymentId]
GO
USE [master]
GO
ALTER DATABASE [NerdStoreEnterprise] SET  READ_WRITE 
GO
