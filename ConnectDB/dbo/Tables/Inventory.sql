CREATE TABLE [dbo].[Inventory] (
    [ItemNumber]             VARCHAR (50)   NOT NULL,
    [Description]            VARCHAR (300)  NULL,
    [FamilyCode]             VARCHAR (50)   NULL,
    [Manufacturer]           VARCHAR (255)  NULL,
    [ManufacturerItemNumber] VARCHAR (255)  NULL,
    [Material]               VARCHAR (125)  NULL,
    [Spare]                  VARCHAR (50)   NULL,
    [DesignedBy]             VARCHAR (50)   NULL,
    [FamilyType]             VARCHAR (50)   NULL,
    [SurfaceProtection]      VARCHAR (125)  NULL,
    [HeatTreatment]          VARCHAR (125)  NULL,
    [LastSavedBy]            VARCHAR (50)   NULL,
    [Rupture]                VARCHAR (50)   NULL,
    [JobPlanning]            VARCHAR (50)   NULL,
    [Notes]                  VARCHAR (1000) NULL,
    [DateCreated]            DATETIME       NULL,
    [LastEdited]             DATETIME       NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY NONCLUSTERED ([ItemNumber] ASC)
);


GO
CREATE UNIQUE CLUSTERED INDEX [ItemNumber]
    ON [dbo].[Inventory]([ItemNumber] DESC);


GO
CREATE NONCLUSTERED INDEX [Manufacturer]
    ON [dbo].[Inventory]([Manufacturer] ASC);


GO
CREATE NONCLUSTERED INDEX [Description]
    ON [dbo].[Inventory]([Description] ASC);


GO
CREATE NONCLUSTERED INDEX [Family]
    ON [dbo].[Inventory]([FamilyCode] ASC);


GO
CREATE NONCLUSTERED INDEX [Material]
    ON [dbo].[Inventory]([Material] ASC);


GO
CREATE NONCLUSTERED INDEX [DateCreated]
    ON [dbo].[Inventory]([DateCreated] ASC);


GO
CREATE NONCLUSTERED INDEX [OEMItem]
    ON [dbo].[Inventory]([ManufacturerItemNumber] ASC);

