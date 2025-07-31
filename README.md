# Ambev Developer Evaluation - Sales API

Esta é uma API desenvolvida para o desafio técnico da Ambev. Ela gerencia o cadastro de vendas e seus itens, com persistência em banco de dados PostgreSQL.

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core (PostgreSQL)
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- Serilog
- Swagger (Swashbuckle)
- Docker (opcional)
- PostgreSQL

## 📁 Estrutura do Projeto

- `Domain/` - Entidades e contratos de repositórios
- `Application/` - DTOs, Handlers, Commands e Queries
- `ORM/` - Mapeamento EF Core e repositórios
- `WebApi/` - Camada de apresentação e configuração
- `IoC/` - Injeção de dependências

## 🔧 Configuração

Configure a string de conexão no arquivo `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=salesdb;Username=postgres;Password=suasenha"
}
```

## 🐘 Migrations

```bash
dotnet ef migrations add InitialCreate -p Ambev.DeveloperEvaluation.ORM -s Ambev.DeveloperEvaluation.WebApi
dotnet ef database update -s Ambev.DeveloperEvaluation.WebApi
```

## ▶️ Executando

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Acesse: `https://localhost:44312/swagger`

## ✅ Endpoints

- `POST /api/Sales` - Cria uma nova venda
- `GET /api/Sales` - Lista todas as vendas
- `GET /api/Sales/{id}` - Detalhes de uma venda
- `PUT /api/Sales/{id}` - Atualiza uma venda
- `DELETE /api/Sales/{id}` - Remove uma venda

## ⚠️ Observações

- Certifique-se de que os valores de `DateTime` estejam com `DateTimeKind.Utc`, especialmente no `CreateSaleCommandHandler`, para evitar problemas com PostgreSQL (`timestamp with time zone`).
- O método `SaveChangesAsync()` **deve ser chamado no handler**, não no repositório.

## 👨‍💻 Autor

Desenvolvido por Maurício Carvalho para o desafio técnico da Ambev.
