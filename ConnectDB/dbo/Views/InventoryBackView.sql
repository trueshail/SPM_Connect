

CREATE VIEW InventoryBackView
WITH SCHEMABINDING AS
SELECT       [ItemNumber]
			,[Description]
			,[FamilyCode]
			,[Manufacturer]
			,[ManufacturerItemNumber]
			, ItemId
			, IsNull(ItemNumber,'') + ' ' + 
			IsNull(Description,'') + ' ' + 
			IsNull(FamilyCode,'') + ' ' + 
			IsNull(Manufacturer,'') + ' ' + 
			IsNull(ManufacturerItemNumber,'') + ' ' as fullsearch
FROM          [dbo].[inventorybackup]  

GO
CREATE UNIQUE CLUSTERED INDEX [vcSearchItem_EntityId]
    ON [dbo].[InventoryBackView]([ItemId] ASC);

