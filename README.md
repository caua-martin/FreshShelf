# FreshShelf

Uma API backend em ASP.NET Core pensada para ser uma plataforma B2B conectando restaurantes e fornecedores. O objetivo é simplificar a gestão de pedidos, estoque e catálogos de produtos em um único lugar.

---

## 📌 Por que fiz esse projeto?

A ideia surgiu acompanhando a rotina nos restaurantes dos meus tios. A comunicação com fornecedores, o controle de estoque e o acompanhamento de pedidos eram feitos no papel ou em conversas soltas no WhatsApp, o que gerava desencontros e perda de tempo.

Criei o **FreshShelf** para estruturar esse fluxo no backend e aplicar na prática conceitos que eu estava estudando:

* **Arquitetura REST:** Estruturação de rotas e uso correto dos status codes HTTP.
* **DTOs e AutoMapper:** Separação entre modelos de domínio e dados expostos na API para evitar *over-posting*.
* **Persistência de Dados:** EF Core com MySQL, lidando com migrations e relacionamentos 1:N.
* **Autenticação e Segurança:** Gerenciamento e hash de senhas com ASP.NET Identity.

---

## Tech Stack

* **Framework:** ASP.NET Core 8 (Web API)
* **Linguagem:** C#
* **ORM & Banco:** Entity Framework Core, MySQL
* **Autenticação:** ASP.NET Identity
* **Bibliotecas:** AutoMapper, NewtonsoftJson (JSON Patch)
* **Documentação & Ferramentas:** Swagger / OpenAPI, Git

---

## Estrutura do Projeto

```text
FreshShelf/
│
├── Controllers/         # Endpoints da API (Order, OrderItem, Product, Restaurant, Supplier, User)
├── Data/
│   ├── Dtos/            # Request/Response DTOs
│   ├── ProductContext.cs# DbContext do negócio
│   └── UserDbContext.cs # DbContext do Identity
├── Models/              # Entidades e Enums
├── Profiles/            # Mapeamentos do AutoMapper
├── Services/            # Lógica de negócio e autenticação
└── Migrations/          # Migrations do EF Core
```

🏗️ Arquitetura
O projeto foi dividido em camadas simples para separar responsabilidades:

Controllers: Recebem as requisições HTTP, validam dados de entrada e retornam as respostas.

Services: Concentram as regras de negócio e operações de usuários.

DTOs + AutoMapper: Impedem a exposição direta das tabelas do banco.

Data Layer: Camada de acesso aos dados via EF Core.

⚙️ Funcionalidades da API
Produtos
Cadastro, edição e remoção.

Atualização parcial via HTTP PATCH.

Listagem por ID e paginada (skip e take).

Fornecedores e Restaurantes
Cadastro e gestão de perfil.

Listagem paginada de estabelecimentos.

Pedidos
Criação de pedidos vinculando produtos e quantidades.

Consulta por ID ou paginada.

Autenticação
Registro assíncrono de usuários.

Login com validação e hash via ASP.NET Identity.

(A API pode ser testada localmente via Swagger UI).

Próximos Passos
[ ] Autenticação por JWT Token

[ ] Controle de acesso por perfil (Admin, Restaurante, Fornecedor)

[ ] Melhorias na documentação do Swagger

[ ] Testes unitários e de integração

[ ] Middleware global de tratamento de exceções

[ ] Conteinerização com Docker

Desenvolvido por: Cauã Martin
