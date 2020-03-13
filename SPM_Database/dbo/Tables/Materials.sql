CREATE TABLE [dbo].[Materials] (
    [MaterialNames] NVARCHAR (255) NULL,
    [id]            INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_Materials] PRIMARY KEY CLUSTERED ([id] ASC)
);

