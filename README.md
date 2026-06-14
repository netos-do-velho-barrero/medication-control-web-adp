# 💊 Sistema de Gestão de Medicamentos e Estoque 🚀

> **Controle inteligente de fornecedores, pacientes, medicamentos, funcionários e movimentações de estoque.**

![C#](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![.NET](https://img.shields.io/badge/.NET-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Razor CSHTML](https://img.shields.io/badge/Razor_CSHTML-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
![Arquitetura MVC](https://img.shields.io/badge/Architecture-MVC-blue?style=for-the-badge)
![Desenvolvimento Web](https://img.shields.io/badge/Dev-Web-orange?style=for-the-badge)
![Status](https://img.shields.io/badge/Status-Conclu%C3%ADdo-brightgreen?style=for-the-badge)

---

## 👥 Desenvolvedores

<table>
  <tr>
    <td align="center">
      <a href="https://github.com/pedrohenriquedsdev">
        <img src="https://github.com/pedrohenriquedsdev.png" width="80px" style="border-radius: 50%"/><br/>
        <sub><b>Pedro Henrique</b></sub>
      </a>
    </td>
    <td align="center">
      <a href="https://github.com/Marco-Oliver">
        <img src="https://github.com/Marco-Oliver.png" width="80px" style="border-radius: 50%"/><br/>
        <sub><b>Marco Oliver</b></sub>
      </a>
    </td>
  </tr>
</table>

<br>

## 📋 Sobre o Projeto

Uma farmácia popular precisa de um sistema para controlar seu estoque de medicamentos, registrar entradas de produtos de fornecedores e gerenciar a saída de medicamentos para pacientes. Para resolver essa necessidade, foi criado o **Sistema de Gestão de Farmácia** — uma aplicação completa para cadastrar fornecedores, pacientes, funcionários e medicamentos, além de controlar todas as movimentações de estoque de forma simples e organizada.

<br>

## ✨ Funcionalidades

### 🏭 Módulo de Fornecedores
- Cadastrar, editar, visualizar e excluir fornecedores
- Campos obrigatórios: nome (3–100 caracteres), telefone e CNPJ (14 dígitos)
- Não é possível cadastrar dois fornecedores com o mesmo CNPJ

### 🧑‍⚕️ Módulo de Pacientes
- Cadastrar, editar, visualizar e excluir pacientes
- Campos obrigatórios: nome (3–100 caracteres), telefone, Cartão do SUS (15 dígitos) e CPF (11 dígitos)
- Formatos de telefone aceitos: `(XX) XXXX-XXXX` ou `(XX) XXXXX-XXXX`
- Não é possível cadastrar dois pacientes com o mesmo Cartão do SUS

### 💊 Módulo de Medicamentos
- Cadastrar, editar, visualizar e excluir medicamentos
- Campos obrigatórios: nome (3–100 caracteres), descrição (5–255 caracteres), quantidade em estoque e fornecedor
- Medicamentos com menos de 20 unidades são destacados como **"Em Falta"**
- Se o medicamento já estiver cadastrado, o sistema atualiza a quantidade automaticamente

### 👨‍💼 Módulo de Funcionários
- Cadastrar, editar, visualizar e excluir funcionários
- Campos obrigatórios: nome (3–100 caracteres), telefone e CPF (11 dígitos)
- Não é possível cadastrar dois funcionários com o mesmo CPF

### 📦 Módulo de Estoque

#### 🔼 Requisições de Entrada
- Registrar e visualizar entradas de medicamentos no estoque
- Campos obrigatórios: data, medicamento, funcionário responsável e quantidade (número positivo)
- O estoque é atualizado automaticamente ao registrar uma entrada

#### 🔽 Requisições de Saída
- Registrar e visualizar saídas de medicamentos do estoque
- Campos obrigatórios: data, paciente e medicamentos requisitados
- Não é permitido registrar saída que exceda o estoque disponível
- O estoque é subtraído automaticamente ao registrar uma saída

<br>

## 🚀 Como Executar o Projeto

### Pré-requisitos

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- Git

### Passo a passo

```bash
# 1. Clone o repositório
git clone https://github.com/pedrohenriquedsdev/controle-de-medicamentos.git

# 2. Acesse a pasta do projeto
cd controle-de-medicamentos

# 3. Restaure os pacotes
dotnet restore

# 4. Execute a aplicação
dotnet run --project src/ControleDeMedicamentos.WebApp
```

Acesse no navegador: `https://localhost:5001`

> Os dados são persistidos em arquivo local — nenhuma configuração de banco de dados é necessária.

<br>

## 🎬 Demonstração

> *(Adicione aqui um GIF ou vídeo demonstrando as principais telas do sistema)*

<!-- Exemplo:
![Demo da aplicação](docs/demo.gif)
-->

<br>

## 🏗️ Arquitetura

O projeto segue o padrão **MVC Modular**, organizando cada contexto de negócio em um módulo independente:

```
ControleDeMedicamentos.WebA/
├── Compartilhado/                     # Recursos transversais reutilizados por todos os módulos
│   ├── Aplicacao/                     # Interfaces, serviços base e contratos de aplicação
│   ├── Apresentacao/                  # ViewModels, helpers e componentes compartilhados de UI
│   ├── Dominio/                       # Entidades base, Value Objects e regras de domínio comuns
│   └── Infra/                         # Persistência, repositórios base e configuração
│
└── Modulos/                           # Módulos de negócio independentes (MVC por módulo)
    ├── ModuloEstoque/                 # Controllers, Views, Services e Repositórios de Estoque
    ├── ModuloFornecedor/              # Controllers, Views, Services e Repositórios de Fornecedor
    ├── ModuloFuncionario/             # Controllers, Views, Services e Repositórios de Funcionário
    ├── ModuloMedicamento/             # Controllers, Views, Services e Repositórios de Medicamento
    └── ModuloPaciente/                # Controllers, Views, Services e Repositórios de Paciente
```

<br>

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Uso |
|---|---|
| ASP.NET MVC (.NET 8) | Framework principal |
| C# | Linguagem de programação |
| Razor / TagHelpers | Renderização de views |
| DataAnnotations | Validações de formulário |
| AutoMapper | Mapeamento entre entidades e ViewModels |
| Injeção de Dependência | Baixo acoplamento entre camadas |
| Serialização em arquivo (JSON) | Persistência de dados |
| Bootstrap | Estilização da interface |

<br>

## ✅ Boas Práticas Aplicadas

- Separação clara em **3 camadas** (Apresentação, Domínio, Infraestrutura)
- **ViewModels** para comunicação com as Views; **Records** para DTOs imutáveis
- **Services** concentrando as regras de negócio
- **Extension Methods** para comportamentos reutilizáveis
- **Delegates, métodos anônimos e Lambdas** para maior legibilidade
- **TempData** para feedback entre requisições
- **ModelState** para validação consistente dos formulários
- Nomenclatura seguindo o padrão **PascalCase / camelCase**
- Tratamento de exceções e validações robustos

<br>

## 📌 Regras de Negócio Principais

- CNPJs duplicados não são permitidos no cadastro de fornecedores
- Cartões do SUS duplicados não são permitidos no cadastro de pacientes
- CPFs duplicados não são permitidos no cadastro de funcionários
- Medicamentos com menos de 20 unidades são sinalizados como **"Em Falta"**
- Se um medicamento já estiver cadastrado, sua quantidade em estoque é somada automaticamente
- Requisições de saída não podem exceder o estoque disponível do medicamento
- O estoque é atualizado automaticamente em toda movimentação de entrada ou saída

<br>

## 📄 Licença

Este projeto foi desenvolvido para fins educacionais na **Academia do Programador**.
