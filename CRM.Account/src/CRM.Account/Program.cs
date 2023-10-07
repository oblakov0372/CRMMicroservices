using System.Text;
using CRM.Account;
using CRM.Account.AccountManagement;
using CRM.Account.Entities;
using CRM.Account.JWT;
using CRM.Common.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:SecretKey"]))
        };
    });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("AdminPolicy", policy =>
        {
            policy.Requirements.Add(new AdminRequirement());
        });
    });

builder.Services.AddSingleton<IAuthorizationHandler, AdminAuthorizationHandler>();

var jwtSettings = new JwtSettings
{
    SecretKey = builder.Configuration.GetValue<string>("JwtSettings:SecretKey"),
    Issuer = builder.Configuration.GetValue<string>("JwtSettings:Issuer"),
    Audience = builder.Configuration.GetValue<string>("JwtSettings:Audience"),
    ExpiresInMinutes = builder.Configuration.GetValue<int>("JwtSettings:ExpiresInMinutes"),
};

builder.Services.AddSingleton(jwtSettings);

builder.Services.AddMongo()
                .AddMongoRepository<AccountEntity>("accounts");
builder.Services.AddScoped<IAccountManagementService, AccountManagementService>();
builder.Services.AddSingleton<IJwtAuthenticationManager, JwtAuthenticationManager>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
