create view Descriptions
as
SELECT DISTINCT Description from Inventory
where (isnull(Description, '') <> '' and Description <> '-')
