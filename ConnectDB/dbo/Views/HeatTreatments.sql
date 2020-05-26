create view  HeatTreatments
as

select distinct HeatTreatment  from Inventory
where (isnull(HeatTreatment, '') <> '' and HeatTreatment <> '-')