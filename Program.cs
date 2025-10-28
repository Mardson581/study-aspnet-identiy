using Learn.Identity.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Adiciona um MessageService para toda a aplicação com Injeção de Dependência
// Toda classe que requisite um IMessageService (interface) no construtor, receberá
// A implementação que o IoC do Asp.Net instanciar (MessageService, no caso)
builder.Services.AddSingleton<IMessageService, MessageService>();

var app = builder.Build();

// Importante para o Asp.Net criar endpoints de todos os Controller com a anotação Route
app.MapControllers();

app.UseStaticFiles();

app.Run();