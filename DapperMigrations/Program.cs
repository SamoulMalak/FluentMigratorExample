using FluentMigrator.Runner;
using System.Reflection;

namespace DapperMigrations
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddFluentMigratorCore()
                .ConfigureRunner(
                w => w.AddSqlServer()
                .WithGlobalConnectionString("Data Source=.;Initial Catalog=LibraryDataBase;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .ScanIn(Assembly.GetExecutingAssembly()).For.All()
                
                ).AddLogging(config => config.AddFluentMigratorConsole());
            
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            using var scope = app.Services.CreateScope();
            var migrator = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
            migrator.ListMigrations();
            migrator.MigrateUp();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}