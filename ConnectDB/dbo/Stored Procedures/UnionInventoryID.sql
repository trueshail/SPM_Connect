CREATE procedure UnionInventoryID
AS
  SELECT  u.ItemNumber, u.Description, u.FamilyCode, u.Manufacturer, u.ManufacturerItemNumber, u.DesignedBy,u.DateCreated,u.LastSavedBy, u.LastEdited,   ROW_NUMBER() OVER(ORDER BY u.ItemNumber, u.ItemNumber) AS 'ID',concat(u.ItemNumber,' ',u.Description,' ',u.FamilyCode,' ',u.Manufacturer,' ',u.ManufacturerItemNumber) as FullSearch from 
(select a.[ItemNumber],a.[Description],a.[FamilyCode],a.[Manufacturer],a.[ManufacturerItemNumber],a.DesignedBy,a.DateCreated,a.LastSavedBy,a.LastEdited from [SPM_Database].dbo.Inventory a
union 
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
where  substring(u.ItemNumber, 1, 1)  NOT LIKE  '[0-9]%'
and isnumeric(substring(u.ItemNumber, 2, 5)) = 1
and LEN(u.ItemNumber) < 7
   
