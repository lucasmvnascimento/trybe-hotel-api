# Projeto Trybe Hotel

Projeto desenvolvido durante aprendizado na Trybe que envolve a criação de uma API REST para aplicação de booking de várias redes de hotéis.

## Desenvolvimento

Esta aplicação foi desenvolvida utilizando C# e ASP.NET, juntamente com banco de dados SQL Server. A partir dela, é possível fazer o gerenciamento de `Users`, `Cities`, `Hotels`, `Rooms` e `Bookings`, juntamente com a funcionalidade de busca de hotéis mais próximos de acordo com um endereço. Aplicação contém camadas de autorização e autenticação utilizando JWT.

### Tecnologias

- C#
- ASP.NET
- Entity Framework
- JWT
- Sql Server

## Rodando a Aplicação

1. Clone o repositório

- `https://github.com/lucasmvnascimento/trybe-hotel-api.git`.
- Entre na pasta do repositório que você acabou de clonar:
  - `cd trybe-hotel-api`

2. Instale as dependências

- `dotnet restore`

3. Criação do banco de dados

- `docker-compose up -d --build` para criar container com sql server.
- `cd src/TrybeHotel && dotnet ef database update` para rodar as migrations do banco.

4. Iniciar servidor

- `dotnet run`
- A aplicação será iniciada nas portas 5000 (HTTP) e 5001 (HTTPS).
