CREATE PROCEDURE UnionItems
as

With cte_duplicate ([ItemNumber], Description, FamilyCode, Manufacturer, ManufacturerItemNumber, rownumber)
as (
select [ItemNumber]
, Description
, FamilyCode
, Manufacturer
, ManufacturerItemNumber
, row_number()over(partition by [ItemNumber] order by ItemNumber DESC)as rownumber
 from [SPM_Database].[dbo].[UnionInventory]

)
Select * from cte_duplicate where rownumber = 1 and ItemNumber BETWEEN 'A00000' AND 'A99999'
order by ItemNumber DESC
