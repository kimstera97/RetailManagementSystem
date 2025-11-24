using Microsoft.EntityFrameworkCore;
using StoreApp.Data;
using StoreApp.Repositories;
using StoreApp.Services;
using StoreApp.Settings;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<StoreDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddScoped<IStoreProductRepository, StoreProductRepository>();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IMessageService, MessageService>(client =>
{
    client.BaseAddress = new Uri("https://central-app-url.com");
});

builder.Services.Configure<StoreSettings>(builder.Configuration.GetSection("StoreSettings"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
