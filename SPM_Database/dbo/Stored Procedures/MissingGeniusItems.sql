
CREATE procedure [dbo].[MissingGeniusItems]
as
insert [SPM_Database].[dbo].[Inventory] (ItemNumber,Description,FamilyCode,Manufacturer,ManufacturerItemNumber,DesignedBy,DateCreated,LastSavedBy,LastEdited)
select * from 
(
select e.Item AS ItemNumber,
e.Des as Description,
e.Famille as FamilyCode,
e.Des4 as Manufacturer,
e.Des2 as ManufactureItemNumber,
e.Cree_Par as DesignedBy,
e.Cree_Le as DateCreated,
e.Modif_Par as LastSavedBy,
e.Modif_Le as LastEdited

 from [SPMDB].[dbo].[Edb] e 
 where not exists (select 1 from [SPM_Database].dbo.Inventory i
					where i.ItemNumber = e.Item )) u
where  substring(ItemNumber, 1, 1)  NOT LIKE  '[0-9]%'
and isnumeric(substring(ItemNumber, 2, 5)) = 1
and LEN(ItemNumber) = 6
