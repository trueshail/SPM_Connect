create procedure [dbo].[findInventory]
@itemnumber varchar(30)

as

select * from Inventory
where ItemNumber = @itemnumber
