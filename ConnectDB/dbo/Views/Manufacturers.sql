create view  Manufacturers
as

select distinct Manufacturer from Inventory
where (isnull(Manufacturer, '') <> '' and Manufacturer <> '-')