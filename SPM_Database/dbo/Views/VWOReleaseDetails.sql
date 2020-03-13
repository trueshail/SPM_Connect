






CREATE VIEW [dbo].[VWOReleaseDetails]
AS
select a.* 
, eb2.Famille AS AssyFamily
, eb2.Des AS AssyDescription
, eb2.Des4 as AssyManufacturer
, eb2.Des2 as AssyManufacturerItemNumber
from (
SELECT    bm.ItemId AS ItemNumber
		, bm.WO
		, bm.RlogNo
		,  CONVERT(INT, bm.ItemQty) AS QuantityPerAssembly
		, bm.ItemNotes
		, bm.IsRevised
		, eb.Description AS Description
		, eb.FamilyCode AS ItemFamily
		, eb.Manufacturer AS Manufacturer
		, eb.ManufacturerItemNumber AS ManufacturerItemNumber
		, bm.[AssyNo] AS AssyNo
FROM  [SPM_Database].[dbo].[WOReleaseDetails] bm 
	INNER JOIN  [SPM_Database].[dbo].[Inventory] eb 
		ON bm.ItemId = eb.ItemNumber

)a
inner join SPMDB.dbo.Edb eb2 
	ON a.AssyNo = eb2.Item
