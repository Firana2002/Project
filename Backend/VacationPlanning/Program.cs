using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using Microsoft.IdentityModel.Tokens;
using System.Text;
var builder = WebApplication.CreateBuilder(args);

// Добавление строки подключения к базе данных
builder.Services.AddDbContext<VacationPlanningContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Настройка Identity
builder.Services.AddIdentity<ApplicationUser , IdentityRole>()
    .AddEntityFrameworkStores<VacationPlanningContext>()
    .AddDefaultTokenProviders();

// Настройка JWT
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

// Добавление контроллеров
builder.Services.AddControllers();

// Добавление Swagger для документации API (опционально)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Настройка middleware
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    // Включение Swagger в режиме разработки
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/Home/Error"); // Обработка ошибок
    app.UseHsts(); // Безопасный HTTP
}

// Настройка HTTPS редиректа
app.UseHttpsRedirection();
app.UseStaticFiles();

// Настройка маршрутизации
app.UseRouting();

// Настройка аутентификации и авторизации
app.UseAuthentication();
app.UseAuthorization();

// Определение маршрутов
app.MapControllers();

// Запуск приложения
app.Run();
