using Infrastructure.Repositories;
using ProductService.Application.Abstractions;
using ProductService.Application.Abstractions.Products.Commands;
using ProductService.Application.Abstractions.Stores.Commands;
using ProductService.Presentation.Repositories.StoreRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// TODO: Refactor this by extracting to a separate class
// Dependency Injections
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInventorySaleRepository, InventorySaleRepository>();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateStore).Assembly));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateProduct).Assembly));
var app = builder.Build();


// Always enable swagger
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();