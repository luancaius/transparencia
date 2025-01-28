using Deputies.Adapter.Out.EFCoreSqlServer;
using Deputies.Adapter.Out.EFCoreSqlServer.Repositories;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<DeputiesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeputiesSqlServerConnection"));
});

builder.Services.AddScoped<IGetDeputiesExpensesQuery, DeputiesExpensesQueryService>();
builder.Services.AddScoped<IDeputyRepository, DeputyRepository>();

// CORS
// The environment variable could be something like ALLOWED_ORIGIN=https://yourdomain.com
string? allowedOrigin = builder.Configuration["ALLOWED_ORIGIN"];

builder.Services.AddCors(options =>
{
    options.AddPolicy("DevPolicy", policy =>
    {
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
    options.AddPolicy("ProdPolicy", policy =>
    {
        if (!string.IsNullOrEmpty(allowedOrigin))
        {
            policy.WithOrigins(allowedOrigin).AllowAnyMethod().AllowAnyHeader();
        }
        else
        {
            // Fallback, or handle missing ALLOWED_ORIGIN
            policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
        }
    });
});

var app = builder.Build();

// Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseCors("DevPolicy");
}
else
{
    app.UseCors("ProdPolicy");
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();