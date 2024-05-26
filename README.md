# Rent Bike API

## Descrição

Este projeto é uma Web API criada com .NET 8, desenvolvida para fornecer um CRUD (Create, Read, Update, Delete) de aluguel de bicicletas. A ideia da API é permitir o aluguel de bicicletas, incluindo a criação de novos registros de bicicletas, a leitura de registros existentes, a atualização de informações e a exclusão de registros.

## Arquitetura do Projeto

O projeto está estruturado nas seguintes camadas:

- **Controller Layer:** Responsável por lidar com as requisições HTTP e retornar as respostas apropriadas. Esta camada atua como um intermediário entre a camada de apresentação e a camada de negócios.
- **Service Layer:** Contém a lógica de negócios da aplicação. Esta camada realiza a validação e manipulação dos dados antes de passá-los para a camada de repositório ou de retornar respostas para a camada de controle.
- **Repository Layer:** Responsável pela interação direta com o banco de dados. Esta camada implementa o padrão Repository para abstrair as operações de persistência de dados.

## Padrões e Práticas

- **Unit of Work:** Foi utilizado o padrão Unit of Work para gerenciar as transações do banco de dados, garantindo que múltiplas operações de repositório sejam tratadas como uma única transação.
- **Test-Driven Development (TDD):** A prática de desenvolvimento orientado a testes foi seguida para garantir a qualidade e a robustez do código. Testes unitários foram criados para validar a funcionalidade das diferentes camadas do projeto.

## Autenticação

- **Entity Framework Core:** Para a autenticação, utilizamos o Entity Framework Core, que facilita o gerenciamento de dados de autenticação e autorização, integrando-se facilmente com o banco de dados.

## Tecnologias Utilizadas

- .NET 8
- [ASP.NET](http://asp.net/) Core
- MySql
- Entity Framework Core
- xUnit (para testes)
- Azure DevOps (para CI/CD e gerenciamento ágil)

## Como Executar o Projeto

1. **Clone o repositório:**
    
    ```bash
    git clone <https://github.com/gpinheirodecampos/rent-bike-api.git>
    ```
    
2. **Navegue até a pasta do projeto:**
    
    ```bash
    cd rent-bike-api
    ```
    
3. **Restaure as dependências:**
    
    ```bash
    dotnet restore
    ```
    
4. **Configure o banco de dados:**
    
    Atualize a string de conexão no arquivo `appsettings.json` conforme necessário.
    
5. **Execute as migrações do Entity Framework:**
    
    ```bash
    dotnet ef database update
    ```
    
6. **Inicie a aplicação:**
    
    ```bash
    dotnet run
    ```
    

## Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues e pull requests.
