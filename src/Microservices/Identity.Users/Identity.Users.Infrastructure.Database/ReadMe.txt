#add migration
cd .\Identity.Users.Infrastructure.Database
dotnet ef migrations add MigrationName -s ..\Identity.Users.Web --context UsersDbContext

#remove migration
cd .\Identity.Users.Infrastructure.Database
dotnet ef migrations remove -s ..\Identity.Users.Web --context UsersDbContext

#update database
cd .\Identity.Users.Infrastructure.Database
dotnet ef database update -s ..\Identity.Users.Web --context UsersDbContext

#update database (or rollback) to specific migration
cd .\Identity.Users.Infrastructure.Database
dotnet ef database update MigrationName -s ..\Identity.Users.Web --context UsersDbContext