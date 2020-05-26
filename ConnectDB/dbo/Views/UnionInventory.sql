

CREATE VIEW [dbo].[UnionInventory]
AS
     
SELECT
  [ItemNumber],
  [Description],
  [FamilyCode],
  [Manufacturer],
  [ManufacturerItemNumber],
  [DesignedBy],
  [DateCreated],
  [LastSavedBy],
  [LastEdited],
  [Material],
  [SurfaceProtection],
  [HeatTreatment],
  [Notes],
  Concat(itemnumber, ' ', description, ' ', familycode, ' ', manufacturer,
  ' ',
  manufactureritemnumber) AS FullSearch
FROM (SELECT
  --Item Number
  ISNULL(tt2.item, tt1.itemnumber) AS [ItemNumber],
  --Description 
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.Description, tt2.Des)
    ELSE ISNULL(tt2.Des, tt1.Description)
  END AS [Description],
  --FamilyCode
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.FamilyCode, tt2.Famille)
    ELSE ISNULL(tt2.Famille, tt1.FamilyCode)
  END AS [FamilyCode],
  --Manufacturer
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.Manufacturer, tt2.Des4)
    ELSE ISNULL(tt2.Des4, tt1.Manufacturer)
  END AS [Manufacturer],
  --Manufacturer Item Number
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.ManufacturerItemNumber, tt2.Des2)
    ELSE ISNULL(tt2.Des2, tt1.ManufacturerItemNumber)
  END AS [ManufacturerItemNumber],
  --Designed By
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.DesignedBy, tt2.UsagerCreation)
    ELSE ISNULL(tt2.UsagerCreation, tt1.DesignedBy)
  END AS [DesignedBy],
  --Date Created
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.DateCreated, tt2.DateCreation)
    ELSE ISNULL(tt2.DateCreation, tt1.DateCreated)
  END AS [DateCreated],
  --Last Saved By
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.LastSavedBy, tt2.Modif_Par)
    ELSE ISNULL(tt2.Modif_Par, tt1.LastSavedBy)
  END AS [LastSavedBy],
  --Last Edited
  CASE
    WHEN CONVERT(datetime, tt2.modif_le) < CONVERT(datetime, tt1.lastedited) THEN ISNULL(tt1.lastedited, tt2.modif_le)
    ELSE ISNULL(tt2.modif_le, tt1.lastedited)
  END AS [LastEdited],
  --Material
  tt1.Material AS [Material],
  --Surface Protection
  tt1.SurfaceProtection AS [SurfaceProtection],
  --Heat Treatment
  tt1.HeatTreatment AS [HeatTreatment],
  --Notes
  tt1.Notes AS [Notes],
  ISNULL(tt2.actif, 'O') AS Active

FROM [SPMDB].[dbo].[edb] tt2
FULL
OUTER JOIN [SPM_Database].[dbo].[inventory] tt1
  ON tt1.itemnumber = tt2.item) a
WHERE active = 'O'
AND SUBSTRING(ItemNumber, 1, 1) NOT LIKE '[0-9]%'
AND ISNUMERIC(
SUBSTRING(ItemNumber, 2, 5)
) = 1
AND LEN(ItemNumber) = 6
AND SUBSTRING(ItemNumber, 2, 1) NOT LIKE '-';
