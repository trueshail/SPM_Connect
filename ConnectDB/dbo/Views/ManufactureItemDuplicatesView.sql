


CREATE view  [dbo].[ManufactureItemDuplicatesView]
as	
	
	select *  ,
	row_number() over (partition by ManufacturerItemNumber order by ItemNumber) as RNumber
	from [SPM_Database].[dbo].[UnionInventory] i 
	where ManufacturerItemNumber in (
	select ManufacturerItemNumber from (
		select ManufacturerItemNumber , 
		row_number() over (partition by ManufacturerItemNumber order by ManufacturerItemNumber) as RowNumber
		from [SPM_Database].[dbo].[UnionInventory]
		where (isnull(ManufacturerItemNumber, '') <> '' and ManufacturerItemNumber <> '-')
		) a
	where RowNumber > 1) 
