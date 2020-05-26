create view  Designers
as

select distinct DesignedBy  from Inventory
where (isnull(DesignedBy, '') <> '' and DesignedBy <> '-')