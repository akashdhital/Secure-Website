CREATE TABLE [dbo].[Users] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [full_name]     VARCHAR (50) NOT NULL,
    [email_address] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Email] UNIQUE NONCLUSTERED ([email_address] ASC)
);

CREATE TABLE [dbo].[Users] (
    [Id]            INT          IDENTITY (1, 1) NOT NULL,
    [full_name]     VARCHAR (50) NOT NULL,
    [email_address] VARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [AK_Email] UNIQUE NONCLUSTERED ([email_address] ASC)
);

CREATE TABLE [dbo].[Password] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [user_id]       INT            NOT NULL,
    [password]      NVARCHAR (MAX) NOT NULL,
    [password_date] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

CREATE TABLE [dbo].[TempPassword] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [user_id]       INT            NOT NULL,
    [password]      NVARCHAR (MAX) NOT NULL,
    [password_date] DATETIME       NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



