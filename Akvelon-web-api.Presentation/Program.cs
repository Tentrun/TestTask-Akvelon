using Akvelon_web_api.DAL;
using Akvelon_web_api.DAL.Interfaces.BaseInterfaces;
using Akvelon_web_api.DAL.Interfaces.Implementations;
using Akvelon_web_api.Domain.Entity;
using Akvelon_web_api.Service.Implementations;
using Akvelon_web_api.Service.Interfaces.BaseInterfaces;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//Register services in DI container
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc();

builder.Services.AddScoped(typeof(IGenericRepository < > ), typeof(GenericRepository < > ));
builder.Services.AddScoped(typeof(IGenericService<ProjectEntity>), typeof(ProjectService));
builder.Services.AddScoped(typeof(IGenericService<TaskEntity>), typeof(TaskService));


// Database connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register Database context
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //Enable middleware to serve Swagger
    app.UseSwagger();
    app.UseSwaggerUI();
}

// If exception is called this function hook this exception
app.UseExceptionHandler(c => c.Run(async context =>
{
    var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
    var exception = exceptionHandlerPathFeature?.Error;
    
    await context.Response.WriteAsJsonAsync(new { error = exception?.Message });
}));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();