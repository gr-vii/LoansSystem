using LoansManagementSystem.DataServices.Contexts;
using LoansManagementSystem.DataServices.Repositories.implementations;
using LoansManagementSystem.DataServices.Repositories.Interfaces;
using LoansManagementSystem.MessageQueue;
using LoansManagementSystem.Security;
using LoansManagementSystem.Services;
using LoansManagementSystem.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

IConfigurationRoot Configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

builder.Services.Configure<Configurations>(Configuration.GetSection("ApplicationConfiguration"));

builder.Services.AddDbContext<Db>(options => options.UseNpgsql(connection));

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<ExceptionHandler>();
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme.",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddScoped<IMessageProducer, MessageProducer>();

builder.Services.AddSingleton<IMessageConsumer, MessageConsumer>();

builder.Services.AddSingleton<IJwt, Jwt>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<ILoansSystem, LoansSystem>();

builder.Services.AddMediatR(opt => opt.RegisterServicesFromAssemblies(typeof(Program).Assembly));

//builder.Services.AddHostedService<ConsumerService>();

builder.Services.Configure<SmtpSettings>(Configuration.GetSection("SmtpSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddMemoryCache();

var tsk = Configuration["Jwt:Secret"];
var expiry = int.Parse(Configuration["Jwt:Expiry"]);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = "LMS",
            ValidAudience = "LoansManagementSystem",
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(tsk))
        };
    });

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
app.UseStaticFiles();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger.json", "Loans Management System");
    c.OAuthUseBasicAuthenticationWithAccessCodeGrant();
    c.OAuthClientId("test-id");
    c.OAuthClientSecret("test-secret");
    c.OAuthRealm("test-realm");
    c.OAuthAppName("Swagger UI");
    c.OAuthScopeSeparator(" ");
    c.OAuthAdditionalQueryStringParams(new Dictionary<string, string> { { "audience", "LoansManagementSystem" } });
});

app.UseHttpsRedirection();

app.MapControllers();

using (var scoped = app.Services.CreateScope())
{
    var cache = app.Services.GetService<IMemoryCache>();

    Configurations.Reinit(cache, tsk, expiry);
}

app.Run();
