# Ideal Soft Teste

![GitHub repo size](https://img.shields.io/github/repo-size/iuricode/README-template?style=for-the-badge)
![GitHub language count](https://img.shields.io/github/languages/count/iuricode/README-template?style=for-the-badge)

> Teste técnico para Ideal Soft

## 💻 Pré-requisitos

Antes de começar, verifique se você atendeu aos seguintes requisitos:

- Visual Studio 2022
- .NET 8
- Postgresql

## 🚀 Instalando

Windows:

```
- Clone este repositório
- Abra o projeto no Visual Studio
- Expanda o projeto IdealSoftApi e abra o arquivo appsettings.json
- Altere os valores username e password para os dados de acesso ao postgres
- Altere o nome da Database para o nome do banco de dados criado
- Navegue até Tools > NuGet Package Manager > Package Manager Console
- No console rode o comando "Add-Migration InitialMigration"
- Em seguida rode o comando "Update-Database"
  *Esses comandos criam a tabela no banco de dados através do Entity Framework.
- Clique com o botão direito na solution ou em um dos projetos e selecione Configure Startup Projects
- Selecione Multiple startup projects e marque ambos os projetos para iniciar
- Rode o projeto
```

## ☕ CRUD

```
- Através do CRUD é possível:
  - Adicionar novos registros preenchendo os campos na parte inferior da tela e clicando no botão Criar
  - Editar registros ao mudar os dados na tabela e pressionar o botão Update
  - Deletar registros ao selecionar um registro na tabela e pressionar o botão Excluir
  - Atualizar os dados da tabela através do botão Atualizar
```
