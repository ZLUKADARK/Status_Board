DROP TABLE A1, A2, A3, A4

DROP TABLE Departments

SELECT * FROM A1
SELECT * FROM  A2
SELECT * FROM  A3
SELECT * FROM  A4
SELECT * FROM  Departments


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


