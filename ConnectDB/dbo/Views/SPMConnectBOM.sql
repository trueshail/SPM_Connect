



CREATE VIEW [dbo].[SPMConnectBOM]
AS
select a.* 
, eb2.Famille AS AssyFamily
, eb2.Des AS AssyDescription
, eb2.Des4 as AssyManufacturer
, eb2.Des2 as AssyManufacturerItemNumber
, eb2.Spec1 as AssySpare
from (
SELECT       bm.Item AS ItemNumber
		,  CONVERT(INT, bm.Qte) AS QuantityPerAssembly
		, eb.Des AS Description
		, eb.Famille AS ItemFamily
		, eb.Des4 AS Manufacturer
		, eb.Des2 AS ManufacturerItemNumber
		, eb.Spec1 AS Spare
		, bm.Produit AS AssyNo
FROM  [SPMDB].[dbo].[Bom] bm 
	INNER JOIN  [SPMDB].[dbo].[Edb] eb 
		ON bm.Item = eb.Item
)a
inner join SPMDB.dbo.Edb eb2 
	ON a.AssyNo = eb2.Item
						 
