using LoansManagementSystem.BackgroundServices;
using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.MessageQueue;
using LoansManagementSystem.Services;
using LoansManagementSystem.Utilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

IConfigurationRoot Configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.Configure<Configurations>(Configuration.GetSection("ApplicationConfiguration"));

builder.Services.AddDbContext<Db>(options => options.UseNpgsql(connection));

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMessageProducer, MessageProducer>();
builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ILoansSystem, LoansSystem>();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddHostedService<ConsumerService>();

builder.Services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

//todo:https for docker
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
