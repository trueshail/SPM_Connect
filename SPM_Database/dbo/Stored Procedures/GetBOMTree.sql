


CREATE procedure [dbo].[GetBOMTree] 
 @product varchar(25)
as

WITH  q AS 
        (
        SELECT  *
        FROM    [dbo].SPMConnectBOM
       WHERE   AssyNo = @product

        UNION ALL

        SELECT  m.*
        FROM  [dbo].SPMConnectBOM  m
		JOIN    q
        ON       q.ItemNumber = m.AssyNo 
        )
SELECT  q.AssyNo as parent_id, q.ItemNumber  as children,q.ItemNumber  , q.QuantityPerAssembly,q.Description,q.Manufacturer,q.ManufacturerItemNumber
FROM    q

 UNION  
SELECT top(1) NUll as parent_id , a.ItemNumber  as children, a.ItemNumber  , 0 as QuantityPerAssembly ,a.Description,a.Manufacturer,a.ManufacturerItemNumber
from [dbo].UnionInventory a
where a.ItemNumber = @product

