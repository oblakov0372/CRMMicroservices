using CRM.Common.Authentication;
using CRM.Common.MongoDb;
using CRM.TelegramMessage.Service.Entities;
using CRM.TelegramMessage.Service.TelegramMessageManagement;
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

AuthenticationHelper.ConfigureAuthentication(builder.Services, builder.Configuration);

builder.Services.AddAuthorization();


builder.Services.AddScoped<ITelegramMessageManagementService, TelegramMessageManagementService>();
builder.Services.AddMongo()
                .AddMongoRepository<TelegramMessageEntity>("telegramMessages");

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

app.UseAuthorization();
app.UseCors("default");
app.MapControllers();

app.Run();
