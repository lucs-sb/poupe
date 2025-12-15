# 💸 Poupe

API para controle simples de finanças pessoais, com cadastro de **pessoas** e suas **transações financeiras** (receitas e despesas), cálculo de **receitas/despesas** e **saldo líquido**.

---

A API expõe operações para:

- Cadastrar pessoas  
- Cadastrar transações  
- Cadastrar categorias

---

## 🧰 Tecnologias

- **.NET (ASP.NET Core Web API)**
- **Entity Framework Core**
- **SQL Server 2022 (Docker)**
- **FluentValidation**
- **Mapster** para mapeamento entre entidades e DTOs
- **NUnit** para testes
- **Docker**

---

## 🚀 Como executar o projeto

### Pré-requisitos

- [Docker](https://www.docker.com/) instalado

### Passos

1. Clone o repositório:

   ```bash
   git clone https://github.com/lucs-sb/poupe.git
   cd poupe

2. Suba os containers (SQL Server + API):

   ```bash
   docker compose up --build

3. Após subir tudo, acesse no navegador ou via HTTP client:

   ```bash
   http://localhost:8080
