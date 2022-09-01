using Kartverket.Finnpos.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = builder.Build();

app.UseCors(policy => policy.AllowAnyOrigin());
app.UseSwagger(o => o.RouteTemplate = "swagger/{documentName}/openapi.json");
app.UseSwaggerUI(o =>
{
    o.SwaggerEndpoint("v1/openapi.json", $"{builder.Environment.ApplicationName} v1");
});

app.MapGet("/positions", Finnpos.GetPositions);

app.Run();
