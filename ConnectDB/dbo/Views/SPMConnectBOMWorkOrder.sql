


CREATE view [dbo].[SPMConnectBOMWorkOrder]
as

select a.* 
, eb2.Famille AS AssyFamily
, eb2.Des AS AssyDescription
, eb2.Des4 as AssyManufacturer
, eb2.Des2 as AssyManufacturerItemNumber
, eb2.Spec1 as AssySpare
from (
SELECT   bm.Item AS ItemNumber
		,CONVERT(INT, bm.Qte_Ass) AS QuantityPerAssembly
		, eb.Des AS Description
		, eb.Famille
		, eb.Des4 AS Manufacturer
		, eb.Des2 as ManufacturerItemNumber
		, eb.Spec1 as Spare
		, bm.Type 
		, bm.Ordre as [Order]
		, bm.Wo as WorkOrderNo
		, bm.Woprec as AssyWo
		, bm.Piece as AssyNo		
FROM  [SPMDB].[dbo].[Mrpres] bm 
	INNER JOIN  [SPMDB].[dbo].[Edb] eb 
		ON bm.Item = eb.Item
)a
inner join SPMDB.dbo.Edb eb2 
	ON a.AssyNo = eb2.Item

