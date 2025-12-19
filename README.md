# 📊 Poupe — Sistema de Controle de Gastos Residenciais

O **Poupe** é um sistema **controle de gastos residenciais**, permitindo o gerenciamento de **pessoas**, **categorias** e **transações financeiras**s, com regras de negócio claras e separação entre **backend** e **frontend**.

---

## 🏗️ Arquitetura Geral

```bash
poupe/
├── Backend/ # API REST em .NET
└── Frontend/ # SPA em React + Vite
```

A comunicação entre frontend e backend é feita via **API REST**, utilizando **JSON** e **HTTP padrão**.

## 🚀 Backend (API)

### 🧱 Tecnologias Utilizadas
- **.NET 8**
- **ASP.NET Core**
- **Entity Framework Core**
- **FluentValidation**
- **Mapster** para mapeamento entre entidades e DTOs
- **NUnit** para testes
- **SQL Server**
- **Docker & Docker Compose**
- 
### 📐 Estrutura

```bash
Backend/
├── Poupe.API
├── Poupe.Application
├── Poupe.Domain
├── Poupe.Infrastructure
└── Poupe.CrossCutting
```


### 📌 Funcionalidades do Backend
- CRUD de **usuários**
- CRUD de **categorias**
- CRUD de **transações**
- Retorno de dados estruturados via **DTOs**
- Tratamento de erros padronizado
- Integração segura com o frontend

---

## 🎨 Frontend (SPA)

### ⚙️ Tecnologias Utilizadas
- **React**
- **React Router v7**
- **Vite**
- **TypeScript**
- **Material UI (MUI)**
- **React Hook Form**
- **Docker**

### 📐 Estrutura

```bash
├── app/
│ ├── components/ # Componentes reutilizáveis
│ ├── domain/ # Tipos, enums e mappers
│ ├── hooks/ # Hooks customizados
│ ├── integration/ # Comunicação com a API
│ ├── routes/ # Rotas (React Router v7)
│ └── styles/ # Tema e estilos
```

---

## 🐳 Docker e Execução

O projeto utiliza **Docker Compose** para subir todos os serviços necessários:

- API (.NET)
- Frontend (React)
- Banco de dados (SQL Server)

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
   Frontend: http://localhost:5173
   Backend (API): http://localhost:8080
