using ControleVacinas___MVC_ASP_NET.Data;
using ControleVacinas___MVC_ASP_NET.Helper;
using ControleVacinas___MVC_ASP_NET.Repositorio;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

//Configurar cultura padrão (temporário)
//var defaultCulture = new CultureInfo("en-US"); //Formato ISO compatível
//var defaultCulture = new CultureInfo("pt-BR"); //Formato ISO compatível
//CultureInfo.DefaultThreadCurrentCulture = defaultCulture;
//CultureInfo.DefaultThreadCurrentUICulture = defaultCulture;

// Add services to the container.
builder.Services.AddControllersWithViews();

//Conexão com SQL Server Express
builder.Services.AddEntityFrameworkSqlServer().AddDbContext<BancoContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DataBase")));

//Conexão com MySQL Workbench
//var connectionString = builder.Configuration.GetConnectionString("DataBase");
//try
//{
//    builder.Services.AddDbContext<BancoContext>(o =>
//    {
//        o.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//        //Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.38-mysql"); // Versão obtida em: 26/12/2024 - 11:20
//    });
//    // Se a configuração do DbContext foi bem-sucedida, você pode fazer algum log ou ação adicional, se necessário
//    Console.WriteLine("Conexão com sucesso.");
//}
//catch (Exception ex)
//{
//    // Captura qualquer outro tipo de exceção
//    Console.WriteLine($"Erro ao conectar com o banco de dados: {ex.Message}");    
//}

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IListaDeCadastrosRepositorio, ListaDeCadastrosRepositorio>();
builder.Services.AddScoped<IListaDeUsuariosRepositorio, ListaDeUsuariosRepositorio>();
builder.Services.AddScoped<ISessao, Sessao>();
builder.Services.AddScoped<IEmail, Email>();

builder.Services.AddSession(o => 
{
    o.Cookie.HttpOnly = true;
    o.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}"); 
    // pattern: "{controller=Home}/{action=Index}/{id?}"); (página padrão sem tela de loegin //Parâmetro depois da ação pode ser nulo ou vim informação (pode ser preenchido ou não)

app.Run();
