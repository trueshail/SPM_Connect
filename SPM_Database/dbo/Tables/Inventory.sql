CREATE TABLE [dbo].[Inventory] (
    [ItemNumber]             NVARCHAR (255) NULL,
    [Description]            NVARCHAR (255) NULL,
    [FamilyCode]             NVARCHAR (255) NULL,
    [Manufacturer]           NVARCHAR (255) NULL,
    [ManufacturerItemNumber] NVARCHAR (255) NULL,
    [Material]               NVARCHAR (255) NULL,
    [Spare]                  NVARCHAR (255) NULL,
    [DesignedBy]             NVARCHAR (255) NULL,
    [FamilyType]             NVARCHAR (255) NULL,
    [SurfaceProtection]      NVARCHAR (255) NULL,
    [HeatTreatment]          NVARCHAR (255) NULL,
    [LastSavedBy]            NVARCHAR (255) NULL,
    [Rupture]                NVARCHAR (255) NULL,
    [JobPlanning]            NVARCHAR (255) NULL,
    [Notes]                  NVARCHAR (MAX) NULL,
    [DateCreated]            DATETIME       NULL,
    [LastEdited]             DATETIME       NULL
);


GO
CREATE UNIQUE CLUSTERED INDEX [ClusteredIndex-20191118-125122]
    ON [dbo].[Inventory]([ItemNumber] ASC);

