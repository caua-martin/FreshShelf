//using FreshShelf.Models;
//using FreshShelf.Models.Enums;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace FreshShelf.Data;

//public class MockDataStore
//{
//    public static List<Restaurant> Restaurants { get; } = new();
//    public static List<Supplier> Suppliers { get; } = new();
//    public static List<Product> Products { get; } = new();
//    public static List<Order> Orders { get; } = new();

//    // Construtor Estático: Executa automaticamente UMA VEZ assim que a API liga
//    static MockDataStore()
//    {
//        SeedData();
//    }

//    private static void SeedData()
//    {
//        #region 1. Seed de Restaurantes
//        var rest1 = new Restaurant
//        {
//            Id = 1,
//            UserId = "user-rest-01",
//            Name = "Bistro Central",
//            Description = "Alta gastronomia no coração da cidade",
//            Cnpj = "12.345.678/0001-99",
//            Region = "Centro",
//            Status = RestaurantStatus.Approved
//        };

//        var rest2 = new Restaurant
//        {
//            Id = 2,
//            UserId = "user-rest-02",
//            Name = "Pizzaria Bella Italia",
//            Description = "Pizzas artesanais no forno a lenha",
//            Cnpj = "98.765.432/0001-88",
//            Region = "Zona Sul",
//            Status = RestaurantStatus.Approved
//        };

//        var rest3 = new Restaurant
//        {
//            Id = 3,
//            UserId = "user-rest-03",
//            Name = "Burguer Tech",
//            Description = "Hamburgueria gourmet conceitual",
//            Cnpj = "45.678.123/0001-77",
//            Region = "Zona Norte",
//            Status = RestaurantStatus.Pending // Restaurante ainda inativo/pendente
//        };

//        Restaurants.AddRange(new[] { rest1, rest2, rest3 });
//        #endregion

//        #region 2. Seed de Fornecedores (Suppliers)
//        var sup1 = new Supplier
//        {
//            Id = 1,
//            UserId = "user-sup-01",
//            Name = "Distribuidora Vale Verde",
//            Description = "Hortifrúti direto do produtor",
//            Cnpj = "11.222.333/0001-44",
//            Region = "Interior",
//            Status = SupplierStatus.Approved
//        };

//        var sup2 = new Supplier
//        {
//            Id = 2,
//            UserId = "user-sup-02",
//            Name = "Laticínios Alvorada",
//            Description = "Queijos, leites e derivados em atacado",
//            Cnpj = "55.666.777/0001-22",
//            Region = "Sul do Estado",
//            Status = SupplierStatus.Approved
//        };

//        Suppliers.AddRange(new[] { sup1, sup2 });
//        #endregion

//        #region 3. Seed de Produtos (Products)
//        // Produtos do Fornecedor 1 (Hortifrúti)
//        var prod1 = new Product
//        {
//            Id = 1,
//            Name = "Tomate Italiano (Kg)",
//            Description = "Tomates maduros selecionados para molho",
//            Price = 6.50m,
//            SupplierId = sup1.Id,
//            Supplier = sup1,
//            Status = ProductStatus.Active
//        };

//        var prod2 = new Product
//        {
//            Id = 2,
//            Name = "Cebola Roxa (Kg)",
//            Description = "Cebola roxa fresca tipo exportação",
//            Price = 4.80m,
//            SupplierId = sup1.Id,
//            Supplier = sup1,
//            Status = ProductStatus.Active
//        };

//        // Produtos do Fornecedor 2 (Laticínios)
//        var prod3 = new Product
//        {
//            Id = 3,
//            Name = "Queijo Muçarela Peça (5kg)",
//            Description = "Muçarela de alta qualidade com excelente derretimento",
//            Price = 180.00m,
//            SupplierId = sup2.Id,
//            Supplier = sup2,
//            Status = ProductStatus.Active
//        };

//        var prod4 = new Product
//        {
//            Id = 4,
//            Name = "Creme de Leite Fresco (1L)",
//            Description = "Creme de leite pasteurizado 35% de gordura",
//            Price = 22.90m,
//            SupplierId = sup2.Id,
//            Supplier = sup2,
//            Status = ProductStatus.Active
//        };

//        Products.AddRange(new[] { prod1, prod2, prod3, prod4 });

//        // Amarrando a lista de produtos de volta no catálogo do Fornecedor (POO Pura)
//        sup1.Products.Add(prod1);
//        sup1.Products.Add(prod2);
//        sup2.Products.Add(prod3);
//        sup2.Products.Add(prod4);
//        #endregion

//        #region 4. Seed de Pedidos (Orders)
//        // Pedido 1: Pizzaria Bella Italia comprando do Laticínios Alvorada
//        var order1 = new Order
//        {
//            Id = 1,
//            RestaurantId = rest2.Id,
//            Restaurant = rest2,
//            SupplierId = sup2.Id,
//            Supplier = sup2,
//            DeliveryPrice = 15.00m,
//            Status = OrderStatus.Pending, // Pedido Pendente
//            CreatedAt = DateTime.UtcNow.AddHours(-2)
//        };

//        // Adicionando os itens desse pedido
//        order1.Items.Add(new OrderItem
//        {
//            Id = 1,
//            ProductId = prod3.Id,
//            Product = prod3,
//            Quantity = 2, // 2 peças de queijo muçarela
//            PriceAtPurchase = prod3.Price // Fotografando o preço atual: R$ 180.00
//        });

//        order1.Items.Add(new OrderItem
//        {
//            Id = 2,
//            ProductId = prod4.Id,
//            Product = prod4,
//            Quantity = 5, // 5 litros de creme de leite
//            PriceAtPurchase = prod4.Price // Fotografando o preço atual: R$ 22.90
//        });

//        // Pedido 2: Bistro Central comprando do Vale Verde
//        var order2 = new Order
//        {
//            Id = 2,
//            RestaurantId = rest1.Id,
//            Restaurant = rest1,
//            SupplierId = sup1.Id,
//            Supplier = sup1,
//            DeliveryPrice = 10.00m,
//            Status = OrderStatus.Delivered, // Pedido já entregue
//            CreatedAt = DateTime.UtcNow.AddDays(-2)
//        };

//        order2.Items.Add(new OrderItem
//        {
//            Id = 3,
//            ProductId = prod1.Id,
//            Product = prod1,
//            Quantity = 20, // 20 kg de tomate
//            PriceAtPurchase = 5.90m // Simulando que no passado o preço era menor (R$ 5.90 em vez de R$ 6.50)
//        });

//        Orders.AddRange(new[] { order1, order2 });

//        // Amarrando o histórico de pedidos de volta no restaurante (POO Pura)
//        rest2.Orders.Add(order1);
//        rest1.Orders.Add(order2);
//        #endregion
//    }
//}
