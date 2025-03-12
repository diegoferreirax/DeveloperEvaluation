# Developer Evaluation Project

Este documento fornece instruções detalhadas para configurar, executar e testar o projeto **Developer Evaluation**.

> **Observação:** Implementação realizada na branch `dev` enquanto o projeto base estava na `main`. Para analisar as modificações realizadas acesse o [Pull Request](https://github.com/diegoferreirax/DeveloperEvaluation/pull/1).

## Configuração

Para configurar o ambiente de desenvolvimento, siga os passos abaixo:

1. **Baixar e instalar o Docker Desktop**
   - O projeto utiliza Docker para gerenciar os serviços necessários. Certifique-se de baixar e instalar o [Docker Desktop](https://www.docker.com/products/docker-desktop/) conforme o sistema operacional utilizado.

2. **Configurar o .NET 8**
   - Certifique-se de ter o [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0) instalado.
   - Para verificar a instalação, execute:
     ```sh
     dotnet --version
     ```

3. **Baixar o projeto**
   - Clone o repositório utilizando o comando:
     ```sh
     git clone https://github.com/diegoferreirax/DeveloperEvaluation
     ```
   - Alternativamente, faça o download do código-fonte manualmente e extraia os arquivos.

4. **Navegar até o diretório contendo o `docker-compose.yml`**
   - Acesse o diretório correto onde está localizado o arquivo `docker-compose.yml`:
     ```sh
     cd DeveloperEvaluation\template\backend
     ```

5. **Iniciar os serviços Docker**
   - Utilize o seguinte comando para iniciar os serviços do Docker:
     ```sh
     docker compose -f docker-compose.yml up -d --force-recreate
     ```
   - Certifique-se de que todos os containers foram iniciados corretamente com:
     ```sh
     docker ps
     ```

6. **Criar o banco de dados (processo pode ser melhorado)**
   - Após os serviços do docker forem criados e iniciados, é necessário conectar no banco e criar a base de dados `developer_evaluation`.

7. **Executar os scripts SQL (processo pode ser melhorado)**
   - Copie todo o conteúdo do arquivo `scripts.sql` localizado no diretório do projeto Ambev.DeveloperEvaluation.ORM e execute-os na base recém-criada.

---

## Como executar (processo pode ser melhorado)

1. **Entrar no diretório do projeto Web API**
   - Navegue até o diretório onde o código da Web API está localizado:
     ```sh
     cd src\Ambev.DeveloperEvaluation.WebApi
     ```

2. **Compilar e executar a aplicação**
   - Execute os seguintes comandos para compilar e rodar a aplicação:
     ```sh
     dotnet build
     dotnet run
     ```

3. **Acessar a aplicação**
   - A API estará disponível na porta `5119`. Acesse pelo navegador ou ferramenta de teste HTTP:
     ```
     http://localhost:5119/swagger
     ```

---



## Testando o projeto

Para garantir o correto funcionamento da API, os seguintes endpoints podem ser testados:

1. **Registrar uma nova venda (`api/v1/sales`)**
   - Método: `POST`
   - Enviar um objeto JSON no corpo da requisição.
   - Exemplo de requisição:
     ```json
     {
       "customerId": "3b765f33-6d77-4da6-906f-511b1e2d009d",
       "saleNumber": 1,
       "saleDate": "2025-03-07T21:31:55",
       "isCanceled": false,
       "branch": "Filial 1",
       "saleItens": [
         { "itemId": "2ccb7715-03fc-447f-9632-73a8a8bcc816", "quantity": 2 },
         { "itemId": "5818a8f0-cd7c-4a5e-a7f2-a99507e9260d", "quantity": 1 }
       ]
     }
     ```

2. **Atualizar uma venda (`api/v1/sales`)**
   - Método: `PUT`
   - Enviar um objeto JSON atualizado no corpo da requisição.
   - Exemplo de requisição:
     ```json
     {
       "id": "3b765f33-6d77-4da6-906f-511b1e2d009d",
       "saleDate": "2025-03-07T21:31:55",
       "isCanceled": false,
       "branch": "Filial 1",
       "saleItens": [
         { "itemId": "2ccb7715-03fc-447f-9632-73a8a8bcc816", "quantity": 2 },
         { "itemId": "5818a8f0-cd7c-4a5e-a7f2-a99507e9260d", "quantity": 1 }
       ]
     }
     ```

3. **Deletar uma venda (`api/v1/sales/{saleId}`)**
   - Método: `DELETE`
   - Informar o `saleId` na rota.
   - Exemplo de requisição:
     ```sh
     DELETE http://localhost:5119/api/v1/sales/{saleId}
     ```

4. **Listar vendas (`api/v1/sales`)**
   - Método: `GET`
   - Aceita parâmetros de consulta: `page`, `size`, `order`, `descending`.
   - Exemplo de requisição:
     ```sh
     GET http://localhost:5119/api/v1/sales?page=1&size=10&order=date&descending=true
     ```
   - Exemplo de response:
     ```json
      [
         {
            "id": "3b765f33-6d77-4da6-906f-511b1e2d009d",
            "customerId": "3b765f33-6d77-4da6-906f-511b1e2d009d",
            "saleDate": "2025-03-07T21:31:55",
            "isCanceled": false,
            "branch": "Filial 1",
            "totalAmount": 89.99
         }
      ]
     ```

5. **Listar itens de uma venda (`api/v1/sales/{saleId}/items`)**
   - Método: `GET`
   - Informar o `saleId` na rota para obter os itens de uma venda específica.
   - Exemplo de requisição:
     ```sh
     GET http://localhost:5119/api/v1/sales/{saleId}/items
     ```
   - Exemplo de response:
     ```json
      [
         {
            "itemId": "3b765f33-6d77-4da6-906f-511b1e2d009d",
            "quantity": 2,
            "discount": 0,
            "totalItemAmount": 10,
            "item": {
               "product": "Brahma",
               "unitPrice": 5
            }
         },
         {
            "itemId": "5c765f33-6d77-4da6-906f-511b1e2d009d",
            "quantity": 3,
            "discount": 0,
            "totalItemAmount": 15,
            "item": {
               "product": "Skol",
               "unitPrice": 5
            }
         }
      ]
     ```

---
