







CREATE VIEW [dbo].[ItemsDependenciesBOM]
AS
SELECT
	a.*
   ,eb2.FamilyCode AS AssyFamily
   ,eb2.Description AS AssyDescription
   ,eb2.Manufacturer AS AssyManufacturer
   ,eb2.ManufacturerItemNumber AS AssyManufacturerItemNumber
FROM (SELECT
		bm.[ItemNo] AS ItemNumber
	   ,CONVERT(INT, bm.[Qty]) AS QuantityPerAssembly
	   ,eb.Description AS Description
	   ,eb.FamilyCode AS ItemFamily
	   ,eb.Manufacturer AS Manufacturer
	   ,eb.ManufacturerItemNumber AS ManufacturerItemNumber
	   ,bm.[AssyNo] AS AssyNo
	FROM [SPM_Database].[dbo].[ItemDependencies] bm
	INNER JOIN [SPM_Database].[dbo].[Inventory] eb
		ON bm.[ItemNo] = eb.ItemNumber) a
INNER JOIN [SPM_Database].[dbo].[Inventory] eb2
	ON a.AssyNo = eb2.ItemNumber
