//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Text.Json;
//using System.Text.Json.Nodes;
//using System.Text.Json.Serialization;
//using System.Threading.Tasks;
//using FreshShelf.Data;
//using FreshShelf.Models;
//using FreshShelf.Models.Enums;

//namespace FreshShelf.Repositories;

//public class FreshShelfRepository
//{
//    public List<Restaurant> restaurants = MockDataStore.Restaurants;
//    public List<Supplier> suppliers = MockDataStore.Suppliers;
//    public List<Product> products = MockDataStore.Products;
//    public List<Order> orders = MockDataStore.Orders;

//    public List<object> RestaurantesAtivos()
//    {
//        var restaurantesAtivos = restaurants
//        .Where(a => a.Status == RestaurantStatus.Approved)
//        .Select(a => (object)new
//        {
//            Chave = a.Id,
//            Nome = a.Name,
//            Regiao = a.Region,
//        }).ToList();

//        return restaurantesAtivos;
//    }

//    public List<object> Pendentes()
//    {
//        var pedidosPendentes = orders
//            .Where(p => p.Status == OrderStatus.Pending)
//            .Select(p => (object)new
//            {
//                Chave = p.Id,
//                Preco = p.DeliveryPrice,
//                Criacao = p.CreatedAt,
//            }).ToList ();

//        return pedidosPendentes;
//    }

//    public List<object> ProdutosPorFornecedor(int id)
//    {
//        var produtosPorFornecedor = products
//            .Where(s => s.SupplierId == id)
//            .Select(s => (object)new
//            {
//                SupplierName = s.Supplier.Name,
//                ProductName = s.Name,
//                DescriptionAttribute = s.Description,
//            }).ToList();

//        return produtosPorFornecedor;
//    }
//}
