using ICICIBANK_HOMELOANS_BusinessEntities.Interfaces;
using ICICIBANK_HOMELOANS_DbConnectivity.Data;
using ICICIBANK_HOMELOANS_Repositories;
using ICICIBANK_HOMELOANS_Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
//=========================================================================
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
//===========================================================================
builder.Services.AddScoped<IOrderRepository, OrdersRepository>();
builder.Services.AddScoped<IOrdersService, OrdersService>();
//===========================================================================

#region AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
