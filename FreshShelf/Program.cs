using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Data;

//using FreshShelf.Data;
using FreshShelf.Models;
using FreshShelf.Models.Enums;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

builder.Services.AddDbContext<ProductContext>(opts =>
    opts.UseMySql(
        connectionString,
        new MySqlServerVersion(new Version(8, 0, 36))
    ));

var configuration = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

string conexao = configuration.GetConnectionString("ProductConnection");

Console.WriteLine(conexao);
// Adiciona os serviços do Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Ativa o Swagger no navegador
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.MapControllers();
app.Run();
