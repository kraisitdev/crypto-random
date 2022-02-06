ef-update: 
	dotnet ef dbcontext scaffold "Server=localhost;Port=54301;Database=CryptoRandomDb;User Id=pgadmin;Password=P@ssw0rd;" -d -c AppDb --context-dir DataAccesses/CryptoRandomDbContext/Context  Npgsql.EntityFrameworkCore.PostgreSQL -o DataAccesses/CryptoRandomDbContext -f
