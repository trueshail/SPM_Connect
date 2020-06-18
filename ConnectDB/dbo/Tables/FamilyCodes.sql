CREATE TABLE [dbo].[FamilyCodes] (
    [FamilyCodes] NVARCHAR (50) NOT NULL,
    [Category]    NVARCHAR (50) NOT NULL,
    [Rupture]     VARCHAR (50)  CONSTRAINT [DF_FamilyCodes_Rupture] DEFAULT ('ALWAYS') NOT NULL,
    CONSTRAINT [PK_FamilyCodes] PRIMARY KEY CLUSTERED ([FamilyCodes] ASC)
);



