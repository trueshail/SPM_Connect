create View Carriers
as
select Description1 from [SPMDB].dbo.dimCarriers
where Description1 != ''