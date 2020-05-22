CREATE TABLE [dbo].[inventorybackup] (
    [ItemNumber]             NVARCHAR (255) NOT NULL,
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
    [LastEdited]             DATETIME       NULL,
    [ItemId]                 INT            IDENTITY (1, 1) NOT NULL,
    CONSTRAINT [PK_inventorybackup] PRIMARY KEY CLUSTERED ([ItemNumber] ASC),
    CONSTRAINT [ItemID] UNIQUE NONCLUSTERED ([ItemId] ASC) WITH (FILLFACTOR = 90)
);

