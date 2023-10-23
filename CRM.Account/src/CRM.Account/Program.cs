using System.Text;
using CRM.Account.AccountManagement;
using CRM.Account.Entities;
using CRM.Account.JWT;
using CRM.Common.Authentication;
using CRM.Common.MongoDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AuthenticationHelper.ConfigureAuthentication(builder.Services, builder.Configuration);
AuthenticationHelper.ConfigureAuthorization(builder.Services);

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


#region CORS
builder.Services.AddCors(options =>
{
    // this defines a CORS policy called "default"
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("default");

app.MapControllers();

app.Run();
