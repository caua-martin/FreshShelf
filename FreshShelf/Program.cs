using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FreshShelf.Data;
using FreshShelf.Services;
using Microsoft.IdentityModel.Tokens;
using FreshShelf.Authorization;
using Microsoft.AspNetCore.Authorization;

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

var connectionString = builder.Configuration["ConnectionStrings:ProductConnection"];
var connectionString2 = builder.Configuration["ConnectionStrings:UserConnection"];

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

builder.Services
    .AddIdentity<User, AcessProfile>()
    .AddEntityFrameworkStores<UserDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<AcessProfile>();

builder.Services.
    AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<IAuthorizationHandler, AgeAuthorization>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<RestaurantService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderItemService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<TokenService>();

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
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ProductsAPI", Description = "API to connect restaurants to suppliers",
        Contact = new OpenApiContact
        {
            Name = "Suporte",
            Email = "cauamartin220329@gmail.com",
            Url = new Uri("https://github.com/caua-martin/FreshShelf")
        },Version = "v1" });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Digite: Bearer {seu token JWT}"
    });

    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["SymmetricSecurityKey"])),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MinimumAge", policy =>
         policy.AddRequirements(new MinimumAge(18))
    );
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await RoleSeeder.SeedRolesAsync(services);
}

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
