# crudrazor

Comandos usados:

# dotnet new mvc -n CrudProdutos
→ Cria um novo projeto ASP.NET Core MVC chamado CrudProdutos.

# dotnet add package Microsoft.EntityFrameworkCore.Tools
→ Adiciona a ferramenta do Entity Framework Core para executar comandos como migrations e update database.

# dotnet add package Microsoft.EntityFrameworkCore.SqlServer
→ Adiciona o provedor do Entity Framework Core para conectar o projeto ao SQL Server.

# dotnet add package Microsoft.EntityFrameworkCore.Design
→ Adiciona ferramentas de design do Entity Framework Core, necessárias para gerar migrações e trabalhar com scaffolding.

# dotnet aspnet-codegenerator controller -name ProdutosController -m Produto -dc AppDbContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
→ Gera automaticamente um controlador (ProdutosController) baseado no modelo Produto e no contexto do banco de dados (AppDbContext), criando as ações CRUD (Create, Read, Update, Delete).

# dotnet ef migrations add InitialCreate
→ Cria uma migração chamada InitialCreate, que contém as instruções para criar as tabelas do banco de dados com base nos modelos definidos no código.

# dotnet ef database update
→ Aplica a migração ao banco de dados, criando as tabelas e estruturas necessárias.
