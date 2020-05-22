














CREATE view [dbo].[EFTHome]
as

SELECT
  tcapp.[Id],
  tcapp.[PaymentNo],
  CONVERT(varchar, tcapp.[PaymentDate], 23) AS PaymentDate,
  tcapp.[VendorName],
  tcapp.[VendorId],
  pm.Code as PaymentType,
  tcapp.[TotalAmount],
  CONVERT(varchar, tcapp.[LastUpdate], 20) AS LastUpdate,
  CONVERT(varchar, tcapp.[CreateDate], 20) AS DateCreated,
  CASE
    WHEN ISNULL(et.EmailSent, 0) = 1 THEN 'Yes'
    WHEN ISNULL(et.EmailSent, 0) = 0 THEN 'No'
    ELSE 'No'
  END AS EmailSent,
  ce.FirstName,
  ce.LastName,
  ce.Email
FROM [SPMDB].[dbo].[tcAPPayment] tcapp
FULL
OUTER JOIN [SPM_Database].[dbo].[EFTEmailTracker] et
  ON et.ID = tcapp.Id
INNER JOIN [SPM_Database].[dbo].[EFTVendorsToEmail] ce
  ON ce.[VendorID] = tcapp.VendorId
INNER JOIN [SPMDB].[dbo].[tcAPPaymentMode] pm
  ON tcapp.PaymentModeId = pm.ID

WHERE ISNULL(tcapp.VendorId, '') <> ''
AND tcapp.PaymentTypeID = '1380'
AND (tcapp.PaymentModeId = '7' OR tcapp.PaymentModeId = '11')
AND tcapp.APPaymentStatusId = '1385'
AND tcapp.PaymentDate > '2020-04-01'
