global using MyCrypto.Backend.Repositories;
global using MyCrypto.Backend.Services;
global using MyCrypto.Backend.Models;
global using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository, FakeRepository>();
builder.Services.AddSingleton<IStreamData, FakeStreamData>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();