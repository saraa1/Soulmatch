
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/10/2018 00:35:20
-- Generated from EDMX file: C:\Users\Sara Malik\Desktop\Soulmatch\Soul\Models\DbModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [mydatabase];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[brokers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[brokers];
GO
IF OBJECT_ID(N'[dbo].[registered_users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[registered_users];
GO
IF OBJECT_ID(N'[dbo].[requests]', 'U') IS NOT NULL
    DROP TABLE [dbo].[requests];
GO
IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'brokers'
CREATE TABLE [dbo].[brokers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nchar(50)  NOT NULL,
    [Name] nchar(50)  NOT NULL,
    [Email] nchar(50)  NOT NULL,
    [City] nchar(50)  NOT NULL,
    [CNIC] nchar(50)  NOT NULL,
    [Contact_No] nchar(50)  NOT NULL,
    [BrokerID] int  NOT NULL
);
GO

-- Creating table 'registered_users'
CREATE TABLE [dbo].[registered_users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Image] varchar(max)  NULL,
    [Fullname] varchar(100)  NULL,
    [Username] varchar(100)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Age] int  NOT NULL,
    [CNIC] varchar(150)  NOT NULL,
    [Adress] varchar(50)  NOT NULL,
    [Contact_no] varchar(150)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Salary] int  NOT NULL,
    [Gender] varchar(50)  NOT NULL,
    [Religion] varchar(max)  NOT NULL,
    [Cast] varchar(max)  NOT NULL,
    [Profession] varchar(max)  NOT NULL,
    [Account_no] varchar(150)  NOT NULL,
    [City] varchar(max)  NOT NULL
);
GO

-- Creating table 'requests'
CREATE TABLE [dbo].[requests] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [sender] varchar(50)  NOT NULL,
    [receiver] varchar(50)  NOT NULL,
    [status] bit  NULL
);
GO

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [UserID] int IDENTITY(1,1) NOT NULL,
    [Image] varchar(max)  NULL,
    [Fullname] varchar(100)  NULL,
    [Username] varchar(100)  NOT NULL,
    [Password] varchar(50)  NOT NULL,
    [Age] int  NOT NULL,
    [CNIC] varchar(150)  NOT NULL,
    [Adress] varchar(50)  NOT NULL,
    [Contact_no] varchar(150)  NOT NULL,
    [Email] varchar(50)  NOT NULL,
    [Salary] int  NOT NULL,
    [Gender] varchar(50)  NOT NULL,
    [Religion] varchar(max)  NOT NULL,
    [Cast] varchar(max)  NOT NULL,
    [Profession] varchar(max)  NOT NULL,
    [Account_no] varchar(150)  NOT NULL,
    [City] varchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'brokers'
ALTER TABLE [dbo].[brokers]
ADD CONSTRAINT [PK_brokers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [UserID] in table 'registered_users'
ALTER TABLE [dbo].[registered_users]
ADD CONSTRAINT [PK_registered_users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- Creating primary key on [ID] in table 'requests'
ALTER TABLE [dbo].[requests]
ADD CONSTRAINT [PK_requests]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [UserID] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([UserID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------