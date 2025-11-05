using Learn.Identity.Data;
using Learn.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Vamos focar em uma API RestFul em vez de MVC completo
builder.Services.AddControllers();

// Vamos começar configurando o banco de dados em memória
// Por baixo dos panos, o asp.net vai criar um UserDbContext e
// Vai configurá-lo para trabalhar em memória
builder.Services.AddDbContext<UserDbContext>(options =>
{
    options.UseInMemoryDatabase("AppDB");
});

// Agora configuraremos o Identity
// Vamos adicionar um Identity (ApplicationUser) e dizer ao asp.net
// para usar o UserDbContext para armazenar as informações de trabalho (runtime auth, users, etc)
// AddDefaultTokenProviders adiciona configurações internas padrão
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 4;
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<UserDbContext>()
.AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.LogoutPath = "/logout";
});

var app = builder.Build();

// MapControllers mapeia todos os controladores com a anotação Route
app.UseRouting();
app.MapControllers();

// Devemos configurar o app para usar autenticação e autorização
app.UseAuthentication();
app.UseAuthorization();

app.Run();
