FreshShelf
Uma API backend em ASP.NET Core que foi pensada em ser uma plataforma B2B conectando restaurantes e fornecedores. O objetivo é simplificar a gestão de pedidos, estoque e catálogos de produtos em um único lugar.

Por que fiz esse projeto?
A ideia nasceu vendo a rotina nos restaurantes dos meus tios. A comunicação com fornecedores, o controle de estoque e o acompanhamento de pedidos eram quase todos feitos no papel ou em conversas soltas no WhatsApp, o que sempre gerava confusão.

Criei o FreshShelf para estruturar esse fluxo do zero no backend e, ao mesmo tempo, colocar em prática conceitos nos quais estava aprendendo sobre:

Arquitetura REST: Rotas e uso dos status code HTTP.

DTOs e AutoMapper: Proteção do modelo de domínio e separação entre as entidades internas e o que é exposto na API.

Persistência de Dados: Entity Framework Core integrado ao MySQL, lidando com migrations, com modelagem relacional 1:N

Autenticação e Segurança: Gerenciamento e hash de senhas com ASP.NET Identity.

Tech Stack
Framework: ASP.NET Core 8 (Web API)

Linguagem: C#

ORM & Banco de Dados: Entity Framework Core, MySQL

Autenticação: ASP.NET Identity

Bibliotecas: AutoMapper, NewtonsoftJson (Suporte a JSON Patch)

Documentação & Ferramentas: Swagger / OpenAPI, Git

Estrutura do Projeto
FreshShelf/
│
├── Controllers/         # Endpoints da API (Order, OrderItem, Product, Restaurant, Supplier, User)
├── Data/
│   ├── Dtos/            # Request/Response DTOs (Create, Read, Update, Login)
│   ├── ProductContext.cs# DbContext das entidades de negócio
│   └── UserDbContext.cs # DbContext do ASP.NET Identity
├── Models/              # Entidades de domínio e Enums
├── Profiles/            # Mapeamentos do AutoMapper
├── Services/            # Camada de regras de negócio e auth
└── Migrations/          # Migrations do EF Core

Arquitetura
O projeto segue uma estrutura em camadas simples e funcional para manter as responsabilidades bem separadas:

Controllers: Recebem as requisições HTTP, validam a entrada e retornam as respostas com os status corretos.

Services: Concentram a lógica de negócio e operações do usuário.

DTOs + AutoMapper: Evitam exposição direta das tabelas do banco e impedem problemas como over-posting.

Data Layer: Camada de acesso ao banco gerenciada pelo EF Core.

Funcionalidades da API
Produtos
Cadastro, atualização e remoção de produtos.

Atualização parcial via PATCH (JSON Patch).

Listagem individual ou paginada (skip e take).

Fornecedores e Restaurantes
Cadastro e gestão de perfis.

Listagem paginada de estabelecimentos.

Pedidos
Criação de pedidos vinculando produtos e quantidades.

Consulta de detalhes do pedido por ID ou em blocos paginados.

Autenticação (Identity)
Registro assíncrono de usuários.

Login com verificação segura e hash de senha via ASP.NET Identity.

(Toda a API pode ser testada via Swagger UI ao rodar o projeto localmente).

Próximos Passos
Implementar autenticação via JWT Token

Adicionar controle de acesso por perfil (Admin, Restaurante, Fornecedor)

Aprimorar a documentacao no Swagger

Criar testes unitários e de integração

Implementar middleware global para tratamento de exceções

Dockerizar a aplicação

Desenvolvido por: Cauã Martin
