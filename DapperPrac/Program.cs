using DapperPrac.Repository.Interfaces;
using DapperPrac.Repository;
using DapperPrac.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
});

builder.Services.AddSingleton<IDbService, DbService>();
builder.Services.AddSingleton<IEmployeeService, EmployeeService>();

var app = builder.Build();

//// Add Fake data, do more research
//builder.Services.AddSingleton<IAddData, AddData>();
//var addData = app.Services.GetRequiredService<IAddData>();
//addData.AddFakeEmployee();


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

