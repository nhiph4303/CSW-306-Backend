using LibraryManagementSystem.Helpers;
using LibraryManagementSystem.Requirements;
using LibraryManagementSystem.Requirements.Handlers;
using LibraryManagementSytem.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Load JWT settings from configuration
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var key = jwtSettings["SecretKey"];
var issuer = jwtSettings["Issuer"];
var audience = jwtSettings["Audience"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = issuer,
            ValidAudience = audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
            NameClaimType = JwtRegisteredClaimNames.Sub,
            RoleClaimType = "role"
        };
        options.MapInboundClaims = false;
    });

// inject services
builder.Services
    .AddSingleton<JwtHelpers>()
    .AddSingleton<IAuthorizationHandler, ActiveUserHandler>()
    .AddSingleton<IAuthorizationHandler, MinimumMembershipHandler>();

//  Policy
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("ActiveUserOnly", policy => policy.Requirements.Add(new ActiveUserRequirement()))
    .AddPolicy("AdminOrLibrarian", policy => policy.RequireRole("ADMIN", "LIBRARIAN"))
    .AddPolicy("MinimumMembership", policy => policy.Requirements.Add(new MinimumMembershipRequirement(30)))
    .AddPolicy("CanManageCategories", policy => { 
        policy.RequireClaim("can_manage_categories", "True");
    })
    .AddPolicy("VerifiedEmailOnly", policy => {
        policy.RequireClaim("email_confirmed", "True");
    });



builder.Services.AddAuthorization();





// Đăng ký DbContext
builder.Services.AddDbContext<LibraryDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


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
app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
