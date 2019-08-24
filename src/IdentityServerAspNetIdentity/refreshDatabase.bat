ECHO start refreshing database
:: reset migration in db of ApplicationDbContext
Update-Database 0 -Context ApplicationDbContext
:: reset migration in db of PersistedGrantDbContext
Update-Database 0 -Context PersistedGrantDbContext
:: reset migration in db of ConfigurationDbContext
Update-Database 0 -Context ConfigurationDbContext
:: re-seed above migration to db 
dotnet run /seed
ECHO done!!
