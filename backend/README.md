# Ambev Developer Evaluation - Backend API

## Visão Geral
Este projeto é a API Backend do desafio **Ambev Developer Evaluation**, desenvolvida em **.NET 8** com arquitetura limpa e uso de **CQRS + MediatR**.
Este repositório contém o **template inicial do projeto do desafio** devidamente configurado e funcionando localmente.  
O objetivo deste commit é documentar as etapas realizadas até o momento para deixar o projeto funcional, antes de iniciar as atividades solicitadas no desafio.

## Contexto
O template enviado originalmente apresentava pequenas inconsistências que impediam a execução local imediata.  
Para possibilitar o início do desenvolvimento, foram realizados ajustes na configuração do projeto, banco de dados e mapeamentos.

---

## Tecnologias Principais
- .NET 8
- Entity Framework Core
- MediatR (CQRS)
- AutoMapper
- FluentValidation
- Serilog (Logs)
- PostgreSQL
- Docker

---

## Requisitos
Antes de rodar o projeto, certifique-se de ter instalado:

- [SDK do .NET 8](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/)
- [PostgreSQL](https://www.postgresql.org/download/)

---

## Configuração do Ambiente

### 1. Clonar o Repositório
```bash
git clone https://github.com/seu-usuario/ambev-developer-evaluation.git
cd ambev-developer-evaluation
```

### 2. Configurar o Banco de Dados
No arquivo `appsettings.json` ou `.env` dentro do projeto WebApi, atualize a string de conexão com o banco PostgreSQL:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=ambev_sales;Username=postgres;Password=admin"
}
```

### 3. Aplicar as Migrações
```bash
cd src/Ambev.DeveloperEvaluation.WebApi
dotnet ef database update
```

### 4. Rodar o Projeto
```bash
dotnet run
```

A API será iniciada em:
```
https://localhost:5001
http://localhost:5000
```

---

## Executar Testes
```bash
cd tests/Ambev.DeveloperEvaluation.UnitTests
dotnet test
```

---

## Logs de Execução
Os logs são gerados automaticamente via **Serilog** e ficam armazenados em:

```
src/Ambev.DeveloperEvaluation.WebApi/logs/log-<data>.txt
```

Exemplo:
```
src/Ambev.DeveloperEvaluation.WebApi/logs/log-20251005.txt
```

Cada arquivo contém o histórico diário de execução e operações registradas na aplicação (criação, atualização e deleção de registros).

---

## 📁 Estrutura do Projeto
```
src/
 ├── Ambev.DeveloperEvaluation.Application
 ├── Ambev.DeveloperEvaluation.Domain
 ├── Ambev.DeveloperEvaluation.ORM
 ├── Ambev.DeveloperEvaluation.WebApi
tests/
 ├── Ambev.DeveloperEvaluation.UnitTests
```

---

## 🧱 Padrões Utilizados
- **CQRS + Mediator Pattern**
- **Dependency Injection (IoC)**
- **Repository Pattern**
- **Validation Pipeline**
- **Logging Centralizado (Serilog)**

---

## 🧾 Autor
Desenvolvido por **Deyvison J Paula**  
Contato: [LinkedIn](https://www.linkedin.com/in/deyvisonjpaula/) | [GitHub](https://github.com/deyvisonjp)
