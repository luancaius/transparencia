
using Deputies.Application.Ports.In;
using Deputies.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGetDeputiesExpensesQuery, DeputiesExpensesQueryService>();

var app = builder.Build();

// Configure the HTTP request pipeline (Swagger, HTTPS, etc.)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.Run();

