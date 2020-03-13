CREATE FULLTEXT INDEX ON [dbo].[InventoryBackView]
    ([ItemNumber] LANGUAGE 1033, [Description] LANGUAGE 1033, [FamilyCode] LANGUAGE 1033, [Manufacturer] LANGUAGE 1033, [ManufacturerItemNumber] LANGUAGE 1033, [fullsearch] LANGUAGE 1033)
    KEY INDEX [vcSearchItem_EntityId]
    ON [ConnectItemSearch];

