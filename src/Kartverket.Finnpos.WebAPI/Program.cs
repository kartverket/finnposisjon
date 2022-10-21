using Kartverket.Finnpos.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.SwaggerDoc("1.0.0", new OpenApiInfo { Title = "Finnpos", Version = "1.0.0" });
});
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin());
app.UseSwagger(o => o.RouteTemplate = "{documentName}/openapi.json");
app.UseSwaggerUI(o =>
{
    o.RoutePrefix = string.Empty;
    o.SwaggerEndpoint("1.0.0/openapi.json", "Finnpos 1.0.0");
});

app.MapGet("/positions", Finnpos.GetPositions);

app.Run();
