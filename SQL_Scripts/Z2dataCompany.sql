SELECT * FROM [Z2dataCompany].[dbo].[Departments]
SELECT * FROM [Z2dataCompany].[dbo].[Employees]

SELECT IDENT_CURRENT('[Z2dataCompany].[dbo].[Employees]')

DBCC CHECKIDENT ('[Z2dataCompany].[dbo].[Employees]', RESEED, 1003);


SET IDENTITY_INSERT [Z2dataCompany].[dbo].[Employees] OFF
