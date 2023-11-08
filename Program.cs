using API.Contexts;

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
}

app.UseAuthorization();

app.MapControllers();

app.Run();
