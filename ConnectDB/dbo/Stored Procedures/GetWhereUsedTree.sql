



CREATE procedure [dbo].[GetWhereUsedTree] 
 @product varchar(25)
as

WITH  q AS 
        (
        SELECT  *
        FROM    [dbo].SPMConnectBOM
       WHERE   ItemNumber = @product

        UNION ALL

        SELECT  m.*
        FROM  [dbo].SPMConnectBOM  m
		JOIN    q
        ON       q.AssyNo = m.ItemNumber 
        )
SELECT  q.ItemNumber as parent_id, q.AssyNo  as children,q.AssyNo  , q.QuantityPerAssembly,q.AssyDescription,q.AssyManufacturer,q.AssyManufacturerItemNumber
FROM    q

 UNION  
SELECT top(1) NUll as parent_id , a.ItemNumber  as children, a.ItemNumber  , 0 as QuantityPerAssembly ,a.Description,a.Manufacturer,a.ManufacturerItemNumber
from [dbo].Inventory a
where a.ItemNumber = @product

