# Gerenciador De Vendas

## Descrição
O **Gerenciador De Vendas** é uma aplicação web desenvolvida em ASP.NET Core (.NET 8) com Razor Pages, voltada para o gerenciamento de produtos, vendas e controle de estoque. O sistema permite cadastrar, editar, remover e listar produtos, além de realizar vendas, controlar reservas de estoque.

## Funcionalidades Principais
- Cadastro, edição e exclusão de produtos
- Controle de estoque e reservas
- Registro e gerenciamento de vendas
- Autenticação de usuários (login, logout, registro)
- Sessão para controle de itens em venda

## Tecnologias Utilizadas
- ASP.NET Core (.NET 8)
- Razor Pages
- Entity Framework Core
- Identity para autenticação
- SQL Server
- SQLite

## Estrutura de Pastas
- `Controllers/` — Lógica de controle das páginas e APIs
- `Entities/` — Modelos de dados da aplicação
- `Service/` — Serviços de negócio (Produto, Venda, etc.)
- `Repository/` — Interfaces e implementações de acesso a dados
- `Views/` — Páginas Razor (UI)
- `wwwroot/` — Arquivos estáticos (CSS, JS, imagens)

## Como Executar Localmente
1. Clone o repositório:git clone <url-do-repositorio>2. Navegue até a pasta do projeto:cd Gerenciador_De_Vendas/Gerenciador_De_Vendas3. Restaure os pacotes:dotnet restore4. Atualize a string de conexão do banco de dados em `appsettings.json` se necessário.
5. Execute as migrações para criar o banco:dotnet ef database update6. Rode a aplicação:dotnet run7. Acesse via navegador: `https://localhost:5001` ou `http://localhost:5000`

## Observações
- Certifique-se de ter o .NET 8 SDK instalado.
- O projeto utiliza autenticação, então é necessário registrar um usuário para acessar as funcionalidades protegidas.

---

Sinta-se à vontade para contribuir ou sugerir melhorias!

# Algumas telas da aplicação

Tela de login:
<img width="1280" alt="image" src="https://github.com/user-attachments/assets/645f873b-d4d2-4cf6-ac1a-9574195b108d" />

Tela de cadastro de produtos:
<img width="1274" alt="image" src="https://github.com/user-attachments/assets/92c91eeb-37a8-4621-8cf3-30fc430fbf18" />

Tela de vendas:
<img width="1277" alt="image" src="https://github.com/user-attachments/assets/1b844c48-b4d5-4da5-98dc-02083694f166" />
