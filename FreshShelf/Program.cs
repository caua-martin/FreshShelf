using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Data;
using FreshShelf.Models;
using FreshShelf.Models.Enums;
using FreshShelf.Repositories;
// 1. Instancia o seu repositório
var repositorio = new FreshShelfRepository();

// 2. Chama o seu método LINQ que retorna a List<object>
var listaDeAtivos = repositorio.RestaurantesAtivos();

Console.WriteLine("--- RESTAURANTES ATIVOS ENCONTRADOS ---");

// 3. Varre a lista imprimindo cada objeto na tela
foreach (var restaurante in listaDeAtivos)
{
    Console.WriteLine(restaurante);
}

var listaDePendentes = repositorio.Pendentes();

Console.WriteLine("--- PEDIDOS PENDENTES ENCONTRADOS ---");

// 3. Varre a lista imprimindo cada objeto na tela
foreach (var order in listaDePendentes)
{
    Console.WriteLine(order);
}

var listaProdutosPorFornecedor = repositorio.ProdutosPorFornecedor(1);

Console.WriteLine("--- PRODUTOS POR FORNECEDOR ENCONTRADOS ---");

// 3. Varre a lista imprimindo cada objeto na tela
foreach (var product in listaProdutosPorFornecedor)
{
    Console.WriteLine(product);
}

Console.WriteLine("---------------------------------------");
