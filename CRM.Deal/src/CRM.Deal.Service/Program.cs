using System.Text;
using CRM.Common.MongoDb;
using CRM.Common.Authentication;
using CRM.Deal;
using CRM.Deal.Service;
using CRM.Deal.Service.Client;
using CRM.Deal.Service.DealManagement;
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

builder.Services.AddHttpClient<AccountClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7205");
});

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

builder.Services.AddMongo()
                .AddMongoRepository<DealEntity>("deals");

builder.Services.AddScoped<IDealManagementService, DealManagementService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("default");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
