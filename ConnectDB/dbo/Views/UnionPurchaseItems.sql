
CREATE VIEW [dbo].[UnionPurchaseItems]
as
select * from 
(select [ItemNumber],[Description],[FamilyCode],[Manufacturer],[ManufacturerItemNumber] from [SPM_Database].dbo.Inventory 
union 
select e.Item AS ItemNumber,
e.Des as Description,
e.Famille as FamilyCode,
e.Des4 as Manufacturer,
e.Des2 as ManufactureItemNumber
 from [SPMDB].[dbo].[Edb] e 
 where not exists (select 1 from [SPM_Database].dbo.Inventory i
					where i.ItemNumber = e.Item )) u
where  substring(ItemNumber, 1, 1)  NOT LIKE  '[0-9]%'
and isnumeric(substring(ItemNumber, 2, 5)) = 1
and LEN(ItemNumber) < 7
and FamilyCode in('ECC', 'ASEL','PCC','ASPN', 'PU','MPC')
