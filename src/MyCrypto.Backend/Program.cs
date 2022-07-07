global using MyCrypto.Backend.Repositories;
global using MyCrypto.Backend.Services;
global using MyCrypto.Backend.Models;
global using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "ApenasParaTestes",
                      builder => builder.WithOrigins("http://localhos:51234")
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
});

builder.Services.Configure<KestrelServerOptions>(o => o.AllowSynchronousIO = true);

builder.Services.AddSingleton<IRepository, FakeRepository>();
builder.Services.AddSingleton<IStreamData, FakeStreamData>();
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

app.UseAuthorization();
app.MapControllers();
app.Run();