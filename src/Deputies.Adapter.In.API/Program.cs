
using Deputies.Adapter.Out.EFCoreSqlServer;
using Deputies.Adapter.Out.EFCoreSqlServer.Repositories;
using Deputies.Application.Ports.In;
using Deputies.Application.Ports.Out;
using Deputies.Application.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DeputiesDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DeputiesSqlServerConnection"));
});

builder.Services.AddScoped<IGetDeputiesExpensesQuery, DeputiesExpensesQueryService>();
builder.Services.AddScoped<IDeputyRepository, DeputyRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

