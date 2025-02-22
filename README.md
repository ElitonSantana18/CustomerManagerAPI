# CustomerManager WebAPI

## Descrição
CustomerManager é uma API REST desenvolvida em ASP.NET Core para gerenciamento de clientes e endereços. A API permite a criação, atualização, consulta e remoção de clientes e endereços, incluindo a funcionalidade de upload de logotipos.

## Tecnologias Utilizadas
- **ASP.NET Core**
- **C#**
- **Entity Framework Core**
- **Sql Server**

## Funcionalidades
- CRUD de clientes
- Upload de logotipo do cliente
- CRUD de endereços
- Autenticação e Autorização via JWT

## Requisitos
- .NET 6 ou superior
- SqlServer

## Instalação e Configuração
### 1. Clone o repositório
```sh
git clone https://github.com/elitonsantana18/CustomerManager.git
cd CustomerManager
```

### 2. Executar a API
```sh
dotnet run --project CustomerManager.WebAPI
```

## Endpoints

### Cliente
#### Listar todos os clientes
```http
GET /api/customer
```
#### Obter um cliente pelo ID
```http
GET /api/customer/{id}
```
#### Criar um novo cliente
```http
POST /api/customer
```
#### Atualizar um cliente existente
```http
PUT /api/customer/{id}
```
#### Remover um cliente
```http
DELETE /api/customer/{id}
```

### Endereço
#### Listar todos os endereços
```http
GET /api/customer/address
```
#### Obter um endereço pelo ID
```http
GET /api/customer/address/{id}
```
#### Criar um novo endereço
```http
POST /api/customer/address
```
#### Atualizar um endereço existente
```http
PUT /api/customer/address/{id}
```
#### Remover um endereço
```http
DELETE /api/customer/address/{id}
```

## Contribuição
Contribuições são bem-vindas! Para contribuir:
1. Faça um **fork** do projeto
2. Crie um **branch** com sua feature (`git checkout -b minha-feature`)
3. Commit suas mudanças (`git commit -m 'Adicionando nova feature'`)
4. Faça um **push** para o branch (`git push origin minha-feature`)
5. Abra um **Pull Request**

## Licença
Este projeto está licenciado sob a **MIT License**. Consulte o arquivo `LICENSE` para mais detalhes.
