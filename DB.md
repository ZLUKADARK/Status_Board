CREATE TABLE [dbo].[A1] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [DATETIME] DATETIME        NULL,
    [NAME]     TEXT            NOT NULL,
    [PRESS]    DECIMAL (12, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[A2] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [DATETIME] DATETIME        NULL,
    [NAME]     TEXT            NOT NULL,
    [PRESS]    DECIMAL (12, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[A3] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [DATETIME] DATETIME        NULL,
    [NAME]     TEXT            NOT NULL,
    [PRESS]    DECIMAL (12, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[A4] (
    [Id]       INT             IDENTITY (1, 1) NOT NULL,
    [DATETIME] DATETIME        NULL,
    [NAME]     TEXT            NOT NULL,
    [PRESS]    DECIMAL (12, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[Departments] (
    [Id]            INT             IDENTITY (1, 1) NOT NULL,
    [DATETIME]      DATETIME        NULL,
    [AZOT]          DECIMAL (12, 2) NULL,
    [Compress]      DECIMAL (12, 2) NULL,
    [CTFS_Perezhim] DECIMAL (12, 2) NULL,
    [CTFS_Pech]     DECIMAL (12, 2) NULL,
    [CTFS_Reznoe]   DECIMAL (12, 2) NULL,
    [DSC_Gal]       DECIMAL (12, 2) NULL,
    [DSC_Ramp]      DECIMAL (12, 2) NULL,
    [DSC_Drob]      DECIMAL (12, 2) NULL,
    [DSC_Shihta]    DECIMAL (12, 2) NULL,
    [H]             DECIMAL (12, 2) NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

