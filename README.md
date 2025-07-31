
# Ambev Developer Evaluation - Sales API

Esta é uma API desenvolvida para o desafio técnico. Ela gerencia o cadastro de vendas e seus itens, com persistência em banco de dados PostgreSQL.

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
dotnet ef database update -s Ambev.DeveloperEvaluation.WebApi
```

## ▶️ Executando

```bash
dotnet run --project src/Ambev.DeveloperEvaluation.WebApi
```

Acesse: `https://localhost:44312/swagger`

## ✅ Endpoints

### 📦 Vendas

- `POST /api/Sales` - Cria uma nova venda
- `GET /api/Sales` - Lista todas as vendas (com filtros, ordenação e paginação)
- `GET /api/Sales/{id}` - Detalhes de uma venda
- `PUT /api/Sales/{id}` - Atualiza uma venda
- `DELETE /api/Sales/{id}` - Remove uma venda

### 🔐 Autenticação

- `POST /api/Auth` - Autentica um usuário e retorna um token JWT

### 👤 Usuários

- `POST /api/Users` - Cria um novo usuário
- `GET /api/Users/{id}` - Consulta um usuário pelo ID
- `DELETE /api/Users/{id}` - Remove um usuário pelo ID

## ⚙️ Funcionalidades Avançadas

- 🔍 **Filtros**: por `ClientName`, `StartDate`, `EndDate`
- 🧭 **Ordenação**: por `SaleDate`, `ClientName` ou `SaleNumber`
- 📄 **Paginação**: parâmetros `Page` e `PageSize`
- 🧾 **Eventos de domínio via Log**:
  - `SaleCreated`
  - `SaleModified`
  - `SaleCancelled`
  - `ItemCancelled` (ao remover item de uma venda existente)
- 🚫 **Tratamento global de erros** com middleware customizado
- 🔐 **Autenticação JWT** para proteger endpoints

## ⚠️ Observações

- Certifique-se de que os valores de `DateTime` estejam com `DateTimeKind.Utc`, especialmente no `CreateSaleCommandHandler`, para evitar problemas com PostgreSQL (`timestamp with time zone`).
- O método `SaveChangesAsync()` **deve ser chamado no handler**, não no repositório.

## 👨‍💻 Autor

Desenvolvido por Maurício Carvalho para o desafio técnico.
