create view  SurfaceProtections
as

select distinct SurfaceProtection  from Inventory
where (isnull(SurfaceProtection, '') <> '' and SurfaceProtection <> '-')