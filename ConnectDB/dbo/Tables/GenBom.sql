CREATE TABLE [dbo].[GenBom] (
    [Id]                       NVARCHAR (MAX) NULL,
    [Product]                  NVARCHAR (MAX) NULL,
    [Order]                    NVARCHAR (MAX) NULL,
    [ItemCode]                 NVARCHAR (MAX) NULL,
    [QuantityInConversionUnit] NVARCHAR (MAX) NULL,
    [ConversionUnitCode]       NVARCHAR (MAX) NULL,
    [OptionType]               NVARCHAR (MAX) NULL,
    [QuantityPerAssembly]      NVARCHAR (MAX) NULL,
    [UnitCode]                 NVARCHAR (MAX) NULL,
    [RejectPercentage]         NVARCHAR (MAX) NULL,
    [IsPhantom]                NVARCHAR (MAX) NULL,
    [IsGroupingWo]             NVARCHAR (MAX) NULL,
    [Reserved]                 NVARCHAR (MAX) NULL,
    [Balloon]                  NVARCHAR (MAX) NULL,
    [BomRoutingLink]           NVARCHAR (MAX) NULL,
    [BomRoutingLinkOrder]      NVARCHAR (MAX) NULL,
    [Description1]             NVARCHAR (MAX) NULL,
    [Note]                     NVARCHAR (MAX) NULL,
    [LastUser]                 NVARCHAR (MAX) NULL,
    [LastUpdate]               NVARCHAR (MAX) NULL,
    [CreationDate]             NVARCHAR (MAX) NULL
);

