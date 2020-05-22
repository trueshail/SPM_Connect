CREATE TABLE [dbo].[ItemDependencies] (
    [Id]          INT          IDENTITY (1, 1) NOT NULL,
    [AssyNo]      VARCHAR (50) NOT NULL,
    [ItemNo]      VARCHAR (50) NOT NULL,
    [Qty]         INT          CONSTRAINT [DF_ItemDependencies_Qty] DEFAULT ((1)) NOT NULL,
    [DateCreated] DATETIME     NULL,
    [CreatedBy]   VARCHAR (50) NULL,
    [LastSaved]   DATETIME     NULL,
    [LastUser]    VARCHAR (50) NULL,
    CONSTRAINT [PK_ItemDependencies] PRIMARY KEY CLUSTERED ([Id] ASC)
);

