using API.Contexts;
using API.Migrations;
using FluentMigrator.Runner;
using FluentMigrator.Runner.Initialization;

var builder = WebApplication.CreateBuilder(args);

// Add configuration
builder.Configuration
    .SetBasePath(System.IO.Directory.GetCurrentDirectory())
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddSingleton<MySqlContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddFluentMigratorCore();
// builder.Services.ConfigureRunner(rb => rb
//     .AddMySql5()
//     .WithGlobalConnectionString(MySqlContext.GetGlobalConnection())
//     .ScanIn(typeof(InitialMigration).Assembly).For.Migrations()
// );
builder.Services.AddLogging(lb => lb.AddFluentMigratorConsole());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MySqlContext>();
    await context.Init();

    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
