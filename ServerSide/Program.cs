using ServerSide.Database;
using Microsoft.EntityFrameworkCore;
using ServerSide.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicatonDbContext>(options => options.UseSqlite($"Data Source=C:\\Users\\User\\Desktop\\MVC.db"));
builder.Services.AddScoped<CandyRepository>();
builder.Services.AddScoped<CatigoryRepository>();
builder.Services.AddScoped<OrderRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
