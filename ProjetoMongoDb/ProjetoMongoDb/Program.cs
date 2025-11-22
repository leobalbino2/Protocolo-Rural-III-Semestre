using Microsoft.AspNetCore.Identity;
using ProtocoloRural.Models;
using ProtocoloRural.Services;
using ProtocoloRural.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ProtocoloRural.Seed;
using System;

// Add services to the container.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexão com o mongodb
ContextMongodb.ConnectionString = builder.Configuration.GetSection("MongoConnection:ConnectionString").Value;
ContextMongodb.Database = builder.Configuration.GetSection("MongoConnection:Database").Value;
ContextMongodb.IsSSL = Convert.ToBoolean(builder.Configuration.GetSection("MongoConnection:Isssl").Value);

//configuração Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
    .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
    (ContextMongodb.ConnectionString, ContextMongodb.Database)
    .AddDefaultTokenProviders();
builder.Services.ConfigureApplicationCookie(options =>
{
    // Rota usada quando o usuário TENTA ACESSAR uma página protegida SEM ESTAR LOGADO.
    options.LoginPath = "/Account/Login";

    // Rota usada quando o usuário ESTÁ LOGADO, mas não tem a permissão (Role/Claim) necessária.
    options.AccessDeniedPath = "/Account/AccessDenied";
});

//configuração do envio email (mantido caso queira usar e-mails em outros fluxos)
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddSingleton<EmailService>();

builder.Services.AddScoped<ContextMongodb>();
builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, UserClaimsPrincipalFactory<ApplicationUser, ApplicationRole>>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    // --- LÓGICA DE SEEDING DO ADMINISTRADOR ---
    var defaultAdminPassword = app.Configuration["AdminSettings:DefaultPassword"]
        ?? throw new InvalidOperationException("AdminSettings:DefaultPassword não configurada.");

    // Chama o método de seed para criar a Role e o Usuário Admin
    await IdentitySeeder.SeedRolesAndAdminUser(scope.ServiceProvider, defaultAdminPassword);
    // --- FIM DA LÓGICA DE SEEDING ---
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();