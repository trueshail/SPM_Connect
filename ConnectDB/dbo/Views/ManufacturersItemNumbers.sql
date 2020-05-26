create view  ManufacturersItemNumbers
as

select distinct ManufacturerItemNumber from Inventory
where (isnull(ManufacturerItemNumber, '') <> '' and ManufacturerItemNumber <> '-')