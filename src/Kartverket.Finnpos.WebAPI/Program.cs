using Kartverket.Finnpos.Core;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o =>
{
    o.AddServer(new OpenApiServer { Url = "/finnpos/v1" });
    o.SwaggerDoc("v1", new OpenApiInfo { Title = "Finnpos", Version = "1.0.0" });
});
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin());
app.UseSwagger(o => o.RouteTemplate = "/finnpos/{documentName}/openapi.json");
app.UseSwaggerUI(o =>
{
    o.RoutePrefix = string.Empty;
    o.SwaggerEndpoint("/finnpos/v1/openapi.json", "Finnpos 1.0.0");
});

app.MapGet("/positions", Finnpos.GetPositions);

app.Run();
