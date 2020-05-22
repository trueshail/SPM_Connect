








CREATE view  [dbo].[ItemsToSelect]
as
select concat(ItemNumber,' ',Description,' ',FamilyCode,' ',Manufacturer,' ',ManufacturerItemNumber) as Items from [dbo].[UnionInventory]

