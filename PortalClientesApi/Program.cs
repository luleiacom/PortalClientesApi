using PortalClientesApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OData;
using Microsoft.OData.ModelBuilder;
using PortalClientesApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var modelBuilder = new ODataConventionModelBuilder();
modelBuilder.EntitySet<Cliente>("ClientesOData");

builder.Services.AddControllers().AddOData(options =>
    options.Select().Filter().OrderBy().Expand().Count().SetMaxTop(100)
        .AddRouteComponents("odata", modelBuilder.GetEdmModel()));
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapClienteEndpoints();

app.MapPedidoEndpoints();
app.Run();