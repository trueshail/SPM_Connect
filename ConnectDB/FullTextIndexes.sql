CREATE FULLTEXT INDEX ON [dbo].[Inventory]
    ([ItemNumber] LANGUAGE 1033, [Description] LANGUAGE 1033, [Manufacturer] LANGUAGE 1033, [ManufacturerItemNumber] LANGUAGE 1033)
    KEY INDEX [ItemNumber]
    ON [ConnectItemSearch];

