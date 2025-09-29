# Developer Evaluation Project - Template Inicial

## Resumo
Este repositório contém o **template inicial do projeto do desafio** devidamente configurado e funcionando localmente.  
O objetivo deste commit é documentar as etapas realizadas até o momento para deixar o projeto funcional, antes de iniciar as atividades solicitadas no desafio.

---

## Contexto
O template enviado originalmente apresentava pequenas inconsistências que impediam a execução local imediata.  
Para possibilitar o início do desenvolvimento, foram realizados ajustes na configuração do projeto, banco de dados e mapeamentos.

---

## Etapas realizadas

### 1. Ambiente
- Utilizado **.NET 8** e **Visual Studio 2022 / VS Code**.
- Banco de dados: PostgreSQL (local e Neon online, para contornar limitações do Docker na máquina pessoal).
- Pacotes principais: `Entity Framework Core`, `AutoMapper`, `MediatR`.

### 2. Problemas encontrados
- Template inicial com mapeamentos AutoMapper faltando em algumas rotas (Users e Auth).
- Estrutura do banco de dados incompleta ou incompatível com PostgreSQL local.
- Endpoints de autenticação e usuário não rodavam devido a mapeamentos faltantes.
- Docker na máquina pessoal não suportava execução do container, sendo necessário alternativas de banco on-line.

### 3. Correções realizadas
- Criadas as tabelas necessárias no banco de dados (Users) com colunas corretas.
- Corrigidos mapeamentos AutoMapper para:
  - `CreateUserRequest -> CreateUserCommand`
  - `GetUserResult -> GetUserResponse`
  - `AuthenticateUserRequest -> AuthenticateUserCommand`
  - `AuthenticateUserResult -> AuthenticateUserResponse`
- Ajustes no DbContext e nas migrations para compatibilidade com PostgreSQL.
- API inicial funcional localmente, endpoints testados para criação e consulta de usuários.

---

## Status atual
- Template inicial **funcionando localmente**.
- API pronta para receber testes.
- Próximo passo: iniciar o desenvolvimento das atividades solicitadas no desafio (CRUD de vendas, regras de negócio, etc).

---

## Observações
- Documentei os passos e ajustes realizados para que a equipe de avaliação possa entender rapidamente as alterações necessárias para executar o template.
- Não foram implementadas funcionalidades do desafio ainda, apenas ajustes para rodar a base enviada.

---

## Como rodar o projeto localmente
1. Clonar o repositório
`git clone <link-do-repositorio>`

2. Configurar connection string para PostgreSQL (local ou Neon)

3. Executar migrations
`dotnet ef database update`
