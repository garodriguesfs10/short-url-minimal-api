# 🔗 Link Shortener API

Este projeto é um encurtador de link simples construído com as Minimal APIs do .NET 8, Entity Framework Core (EF Core) e MSSQL. Ele foi projetado para explorar a facilidade e o desempenho das Minimal APIs no .NET, além de demonstrar como usar o EF Core com o MSSQL em um ambiente Dockerizado.

## 🚀 Tecnologias

- **.NET 8** - Minimal APIs para uma configuração mais simples e leve.
- **Entity Framework Core** - ORM para facilitar a interação com o banco de dados MSSQL.
- **SQL Server** - Banco de dados relacional, executado em um contêiner Docker.
- **Docker** - Contêineres para MSSQL e API, simplificando o ambiente de desenvolvimento.

## 🛠 Configuração para rodar o Projeto

** Necessário ter o Docker instalado

1. **Clone o Repositório**:  
 ```git clone https://github.com/garodriguesfs10/short-url-minimal-api.git ```

2. **Configurar o Docker Compose**: Este projeto inclui um arquivo `docker-compose.yml` que configura os contêineres para a API e para o banco de dados MSSQL.

   Inicie os contêineres na raiz do projeto:
   ``` docker-compose up  ```

3. **Acesse a aplicação**:
     ``` http://localhost:8080/swagger/index.html ```

## 🚀 Exemplo da aplicação em funcionamento:

https://github.com/user-attachments/assets/ba953785-2e99-43d1-b85e-3b4618361a5b
