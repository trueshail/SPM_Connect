CREATE TABLE [dbo].[ConnectParamaters] (
    [Id]             INT            IDENTITY (1, 1) NOT NULL,
    [Parameter]      NVARCHAR (50)  NULL,
    [Description]    NVARCHAR (MAX) NULL,
    [ParameterValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_ConnectParamaters] PRIMARY KEY CLUSTERED ([Id] ASC)
);

