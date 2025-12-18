using EvergreenWebAPI;
using EvergreenWebAPI.Models;
using EvergreenWebAPI.Repositories;
using EvergreenWebAPI.Repositories.Abstractions;
using EvergreenWebAPI.Seeding;
using EvergreenWebAPI.Services;
using EvergreenWebAPI.Services.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));

// Add repositories.
builder.Services.AddScoped<IFlowersHexColorRepository, FlowersHexColorRepository>();
builder.Services.AddScoped<IFlowerRepository, FlowerRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Add services.
builder.Services.AddScoped<IFlowerService, FlowerService>();
builder.Services.AddScoped<IDropboxService, DropboxService>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddHttpClient();

// JWT.
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:Audience"],
        ValidIssuer = builder.Configuration["JWT:Issuer"],
        RequireExpirationTime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddHttpClient<IFlowerMlClient, FlowerMlClient>(http =>
{
    http.BaseAddress = new Uri("http://localhost:8000");
    http.Timeout = TimeSpan.FromSeconds(30);
});

var app = builder.Build();

// Seed database.
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    dbContext.Database.Migrate();
    DbInitializer.Seed(dbContext).Wait();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();

app.MapControllers();

app.Run();