Create View FilterMaterialsMerged
as

select distinct Material from [dbo].[Inventory]
where (isnull(Material, '') <> '' and Material <> '-')
union
  select [MaterialNames] from [dbo].[Materials]


