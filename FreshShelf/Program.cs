using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Data;
using FreshShelf.Services;

//using FreshShelf.Data;
using FreshShelf.Models;
using FreshShelf.Models.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
//using FreshShelf.Repositories;
// 1. Instancia o seu repositório
//var repositorio = new FreshShelfRepository();

//// 2. Chama o seu método LINQ que retorna a List<object>
//var listaDeAtivos = repositorio.RestaurantesAtivos();

//Console.WriteLine("--- RESTAURANTES ATIVOS ENCONTRADOS ---");

//// 3. Varre a lista imprimindo cada objeto na tela
//foreach (var restaurante in listaDeAtivos)
//{
//    Console.WriteLine(restaurante);
//}

//var listaDePendentes = repositorio.Pendentes();

//Console.WriteLine("--- PEDIDOS PENDENTES ENCONTRADOS ---");

//// 3. Varre a lista imprimindo cada objeto na tela
//foreach (var order in listaDePendentes)
//{
//    Console.WriteLine(order);
//}

//var listaProdutosPorFornecedor = repositorio.ProdutosPorFornecedor(1);

//Console.WriteLine("--- PRODUTOS POR FORNECEDOR ENCONTRADOS ---");

//// 3. Varre a lista imprimindo cada objeto na tela
//foreach (var product in listaProdutosPorFornecedor)
//{
//    Console.WriteLine(product);
//}

//Console.WriteLine("---------------------------------------");

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("ProductConnection");
var connectionString2 = builder.Configuration.GetConnectionString("UserConnection");

builder.Services.AddDbContext<UserDbContext>
    (opts =>
    {
        opts.UseMySql
            (connectionString2,
            ServerVersion.AutoDetect
            (connectionString2));
    });

builder.Services.AddDbContext<ProductContext>(opts =>
    opts.UseLazyLoadingProxies().UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders();

builder.Services.
    AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<UserService>();

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string conexao = configuration.GetConnectionString("ProductConnection");

Console.WriteLine(conexao);
// Adiciona os serviços do Swagger
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "FilmesAPI", Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa o Swagger no navegador
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
