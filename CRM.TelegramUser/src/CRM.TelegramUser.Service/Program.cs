using CRM.Common.Authentication;
using CRM.Common.HttpHandler;
using CRM.Common.MongoDb;
using CRM.TelegramUser.Service.Entities;
using CRM.TelegramUser.Service.TelegramUserManagement;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


AuthenticationHelper.ConfigureAuthentication(builder.Services, builder.Configuration);

builder.Services.AddAuthorization();

builder.Services.AddScoped<ITelegramUserManagementService, TelegramUserManagementService>();

builder.Services.AddTransient<HttpTrackerHandler>();

builder.Services
    .AddHttpClient("auth")
    .AddHttpMessageHandler<HttpTrackerHandler>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("default", policy =>
    {
        policy.WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddMongo()
                .AddMongoRepository<TelegramUserEntity>("telegramUsers");

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("default");
app.MapControllers();

app.Run();
